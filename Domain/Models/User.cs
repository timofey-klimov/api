using System;

namespace Domain.Models
{
    public class User : BaseEntity<int>
    {
        public string Login { get; private set; }

        public string Email { get; private set; }

        public DateTime CreateDate { get; private set; }

        public DateTime? UpdateDate { get; private set; }

        public byte[] Password { get; private set; }

        public int? SessionId { get; private set; }

        public virtual Session Session { get; set; }

        private User()
            :base()
        {

        }

        public User(string login, string email, byte[] password)
            :base()
        {
            Login = login;
            Email = email;
            Password = password;
        }
    }
}
