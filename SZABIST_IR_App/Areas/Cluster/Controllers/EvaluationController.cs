using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SZABIST_IR_App.Models;

namespace SZABIST_IR_App.Areas.Cluster.Controllers
{
    public class EvaluationController : Controller
    {
        private AEAuditDBEntities db;
        // GET: Cluster/Evaluation
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["userID"])))
                return RedirectToAction("Index", "Account");
            using (db = new AEAuditDBEntities())
            {
                var userID = Session["userID"].ToString();
                var list = db.uspAssignedCoursesList(userID, true, null).ToList();
                var orderedList = list.OrderByDescending(x => x.Year).ToList();
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
        // GET: Cluster/Evaluation/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Cluster/Evaluation/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
