using System;
using System.ComponentModel.DataAnnotations;

namespace School_Management_System.ViewModel
{
    internal class CustomDateOfBirthAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dateTime = Convert.ToDateTime(value);
            return dateTime <= DateTime.Now;
        }
    }
}