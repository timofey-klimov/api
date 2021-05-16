using DAL;
using Logic.Abstract;
using Logic.Dto;
using Logic.Enums;
using Logic.Exceptions;
using Logic.Operations.Requests;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Utils;

namespace Logic.Operations.Handlers
{
    public class SendConfirmCodeHandler : IRequestHandler<SendConfirmCodeRequest>
    {
        private readonly IMailSenderClient _mailClient;
        private readonly Func<DatabaseContext> _dbCreator;
        public SendConfirmCodeHandler(IMailSenderClient mailClient, Func<DatabaseContext> dbCreator)
        {
            _mailClient = mailClient;
            _dbCreator = dbCreator;
        }
        public async Task<Unit> Handle(SendConfirmCodeRequest request, CancellationToken cancellationToken)
        {
            using (var dbContext = _dbCreator())
            {
                var template = dbContext.NotificationTemplates.FirstOrDefault(x => x.NotificationType == (byte)NotificatonTemplate.ConfirmSmsCode);

                if (template == null)
                    throw new NotificationTemplateNotFoundException("NotificationTempateWasNotFound");

                var code = Generators.GenerateRandomCode(6);
                var message = template.NotificationMessage.Replace("{code}", code);

                dbContext.TwoFactorAuths.Add(new Domain.Models.TwoFactorAuth(request.UserId, code));

                await dbContext.SaveChangesAsync();

                var mailDto = new MailDto("Код подтверждения", message, request.Email);

                var result = await _mailClient.SendMail(mailDto);

                if (!result.Succsess)
                    throw new OperatinFailedException(result.ErrorMessage);

            }

            return Unit.Value;
        }
    }
}
