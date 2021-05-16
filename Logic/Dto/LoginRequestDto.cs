using Utils.Guards;

namespace Logic.Dto
{
    public class LoginRequestDto
    {
        public string Login { get; private set; }

        public string Password { get; private set; }

        public LoginRequestDto(string login, string password)
        {
            Guard.GuardAgainstNull(login, nameof(login));
            Guard.GuardAgainstNull(password, nameof(password));

            Login = login;
            Password = password;
        }
    }
}
