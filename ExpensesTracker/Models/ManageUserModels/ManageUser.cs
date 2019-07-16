using ExpensesTracker.BusinessLogic;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpensesTracker.Models.ManageUserModels
{
    public class ManageUser
    {
        public ManageUser(AppUser user) : base()
        {
            First_Name = user.FirstName;
            Last_Name = user.LastName;
            Email = user.Email;
            //HasPassword = user.Email != null ? true : false;
            Logins = new List<UserLoginInfo>();
            SetId(user.Id);
            RoleId = user.RoleId;       
        }

        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
        //public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }

        public string id;

        public string GetId()
        {
            return id;
        }

        public void SetId(string value)
        {
            id = value;
        }

        public int RoleId { get; set; }
    }
}







