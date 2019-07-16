using ExpensesTracker.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpensesTracker.Models.AdminModels
{
    public class AdminUser
    {
         public AdminUser(AppUser user) : base()
        {
            First_Name = user.FirstName;
            Last_Name = user.LastName;
            Email = user.Email;
            SetId(user.Id);
            RoleId = user.RoleId;
            RoleName = new AppRole(user).Name;         
        }

        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }

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
        public string RoleName { get; set; }
    }
}