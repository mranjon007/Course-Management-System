using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using CMS.Models;
using Microsoft.Ajax.Utilities;

namespace CMS.Controllers
{
    public class CourseController : Controller
    {
        private CourseManagementDbContext db = new CourseManagementDbContext();

        // GET: /Course/

        public ActionResult CourseAssignToTeacher()
        {
            ViewBag.TeacherList = db.Teachers.ToList();
            var courses = db.Courses.ToList();
            var l = new List<string>();
            foreach (var course in courses)
            {
                string code = course.Code;
                l.Add(code);
            }

            ViewBag.CourseList = l.Distinct();
            ViewBag.TimeList = db.Times.ToList();




            //  var query =
            //from sc in db.StudentCourses
            //where sc.RegNo == studentCourse.RegNo &&
            //sc.CourseId == studentCourse.CourseId
            //select sc;

            //  foreach (StudentCourse sc in query)
            //  {
            //      sc.Grade = studentCourse.Grade;
            //  }

            //  // Submit the changes to the database.
            //  try
            //  {
            //      db.SaveChanges();
            //      ViewBag.Notification = "Grade is saved successfully";
            //  }
            //  catch (Exception e)
            //  {
            //      Console.WriteLine(e);
            //      // Provide for exceptions.
            //  }

            return View();
        }

        [HttpPost]
        public ActionResult CourseAssignToTeacher(CourseTeacher courseTeacher)
        {
            ViewBag.TeacherList = db.Teachers.ToList();
            var courses = db.Courses.ToList();
            var l = new List<string>();
            foreach (var course in courses)
            {
                string code = course.Code;
                l.Add(code);
            }

            ViewBag.CourseList = l.Distinct();
            ViewBag.TimeList = db.Times.ToList();

            var query = (from c in db.Courses
                where c.Code == courseTeacher.CourseCode &&
                      c.Section == courseTeacher.Section
                select c).ToList();
            Course aCourse = new Course();

            foreach (var course in query)
            {
                

                course.TeacherId = courseTeacher.Teacher;
                course.TimeId = courseTeacher.TimeId;
            }

            db.SaveChanges();


            return View();
        }



        public ActionResult Index()
        {

            return View(db.Courses.ToList());
        }

        // GET: /Course/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: /Course/Create
        public ActionResult Create()
        {
            ViewBag.SemesterList = db.Semesters.ToList();
            ViewBag.TimeList = db.Times.ToList();
            ViewBag.TeacherList = db.Teachers.ToList();
            ViewBag.YesNoList = new[]
            {
                new {Name = "No"},
                new {Name = "Yes"}
            };
            return View();
        }

        // POST: /Course/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "Id,Code,Name,Section,LabRequired,IsItaLabCourse,Semester,TeacherId,TimeId")] Course course)
        {
            ViewBag.SemesterList = db.Semesters.ToList();
            ViewBag.TimeList = db.Times.ToList();
            ViewBag.TeacherList = db.Teachers.ToList();
            ViewBag.YesNoList = new[]
            {
                new {Name = "No"},
                new {Name = "Yes"}
            };
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(course);
        }

        // GET: /Course/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: /Course/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "Id,Code,Name,Section,LabRequired,IsItaLabCourse,Semester,TeacherId,TimeId")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(course);
        }

