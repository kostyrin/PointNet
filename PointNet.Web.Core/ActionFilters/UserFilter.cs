using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Security.Principal;
using PointNet.Web.Core.Models;
using PointNet.Web.Core.Extensions;

namespace PointNet.Web.Core.ActionFilters
{
    //Inject a ViewBag object to Views for getting information about an authenticated user
    public class UserFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            UserModel userModel;

            //if (filterContext.Controller.ViewBag.UserModel == null)
            //{
            //    userModel = new UserModel();
            //    filterContext.Controller.ViewBag.UserModel = userModel;
            //}
            //else
            //{
            //    userModel = filterContext.Controller.ViewBag.UserModel as UserModel;
            //}

            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                //PointNetUser PointNetUser = filterContext.HttpContext.User.GetPointNetUser();
                //userModel.IsUserAuthenticated = PointNetUser.IsAuthenticated;
                //userModel.UserName = PointNetUser.DisplayName;
                //userModel.RoleName = PointNetUser.RoleName;
                IPrincipal user = filterContext.HttpContext.User;
                bool isInrole = user.IsInRole("Admin");
            }

            base.OnActionExecuted(filterContext);
        }
    }

    public class UserModel
    {
        public bool IsUserAuthenticated { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
    }
}
