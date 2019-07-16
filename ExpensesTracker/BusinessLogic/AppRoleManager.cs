using ExpensesTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace ExpensesTracker.BusinessLogic
{
    public class AppRoleManager //: RoleProvider
    {
        public AppRole FindRoleById(AppUser user)
        {
            using (UserContext db = new UserContext())
            {
                var role = db.Roles.Where(u => u.Role_Id == user.RoleId).FirstOrDefault();

                if (role != null)
                {
                    AppRole appRole = new AppRole(user);
                    return appRole;
                }
                else
                {
                    return null;
                }
            }
        }
        //public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        //public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        //{
        //    throw new NotImplementedException();
        //}

        //public override void CreateRole(string roleName)
        //{
        //    throw new NotImplementedException();
        //}

        //public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        //{
        //    throw new NotImplementedException();
        //}

        //public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        //{
        //    throw new NotImplementedException();
        //}

        //public override string[] GetAllRoles()
        //{
        //    using (var userContext = new UserContext())
        //    {
        //        return userContext.Roles.Select(r => r.Name).ToArray();
        //    }
        //}

        //public override string[] GetRolesForUser(string email)
        //{
        //    using (var userContext = new UserContext())
        //    {
        //        var user = userContext.Users.SingleOrDefault(u => u.Email == email);
        //        var userRoles = userContext.Roles.Select(r => r.Name);

        //        if (user == null)
        //            return new string[] { };
        //        return user.Roles == null ? new string[] { } :
        //            userRoles.ToArray();
        //    }
        //}

        //public override string[] GetUsersInRole(string roleName)
        //{
        //    throw new NotImplementedException();
        //}

        //public override bool IsUserInRole(string email, string roleName)
        //{
        //    using (var userContext = new UserContext())
        //    {
        //        var user = userContext.Users.SingleOrDefault(u => u.Email == email);
        //        var userRoles = userContext.Roles.Select(r => r.Name);

        //        if (user == null)
        //            return false;
        //        return user.Roles != null &&
        //            userRoles.Any(r => r == roleName);
        //    }
        //}

        //public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        //{
        //    throw new NotImplementedException();
        //}

        //public override bool RoleExists(string roleName)
        //{
        //    throw new NotImplementedException();
        //}
    }
}