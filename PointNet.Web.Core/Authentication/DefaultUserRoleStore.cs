using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using PointNet.CommandProcessor.Dispatcher;
using PointNet.Data.Repositories;
using AutoMapper;
using PointNet.Model;
using PointNet.Web.Core.Models;

namespace PointNet.Web.Core.Authentication
{
    public class DefaultUserRoleStore : IUserRoleStore<PointNetUser, int> 
    {
        private readonly IUserRepository userRepository;

        public DefaultUserRoleStore(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public Task CreateAsync(PointNetUser user)
        {
            throw new NotImplementedException();
        }
        public Task DeleteAsync(PointNetUser user)
        {
            throw new NotImplementedException();
        }
        public Task<PointNetUser> FindByIdAsync(int userId)
        {
            throw new NotImplementedException();
        }
        public Task<PointNetUser> FindByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }
        public Task UpdateAsync(PointNetUser user)
        {
            throw new NotImplementedException();
        }

        public Task AddToRoleAsync(PointNetUser user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<IList<string>> GetRolesAsync(PointNetUser user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsInRoleAsync(PointNetUser user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(PointNetUser user, string roleName)
        {
            throw new NotImplementedException();
        }
        public void Dispose()
        {
        }
    }
}
