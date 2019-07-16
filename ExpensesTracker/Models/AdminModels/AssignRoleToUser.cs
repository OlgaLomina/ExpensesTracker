using ExpensesTracker.BusinessLogic;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ExpensesTracker.Models.AdminModels
{
    public class AssignRoleToUser
    {
         public string Email { get; set; }

        [DisplayName("User")]
        public string UserId { get; set; }

        [DisplayName("Role")]
        public string RoleId { get; set; }
        public string RoleName { get; set; }

        public List<User> UsersList { get; set; }
        public List<Role> RolesList { get; set; }
    }


}