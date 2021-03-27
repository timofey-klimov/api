using System;
using System.Collections;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Session : BaseEntity<int>
    {
        public string Token { get; private set; }

        public DateTime CreateDate { get; private set; }

        public DateTime ExpireDate { get; private set; }

        public int UserId { get; private set; }

        private Session()
            :base()
        {

        }

        public Session(string token, DateTime createDate, DateTime expireDate, int userId)
        {
            Token = token;
            CreateDate = createDate;
            ExpireDate = expireDate;
            UserId = userId;
        }
    }
}
