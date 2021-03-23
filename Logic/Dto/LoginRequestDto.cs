namespace Logic.Dto
{
    public class LoginRequestDto
    {
        public string Login { get; private set; }

        public string Password { get; private set; }

        public LoginRequestDto(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}
