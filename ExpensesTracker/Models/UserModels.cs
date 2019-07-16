using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpensesTracker.Models
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string User_Password { get; set; }
    }

    public class RegisterModel
    {
        public RegisterModel()
        {
        }

        public RegisterModel(User user) : base()
        {
            this.First_Name = user.First_Name;
            this.Last_Name = user.Last_Name;
            this.Email = user.Email;
            this.User_Password = user.User_Password;
        }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
        public string User_Password { get; set; }
    }

    public class UserViewModel
    {
        public UserViewModel()
        {
        }
        public UserViewModel(User user) : base()
        {
            this.First_Name = user.First_Name;
            this.Last_Name = user.Last_Name;
            this.Email = user.Email;
            this.Role_Id = user.Role_Id;
            RoleName = user.RoleName.Name;
        }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
        public int Role_Id { get; set; }
        public string RoleName { get; set; }
    }
}