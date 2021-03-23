using DAL;
using Domain.Models;
using Logic.Dto;
using Logic.Exceptions;
using Logic.Services.Base;
using System;
using System.Threading.Tasks;
using Utils.Encription;

namespace Logic.Services
{
    public class AuthorizationService : UserServiceBase, IAuthorizationService
    {
        private readonly JwtService _jwtService;
        public AuthorizationService(JwtService jwtService, Func<DatabaseContext> dbCreator)
            :base(dbCreator)
        {
            _jwtService = jwtService;
        }

        public async Task<string> Login(LoginRequestDto request)
        {
            using(var context = DbCreator())
            {
                var user = FindUser(request.Login, request.Password);

                if (user == null)
                    throw new UserNotFoundException("User was not found");

                return _jwtService.CreateToken(new CreateTokenDto(user.Id));
            }
        }

        public async Task<string> Register(RegisterRequestDto request)
        {
            using(var context = DbCreator())
            {
                var user = FindUser(request.Login, request.Password);
                if (user != null)
                    throw new UserAlreadyExists("Пользователь уже существует");
                var newUser = new User(request.Login, request.Email, Sha256Hash.ComputeSha256Hash(request.Password));

                context.Users.Add(newUser);

                await context.SaveChangesAsync();

                return _jwtService.CreateToken(new CreateTokenDto(user.Id));
            }
        }
    }
}
