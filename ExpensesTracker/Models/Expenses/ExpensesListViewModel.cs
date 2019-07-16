using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpensesTracker.Models.Expenses
{
    public class ExpensesListViewModel
    {
        public ExpensesListViewModel()
        {
            this.DateBegin = new DateTime(DateTime.Now.Year, 1, 1);
            this.DateEnd = new DateTime(DateTime.Now.Year, 12, 31);
        }
        public IEnumerable<Expense> Expenses { get; set; }
        public DateTime DateBegin { get; set; }
        public DateTime DateEnd { get; set; }
    }
}