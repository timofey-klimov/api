using DAL;
using Domain.Models;
using Logic.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Utils.Encription;

namespace Logic.Services.Base
{
    public abstract class UserServiceBase
    {
        protected Func<DatabaseContext> DbCreator;
        public UserServiceBase(Func<DatabaseContext> dbCreator)
        {
            DbCreator = dbCreator;
        }

        public virtual async Task<User> FindUser(string login, string password)
        {
            using(var context = DbCreator())
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Login == login && Encription.ComputeSha256Hash(password) == x.Password);
                return user;
            }
        }

        public virtual async Task<User> FindUser(int id)
        {
            using(var context = DbCreator())
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
                return user;
            }
        }
    }
}
