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
            if (string.IsNullOrEmpty(Convert.ToString(Session["userID"])))
                return RedirectToAction("Index", "Account");

            using (db = new AEAuditDBEntities())
            {
                var campus = db.tblCampus.ToList();
                ViewBag.campusName = new SelectList(campus, "tCampus_Id", "sCampus_ShortDesc").ToList();
            }
            return View();
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