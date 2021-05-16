using DAL;
using Domain.Models;
using Logic.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Logic.Operations.Base
{
    public abstract class UserOperation
    {
        protected Func<DatabaseContext> DbCreator;
        public UserOperation(Func<DatabaseContext> dbCreator)
        {
            DbCreator = dbCreator;
        }

        public virtual async Task<User> FindUser(int userId)
        {
            using (var dbContext = DbCreator())
            {
                var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
                if (user == null)
                    throw new UserNotFoundException("UserNotFind");
                return user;
            }
        }
    }
}
