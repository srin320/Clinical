using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using ClinicalDAL.EF;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CASProject.ViewModel
{


    public class UserViewModel
    {

        public int UserId { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is a required field")]
        [MinLength(3, ErrorMessage = "Name must be at least 3 character")]
        public string Name { get; set; }


        [Required(ErrorMessage = "DOB is required")]
        public DateTime DOB { get; set; }



        [Required(ErrorMessage = "Phone is required")]
        [RegularExpression(@"\d{10}", ErrorMessage = "Phone number invalid")]
        public string Phone { get; set; }



        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"\w+@\w+\.\w+", ErrorMessage = "Email is invalid format")]
        public string Email { get; set; }



        public string Gender { get; set; }

        public string Password { get; set; }


        public string Username { get; set; }
        public string Address { get; set; }

        public int RoleId { get; set; }
        public string Qualification { get; set; }
        public string RegNo { get; set; }
        public string DeptName { get; set; }

        // [Range(typeof(byte), "1", "4", ErrorMessage = "Status should be 1 through 4")]


        public HashSet<Role> Roles { get; set; }

    }
}