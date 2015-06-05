﻿using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using ElevenNote.WebServices.Models;
using ElevenNote.DataAccess;

namespace ElevenNote.WebServices
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

    public class ApplicationUserManager : UserManager<ElevenNoteApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ElevenNoteApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ElevenNoteApplicationUser>(context.Get<ElevenNoteDataContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ElevenNoteApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<ElevenNoteApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }
}
