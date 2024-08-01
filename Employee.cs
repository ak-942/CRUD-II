using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeDetails.Models
{
    public class Employee
    {
        [Key]
        [DisplayName("Employee ID")]
        public int Empid { get; set; }

        
        [Required]
        [DisplayName("Employee Name")]
        public string Empname { get; set; }
        [Required]
        [DisplayName("Employee Code")]
        public string Empcode { get; set; }
        [Required]
        [DisplayName("Joining Date")]
        public string Joiningdate { get; set; }
        [Required]
        [DisplayName("Date of Birth")]
        public string DoB { get; set; }
        [Required]
        [DisplayName("Mobile number")]
        public int Mobile { get; set; }
        [Required]
        [DisplayName("Aadhaar number")]
        public int Aadhaar { get; set; }
        [Required]
        [DisplayName("Present address")]
        public string Presentaddress { get; set; }
        [Required]
        [DisplayName("School name")]
        public string School { get; set; }
        [Required]
        [DisplayName("College name")]
        public string College { get; set; }
        [DisplayName("Higher Education(if any)")]
        public string Highereducation { get; set; }

        public string Isdeleted { get; set; }
    }
}