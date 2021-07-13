using School_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace School_Management_System.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult CreateRole()
        {
            using (var _context = new StudentModel())
            {
                List<tblUser> userList = _context.TblUsers.ToList();
                ViewBag.Users = new SelectList(userList, "ID", "UserName");
                return View();
            }
        }
        [HttpPost]
        public ActionResult CreateRole(tblRole obj)
        {
            using (var _context = new StudentModel())
            {
                _context.TblRoles.Add(obj);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Index()
        {
            using (var _context = new StudentModel())
            {
                var UserRoleList = (from user in _context.TblUsers join role in _context.TblRoles on user.ID equals role.UserID select new { UserID = user.ID, RoleID = role.ID, UserName = user.UserName, RoleName = role.RoleName }).ToList();
                List<ViewModelUser> list = new List<ViewModelUser>();
                foreach (var item in UserRoleList)
                {
                    ViewModelUser obj = new ViewModelUser();
                    obj.RoleID = item.RoleID;
                    obj.RoleName = item.RoleName;
                    obj.ID = item.UserID;
                    obj.UserName = item.UserName;
                    list.Add(obj);
                }
                return View(list);
            }
        }
        public ActionResult Edit(int id)
        {
            using (var _context = new StudentModel())
            {
                tblRole role = _context.TblRoles.Find(id);
                List<tblUser> userList = _context.TblUsers.ToList();
                ViewBag.Users = new SelectList(userList, "ID", "UserName");
                return View(role);
            }
        }
        [HttpPost]
        public ActionResult Edit(tblRole obj)
        {
            using (var _context = new StudentModel())
            {
                bool IsExist = !_context.TblRoles.Any(u => u.RoleName == obj.RoleName && u.UserID == obj.UserID);
                if (IsExist)
                {
                    tblRole roleObj = _context.TblRoles.Find(obj.ID);
                    roleObj.RoleName = obj.RoleName;
                    roleObj.UserID = obj.UserID;
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    tblRole role = _context.TblRoles.Find(obj.ID);
                    List<tblUser> userList = _context.TblUsers.ToList();
                    ViewBag.Users = new SelectList(userList, "ID", "UserName");
                    ModelState.AddModelError("", "Role Already Exist");
                    return View();
                }

            }

        }
        public ActionResult Delete(int id)
        {
            using (var _context = new StudentModel())
            {
                tblRole role = _context.TblRoles.Find(id);
                role.TblUser = _context.TblUsers.Find(role.ID);
                return View(role);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            using (var _context = new StudentModel())
            {
                tblRole role = _context.TblRoles.Find(id);
                if (role != null)
                {
                    _context.TblRoles.Remove(role);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    role.TblUser = _context.TblUsers.Find(role.ID);
                    return View(role);
                }

            }

        }
        public ActionResult Details(int id)
        {
            using (var _context = new StudentModel())
            {
                tblRole role = _context.TblRoles.Find(id);
                List<tblUser> userList = _context.TblUsers.ToList();
                ViewBag.Users = new SelectList(userList, "ID", "UserName");
                return View(role);
            }
        }
    }
}