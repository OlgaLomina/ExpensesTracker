using ExpensesTracker.BusinessLogic;
using ExpensesTracker.Models;
using ExpensesTracker.Models.ManageUserModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;

namespace ExpensesTracker.Controllers
{
    public class ManageUserController : Controller
    {
        private readonly UserManager _userManager;
        private readonly ManageUser _currentUser;

        public ManageUserController()
        {
            _currentUser = new ManageUser(UserSignInManager.CurrentUser);
            _userManager = new UserManager();
        }

        // GET: ManageUser
        public ActionResult Index()
        {
            return View(_currentUser);
        }

        
        // GET: /ManageUser/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /ManageUser/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordUser model)
        {
            if (ModelState.IsValid)
            {
                bool result = _userManager.ChangePassword(UserSignInManager.CurrentUser, model.OldPassword, model.NewPassword);
                if (result)
                {
                    return RedirectToAction("Index", new { Message = "Your password has been changed successfully." });
                }
                else
                {
                    ModelState.AddModelError("Something wrong", "Try again");
                }
            }
            return View(model);
        }

        // GET: /ManageUser/ManageLoginsUser
        public ActionResult ManageLoginsUser()
        {
            return View();
        }

        //
        // POST: /ManageUser/ManageLoginsUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageLoginsUser(ManageLoginsUser model)
        {
            if (ModelState.IsValid)
            {
 
            }
            return View(model);
        }

    }

}
