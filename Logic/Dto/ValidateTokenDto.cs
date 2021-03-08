namespace Logic.Dto
{
    public class ValidateTokenDto
    {
        public string Token { get; private set; }

        public ValidateTokenDto(string token)
        {
            Token = token;
        }
    }
}
