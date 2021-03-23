namespace Logic.Dto
{
    public class RegisterRequestDto
    {
        public string Login { get; private set; }

        public string Password { get; private set; }

        public string Email { get; private set; }

        public RegisterRequestDto(string login, string password, string email)
        {
            Login = login;
            Password = password;
            Email = email;
        }
    }
}
