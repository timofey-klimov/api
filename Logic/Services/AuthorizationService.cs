using DAL;
using Domain.Models;
using Logic.Dto;
using Logic.Exceptions;
using Logic.Operations.Requests;
using Logic.Services.Base;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Utils.Encription;

namespace Logic.Services
{
    public class AuthorizationService : UserServiceBase, IAuthorizationService
    {
        private readonly JwtService _jwtService;
        private readonly IMediator _mediatr;
        public AuthorizationService(JwtService jwtService, Func<DatabaseContext> dbCreator, IMediator mediator)
            :base(dbCreator)
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
            using(var context = DbCreator())
            {
                var user = await FindUser(request.Login, request.Password);

                if (user == null)
                    throw new UserNotFoundException("Пользователь не найден");

                return _jwtService.CreateToken(new CreateTokenDto(user.Id)).Token;
            }
        }

        public async Task<int> Register(RegisterRequestDto request)
        {
            using(var context = DbCreator())
            {
                var user = await FindUser(request.Login, request.Password);
                if (user != null)
                    throw new UserAlreadyExistsException("Пользователь уже существует");
                var userToCreate = new User(request.Login, request.Email, request.PhoneNumber, Encription.ComputeSha256Hash(request.Password));

                context.Users.Add(userToCreate);

                await context.SaveChangesAsync();

                await _mediatr.Send(new SendConfirmCodeRequest(userToCreate.Email, userToCreate.Id));

                return userToCreate.Id;
            }
        }
    }
}