        // GET: /Course/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: /Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
           {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //public JsonResult IsCodeExist(string code)
        //{
        //    return Json(!db.Courses.Any(p => p.Code == code), JsonRequestBehavior.AllowGet);
        //}

        ////public JsonResult IsCodeExist(string code)
        ////{
        ////    bool result;
        ////    var query = (from c in db.Courses
        ////        where c.Code == code
        ////        select c).ToList();

        ////    if (query.Any(c=>c.Code==code))
        ////    {
        ////        result = false;
        ////    }
        ////    else
        ////    {
        ////        result = true;
        ////    }

        ////    return Json(result, JsonRequestBehavior.AllowGet);
        ////}

        //public JsonResult IsNameExist(string name)
        //{
        //    if (db.Courses.Any(x => x.Name == name))
        //    {
        //        return Json(false, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(true, JsonRequestBehavior.AllowGet);
        //    }
        //}

        public JsonResult IsSectionValid(int section, string name, string code)
        {
            var query = (from c in db.Courses
                where c.Code == code &&
                      c.Name == name &&
                      c.Section == section
                select c).ToList();
            int count = query.Count();

            return Json(query);


        }

        public JsonResult GetNumberOfCourseAndReamingCourseByTeacherId(int teacherId)
        {

            var teacher = db.Teachers.Where(a => a.Id == teacherId).ToList();
            var course = db.Courses.Where(a => a.TeacherId == teacherId && a.IsItaLabCourse=="No").ToList();


            int numberofCourse = 0;
            int reaminingCourse = 0;

            foreach (var person in teacher)
            {
                numberofCourse = person.NumberOfCourse;
                reaminingCourse = person.NumberOfCourse - course.Count();
            }

            var teacherCourseInfo = new[]
            {
                new {NumberOfCourse = numberofCourse, ReaminingCourse = reaminingCourse}
            };

            return Json(teacherCourseInfo);
        }

        public JsonResult GetCourseNameSemesterSectionbyCourseCode(string courseCode)
        {
            var courseList = (from c in db.Courses
                where c.Code == courseCode
                select new
                {
                    CourseName = c.Name,
                    c.Semester,
                    c.Section
                }).ToList();
            return Json(courseList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckIfCourseIsAlreadyAssigned(string courseCode, int section)
        {
            string teacherName = "";
            int teacherId = 0;
            var course = (from c in db.Courses
                where c.Code == courseCode &&
                      c.Section == section &&
                      c.TeacherId != 0
                select c);

            int count = course.Count();
            foreach (var courseInfo in course)
            {
                teacherId = courseInfo.TeacherId;
            }

            var teacher = (from t in db.Teachers
                where t.Id == teacherId
                select t).ToList();
            foreach (var teacherInfo in teacher)
            {
                teacherName = teacherInfo.Name;
            }


            return Json(teacherName, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckIfAnotherSectionIsAssignedInSameTime(string courseCode, int section, int time)
        {
            var courseList = (from c in db.Courses
                where c.Code == courseCode &&
                      c.Section != section &&
                      c.TimeId == time
                select c).ToList();
            return Json(courseList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckIfConsecutiveCoursesForSameTeacher(int teacherId, int timeId, string code, int section)
        {
            string report = "";
            string IsItALabCourse = "";
            var courseInfo = (from c in db.Courses
                where c.Code == code &&
                      c.Section == section
                select c).ToList();

            foreach (var course in courseInfo)
            {
                IsItALabCourse = course.IsItaLabCourse;
            }

            if (IsItALabCourse == "Yes")
            {
                report = "";
            }
            else
            {
                List<Course> courseList = new List<Course>();
                if (timeId == 6)
                {
                    courseList = (from c in db.Courses
                                  where c.TeacherId == teacherId &&
                                        c.TimeId == (timeId - 1) &&
                                        c.IsItaLabCourse == "No"
                                  select c).ToList();
                }
                else if (timeId == 7)
                {
                    courseList = (from c in db.Courses
                                  where c.TeacherId == teacherId &&
                                        c.TimeId == (timeId + 1) &&
                                        c.IsItaLabCourse == "No"
                                  select c).ToList();
                }

                else
                {
                    courseList = (from c in db.Courses
                                  where c.TeacherId == teacherId &&
                                        ((c.TimeId == (timeId - 1)) || c.TimeId == (timeId + 1)) &&
                                        c.IsItaLabCourse == "No"
                                  select c).ToList();
                }

                int count = courseList.Count();
                if (count > 0)
                {
                    report = "Error";
                }

    
            }
            
            return Json(report, JsonRequestBehavior.AllowGet);
        }


        public JsonResult CheckIfTwoCourseInSameTimeForSelectedTeacher(int teacherId, int timeId)
        {
            string teacherName = "";
            string courseName = "";
            int courseSection = 0;
            string message = "";

            var courseInSameTime = (from c in db.Courses
                where c.TeacherId == teacherId &&
                      c.TimeId == timeId
                select c).ToList();

            int count = courseInSameTime.Count();
            if (count > 0)
            {
                var teacherInfo = (from t in db.Teachers
                    where t.Id == teacherId
                    select t).ToList();
                foreach (var teacher in teacherInfo)
                {
                    teacherName = teacher.Name;
                }
                foreach (var course in courseInSameTime)
                {
                    courseName = course.Name;
                    courseSection = course.Section;
                }

                message = teacherName + " have a course named " + courseName + "(" + courseSection +
                          ") in the same time. ";
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }

    }

}
