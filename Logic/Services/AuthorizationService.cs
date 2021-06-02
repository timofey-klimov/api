using DAL;
using Domain.Models;
using Domain.Models.ValueObjects;
using Logic.Dto;
using Logic.Exceptions;
using Logic.Operations.Requests;
using Logic.Services.Base;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Threading.Tasks;
using Utils.Encription;

namespace Logic.Services
{
    public class AuthorizationService : UserServiceBase, IAuthorizationService
    {
        private readonly JwtService _jwtService;
        private readonly IMediator _mediatr;
        public AuthorizationService(JwtService jwtService, Func<DatabaseContext> dbCreator, IMediator mediator, ILogger logger)
            :base(dbCreator, logger)
        {
            _jwtService = jwtService;
            _mediatr = mediator;
        }

        public async Task<string> ConfirmEmail(int userId, string code)
        {
            using(var dbContext = DbCreator())
            {
                var twoFactorAuth = await dbContext.TwoFactorAuths.FirstOrDefaultAsync(x => x.Code == code && x.UserId == userId);
                if (twoFactorAuth == null)
                    throw new CodeIsIncorrectException("IncorrectConfirmCode");

                var jwtTokenDto = _jwtService.CreateToken(new CreateTokenDto(userId));

                var session = new Session(jwtTokenDto.Token, DateTime.Now, jwtTokenDto.ExpireDate, userId);

                dbContext.Sessions.Add(session);

                await dbContext.SaveChangesAsync();

                return jwtTokenDto.Token;
            }
        }

        public async Task<string> Login(LoginRequestDto request)
        {
            var user = await FindUser(request.Login, request.Password);

            if (user == null)
                throw new EntityNotFoundException($"{typeof(User).Name} not found");

            return _jwtService.CreateToken(new CreateTokenDto(user.Id)).Token;
        }

        public async Task<int> Register(RegisterRequestDto request)
        {
            using(var context = DbCreator())
            {
                var user = await FindUser(request.Login, request.Password);
                if (user != null)
                    throw new UserAlreadyExistsException("Пользователь уже существует");
                var userToCreate = new User(new Login(request.Login), new Email(request.Email), new PhoneNumber(request.PhoneNumber), new Domain.Models.ValueObjects.HashCode(Encription.ComputeSha256Hash(request.Password)));

                context.Users.Add(userToCreate);

                await context.SaveChangesAsync();

                await _mediatr.Send(new SendConfirmCodeRequest(userToCreate.Email.Value, userToCreate.Id));

                return userToCreate.Id;
            }
        }
    }
}
