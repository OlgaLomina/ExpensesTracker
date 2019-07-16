using ExpensesTracker.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ExpensesTracker.BusinessLogic
{
    public class AppUser : IdentityUser
    {
        public AppUser() : base()
        {

        }
        public AppUser(User user) : base()
        {
            this.FirstName = user.First_Name;
            this.LastName = user.Last_Name;
            this.Email = user.Email;
            Id = user.UserId.ToString();
            PasswordUser = user.User_Password;
            RoleId = user.Role_Id;
        }

        public AppUser(RegisterModel user) : base()
        {
            this.FirstName = user.First_Name;
            this.LastName = user.Last_Name;
            this.Email = user.Email;
        }

        public string FirstName
        {
            get
            {
                return UserName != null ? this.UserName.Split(' ')[0] : "";
            }
            set
            {
                UserName = value + " " + LastName;
            }
        }
        public string LastName
        {
            get
            {
                return UserName != null ? this.UserName.Split(' ')[1] : "";
            }
            set
            {
                UserName = FirstName + " " + value;
            }
        }

        public string PasswordUser { get; set; }
        public int RoleId { get; set; }
    }
}