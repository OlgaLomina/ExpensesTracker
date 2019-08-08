using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExpensesTracker.Models
{
    public class Expense
    {
        [Key] public int Expense_id { get; set; }
        public int Userid {get; set;}

        [Display(Name = "Description")]
        public string Name { get; set; }
        public DateTime Date_Time { get; set; }
        public decimal Amount { get; set; }
        public string Comment { get; set; }
        public virtual User User { get; set; }

    }
}