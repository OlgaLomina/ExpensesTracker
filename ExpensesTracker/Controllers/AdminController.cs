using ExpensesTracker.BusinessLogic;
using ExpensesTracker.Models;
using ExpensesTracker.Models.AdminModels;
using ExpensesTracker.Models.ManageUserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ExpensesTracker.Controllers
{
    public class AdminController : BaseController
    {
        //for access to DB
        ExpenseContext db = new ExpenseContext();
        UserContext dbUser = new UserContext();

        private readonly UserManager _assignRoleUserManager;

        public AdminController()
        {
            _assignRoleUserManager = new UserManager();
        }

        // GET: Admin
        //[Authorize(Roles = AppRole.Admin)]
        public ActionResult Index()
        {
            return View(CurrentUser);
        }

        // GET: Admin
        public ActionResult UserList()
        {
            UserContext db = new UserContext();
            List<User> users = db.Users.ToList();

            List<UserViewModel> userList = users.Select(x => new UserViewModel
            {
                First_Name = x.First_Name,
                Last_Name = x.Last_Name,
                Email = x.Email,
                RoleName = x.RoleName.Name
            }).ToList();

            return View(userList);
        }

        // GET: /Admin/AssignRoleToUser
        public ActionResult AssignRoleToUser()
        {
            AssignRoleToUser assign = new AssignRoleToUser();
            assign.UsersList = dbUser.Users.ToList();
            assign.RolesList = dbUser.Roles.ToList();
            return View(assign);
        }

        //
        // POST: /Admin/AssignRoleToUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = AppRole.Admin)]
        public ActionResult AssignRoleToUser(AssignRoleToUser model)
        {
            if (ModelState.IsValid)
            {
                bool result = _assignRoleUserManager.ChangeRole(model.UserId, model.RoleId);
                if (result)
                {
                    return RedirectToAction("Index", new { Message = "The Role has been changed successfully." });
                }
                else
                {
                    ModelState.AddModelError("Something wrong", "Try again");
                }
            }
            return View(model);
        }

        // GET: /Admin/ManageExpenses
        public ActionResult ManageExpenses(int? userid, string dat11, string dat22)
        {
            ManageExpensesListViewModel mex_lvm = new ManageExpensesListViewModel();
            mex_lvm.UsersList = dbUser.Users.ToList();
            mex_lvm.UserId = userid.HasValue ? userid.Value : 0;
            DateTime dtFrom;
            DateTime dtTo;
            if (dat11 == null)
            {
                dtFrom = mex_lvm.DateBegin;
                dat11 = mex_lvm.Dat1;
            }
            else
            {
                dtFrom = Convert.ToDateTime(dat11);
                mex_lvm.DateBegin = Convert.ToDateTime(dat11);
                mex_lvm.Dat1 = dat11;
            }
            if (dat22 == null)
            {
                dtTo = mex_lvm.DateEnd;
                dat22 = mex_lvm.Dat2;
            }
            else
            {
                dtTo = Convert.ToDateTime(dat22);
                mex_lvm.DateEnd = Convert.ToDateTime(dat22);
                mex_lvm.Dat2 = dat22;
            }
            var user = dbUser.Users.SingleOrDefault(u => u.UserId == userid);
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
                ManageExpensesAvgPDayViewModel mex_avg = new ManageExpensesAvgPDayViewModel
                {
                    Expenses = db.Expenses.Where(e => e.Userid == userid && e.Date_Time >= r && e.Date_Time <= r1)
                };
                mex_avg.Avarage_pday = Math.Round(mex_avg.Expenses.Average(e => e.Amount), 2);

                ManageExpensesWeekViewModel ex_week = new ManageExpensesWeekViewModel();
                ex_week.AllDayExpenses.Add(mex_avg);
                ex_week.Year_Week = ExpensesTracker.Controllers.ExpensesController.GetYearWeek(r);
                ex_week.Sum_pweek = mex_avg.Expenses.Sum(e => e.Amount);
                mex_lvm.WeekExpenses.Add(ex_week);
            }

            if (user == null)
            {
                ViewBag.Message = "User is not selected.";
            }
            else
            {
                ViewBag.Message = user.First_Name + " " + user.Last_Name + "( " + user.Email + " )";
            }
            return View(mex_lvm);
        }

        // POST: /Admin/ManageExpenses
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageExpenses(int userid, string dat1, string dat2)
        {
            return RedirectToAction("ManageExpenses", new { UserId = userid, dat11 = dat1, dat22 = dat2 });
        }

        [HttpGet]
        public ActionResult Add(int id)
        {
            if (id == null && id == 0)
            {
                ViewBag.Message = "You did not choose User";
                return View();
            }
            else
            {
                var user = dbUser.Users.SingleOrDefault(u => u.UserId == id);
                ViewBag.Message = "Add a new expense for user: " + user.Email;
            }
            return View();
         }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Expense model, int id, string dat1, string dat2)
        {
            if (id != null && id != 0)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        model.Date_Time = DateTime.Now;
                        model.Userid = id;
                        db.Expenses.Add(model);
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
                
            }
            else
            {
                ModelState.AddModelError("", "Unable to save changes. You did not choose User");
            }
            return RedirectToAction("ManageExpenses", new { UserId = id, dat11 = dat1, dat22 = dat2 });
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
        public ActionResult Edit(Expense expense, string dat1, string dat2)
        {
            var id = expense.Expense_id;
            int userid = expense.Userid;
            var expenseToUpdate = db.Expenses.Find(id);
            if (TryUpdateModel(expenseToUpdate, "", new string[] { "Name", "Amount", "Comment" }))
            {
                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return RedirectToAction("ManageExpenses", new { UserId = userid, dat11 = dat1, dat22 = dat2 });
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
        public ActionResult Delete(int id, int userid, string dat1, string dat2)
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
                ModelState.AddModelError("", "Unable to delete. Try again, and if the problem persists see your system administrator.");
            }
            return RedirectToAction("ManageExpenses", new { UserId = userid, dat11 = dat1, dat22 = dat2 });
        }
    }
}