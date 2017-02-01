using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CMS.Models
{
    public class SemesterYear
    {
        public int Id { set; get; }
        public int Year { set; get; }
        [DisplayName("Semester")]
        public string TermName { set; get; }

    }
}