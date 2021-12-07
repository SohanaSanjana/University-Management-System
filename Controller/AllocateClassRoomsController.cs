using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UniversityMvcApp.BLL;
using UniversityMvcApp.Models;

namespace UniversityMvcApp.Controllers
{
    public class AllocateClassRoomsController : Controller
    {
        private UniversityDbContext db = new UniversityDbContext();

        AllocateClassRoomManager allocateClassroomManager = new AllocateClassRoomManager();

        // GET: AllocateClassRooms
        public ActionResult Index()
        {
            var allocateClassRooms = db.AllocateClassRooms.Include(a => a.Course).Include(a => a.Department).Include(a => a.Room);
            return View(allocateClassRooms.ToList());
        }

        // GET: AllocateClassRooms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllocateClassRoom allocateClassRoom = db.AllocateClassRooms.Find(id);
            if (allocateClassRoom == null)
            {
                return HttpNotFound();
            }
            return View(allocateClassRoom);
        }

        // GET: AllocateClassRooms/Create
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Code");
            //ViewBag.DepartmentId = new SelectList(db.Departments, "ID", "Code");
            ViewBag.DepartmentId = db.Departments.ToList();

            ViewBag.RoomId = new SelectList(db.Rooms, "Id", "RoomNo");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(AllocateClassRoom allocateClassRoom)
        {
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Code", allocateClassRoom.CourseId);
            ViewBag.DepartmentId = db.Departments.ToList();
            ViewBag.RoomId = new SelectList(db.Rooms, "Id", "RoomNo", allocateClassRoom.RoomId);
            allocateClassRoom.StartTime = DateTime.ParseExact(allocateClassRoom.From, "h:mm tt", CultureInfo.InvariantCulture).TimeOfDay;
            allocateClassRoom.FinishTime = DateTime.ParseExact(allocateClassRoom.TO, "h:mm tt", CultureInfo.InvariantCulture).TimeOfDay;
            if (!AllocateClassRoomManager.HasClass(allocateClassRoom))
            {
                if (allocateClassroomManager.IsTimeAlocated(allocateClassRoom))
                {
                    ViewBag.Errormessage = "Room Is Allocated For This Time Interval";
                    return View();
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        db.AllocateClassRooms.Add(allocateClassRoom);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                        ViewBag.saveMessage = "Allocated Succesfully!!!";
                    }


                    return View(allocateClassRoom);

                }
            }
            else
            {
                ViewBag.Errormessage = "This Course has already been allocated at this time";
                return View(allocateClassRoom);
            }
           
        }

        public JsonResult GetCourseByDepartmentId(int departmentId)
        {
            //List<Course> course = db.Courses.ToList();

            List<Course> course = allocateClassroomManager.GetCourseByDepartmentId();
            var courses = course.Where(a => a.DepartmentId == departmentId).ToList();
            return Json(courses);
            //return Json(courses, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetRoomsByDepartmentId(int departmentId)
        {
            List<Room> rooms = db.Rooms.ToList();
            var room = rooms.Where(a => a.DepartmentId == departmentId).ToList();
            return Json(room);
           
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
