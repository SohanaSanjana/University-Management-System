using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityMvcApp.Models
{
    public class Designation
    {
        public int ID { get; set; }
        public string DesignationName { get; set; }

        public virtual List<Teacher> Teachers { get; set; } 
    }
}