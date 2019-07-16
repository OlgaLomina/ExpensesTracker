using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ExpensesTracker.Models
{
    public class ExpenseContext : DbContext
    {
        public ExpenseContext() : base("ExpenseContext")
        {
        }
        public DbSet<Expense> Expenses { get; set; }
       
    }
}