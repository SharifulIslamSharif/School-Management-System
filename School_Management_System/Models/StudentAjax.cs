namespace School_Management_System.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("StudentAjax")]
    public partial class StudentAjax
    {
        [Key]
        public int StudentID { get; set; }

        [StringLength(50)]
        public string StudentName { get; set; }

        [StringLength(50)]
        public string StudentAddress { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Contact { get; set; }

        [StringLength(50)]
        public string StudentDOB { get; set; }

        [StringLength(500)]
        public string ImagePath { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageUpload { get; set; }

        public StudentAjax()
        {
            ImagePath = "~/AppFiles/Images/default.png";
        }
    }
}
