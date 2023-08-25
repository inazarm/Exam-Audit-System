using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SZABIST_IR_App.Models;

namespace SZABIST_IR_App.Areas.Cluster.Controllers
{
    public class HomeController : Controller
    {
        AEAuditDBEntities db;
        // GET: Cluster/Home
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["userID"])) || string.IsNullOrEmpty(Convert.ToString(Session["roleID"])))
                return RedirectToAction("Index", "Account");
            using (db = new AEAuditDBEntities())
            {
                int RoleID = Convert.ToInt16(Session["roleID"]);
                string RoleName = Convert.ToString(Session["userID"]);
                Assessments assessments = new Assessments();
                if (RoleID == 2)
                {
                    assessments.Completed = db.uspAssignedCoursesList(RoleName, true, null).Count();
                    assessments.Pending = db.uspAssignedCoursesList(RoleName, false, null).Count();
                    assessments.TotalAssigned = db.uspAssignedCoursesList(RoleName, null, null).Count();
                    assessments.RoleId = RoleID;
                    assessments.UserId = RoleName;
                }
                return View(assessments);
            }
        }
    }
}