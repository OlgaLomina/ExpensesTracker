using ExpensesTracker.BusinessLogic;
using ExpensesTracker.Models.ManageUserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpensesTracker.Controllers
{
    public class BaseController : Controller
    {
        private readonly ManageUser _currentUser;

        public BaseController()
        {
            if (UserSignInManager.CurrentUser != null)
            { 
                _currentUser = new ManageUser(UserSignInManager.CurrentUser);
            }
        }

        public ManageUser CurrentUser
        {
            get
            {
                if (_currentUser == null)
                    RedirectToAction("Account", "Login");

                return _currentUser;
            }
        }
    }
}