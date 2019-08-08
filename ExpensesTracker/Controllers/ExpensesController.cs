using ExpensesTracker.BusinessLogic;
using ExpensesTracker.Models;
using ExpensesTracker.Models.Expenses;
using ExpensesTracker.Models.ManageUserModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ExpensesTracker.Controllers
{
    public class ExpensesController : Controller
    {

        //for access to DB
        ExpenseContext db = new ExpenseContext();

        private readonly ManageUser _currentUser = new ManageUser(UserSignInManager.CurrentUser);

        // GET: Expenses
        public ActionResult Index()
        {
            int userid = Int32.Parse(_currentUser.id);
                       
            ExpensesListViewModel ex_lvm = new ExpensesListViewModel();
            var expenses = db.Expenses.Where(e => e.Userid == userid && e.Date_Time >= ex_lvm.DateBegin && e.Date_Time <= ex_lvm.DateEnd).GroupBy(x => x.Date_Time).ToList();

            List<DateTime> dates = new List<DateTime>();
            foreach (var a in expenses)
            {
                dates.Add(a.Key.Date);
            }
            dates = dates.Distinct().ToList();

            foreach (var r in dates)
            {
                DateTime r1 = r.Add(new TimeSpan(0, 23, 59, 59, 999));
                ExpensesAvgPDayViewModel ex_avg = new ExpensesAvgPDayViewModel
                {
                    Expenses = db.Expenses.Where(e => e.Userid == userid && e.Date_Time >= r && e.Date_Time <= r1)
                };
                ex_avg.Avarage_pday = Math.Round(ex_avg.Expenses.Average(e => e.Amount), 2);
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
            int userid = Int32.Parse(_currentUser.id);

            ExpensesListViewModel ex_lvm = new ExpensesListViewModel();
            var expenses = db.Expenses.Where(e => e.Userid == userid && e.Date_Time >= dtFrom && e.Date_Time <= dtTo).GroupBy(x => x.Date_Time).ToList();

            List<DateTime> dates = new List<DateTime>();
            foreach (var a in expenses)
            {
                dates.Add(a.Key.Date);
            }
            dates = dates.Distinct().ToList();

            foreach (var r in dates)
            {
                DateTime r1 = r.Add(new TimeSpan(0, 23, 59, 59, 999));
                ExpensesAvgPDayViewModel ex_avg = new ExpensesAvgPDayViewModel
                {
                    Expenses = db.Expenses.Where(e => e.Userid == userid && e.Date_Time >= r && e.Date_Time <= r1)
                };
                ex_avg.Avarage_pday = Math.Round(ex_avg.Expenses.Average(e => e.Amount), 2);

                ExpensesWeekViewModel ex_week = new ExpensesWeekViewModel();
                ex_week.AllDayExpenses.Add(ex_avg);
                ex_week.Year_Week = GetYearWeek(r);
                ex_week.Sum_pweek = ex_avg.Expenses.Sum(e => e.Amount);
                ex_lvm.WeekExpenses.Add(ex_week);
            }
            ViewBag.Message = _currentUser.First_Name + " " + _currentUser.Last_Name + "( " + _currentUser.Email + " )";
            return View(ex_lvm);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expense expense = db.Expenses.Find(id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            return View(expense);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Expense expense)
        {
            var id = expense.Expense_id;
            var expenseToUpdate = db.Expenses.Find(id);
            if (TryUpdateModel(expenseToUpdate, "", new string[] { "Name", "Amount", "Comment" }))
            {
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(expense);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Expense model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.Date_Time = DateTime.Now;
                    model.Userid = Int32.Parse(_currentUser.id);
                    db.Expenses.Add(model);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(model);
        }


        [HttpGet]
        public ActionResult Delete(int id, bool? saveChangesError = false)
        {
            ViewBag.expensesid = id;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            Expense expense = db.Expenses.Find(id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            return View(expense);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Expense expense = db.Expenses.Find(id);
            try
            {
                db.Expenses.Remove(expense);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = expense.Expense_id, saveChangesError = true });
            }
            return RedirectToAction("Index");
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