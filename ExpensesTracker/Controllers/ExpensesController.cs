using ExpensesTracker.BusinessLogic;
using ExpensesTracker.Models;
using ExpensesTracker.Models.Expenses;
using ExpensesTracker.Models.ManageUserModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpensesTracker.Controllers
{
    public class ExpensesController : Controller
    {

        //for access to DB
        readonly ExpenseContext db = new ExpenseContext();

        private readonly ManageUser _currentUser = new ManageUser(UserSignInManager.CurrentUser);

        // GET: Expenses
        public ActionResult Index()
        {
            ExpenseContext db = new ExpenseContext();
            int userid = Int32.Parse(_currentUser.id);
                       
            ExpensesListViewModel ex_lvm = new ExpensesListViewModel();
            var expenses = db.Expenses.Where(e => e.Userid == userid && e.Date_Time >= ex_lvm.DateBegin && e.Date_Time <= ex_lvm.DateEnd).GroupBy(x => x.Date_Time).ToList();

            List<DateTime> dates = new List<DateTime>();
            foreach (var a in expenses)
            {
                dates.Add(a.Key.Date);
            }
            dates.Distinct();

            foreach (var r in dates)
            {
                DateTime r1 = r.Add(new TimeSpan(0, 23, 59, 59, 999));
                ExpensesAvgPDayViewModel ex_avg = new ExpensesAvgPDayViewModel
                {
                    Expenses = db.Expenses.Where(e => e.Userid == userid && e.Date_Time >= r && e.Date_Time <= r1)
                };
                ex_avg.Avarage_pday = ex_avg.Expenses.Average(e => e.Amount);
                //var aa = ex_avg.Expenses.ToList();

                ExpensesWeekViewModel ex_week = new ExpensesWeekViewModel();
                ex_week.AllDayExpenses.Add(ex_avg);
                ex_week.Year_Week = GetYearWeek(r);
                ex_week.Sum_pweek = ex_avg.Expenses.Sum(e => e.Amount);
                ex_lvm.WeekExpenses.Add(ex_week);
            }
            ViewBag.Message = _currentUser.First_Name + " " + _currentUser.Last_Name + "( " + _currentUser.Email + " )";
            return View(ex_lvm);
        }

        [HttpPost]
        public ActionResult Index(string dat1, string dat2)
        {
            DateTime dtFrom = Convert.ToDateTime(dat1);
            DateTime dtTo = Convert.ToDateTime(dat2);
            ExpenseContext db = new ExpenseContext();
            int userid = Int32.Parse(_currentUser.id);

            ExpensesListViewModel ex_lvm = new ExpensesListViewModel();
            var expenses = db.Expenses.Where(e => e.Userid == userid && e.Date_Time >= dtFrom && e.Date_Time <= dtTo).GroupBy(x => x.Date_Time).ToList();

            List<DateTime> dates = new List<DateTime>();
            foreach (var a in expenses)
            {
                dates.Add(a.Key);
            }
            dates.Distinct();

            foreach (var r in dates)
            {
                DateTime r1 = r.Add(new TimeSpan(0, 23, 59, 59, 999));
                ExpensesAvgPDayViewModel ex_avg = new ExpensesAvgPDayViewModel
                {
                    Expenses = db.Expenses.Where(e => e.Userid == userid && e.Date_Time >= r && e.Date_Time <= r1)
                };
                ex_avg.Avarage_pday = ex_avg.Expenses.Average(e => e.Amount);

                ExpensesWeekViewModel ex_week = new ExpensesWeekViewModel();
                ex_week.AllDayExpenses.Add(ex_avg);
                ex_week.Year_Week = GetYearWeek(r);
                ex_week.Sum_pweek = ex_avg.Expenses.Sum(e => e.Amount);
                ex_lvm.WeekExpenses.Add(ex_week);
            }
            return View(ex_lvm);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var expenses = db.Expenses.Where(e => e.Expense_id == id).FirstOrDefault();
            ViewBag.ExpensesId = expenses.Expense_id;
            ViewBag.Name = expenses.Name;

            return View();
        }

        [HttpPost]
        public string Edit(Expense expense)
        {
            expense.Date_Time = DateTime.Now;
            //db.Expenses.Add(expense);
            db.SaveChanges();
            return $"the expense was added.";
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public string Add(Expense expense)
        {
            expense.Date_Time = DateTime.Now;
            expense.Userid = Int32.Parse(_currentUser.id);
            db.Expenses.Add(expense);
            db.SaveChanges();
            return $"the expense was added.";
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            ViewBag.expensesid = id;
            return View();
        }

        [HttpPost]
        public string Delete(Expense expense)
        {
            db.Expenses.Remove(expense);
            db.SaveChanges();
            return $"the expense was deleted.";
        }


        public static string GetYearWeek(DateTime dat)
        {
            CultureInfo myCI = new CultureInfo("en-US");
            Calendar myCal = myCI.Calendar;

            // Gets the DTFI properties required by GetWeekOfYear.
            CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;
            DayOfWeek myFirstDOW = myCI.DateTimeFormat.FirstDayOfWeek;

            // Displays the number of the current week relative to the beginning of the year.
            //Console.WriteLine("The CalendarWeekRule used for the en-US culture is {0}.", myCWR);
            //Console.WriteLine("The FirstDayOfWeek used for the en-US culture is {0}.", myFirstDOW);
            //Console.WriteLine("Therefore, the current week is Week {0} of the current year.", myCal.GetWeekOfYear(DateTime.Now, myCWR, myFirstDOW));

            int val1 = myCal.GetWeekOfYear(dat, myCWR, myFirstDOW);
            int val2 = myCal.GetYear(dat);
            string ret = val2.ToString() +"/"+ val1.ToString();
            return ret;
            
        }

    }
}