using Newtonsoft.Json;

namespace Api.Dto.Request
{
    public class LoginRequestDto
    {
        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
