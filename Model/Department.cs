using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityMvcApp.Models
{
    public class Department
    {
        public int ID { get; set; }


        [StringLength(7, MinimumLength = 2)]
        [Required(ErrorMessage = "Code entered must be between 2 to 7 characters long")]
        public string Code { get; set; }


       [Required(ErrorMessage = "Please enter a department name")]
        public string Name { get; set; }

       public virtual List<Course> Courses { get; set; }
       public virtual List<Student> Students { get; set; } 
       public virtual List<Teacher> Teachers { get; set; }
       public virtual List<AllocateClassRoom> AllocateClassRooms { get; set; }
       public List<Room> Rooms { get; set; }
       
    }
}