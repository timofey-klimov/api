using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Dto.Request
{
    public class ConfirmEmailRequestDto
    {
        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }
    }
}
