using DAL;
using Logic.Abstract;
using Logic.Dto;
using Logic.Enums;
using Logic.Exceptions;
using Logic.Operations.Base;
using Logic.Operations.Requests;
using MediatR;
using Serilog;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Utils;
using Utils.Extensions;

namespace Logic.Operations.Handlers
{
    public class SendConfirmCodeRequestHandler : OperationBase<SendConfirmCodeRequest, Unit>
    {
        private readonly IMailSenderClient _mailClient;
        private readonly Func<DatabaseContext> _dbCreator;
        public SendConfirmCodeRequestHandler(IMailSenderClient mailClient, Func<DatabaseContext> dbCreator, ILogger logger)
            :base(dbCreator, logger)
        {
            _mailClient = mailClient;
            _dbCreator = dbCreator;
        }

        protected override async Task<Unit> HandleCore(SendConfirmCodeRequest request, CancellationToken token)
        {
            using (var dbContext = _dbCreator())
            {
                var template = dbContext.NotificationTemplates.FirstOrDefault(x => x.NotificationType == NotificatonTemplate.ConfirmSmsCode.To<byte>());

                if (template == null)
                    throw new EntityNotFoundException($"{typeof(NotificatonTemplate)} not found");

                var code = Generators.GenerateRandomCode(6);
                var message = template.NotificationMessage.Replace("{code}", code);

                dbContext.TwoFactorAuths.Add(new Domain.Models.TwoFactorAuth(request.UserId, code));

                await dbContext.SaveChangesAsync();

                var mailDto = new MailDto("MyService", message, request.Email);

                var result = await _mailClient.SendMail(mailDto);

                if (!result.IsSuccess)
                    throw new OperationFailedException(result.ErrorMessage);
            }

            return Unit.Value;
        }
    }
}
