using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExpensesTracker.Models.AdminModels
{
    public class ManageExpensesListViewModel
    {
        public ManageExpensesListViewModel(User user, string dat1, string dat2) : base()
        {
            First_Name = user.First_Name;
            Last_Name = user.Last_Name;
            Email = user.Email;
            this.DateBegin = Convert.ToDateTime(dat1);
            this.DateEnd = Convert.ToDateTime(dat2);
            this.Dat1 = DateBegin.ToString("MM/dd/yyyy");
            this.Dat2 = DateEnd.ToString("MM/dd/yyyy");
            UserId = user.UserId;
        }

        public ManageExpensesListViewModel()
        {
            this.DateBegin = new DateTime(DateTime.Now.Year, 1, 1, 0, 0, 0, 0);
            this.DateEnd = new DateTime(DateTime.Now.Year, 12, 31, 23, 59, 59, 999);
            this.Dat1 = DateBegin.ToString("MM/dd/yyyy");
            this.Dat2 = DateEnd.ToString("MM/dd/yyyy");
        }
        public DateTime DateBegin { get; set; }
        public DateTime DateEnd { get; set; }
        public string Dat1 { get; set; }
        public string Dat2 { get; set; }

        public List<User> UsersList { get; set; }
        public int UserId { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }

        [DisplayName("User Email")]
        public string Email { get; set; }


        public List<ManageExpensesWeekViewModel> WeekExpenses = new List<ManageExpensesWeekViewModel>();
    }
    public class ManageExpensesAvgPDayViewModel
    {
        public IEnumerable<Expense> Expenses { get; set; }
        public decimal Avarage_pday;
    }

    public class ManageExpensesWeekViewModel
    {
        public List<ManageExpensesAvgPDayViewModel> AllDayExpenses = new List<ManageExpensesAvgPDayViewModel>();
        public string Year_Week;
        public decimal Sum_pweek;
    }
}