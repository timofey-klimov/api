using Newtonsoft.Json;

namespace Api.Dto.Request
{
    public class RegisterRequestDto
    {
        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }
}
