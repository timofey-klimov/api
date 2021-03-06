﻿using System;

namespace Domain.Models
{
    public class User : BaseEntity<int>
    {
        public string Login { get; private set; }

        public string Email { get; private set; }

        public DateTime CreateDate { get; private set; }

        public DateTime? UpdateDate { get; private set; }

        public byte[] Password { get; private set; }

        private User()
            :base()
        {

        }

        public User(string login, string email, byte[] password)
        {
            Login = login;
            Email = email;
            Password = password;
        }
    }
}