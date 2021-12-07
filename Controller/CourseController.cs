using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UniversityMvcApp.BLL;
using UniversityMvcApp.DAL.Gateway;
using UniversityMvcApp.Models;

namespace UniversityMvcApp.Controllers
{
    
    public class CourseController : Controller
    {
        DepartmentGateway aDepartmentGateway = new DepartmentGateway();
      
        CourseManager courseManager=new CourseManager();
        private UniversityDbContext db = new UniversityDbContext();

        // GET: /Course/
        // GET: /Course/Create
        public ActionResult Create()
        {
            //ViewBag.Departments =  aDepartmentGateway.GetAllDepartment().Where( x=>x.ID<1000).Select(x=> new SelectListItem{Text = x.Name,Value = x.ID.ToString()});
            
            ViewBag.DepartmentId= new SelectList(db.Departments,"ID","Code");
            ViewBag.SemesterID = new SelectList(db.Semesters,"ID","SemesterNumber");
            return View();
        }

      

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Course course)
        {
            //ViewBag.Departments = aDepartmentGateway.GetAllDepartment().Where(x => x.ID < 100).Select(x => new SelectListItem { Text = x.Name, Value = x.ID.ToString() });
            ViewBag.DepartmentId = new SelectList(db.Departments, "ID", "Code");
            ViewBag.SemesterID = new SelectList(db.Semesters, "ID", "SemesterNumber");
            if (!courseManager.IsCodeExist(course.Code))
            {
                if (!courseManager.IsNameExist(course.Name))
                {
                    if (ModelState.IsValid)
                    {
                        db.Courses.Add(course);
                        db.SaveChanges();
                        @ViewBag.saveMessage = "Course Saved!";

                    }
                    
                    return View(course);

                }
                else
                {
                    @ViewBag.ExistMessage = "Course name already exists!";
                     return View();
                }
            }
            else
            {
                @ViewBag.ExistMessage = " Course code already exists!!";
                 return View();
            }
        }

        // GET: /Course/Edit/5

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
