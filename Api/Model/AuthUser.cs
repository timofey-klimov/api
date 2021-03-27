using System;
using System.Security.Claims;

namespace Api.Model
{
    public class AuthUser : ClaimsPrincipal
    {
        public int Id { get; private set; }

        public string Login { get; private set; }

        public byte[] Password { get; private set; }

        public DateTime CreateDate { get; private set; }
        
        public AuthUser(int id, string login, byte[] password, DateTime createDate)
        {
            Id = id;
            Login = login;
            Password = password;
            CreateDate = createDate;
        }
    }
}
