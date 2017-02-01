using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using CMS.Models;

namespace CMS.Controllers
{
    public class TeacherController : Controller
    {
        private CourseManagementDbContext db = new CourseManagementDbContext();

        // GET: /Teacher/

        public ActionResult FullTimeFacultyCourseLoad()
        {

            var teacherStatistics = (from t in db.Teachers
                join c in db.Courses
                    on t.Id equals c.TeacherId into cGroup
                    where t.Status == "Full Time"
                orderby t.Designation descending  
                select new
                {
                    TeacherInfo = t,
                    CourseInfo = from cg in cGroup
                                 orderby cg.Code ascending 
                                 select cg
                }).ToList();
            List<TeacherStatistics> teacherStatisticses = new List<TeacherStatistics>();
            int count=0;
            foreach (var teacherStatistic in teacherStatistics)
            {
                TeacherStatistics aTeacherStatistics = new TeacherStatistics();
                aTeacherStatistics.Name = teacherStatistic.TeacherInfo.Name;
                aTeacherStatistics.Designation = teacherStatistic.TeacherInfo.Designation;
                aTeacherStatistics.NumberOfCourse = teacherStatistic.TeacherInfo.NumberOfCourse;
                count = 0;
                foreach (var courseInfo in teacherStatistic.CourseInfo)
                {
                    if (count != 0)
                    {
                        aTeacherStatistics.Courses += ", ";
                    }

                    aTeacherStatistics.Courses += courseInfo.Code;
                    aTeacherStatistics.Courses += "(";
                    aTeacherStatistics.Courses += courseInfo.Section;
                    aTeacherStatistics.Courses += ")";
                    count++;
                }

                teacherStatisticses.Add(aTeacherStatistics);
            }

            return View(teacherStatisticses);
        }

        public ActionResult PartTimeFacultyCourseLoad()
        {
            var teacherStatistics = (from t in db.Teachers
                                     join c in db.Courses
                                         on t.Id equals c.TeacherId into cGroup
                                     where t.Status == "Part Time"
                                     orderby t.Designation descending
                                     select new
                                     {
                                         TeacherInfo = t,
                                         CourseInfo = from cg in cGroup
                                                      orderby cg.Code ascending
                                                      select cg
                                     }).ToList();
            List<TeacherStatistics> teacherStatisticses = new List<TeacherStatistics>();
            int count = 0;
            foreach (var teacherStatistic in teacherStatistics)
            {
                TeacherStatistics aTeacherStatistics = new TeacherStatistics();
                aTeacherStatistics.Name = teacherStatistic.TeacherInfo.Name;
                aTeacherStatistics.Designation = teacherStatistic.TeacherInfo.Designation;
                aTeacherStatistics.NumberOfCourse = teacherStatistic.TeacherInfo.NumberOfCourse;
                count = 0;
                foreach (var courseInfo in teacherStatistic.CourseInfo)
                {
                    if (count != 0)
                    {
                        aTeacherStatistics.Courses += ", ";
                    }

                    aTeacherStatistics.Courses += courseInfo.Code;
                    aTeacherStatistics.Courses += "(";
                    aTeacherStatistics.Courses += courseInfo.Section;
                    aTeacherStatistics.Courses += ")";
                    count++;
                }

                teacherStatisticses.Add(aTeacherStatistics);
            }

            return View(teacherStatisticses);
        }
        public ActionResult Index()
        {
            return View(db.Teachers.ToList());
        }

        // GET: /Teacher/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            
                List<TeachersCourseListViewModel> coursesOfATeacher = (from c in db.Courses
                join t in db.Times
                on c.TimeId equals t.Id
                where c.TeacherId == id
                orderby c.Code
                select new TeachersCourseListViewModel()
                {
                    CourseName = c.Name,
                    CourseCode = c.Code,
                    Section = c.Section,
                    Time = t.Description

                }).ToList();

            TeacherCourseAllInfoViewModel teacherCourse = new TeacherCourseAllInfoViewModel();
            teacherCourse.CourseList = coursesOfATeacher;
            teacherCourse.Teacher = teacher;

            
            return View(teacherCourse);
        }

        // GET: /Teacher/Create
        public ActionResult Create()
        {
            ViewBag.DesignationList = db.Designations.ToList();
            ViewBag.StatusList = new[]
            {
                new{Name="Full Time"},
                new{Name="Part Time"}
            };
            return View();
        }

        // POST: /Teacher/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Name,ShortName,Email,ContactNo,Designation,Status,NumberOfCourse")] Teacher teacher)
        {
            ViewBag.DesignationList = db.Designations.ToList();
            ViewBag.StatusList = new[]
            {
                new{Name="Full Time"},
                new{Name="Part Time"}
            };
            if (ModelState.IsValid)
            {
                db.Teachers.Add(teacher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(teacher);
        }

        // GET: /Teacher/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: /Teacher/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Name,ShortName,Email,ContactNo,Designation,NumberOfCourse")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(teacher);
        }

        // GET: /Teacher/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: /Teacher/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Teacher teacher = db.Teachers.Find(id);
            db.Teachers.Remove(teacher);
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
    }
}
