using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using DAL.Models.AuthorizationModel;
using Microsoft.Owin.Security;
using Microsoft.Owin;
using Microsoft.AspNet.Identity;
using System;
using System.Net;

namespace DAL.AuthorizationManagers
{
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        private static IOwinContext Context { get; set; }
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            :base(userManager, MyAuthenticationManager(Context))
        {

        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            Context = context;
            var manager = new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
            manager.AuthenticationManager = MyAuthenticationManager(context);
            return manager;
        }
        private static IAuthenticationManager MyAuthenticationManager(IOwinContext context)
        {
            return context.Authentication;
        }
        public Task ExternalSignInAsync(UserLoginInfo loginInfo, bool isPersistent)
        {
            throw new NotImplementedException();
        }
    }
}
