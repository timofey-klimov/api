using DAL;
using Domain.Models;
using Domain.Models.ValueObjects;
using Logic.Exceptions;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Threading.Tasks;
using Utils.Encription;

namespace Logic.Services.Base
{
    public abstract class UserServiceBase : BaseService
    {
        protected Func<DatabaseContext> DbCreator;
        public UserServiceBase(Func<DatabaseContext> dbCreator, ILogger logger)
            :base(logger)
        {
            DbCreator = dbCreator;
        }

        public virtual async Task<User> FindUser(string login, string password)
        {
            using(var context = DbCreator())
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Login.Value == login && x.Password.Value == Encription.ComputeSha256Hash(password));
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
