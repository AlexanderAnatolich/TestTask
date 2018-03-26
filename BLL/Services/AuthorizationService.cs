using System.Threading.Tasks;
using DAL.AuthorizationManagers;
using DAL.DataContext;
using Microsoft.AspNet.Identity.EntityFramework;
using DAL.Models.AuthorizationModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Model.Models.AuthorizationViewModel;
using AutoMapper;
using System.Security.Claims;
using Microsoft.Owin.Security;
using Microsoft.Owin;
using System.Collections.Generic;

namespace BLL.Services
{
    public class AuthorizationService
    {
        private DataContex _db;
        private ApplicationUserManager _UserManager;
        private ApplicationRoleManager _RoleManager;
        private ApplicationSignInManager _signInManager;


        public AuthorizationService()
        {
            _db = new DataContex("MyDBConnection");
            _UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_db));

            _RoleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(_db));

            _signInManager = new ApplicationSignInManager(_UserManager, null);
        }

        public AuthorizationService(string connectionString)
        {
            _db = new DataContex(connectionString);
            _UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_db));

            _RoleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(_db));

            _signInManager = new ApplicationSignInManager(_UserManager, null);
        }
        public ApplicationUser FindById(string userId)
        {
            var users = _UserManager.FindById(userId);
            return users;
        }
        public async Task<IdentityResult> CreateUser(RegisterViewModel model)
        {
            var user = Mapper.Map<RegisterViewModel, ApplicationUser>(model);
            IdentityResult result = await _UserManager.CreateAsync(user, model.Password);
            return result;
        }
        public async Task<SignInStatus> Login(LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            return result;
        }
        public async Task<string> GetPhoneNumberAsync(string userId)
        {
            var tempValue = await _UserManager.GetPhoneNumberAsync(userId);
            return tempValue;
        }
        public async Task<bool> GetTwoFactorEnabledAsync(string userId)
        {
            var tempValue = await _UserManager.GetTwoFactorEnabledAsync(userId);
            return tempValue;
        }
        public async Task<IList<UserLoginInfo>> GetLoginsAsync(string userId)
        {
            var tempValue = await _UserManager.GetLoginsAsync(userId);
            return tempValue;
        }
        public async Task<bool> TwoFactorBrowserRememberedAsync(string userId)
        {
            var tempValue = await _signInManager.AuthenticationManager.TwoFactorBrowserRememberedAsync(userId);
            return tempValue;
        }
        public void SignOut()
        {
            _signInManager.AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }
       
        public async Task<string> SendVerificationMessage(string userId, string phoneNumber)
        {
            //string phoneNumber = "+380968711604";

            var code = await GenerateChangePhoneNumberTokenAsync(userId, phoneNumber);
            var message = new IdentityMessage
            {
                Destination = phoneNumber,
                Body = "Your security code is: " + code
            };
            await _UserManager.SmsService.SendAsync(message);
            return code;
        }
        public async Task<string> GenerateChangePhoneNumberTokenAsync(string userId, string phoneNumber)
        {
            var tempValue = await _UserManager.GenerateChangePhoneNumberTokenAsync(userId, phoneNumber);
            return tempValue;
        }
        public async Task<IdentityResult> ChangePhoneNumberAsync(string userId, string phoneNumber, string code)
        {
            var result = await _UserManager.ChangePhoneNumberAsync(userId, phoneNumber, code);

            if (result.Succeeded)
            {
                var user = await _UserManager.FindByIdAsync(userId);
                if (user != null)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
            }

            return result;
        }
        public async Task<IdentityResult> SetPhoneNumberAsync(string userId)
        {
            var result = await _UserManager.SetPhoneNumberAsync(userId, null);
            if (!result.Succeeded)
            {
                return result;
            }
            var user = await _UserManager.FindByIdAsync(userId);
            if (user != null)
            {
                await _signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return result;
        }
    }
}
