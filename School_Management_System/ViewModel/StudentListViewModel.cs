using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace School_Management_System.ViewModel
{
    public class StudentListViewModel
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public System.DateTime StudentDOB { get; set; }

        public string ImageName { get; set; }

        public string ImageUrl { get; set; }

    }
}