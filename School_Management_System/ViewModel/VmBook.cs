using School_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace School_Management_System.ViewModel
{
    public class VmBook
    {
        public int BookId { get; set; }
        public string BooktName { get; set; }
        public decimal Rool { get; set; }
        public string ImagePath { get; set; }
        public DateTime? ExpireDate { get; set; }
        public int DepartmentId { get; set; }
        public int Quantity { get; set; }
        public string DepartmentName { get; set; }
        public HttpPostedFileBase ImgFile { get; set; }
        public List<Department> DepartmentList { get; set; }

    }
}