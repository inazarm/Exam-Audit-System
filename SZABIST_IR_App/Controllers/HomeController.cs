using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SZABIST_IR_App.Models;

namespace SZABIST_IR_App.Controllers
{
    public class HomeController : Controller
    {
        AEAuditDBEntities db;
        // GET: Home
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["userID"])) || string.IsNullOrEmpty(Convert.ToString(Session["roleID"])))
                return RedirectToAction("Index", "Account");

            using (db = new AEAuditDBEntities())
            {
                int RoleID= Convert.ToInt16(Session["roleID"]);
                string RoleName = Convert.ToString(Session["userID"]);
                Assessments assessments = new Assessments();
                if (RoleID==2)
                {
                    assessments.Completed = db.uspAssignedCoursesList(RoleName, true, null).Count();
                    assessments.Pending = db.uspAssignedCoursesList(RoleName, false, null).Count();
                    assessments.TotalAssigned = db.uspAssignedCoursesList(RoleName, null, null).Count();
                    assessments.RoleId = RoleID;
                    assessments.UserId = RoleName;
                }
                else if (RoleID == 4)
                {
                    assessments.Completed = db.tblAllocateCoursesDetails.Where(c => c.isCheckedByIR == true).Count();
                    assessments.Pending = db.tblAllocateCoursesDetails.Where(c => c.isCheckedByIR == false).Count();
                    assessments.TotalAssigned = db.tblAllocateCoursesDetails.Count();
                    assessments.TotalCompleted = db.tblAllocateCoursesDetails.Where(c => c.isCheckedByIR == true && c.isChecked == true).Count();
                    assessments.RoleId = RoleID;
                    assessments.UserId = RoleName;
                }
                else
                {

                }
                // var campus = db.tblCampus.ToList();
                // ViewBag.campusName = new SelectList(campus, "tCampus_Id", "sCampus_ShortDesc").ToList();
                return View(assessments);
            }
            
        }

        public ActionResult ProgramList(int campusID)
        {
            int cID = campusID;
            var conStringName = db.tblCampus.FirstOrDefault(x => x.tCampus_Id == cID).dbConnectionString;
            try
            {
                var connection = ConfigurationManager.ConnectionStrings[conStringName].ToString();
                SqlConnection con = new SqlConnection(connection);
                SqlCommand cmd = new SqlCommand("SELECT tCampus_Id,tProgram_Id,sProgram_LongDesc,sProgram_ShortDesc FROM Program", con);
                con.Open();
                SqlDataReader idr = cmd.ExecuteReader();
                List<Programs> program = new List<Programs>();
                if (idr.HasRows)
                {
                    while (idr.Read())
                    {
                        program.Add(new Programs
                        {
                            programID = idr["tProgram_Id"].ToString(),
                            programName = Convert.ToString(idr["sProgram_LongDesc"]) + " - " + Convert.ToString(idr["sProgram_ShortDesc"])
                        });
                    }
                    con.Close();
                }
                return Json(program, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}