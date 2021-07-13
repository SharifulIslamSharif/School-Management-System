using PagedList;
using School_Management_System.Models;
using School_Management_System.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace School_Management_System.Controllers
{
    public class StudentViewModelController : Controller
    {
        // GET: StudentViewModel
        private StudentModel db = new StudentModel();
        [AllowAnonymous]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
             
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var emp = from s in db.Studenttblvms
                      select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                emp = emp.Where(s => s.StudentName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    emp = emp.OrderByDescending(s => s.StudentName);
                    break;

                default:  // Name ascending
                    emp = emp.OrderBy(s => s.StudentName);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(emp.ToPagedList(pageNumber, pageSize));
            
        }
        [HttpGet]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public ActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Admin, SuperAdmin")]
        public ActionResult AddOrEdit(StudentCreateViewModel viewObj)
        {
            var result = false;
            string fileName = Path.GetFileNameWithoutExtension(viewObj.ImageFile.FileName);
            string extension = Path.GetExtension(viewObj.ImageFile.FileName);
            string fileWithExtension = fileName + extension;
            Studenttblvm trObj = new Studenttblvm();
            trObj.StudentName = viewObj.StudentName;
            trObj.Address = viewObj.Address;
            trObj.Email = viewObj.Email;
            trObj.Contact = viewObj.Contact;
            trObj.StudentDOB = viewObj.StudentDOB;
            trObj.ImageName = fileWithExtension;
            trObj.ImageUrl = "~/Images/" + fileName + extension;
            string serverPath = Path.Combine(Server.MapPath("~/Images/" + fileName + extension));
            viewObj.ImageFile.SaveAs(serverPath);
            if (ModelState.IsValid)
            {
                if (viewObj.StudentId == 0)
                {
                    db.Studenttblvms.Add(trObj);
                    db.SaveChanges();
                    result = true;
                }
                else
                {
                    trObj.StudentId = viewObj.StudentId;
                    db.Entry(trObj).State = EntityState.Modified;
                    db.SaveChanges();
                    result = true;
                }
            }
            if (result)
            {
                return RedirectToAction("Index");
            }
            else
            {
                if (viewObj.StudentId == 0)
                {
                    return View("Create");
                }
                else
                {
                    return View("Edit");
                }
            }
        }
        [Authorize(Roles = "Admin, SuperAdmin")]
        public ActionResult Edit(int id)
        {
            Studenttblvm trObj = db.Studenttblvms.SingleOrDefault(t => t.StudentId == id);
            StudentCreateViewModel viewObj = new StudentCreateViewModel();
            viewObj.StudentId = trObj.StudentId;
            viewObj.StudentName = trObj.StudentName;
            viewObj.Address = trObj.Address;
            viewObj.Email = trObj.Email;
            viewObj.Contact = trObj.Contact;
            viewObj.StudentDOB = trObj.StudentDOB;
            viewObj.ImageUrl = trObj.ImageUrl;
            viewObj.ImageName = trObj.ImageName;
            return View(viewObj);
        }
        [Authorize(Roles = "Admin, SuperAdmin")]
        public ActionResult Delete(int id)
        {
            Studenttblvm trObj = db.Studenttblvms.SingleOrDefault(t => t.StudentId == id);
            {
                db.Studenttblvms.Remove(trObj);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}