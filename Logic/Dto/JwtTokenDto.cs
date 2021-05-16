using System;
using Utils.Guards;

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
            Guard.GuardAgainstNull(token, nameof(token));
            Token = token;
        }
    }
}
