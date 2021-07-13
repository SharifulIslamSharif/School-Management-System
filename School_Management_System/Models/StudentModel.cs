using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace School_Management_System.Models
{
    public partial class StudentModel : DbContext
    {
        public StudentModel()
            : base("name=StudentModel")
        {
        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<StudentAjax> StudentAjaxes { get; set; }
        public virtual DbSet<Studenttblvm> Studenttblvms { get; set; }
        public virtual DbSet<tblRole> TblRoles { get; set; }
        public virtual DbSet<tblUser> TblUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<StudentAjax>()
                .Property(e => e.StudentName)
                .IsUnicode(false);

            modelBuilder.Entity<StudentAjax>()
                .Property(e => e.StudentAddress)
                .IsUnicode(false);

            modelBuilder.Entity<StudentAjax>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<StudentAjax>()
                .Property(e => e.Contact)
                .IsUnicode(false);

            modelBuilder.Entity<StudentAjax>()
                .Property(e => e.StudentDOB)
                .IsUnicode(false);

            modelBuilder.Entity<StudentAjax>()
                .Property(e => e.ImagePath)
                .IsUnicode(false);

            modelBuilder.Entity<Studenttblvm>()
                .Property(e => e.StudentName)
                .IsUnicode(false);

            modelBuilder.Entity<Studenttblvm>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Studenttblvm>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Studenttblvm>()
                .Property(e => e.Contact)
                .IsUnicode(false);
        }
    }
}
