using ExpensesTracker.BusinessLogic;
using ExpensesTracker.Models;
using ExpensesTracker.Models.AdminModels;
using ExpensesTracker.Models.ManageUserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpensesTracker.Controllers
{
    public class AdminController : Controller
    {
        //for access to DB
        //UserContext db = new UserContext();
 
        private readonly UserManager _changeUser;
        private readonly ManageUser _currentUser;

        public AdminController()
        {
            _currentUser = new ManageUser(UserSignInManager.CurrentUser);
            _changeUser = new UserManager();
        }

        // GET: Admin
        public ActionResult Index()
        {
             return View(_currentUser);
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
            using (UserContext db = new UserContext())
            {
                assign.UsersList = db.Users.ToList();
                assign.RolesList = db.Roles.ToList();
            }
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
                bool result = _changeUser.ChangeRole(model.UserId, model.RoleId);
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
    }
}