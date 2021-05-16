using Utils.Guards;

namespace Logic.Dto
{
    public class ValidateTokenDto
    {
        public string Token { get; private set; }

        public ValidateTokenDto(string token)
        {
            Guard.GuardAgainstNull(token, nameof(token));

            Token = token;
        }
    }
}
