﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Security.Principal;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Security.Claims;
using PointNet.Model;

namespace PointNet.Web.Core.Models
{ 
    [Serializable]
    public class PointNetUser : IUser<int> 
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string RoleName { get;  set; }
        public string Email { get; set; }
        public bool IsAuthenticated
        {
            get { return true; }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<PointNetUser, int> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            return await Task<ClaimsIdentity>.Factory.StartNew(() =>
            {
                IList<Claim> claimCollection = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, this.UserName),
                    new Claim(ClaimTypes.Email, this.Email),
                    new Claim(ClaimTypes.Role, this.RoleName), //Its user by PointNetAuthorize
                    new Claim(ClaimTypes.NameIdentifier, this.Id.ToString()),
                    new Claim(@"http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", this.Id.ToString())
                };

                return new ClaimsIdentity(claimCollection, DefaultAuthenticationTypes.ApplicationCookie);
            });
        }

        public PointNetUser(){}
        public PointNetUser(string UserName, int userId)
        {
            this.UserName = UserName;
            this.Id = userId;
        }
        public PointNetUser(string UserName, int userId, string roleName)
        {
            this.UserName = UserName;
            this.Id = userId;
            this.RoleName = roleName;
        }

        public PointNetUser(User user)
        {
            this.Id = user.UserId;
            this.RoleName = Enum.GetName(typeof(UserRoles), user.RoleId);
            this.UserName = user.DisplayName;
            this.Email = user.Email;
        }

        public override string ToString()
        {
            return UserName;
        }
    }
}
