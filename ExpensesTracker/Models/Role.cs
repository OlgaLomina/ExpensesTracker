using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExpensesTracker.Models
{
    public class Role
    {
        [Key] public int Role_Id { get; set; }
        [DisplayName("Role Name")]
        public string Name { get; set; }
    }
}