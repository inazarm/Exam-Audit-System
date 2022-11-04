using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SZABIST_IR_App.Models;

namespace SZABIST_IR_App.Controllers
{
    public class AllocatedListController : Controller
    {

        // GET: AllocatedList
        AEAuditDBEntities db;
        ProjectClass p;
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["userID"])))
                return RedirectToAction("Index", "Account");

            using (db = new AEAuditDBEntities())
            {
                List<tblAllocateCours> CourstList = db.tblAllocateCourses.ToList().Where(h => h.Status == true).ToList();
                List<tblCampu> campusDe = db.tblCampus.ToList();
                var courseDetails = (from course in CourstList
                                     join campus in campusDe on course.tCampus_Id equals campus.tCampus_Id
                                     orderby course.CreationDate descending
                                     select new vm_AllocateCrs_CmpsName
                                     {
                                         AllocatedCrs = course,
                                         Campus = campus
                                     }).ToList();
                return View(courseDetails);
            }

        }

        public ActionResult coursesList(int tAllocateID)
        {
            using (db = new AEAuditDBEntities())
            {
                try
                {
                    var courseDetails = db.tblAllocateCoursesDetails.Where(x => x.tAllocateID == tAllocateID).ToList();
                    return PartialView(courseDetails);
                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }

        }

        public ActionResult courseDetails(int id)
        {
            using (db = new AEAuditDBEntities())
            {
                try
                {
                    var courseDetails = db.tblAllocateCoursesDetails.Where(x => x.tAllocateID == id).ToList();
                    if (courseDetails == null)
                    {
                        return Json(new { error = "Courses not Allocated!" }, JsonRequestBehavior.AllowGet);
                    }
                    return PartialView(courseDetails);
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }
    
        public ActionResult deleteRecord(int id)
        {
            using (db = new AEAuditDBEntities())
            {
                try
                {
                    var course = db.tblAllocateCoursesDetails.Find(id);
                    var courseMain = db.tblAllocateCourses.Find(course.tAllocateID);
                    var userID = Session["userID"].ToString();
                    if (course!=null)
                    {
                        if (course.isChecked==false)
                        {
                            p = new ProjectClass();
                            db.tblAllocateCoursesDetails.Remove(course);
                            courseMain.ModifiedDate = p.currentDateTime();
                            courseMain.ModifiedBy = userID;
                            db.SaveChanges();
                        }
                    }
                    var courseList = db.tblAllocateCoursesDetails.Where(x => x.tAllocateID == course.tAllocateID).ToList();
                    if (courseList.Count>0)
                    {
                        return PartialView("courseDetails",courseList);
                    }
                    courseMain.Status = false;
                    db.SaveChanges();
                    return PartialView("courseDetails", courseList);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}