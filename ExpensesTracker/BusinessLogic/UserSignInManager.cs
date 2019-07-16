using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web.Security;
using System.Web.Mvc;

namespace ExpensesTracker.BusinessLogic
{
    public class UserSignInManager
    {
        public static AppUser CurrentUser
        {
            get;
            private set;
        }

        public static AppRole CurrentRole
        {
            get;
            private set;
        }

        private static readonly UserManager _userManager = new UserManager();

        private static void LoginUser(AppUser user)
        {
            FormsAuthentication.SetAuthCookie(user.Email, true);
            if (user != null)
            {
                CurrentUser = user;
            }
        }

        private static void LoginRoleUser(AppRole userRole)
        {
            FormsAuthentication.SetAuthCookie(userRole.Name, true);
            if (userRole != null)
            {
                CurrentRole = userRole;
            }
        }

        public static AppUser LoginUser(string email, string password)
        {
            AppUser user = _userManager.FindByEmailAndPassword(email, password);
            if (user != null)
            {             
                LoginUser(user);

                AppRole role = new AppRole(user); //_roleUserManager.FindRoleById(user);
                if (role.Name == "admin")
                {
                    LoginRoleUser(role);
                }
                return user;
            }
            return null;
        }
    }

}