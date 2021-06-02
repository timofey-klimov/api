using Logic.Dto;
using System.Threading.Tasks;

namespace Logic.Services.Base
{
    /// <summary>
    /// Сервис для авторизации пользователей
    /// </summary>
    public interface IAuthorizationService
    {
        Task<string> Login(LoginRequestDto request);

        Task<int> Register(RegisterRequestDto request);

        Task<string> ConfirmEmail(int userId, string code);
    }
}
