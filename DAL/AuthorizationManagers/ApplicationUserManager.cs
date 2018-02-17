using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using DAL.Models.AuthorizationModel;
using DAL.DataContext;
using System;
using System.Threading.Tasks;
using Twilio.Types;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace DAL.AuthorizationManagers
{
    public class ApplicationUserManager:UserManager<ApplicationUser>
    {
        public ApplicationUserManager (IUserStore<ApplicationUser> store) : base(store)
        {
            UserValidator = new UserValidator<ApplicationUser>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = false,
                RequireUppercase = true,
            };
            UserLockoutEnabledByDefault = true;
            DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            MaxFailedAccessAttemptsBeforeLockout = 5;
            SmsService = new SmsService();
            RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is {0}"
            });
        }
        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options,
                                            IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<DataContex>()));

            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = false,
                RequireUppercase = true,
            };
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;
            manager.SmsService = new SmsService();
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is {0}"
            });

            return manager;
        }
    }
    public class SmsService : IIdentityMessageService
    {
        public SmsService()
        {

        }
        public Task SendAsync(IdentityMessage message)
        {
            TwilioClient.Init(
                "AC8bad72d0e9d960a6fc90b9f2bd3c45a2",
               "9948dd3548a510b2d8f86c98eca86a95"
               );
            var mes = MessageResource.Create(
                new PhoneNumber(message.Destination),
                from: "+14159156498",
                body: message.Body
                );
            return Task.FromResult(mes.Status);
        }
    }
}
