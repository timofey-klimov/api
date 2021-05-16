using System;

namespace Domain.Models
{
    public class TwoFactorAuth : BaseEntity<int>
    {
        public DateTime CreateDate { get; private set; }

        public int UserId { get; private set; }

        public string Code { get; private set; }

        private TwoFactorAuth()
        {

        }

        public TwoFactorAuth(int userId, string code)
        {
            CreateDate = DateTime.Now;
            UserId = userId;
            Code = code;
        }
    }
}
