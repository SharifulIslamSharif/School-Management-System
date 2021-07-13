using School_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace School_Management_System.ViewModel
{
    public class VmBookDepartment
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public List<Department> DepartmentList { get; set; }
        public List<VmBook> BookList { get; set; }

    }
}