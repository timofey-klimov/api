using Logic.Dto;
using System.Threading.Tasks;

namespace Logic.Services.Base
{
    public interface IAuthorizationService
    {
        Task<string> Login(LoginRequestDto request);

        Task<string> Register(RegisterRequestDto request);
    }
}
