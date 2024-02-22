using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace frontend.Models
{
    public class EmployeeModel
    {
        public int c_empid { get; set; }
        public string? c_name { get; set; }
        public string? c_email { get; set; }
        public string? c_password { get; set; }
        public string? c_gender { get; set; }
        public DateTime c_dob { get; set; }
        public string? c_hobby { get; set; }
        public string? c_profile { get; set; }
        public string? c_role { get; set; }
        public int c_cityid { get; set; }
        public int c_departmentid { get; set; }
    }
}