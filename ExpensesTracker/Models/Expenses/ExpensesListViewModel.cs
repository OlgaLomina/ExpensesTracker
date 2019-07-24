using System;
using System.Collections.Generic;

namespace ExpensesTracker.Models.Expenses
{
    public class ExpensesListViewModel
    {
        public ExpensesListViewModel()
        {
            this.DateBegin = new DateTime(DateTime.Now.Year, 1, 1, 0, 0, 0, 0);
            this.DateEnd = new DateTime(DateTime.Now.Year, 12, 31, 23, 59, 59, 999);
            this.Dat1 = DateBegin.ToString("MM/dd/yyyy");
            this.Dat2 = DateEnd.ToString("MM/dd/yyyy");
        }
        //public IEnumerable<Expense> Expenses { get; set; }
        public DateTime DateBegin { get; set; }
        public DateTime DateEnd { get; set; }
        public string Dat1 { get; set; }
        public string Dat2 { get; set; }

        public List<ExpensesWeekViewModel> WeekExpenses = new List<ExpensesWeekViewModel>();
    }
    public class ExpensesAvgPDayViewModel
    {
        public IEnumerable<Expense> Expenses { get; set; }
        public decimal Avarage_pday;
    }

    public class ExpensesWeekViewModel
    {
        public List<ExpensesAvgPDayViewModel> AllDayExpenses = new List<ExpensesAvgPDayViewModel>();
        public string Year_Week;
        public decimal Sum_pweek;
    }
}