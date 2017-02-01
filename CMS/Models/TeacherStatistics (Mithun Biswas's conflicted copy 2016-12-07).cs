using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CMS.Models
{
    public class TeacherStatistics
    {
        public string Name { get; set; }
        public string Designation { get; set; }
        [DisplayName("Number of Course")]
        public int NumberOfCourse { get; set; }
        public string Courses { get; set; }
    }
}