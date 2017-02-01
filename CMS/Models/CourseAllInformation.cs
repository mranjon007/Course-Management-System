using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Models
{
    public class CourseAllInformation
    {
        public int Id { set; get; }
        public string Code { get; set; }
        public string Name { set; get; }
        public int Section { get; set; }
        public string Semester { get; set; }
        public string LabRequired { get; set; }
        public string LabCourse { get; set; }
        public string Faculty { get; set; }
        public string Time { get; set; }

    }
}