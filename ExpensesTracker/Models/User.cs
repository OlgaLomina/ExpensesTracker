using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExpensesTracker.Models
{
    public class User
    {
        public static object Identity { get; internal set; }
        [Key] public int UserId { get; set; }
        public Int32? Manager_Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }

        [DisplayName("User Email")]
        public string Email { get; set; }
        public string User_Password { get; set; }
        public DateTime Date_Time { get; set; }
        public string Comment { get; set; }

        public int Role_Id { get; set; }
        public virtual Role RoleName { get; set; }

        public virtual ICollection<Expense> Expenses { get; set; }
    }

}
