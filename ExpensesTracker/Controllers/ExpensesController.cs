using ExpensesTracker.BusinessLogic;
using ExpensesTracker.Models;
using ExpensesTracker.Models.Expenses;
using ExpensesTracker.Models.ManageUserModels;
using System;
using System.Collections.Generic;
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
        public ActionResult Index(string dat1, string dat2)
        {
            ExpenseContext db = new ExpenseContext();
            int userid = Int32.Parse(_currentUser.id);

            DateTime dateBeg = new DateTime(DateTime.Now.Year, 1, 1);
            DateTime dateEnd = new DateTime(DateTime.Now.Year, 12, 31);

            ViewBag.Dat1 = dateBeg;
            ViewBag.Dat2 = dateEnd;

            ExpensesListViewModel ex_lvm = new ExpensesListViewModel();
            IQueryable<Expense> expenses = db.Expenses.Where(e => e.Userid == userid);
            if (!String.IsNullOrEmpty(dat1))
            {
                expenses = expenses.Where(e => e.Date_Time >= Convert.ToDateTime(dat1));
            }
            else
            {
                expenses = expenses.Where(e => e.Date_Time >= ex_lvm.DateBegin);
            }

            if (!String.IsNullOrEmpty(dat2))
            {
                expenses = expenses.Where(e => e.Date_Time <= Convert.ToDateTime(dat2));
            }
            else
            {
                expenses = expenses.Where(e => e.Date_Time <= ex_lvm.DateEnd);
            }

            ex_lvm.Expenses = expenses.ToList();
            /*{
                Expenses = expenses.ToList()
            };*/

            ViewBag.Message = _currentUser.First_Name + " " + _currentUser.Last_Name + "( " + _currentUser.Email + " )";
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
    }
}