using DAL;
using Domain.Models;
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
                var user = await context.Users.FirstOrDefaultAsync(x => x.Login == login && Sha256Hash.ComputeSha256Hash(password) == x.Password);
                return user;
            }
        }
    }
}
