using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;
using AutoMapper;
using PointNet.Common.Logging;
using PointNet.Web.ViewModels;
using PointNet.Model.Commands;
using PointNet.Web.Core.Models;
using PointNet.CommandProcessor.Dispatcher;
using PointNet.Data.Repositories;
using PointNet.Core.Common;
using PointNet.Web.Core.Extensions;
using PointNet.Web.Core.Authentication;
using PointNet.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Owin;
using System.Threading.Tasks;

namespace PointNet.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ICommandBus _commandBus;
        private readonly IUserRepository _userRepository;
        private readonly ApplicationUserManager<PointNetUser, int> _userManager;
        private readonly ILogger _logger;
        private readonly IMappingEngine _mapper;

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public AccountController(IMappingEngine mapper, ICommandBus commandBus, IUserRepository userRepository, ApplicationUserManager<PointNetUser, int> userManager
            , ILogger logger)
        {
            this._commandBus = commandBus;
            this._userRepository = userRepository;
            this._userManager = userManager;
            _logger = logger;
            _mapper = mapper;
        }

        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LogOnFormModel form, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = this._userRepository.Get(x => x.Email.ToUpper() == form.Email.ToUpper() && Md5Encrypt.Md5EncryptPassword(form.Password) == x.PasswordHash);
                if (user != null)
                {
                    PointNetUser appUser = new PointNetUser(user);
                    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                    AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = true, RedirectUri = returnUrl }, await appUser.GenerateUserIdentityAsync(_userManager));
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            return View(form);
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Register(UserFormModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var command = new UserRegisterCommand
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        Password = model.Password,
                        Activated = true,
                        RoleId = (Int32)UserRoles.User
                    };

                    IEnumerable<ValidationResult> errors = _commandBus.Validate(command);
                    ModelState.AddModelErrors(errors);
                    if (ModelState.IsValid)
                    {
                        var result = _commandBus.Submit(command);
                        if (result.Success)
                        {
                            var user = this._userRepository.Get(x => x.Email.ToUpper() == command.Email.ToUpper() && Md5Encrypt.Md5EncryptPassword(command.Password) == x.PasswordHash);
                            PointNetUser appUser = new PointNetUser()
                            {
                                Id = user.UserId,
                                RoleName = Enum.GetName(typeof(UserRoles), user.RoleId),
                                UserName = user.DisplayName
                            };
                            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = true }, await appUser.GenerateUserIdentityAsync(_userManager));
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ModelState.AddModelError("", "An unknown error occurred.");
                        }
                    }        
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                    throw;
                }
                
                return View(model);
            }

            return View(model);
        }

        private IEnumerable<string> GetErrorsFromModelState()
        {
            return ModelState.SelectMany(x => x.Value.Errors.Select(error => error.ErrorMessage));
        }


        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordFormModel form)
        {
            if (ModelState.IsValid)
            {
                var user = HttpContext.User;
                var command = new ChangePasswordCommand
                {
                    UserId = int.Parse(user.Identity.GetUserId()),
                    OldPassword = form.OldPassword,
                    NewPassword = form.NewPassword
                };
                IEnumerable<ValidationResult> errors = _commandBus.Validate(command);
                ModelState.AddModelErrors(errors);
                if (ModelState.IsValid)
                {
                    var result = _commandBus.Submit(command);
                    if (result.Success)
                    {
                        return RedirectToAction("ChangePasswordSuccess");
                    }
                    else
                    {
                        ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                    }
                }
            }
             
            return View(form);
        }

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

    }
}
