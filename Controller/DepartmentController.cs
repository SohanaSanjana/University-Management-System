using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UniversityMvcApp.BLL;
using UniversityMvcApp.Models;

namespace UniversityMvcApp.Controllers
{
    public class DepartmentController : Controller
    {
        DepartmentManager aDepartmentManager=new DepartmentManager();
        private UniversityDbContext db = new UniversityDbContext();

        // GET: /DepartmentId/
        public ActionResult Index()
        {
            return View(db.Departments.ToList());
        }

        // GET: /DepartmentId/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // GET: /DepartmentId/Create
        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Department department)
        {


            if (!aDepartmentManager.IsCodeExist(department.Code))
            {
                if (!aDepartmentManager.IsNameExist(department.Name))
                {
                    if (ModelState.IsValid)
                    {
                        db.Departments.Add(department);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }

                    return View();
                }
                else
                {
                    @ViewBag.ExistMessage = "Department name already exists!";
                    return View();
                }
            }
            else
            {
                @ViewBag.ExistMessage = "Department code already exists!";
                return View();
            }
        }

       
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
