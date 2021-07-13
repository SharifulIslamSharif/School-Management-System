using School_Management_System.Models;
using School_Management_System.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace School_Management_System.Controllers
{
    public class HomesController : Controller
    {
        // GET: Homes
        [AllowAnonymous]

        public ActionResult Index(int? id)
        {
            var ctx = new StudentModel();

            var departmentWiseBookQty = from p in ctx.Books
                                         group p by p.DepartmentId into g
                                         select new
                                         {
                                             g.FirstOrDefault().DepartmentId,
                                             Qty = g.Sum(s => s.Quantity)
                                         };
            var listDepartment = (from c in ctx.Departments
                                join dwbq in departmentWiseBookQty on c.DepartmentId equals dwbq.DepartmentId
                                select new VmDepartment
                                {
                                    DepartmentName = c.DepartmentName,
                                    DepartmentId = dwbq.DepartmentId,
                                    Quantity = dwbq.Qty
                                }).ToList();
            var listBook = (from p in ctx.Books
                               join c in ctx.Departments on p.DepartmentId equals c.DepartmentId
                               where p.DepartmentId == id
                               select new VmBook
                               {
                                   DepartmentId = p.DepartmentId,
                                   DepartmentName = c.DepartmentName,
                                   ExpireDate = p.ExpireDate,
                                   ImagePath = p.ImagePath,
                                   Rool = p.Rool,
                                   BookId = p.BookId,
                                   BooktName = p.BooktName,
                                   Quantity = p.Quantity
                               }).ToList();

            var oDepartmentWiseBook = new VmDepartmentWiseBook();
            oDepartmentWiseBook.DepartmentList = listDepartment;
            oDepartmentWiseBook.BookList = listBook;
            oDepartmentWiseBook.DepartmentId = listBook.Count > 0 ? listBook[0].DepartmentId : 0;
            oDepartmentWiseBook.DepartmentName = listBook.Count > 0 ? listBook[0].DepartmentName : "";

            return View(oDepartmentWiseBook);
        }
        [Authorize(Roles = "Admin, SuperAdmin")]

        public ActionResult Create()
        {
            var model = new VmBookDepartment();
            var ctx = new StudentModel();
            model.DepartmentList = ctx.Departments.ToList();
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin, SuperAdmin")]

        public ActionResult Create(Department model, string[] BooktName, decimal[] Rool, int[] Quantity, DateTime[] ExpireDate, HttpPostedFileBase[] imgFile)
        {
            var ctx = new StudentModel();
            var oDepartment = (from c in ctx.Departments where c.DepartmentName == model.DepartmentName.Trim() select c).FirstOrDefault();
            if (oDepartment == null)
            {
                ctx.Departments.Add(model);
                ctx.SaveChanges();
            }
            else
            {
                model.DepartmentId = oDepartment.DepartmentId;
            }

            var listBook = new List<Book>();
            for (int i = 0; i < BooktName.Length; i++)
            {
                string imgPath = "";
                if (imgFile[i] != null && imgFile[i].ContentLength > 0)
                {
                    var fileName = Path.GetFileName(imgFile[i].FileName);
                    string fileLocation = Path.Combine(
                        Server.MapPath("~/uploads"), fileName);
                    imgFile[i].SaveAs(fileLocation);

                    imgPath = "/uploads/" + imgFile[i].FileName;
                }

                var newBook = new Book();
                newBook.BooktName = BooktName[i];
                newBook.Rool = Rool[i];
                newBook.ExpireDate = ExpireDate[i];
                newBook.ImagePath = imgPath;
                newBook.Quantity = Quantity[i];
                newBook.DepartmentId = model.DepartmentId;
                listBook.Add(newBook);
            }
            ctx.Books.AddRange(listBook);
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin, SuperAdmin")]

        public ActionResult Edit(int id)
        {
            var ctx = new StudentModel();
            var oBook = (from p in ctx.Books
                            join c in ctx.Departments on p.DepartmentId equals c.DepartmentId
                            where p.BookId == id
                            select new VmBook
                            {
                                DepartmentId = p.DepartmentId,
                                DepartmentName = c.DepartmentName,
                                ExpireDate = p.ExpireDate,
                                ImagePath = p.ImagePath,
                                Rool = p.Rool,
                                BookId = p.BookId,
                                BooktName = p.BooktName,
                                Quantity = p.Quantity
                            }).FirstOrDefault();
            oBook.DepartmentList = ctx.Departments.ToList(); 
            return View(oBook);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, SuperAdmin")]

        public ActionResult Edit(VmBook model)
        {
            var ctx = new StudentModel();

            string imgPath = "";
            if (model.ImgFile != null && model.ImgFile.ContentLength > 0)
            {
                var fileName = Path.GetFileName(model.ImgFile.FileName);
                string fileLocation = Path.Combine(
                    Server.MapPath("~/uploads"), fileName);
                model.ImgFile.SaveAs(fileLocation);

                imgPath = "/uploads/" + model.ImgFile.FileName;
            }

            var oBook = ctx.Books.Where(w => w.BookId == model.BookId).FirstOrDefault();
            if (oBook != null)
            {
                oBook.BooktName = model.BooktName;
                oBook.Quantity = model.Quantity;
                oBook.Rool = model.Rool;
                oBook.ExpireDate = model.ExpireDate;
                oBook.DepartmentId = model.DepartmentId;
                if (!string.IsNullOrEmpty(imgPath))
                {
                    var fileName = Path.GetFileName(oBook.ImagePath);
                    string fileLocation = Path.Combine(Server.MapPath("~/uploads"), fileName);
                    if (System.IO.File.Exists(fileLocation))
                    {
                        System.IO.File.Delete(fileLocation);
                    }
                }
                oBook.ImagePath = imgPath == "" ? oBook.ImagePath : imgPath;

                ctx.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin, SuperAdmin")]

        public ActionResult EditMultiple(int id)
        {
            var ctx = new StudentModel();
            var oDepartmentWiseBook = new VmDepartmentWiseBook();
            var listBook = (from p in ctx.Books
                               join c in ctx.Departments on p.DepartmentId equals c.DepartmentId
                               where p.DepartmentId == id
                               select new VmBook
                               {
                                   DepartmentId = p.DepartmentId,
                                   DepartmentName = c.DepartmentName,
                                   ExpireDate = p.ExpireDate,
                                   ImagePath = p.ImagePath,
                                   Rool = p.Rool,
                                   BookId = p.BookId,
                                   BooktName = p.BooktName,
                                   Quantity = p.Quantity
                               }).ToList();
            oDepartmentWiseBook.BookList = listBook;
            // for showing category list in view
            oDepartmentWiseBook.DepartmentList = (from c in ctx.Departments
                                                 select new VmDepartment
                                                 {
                                                     DepartmentId = c.DepartmentId,
                                                     DepartmentName = c.DepartmentName
                                                 }).ToList();
            oDepartmentWiseBook.DepartmentId = listBook.Count > 0 ? listBook[0].DepartmentId : 0;
            oDepartmentWiseBook.DepartmentName = listBook.Count > 0 ? listBook[0].DepartmentName : "";
            return View(oDepartmentWiseBook);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, SuperAdmin")]

        public ActionResult EditMultiple(Department model, int[] BookId, string[] BooktName, decimal[] Rool, int[] Quantity, DateTime[] ExpireDate, HttpPostedFileBase[] imgFile)
        {
            var ctx = new StudentModel();
            var listBook = new List<Book>();
            for (int i = 0; i < BooktName.Length; i++)
            {
                if (BookId[i] > 0)
                {
                    string imgPath = "";
                    if (imgFile[i] != null && imgFile[i].ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(imgFile[i].FileName);
                        string fileLocation = Path.Combine(
                            Server.MapPath("~/uploads"), fileName);
                        imgFile[i].SaveAs(fileLocation);

                        imgPath = "/uploads/" + imgFile[i].FileName;
                    }
                    int pid = BookId[i];
                    var oBook = ctx.Books.Where(w => w.BookId == pid).FirstOrDefault();
                    if (oBook != null)
                    {
                        oBook.BooktName = BooktName[i];
                        oBook.Quantity = Quantity[i];
                        oBook.Rool = Rool[i];
                        oBook.ExpireDate = ExpireDate[i];
                        oBook.DepartmentId = model.DepartmentId;
                        if (!string.IsNullOrEmpty(imgPath))
                        {
                            var fileName = Path.GetFileName(oBook.ImagePath);
                            string fileLocation = Path.Combine(Server.MapPath("~/uploads"), fileName);
                            if (System.IO.File.Exists(fileLocation))
                            {
                                System.IO.File.Delete(fileLocation);
                            }
                        }
                        oBook.ImagePath = imgPath == "" ? oBook.ImagePath : imgPath;
                        ctx.SaveChanges();
                    }
                }
                else if (!string.IsNullOrEmpty(BooktName[i]))
                {
                    string imgPath = "";
                    if (imgFile[i] != null && imgFile[i].ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(imgFile[i].FileName);
                        string fileLocation = Path.Combine(
                            Server.MapPath("~/uploads"), fileName);
                        imgFile[i].SaveAs(fileLocation);

                        imgPath = "/uploads/" + imgFile[i].FileName;
                    }

                    var newBook = new Book();
                    newBook.BooktName = BooktName[i];
                    newBook.Quantity = Quantity[i];
                    newBook.Rool = Rool[i];
                    newBook.ExpireDate = ExpireDate[i];
                    newBook.ImagePath = imgPath;
                    newBook.Quantity = Quantity[i];
                    newBook.DepartmentId = model.DepartmentId;
                    ctx.Books.Add(newBook);
                    ctx.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin, SuperAdmin")]

        public ActionResult Delete(int id)
        {
            var ctx = new StudentModel();
            var oBook = ctx.Books.Where(p => p.BookId == id).FirstOrDefault();
            if (oBook != null)
            {
                ctx.Books.Remove(oBook);
                ctx.SaveChanges();

                var fileName = Path.GetFileName(oBook.ImagePath);
                string fileLocation = Path.Combine(
                    Server.MapPath("~/uploads"), fileName);
                // Check if file exists with its full path    
                if (System.IO.File.Exists(fileLocation))
                {
                    // If file found, delete it    
                    System.IO.File.Delete(fileLocation);
                }
            }

            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin, SuperAdmin")]

        public ActionResult DeleteMultiple(int id)
        {
            var ctx = new StudentModel();
            var listBook = ctx.Books.Where(p => p.DepartmentId == id).ToList();
            foreach (var oBook in listBook)
            {
                if (oBook != null)
                {
                    ctx.Books.Remove(oBook);
                    ctx.SaveChanges();

                    var fileName = Path.GetFileName(oBook.ImagePath);
                    string fileLocation = Path.Combine(
                        Server.MapPath("~/uploads"), fileName);
                    // Check if file exists with its full path    
                    if (System.IO.File.Exists(fileLocation))
                    {
                        // If file found, delete it    
                        System.IO.File.Delete(fileLocation);
                    }
                }
            }

            var oCategory = ctx.Departments.Where(c => c.DepartmentId == id).FirstOrDefault();
            ctx.Departments.Remove(oCategory);
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}