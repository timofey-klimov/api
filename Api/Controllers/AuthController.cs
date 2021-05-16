using Api.Dto.Request;
using Api.Dto.Response;
using Logic.Services.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ApiController
    {
        private readonly IAuthorizationService _authService;
        public AuthController(IMediator mediator, IAuthorizationService authorizationService)
            :base(mediator)
        {
            _authService = authorizationService;
        }

        /// <summary>
        /// Получение токена для существующего пользователя
        /// </summary>
        /// <param name="request"></param>
        /// <exception cref="UserNotFoundException"></exception>
        /// Ошибки:
        /// 9999 - глобальная
        /// 100 - пользователя не существует
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<ApiResponse<string>> Login([FromBody] LoginRequestDto request)
        {
            var token = await _authService.Login(new Logic.Dto.LoginRequestDto(request.Login, request.Password));
            return Ok(token);
        }

        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<ApiResponse<int>> Register([FromBody] RegisterRequestDto request)
        {
            var userId = await _authService.Register(new Logic.Dto.RegisterRequestDto(request.Login, request.Password, request.Email, request.PhoneNumber));
            return Ok(userId);
        }

        /// <summary>
        /// Подтверждение email при регистрации
        /// </summary>
        /// <returns></returns>
        [HttpPost("confirm")]
        public async Task<ApiResponse<string>> ConfirmEmail([FromBody] ConfirmEmailRequestDto request)
        {
            var token = await _authService.ConfirmEmail(request.UserId, request.Code);
            return Ok(token);
        }
    }
}
