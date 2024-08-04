using Contracts.Interfaces;
using Entities.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Repository.Extensions;
using Shared.RequestFeatures;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace Repository.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
          
        }
        public async Task<IEnumerable<User>> Get_SharedObject_Users_Async(int sharedobjectId, bool trackChanges) => await FindByCondition(c => c.UserObjects.Any(x => x.UserId == c.Id && x.SharedObjectId == sharedobjectId), trackChanges).ToListAsync();
        public async Task<User> Get_User_Async(int userId, bool trackChanges)=> await FindByCondition(c => c.Id.Equals(userId), trackChanges).FirstAsync();
        public void Create_User(User user) => Create(user);
        public void Delete_User(User user)=> Delete(user);
    }
}
