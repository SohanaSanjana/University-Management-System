using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityMvcApp.Models
{
    public class AllocateClassRoom
    {
        public int Id { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }

        public string Day { get; set; }

        public string From { get; set; }

        public string TO { get; set; }
        public TimeSpan StartTime { get; set; }

        public TimeSpan FinishTime { get; set; }

    }
}