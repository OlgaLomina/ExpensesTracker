using ExpensesTracker.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpensesTracker.BusinessLogic
{
    public class AppRole : IdentityRole
    {
        public const string Admin = "admin";
        public const string User = "user";
        public const string Manager = "manager";

        public AppRole() : base()
        {

        }
        public AppRole(AppUser user) : base()
        {
            this.RoleId = user.RoleId.ToString();
        }

        public string RoleId {
            get
            {
                return base.Id;
            }
            set
            {
                Id = value;
                
                if (Int32.Parse(value) == 0)
                {
                    this.Name = Admin;
                }
                else
                {
                    if (Int32.Parse(value) == 1)
                    {
                        Name = Manager;
                    }
                    else
                        Name = User;
                }
            }
        }
    }

}