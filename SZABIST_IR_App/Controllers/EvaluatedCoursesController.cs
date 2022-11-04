using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SZABIST_IR_App.Models;

namespace SZABIST_IR_App.Controllers
{
    public class EvaluatedCoursesController : Controller
    {
        // GET: EvaluatedCourses
        private AEAuditDBEntities db;
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["userID"])))
                return RedirectToAction("Index", "Account");
            using (db = new AEAuditDBEntities())
            {
                var userID = Session["userID"].ToString();
                //var list = db.tblAllocateCourses.ToList().Where(h => h.ClusterHeadID == userID && h.Status == true);
                var list = db.uspAssignedCoursesList(userID,true,null).ToList();
                var orderedList = list.OrderByDescending(x => x.Year).ToList();
                //var orderedList1 = from s in list //Sorts the studentList collection in descending order
                //                  orderby s.Year descending
                //                  select s;
                return View(orderedList);
            }
        }
        public ActionResult coursesList(int? tAllocateID)
        {
            using (db = new AEAuditDBEntities())
            {
                if (string.IsNullOrEmpty(Convert.ToString(Session["userID"])))
                    return RedirectToAction("Index", "Account");

                try
                {
                    if (tAllocateID == null)
                    {
                        return HttpNotFound();
                    }
                    var courseDetails = db.tblAllocateCoursesDetails.Where(x => x.tAllocateID == tAllocateID && x.isChecked == true).ToList();
                    if (courseDetails == null)
                    {
                        return Json(new { error = "There is no Evaluated Courses List!" }, JsonRequestBehavior.AllowGet);
                    }
                    return PartialView("coursesList", courseDetails);
                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }

        }
    }
}