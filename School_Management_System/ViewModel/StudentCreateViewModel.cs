using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace School_Management_System.ViewModel
{
    public class StudentCreateViewModel
    {
        public int StudentId { get; set; }
    
        [Required(ErrorMessage = "Employee Name Is Required")]
        [DataType(DataType.Text)]
        [Display(Name = "Student Name")]
        [StringLength(50, ErrorMessage = "Player Name Must be within 50 Charecter")]
        [CustomExcludeCharacter("/.,!@#$%")]
        public string StudentName { get; set; }
        
        [DisplayName("Student Address")]
        [Required(ErrorMessage = "Address Is Required")]
        [DataType(DataType.Text)]
        [StringLength(500, ErrorMessage = "Address Must be within 500 Charecter")]
        public string Address { get; set; }
        
        [DisplayName("Email")]
        [Required(ErrorMessage = "Email Is Required")]
        [RegularExpression(@"^[\w-\._\+%]+@(?:[\w-]+\.)+[\w]{2,6}$", ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(\d{11})$", ErrorMessage = "Wrong Phone Number")]
        public string Contact { get; set; }

        [DisplayName("Date Of Birth")]
        [CustomDateOfBirth(ErrorMessage = "Date of Birth must be less than or equal to Today's Date")]
        public System.DateTime StudentDOB { get; set; }
        [DisplayName("Image Name")]
        public string ImageName { get; set; }
        public string ImageUrl { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }

    }
}


