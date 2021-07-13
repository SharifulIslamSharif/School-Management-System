using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace School_Management_System.ViewModel
{
    public class VmDepartmentWiseBook
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public List<VmDepartment> DepartmentList { get; set; }
        public List<VmBook> BookList { get; set; }

    }
}