using School_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace School_Management_System.Controllers
{
    [AllowAnonymous]

    public class AjaxCrudController : Controller
    {
        // GET: Ajax
        [AllowAnonymous]

        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]

        public ActionResult ViewAll()
        {
            return View(GetAllEmployee());
        }
        IEnumerable<StudentAjax> GetAllEmployee()
        {
            using (StudentModel db = new StudentModel())
            {
                return db.StudentAjaxes.ToList<StudentAjax>();
            }

        }
        [Authorize(Roles = "Admin, SuperAdmin")]

        public ActionResult AddOrEdit(int id = 0)
        {
            StudentAjax emp = new StudentAjax();
            if (id != 0)
            {
                using (StudentModel db = new StudentModel())
                {
                    emp = db.StudentAjaxes.Where(x => x.StudentID == id).FirstOrDefault<StudentAjax>();
                }
            }
            return View(emp);
        }
        [HttpPost]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public ActionResult AddOrEdit(StudentAjax emp)
        {
            try
            {
                if (emp.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(emp.ImageUpload.FileName);
                    string extension = Path.GetExtension(emp.ImageUpload.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    emp.ImagePath = "~/AppFiles/Images/" + fileName;
                    emp.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/AppFiles/Images/"), fileName));
                }
                using (StudentModel db = new StudentModel())
                {
                    if (emp.StudentID == 0)
                    {
                        db.StudentAjaxes.Add(emp);
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Entry(emp).State = EntityState.Modified;
                        db.SaveChanges();

                    }
                }
                return RedirectToAction("Index");
                //return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllEmployee()), message = "Submitted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [Authorize(Roles = "Admin, SuperAdmin")]
         public ActionResult Delete(int id)
        {
            try
            {
                using (StudentModel db = new StudentModel())
                {
                    StudentAjax emp = db.StudentAjaxes.Where(x => x.StudentID == id).FirstOrDefault<StudentAjax>();
                    db.StudentAjaxes.Remove(emp);
                    db.SaveChanges();
                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllEmployee()), message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}