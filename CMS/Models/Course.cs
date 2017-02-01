using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.Models
{
    public class Course
    {
        public int Id { get; set; }
        //[Remote("IsCodeExist", "Course", ErrorMessage = "Code is already exist")]
        public string Code { get; set; }
        //[Remote("IsNameExist", "Course", ErrorMessage = "Name is already exist")] 
        public string Name { get; set; }
        //[Remote("IsSectionValid", "Course", ErrorMessage = "This section is previouly assigned to this course")]
        public int Section { get; set; }
        [DisplayName("Lab Required?")]
        public string LabRequired { get; set; }

        [DisplayName("Lab Course?")]
        public string IsItaLabCourse { get; set; }
        public string Semester { get; set; }
        public int TeacherId { set; get; }
        public int TimeId { get; set; }

    }
}