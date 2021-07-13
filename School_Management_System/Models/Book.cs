namespace School_Management_System.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Book")]
    public class Book
    {
        public int BookId { get; set; }

        public string BooktName { get; set; }

        public string Session { get; set; }

        public decimal Rool { get; set; }

        public string ImagePath { get; set; }

        public DateTime? ExpireDate { get; set; }

        public int Quantity { get; set; }

        public int DepartmentId { get; set; }
    }
}
