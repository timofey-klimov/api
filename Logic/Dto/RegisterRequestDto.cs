using Utils.Guards;

namespace Logic.Dto
{
    public class RegisterRequestDto
    {
        public string Login { get; private set; }

        public string Password { get; private set; }

        public string Email { get; private set; }

        public string PhoneNumber { get; private set; }

        public RegisterRequestDto(string login, string password, string email, string phoneNumber)
        {
            Guard.GuardAgainstNull(login, nameof(login));
            Guard.GuardAgainstNull(password, nameof(password));
            Guard.GuardAgainstNull(email, nameof(email));
            Guard.GuardAgainstNull(phoneNumber, nameof(phoneNumber));

            Login = login;
            Password = password;
            Email = email;
            PhoneNumber = phoneNumber;
        }
    }
}
