namespace School_Management_System.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Department")]
    public class Department
    {
        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }
    }
}
