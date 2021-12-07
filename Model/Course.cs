using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using Microsoft.AspNet.Identity;

namespace UniversityMvcApp.Models
{
    public class Course
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter course code")]
        [StringLength(100, MinimumLength = 5)]
        public string Code { get; set; }
        [Required(ErrorMessage = "Please enter course name")]
        //[Unique("TestDataAnnotations.EntitiesModel")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter credit")]
        [Range(.5, 5)]
        public decimal Credit { get; set; }
        [Required(ErrorMessage = "Please enter course description")]
        public string Description { get; set; }



        [Required(ErrorMessage = "Please select a Department")]
        [DisplayName("Department")]
        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]

        public virtual Department Department { get; set; }


        public int? TeacherId { get; set; }
        [ForeignKey("TeacherId")]
        public virtual Teacher Teacher { get; set; }

        [Required(ErrorMessage = "Please select a Semester")]
        public int SemesterId { get; set; }
        [ForeignKey("SemesterId")]
        public virtual Semester Semester { get; set; }

        public List<AllocateClassRoom> AllocateClassRooms { get; set; }
        public List<EnrollCourses> EnrollCourses { get; set; }


    }


}