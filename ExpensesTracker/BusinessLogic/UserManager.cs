using System.Threading.Tasks;
using ExpensesTracker.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Linq;
using System;

namespace ExpensesTracker.BusinessLogic
{
    // Configure the application user manager used in this application. 
    // UserManager is defined in ASP.NET Identity and is used by the application.
    public class UserManager
    {
        public bool Create(AppUser user)
        {
            var resultUser = FindByEmail(user.Email);
            if (resultUser != null)
            {
                return true;
            }


            //create new user
            try
            {
                using (UserContext db = new UserContext())
                {
                    db.Users.Add(new User { First_Name = user.FirstName, Last_Name = user.LastName, Email = user.Email, User_Password = user.PasswordHash, Date_Time = DateTime.Now, Role_Id = 2 });
                    db.SaveChanges();
                }
            }
            catch //(Exception e)
            {
                return false;
            }


            return false;
        }

        public AppUser FindByEmail(string email)
        {
            //check is there the same email in the db
            using (UserContext db = new UserContext())
            {
                var user = db.Users.Where(u => u.Email == email).FirstOrDefault();

                if (user != null)
                {
                    AppUser appUser = new AppUser(user);
                    return appUser;
                }
                else
                {
                    return null;
                }
            }
        }

        public AppUser FindByEmailAndPassword(string email, string password)
        {
            using (UserContext db = new UserContext())
            {
                var user = db.Users.Where(u => u.Email == email && u.User_Password == password).FirstOrDefault();

                if (user != null)
                {
                    AppUser appUser = new AppUser(user);
                    return appUser;
                }
                else
                {
                    return null;
                }
            }

        }

        public AppUser FindById(string id)
        {
            using (UserContext db = new UserContext())
            {
                var user = db.Users.Where(u => u.UserId.ToString() == id).FirstOrDefault();

                if (user != null)
                {
                    AppUser appUser = new AppUser(user);
                    return appUser;
                }
                else
                {
                    return null;
                }
            }
        }

        public bool ChangePassword(AppUser user, string oldPassword, string newPassword)
        {
            if (user != null)
            {
                if (user.PasswordUser == oldPassword)
                {
                    //compare oldPassword and user's password 
                    try
                    {
                        using (UserContext db = new UserContext())
                        {
                            var password = db.Users.First(a => a.UserId.ToString() == user.Id);
                            password.User_Password = newPassword;
                            db.SaveChanges();
                        }
                    }
                    catch (Exception e)
                    {
                        return false;
                    }


                    return true;
                }
            }
            return false;
        }

        public bool ChangeRole(string id, string newRole)
        {
            if (id != null && newRole != null)
            {
                try
                {
                    using (UserContext db = new UserContext())
                    {
                        var role = db.Users.First(a => a.UserId.ToString() == id);
                        role.Role_Id = Int32.Parse(newRole);
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    return false;
                }
                return true;
            }
            return false;
        }
    }
   
}