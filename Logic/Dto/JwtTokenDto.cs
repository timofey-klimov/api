using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Dto
{
    public class JwtTokenDto
    {
        public DateTime CreateDate { get; private set; }

        public DateTime ExpireDate { get; private set; }

        public string Token { get; private set; }

        public JwtTokenDto(DateTime createDate, DateTime expireDate, string token)
        {
            CreateDate = createDate;
            ExpireDate = expireDate;
            Token = token;
        }
    }
}
