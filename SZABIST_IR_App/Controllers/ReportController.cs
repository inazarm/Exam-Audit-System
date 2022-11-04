using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SZABIST_IR_App.Models;

namespace SZABIST_IR_App.Controllers
{
    public class ReportController : Controller
    {

        AEAuditDBEntities db;
        private ProjectClass p;
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["userID"])))
                return RedirectToAction("Index", "Account");
            getDll();
            //getInstructors();
            getCoursedll();
            ViewBag.GetYear = GetDropDownYear();
            ProgramList();
            getSectionDll();
            getDepartmentddl();
            CampusList();
            return View();
        }

        [HttpGet]
        public ActionResult ProgramList(byte campusID)
        {
            using (db = new AEAuditDBEntities())
            {
                try
                {
                    p = new ProjectClass();
                    List<Programs> programDropDown = new List<Programs>();
                    var programList = p.getProgramList(campusID).OrderBy(x => x.Program_LongDesc);
                    //programDropDown = programList.ToList();
                    foreach (var item in programList)
                    {
                        programDropDown.Add(new Programs
                        {
                            programID = item.tCampus_Id + "-" + item.tDegree_Id + "-" + item.tProgram_Id,
                            programName = item.Program_LongDesc.ToString() + " (" + item.Program_ShortDesc.ToString() + ")"
                        });
                    }
                    return Json(programDropDown, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public ActionResult GetDepartmentList(byte p_campusID)
        {
            List<DropdownList> list = new List<DropdownList>();
            DataTable dt = new DataTable();
            Util u = new Util();

            string Query = "Select distinct Faculty_LongDesc,tfaculty_id "+
                "from [AEAuditDB].[dbo].[tblAllocateCourses] "+
                "Where tCampus_Id = "+ p_campusID+ " "+
                "order by Faculty_LongDesc";
            //string Query = "Select distinct Faculty_ShortDesc from [AEAuditDB].[dbo].[tblAllocateCourses] order by Faculty_ShortDesc ";
            dt = u.RunAQry(Query);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                list.Add(new DropdownList()
                {
                    value = dt.Rows[i]["tfaculty_id"].ToString(),
                    text = dt.Rows[i]["Faculty_LongDesc"].ToString()
                });
            }

            return Json(list, JsonRequestBehavior.AllowGet);
            //ViewBag.departmentddl = list;
            //return list;
        }

        public void getDll()
        {
           List<DropdownList> list = new List<DropdownList>();
            DataTable dt = new DataTable();
            Util u = new Util();
            //string Query = "Select distinct Instructor,tAllocateDetailID from [AEAuditDB].[dbo].[tblAllocateCoursesDetails] order by Instructor";
            string Query = "Select distinct Instructor,userID,isChecked from [AEAuditDB].[dbo].[tblAllocateCoursesDetails] where isChecked = '1' order by Instructor";
             dt = u.RunAQry(Query);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                list.Add(new DropdownList()
                {
                    value = dt.Rows[i]["userID"].ToString(),
                    text = dt.Rows[i]["Instructor"].ToString()
                });
            }
            ViewBag.Instructor = list.Distinct();
        }
        public void getInstructors()
        {
            using (db=new AEAuditDBEntities())
            {
                try
                {
                    List<DropdownList> list = new List<DropdownList>();
                    //var instructors = db.tblAllocateCoursesDetails.Where(x=>x.isChecked==true).ToList().OrderBy(x=>x.Instructor);
                    var instructors = db.tblAllocateCoursesDetails.Where(x => x.isChecked == true).ToList().OrderBy(x => x.Instructor).Distinct();
                    foreach (var item in instructors)
                    {
                        list.Add(new DropdownList()
                        {
                            value = item.userID.ToString(),
                            text = item.Instructor.ToString()
                        });
                    }
                    ViewBag.Instructor = list.ToList().Distinct();               
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public void getSectionDll()
        {
            List<DropdownList> list = new List<DropdownList>();
            DataTable dt = new DataTable();
            Util u = new Util();
            string Query = "Select distinct Class from [AEAuditDB].[dbo].[tblAllocateCoursesDetails] order by Class";
            dt = u.RunAQry(Query);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                list.Add(new DropdownList()
                {
                    value = dt.Rows[i]["Class"].ToString(),
                    text = dt.Rows[i]["Class"].ToString()
                });
            }
            ViewBag.Section = list;
        }


        public void getDepartmentddl()
        {
            List<DropdownList> list = new List<DropdownList>();
            DataTable dt = new DataTable();
            Util u = new Util();

            string Query = "Select distinct Faculty_LongDesc,tfaculty_id from [AEAuditDB].[dbo].[tblAllocateCourses] order by Faculty_LongDesc ";
            //string Query = "Select distinct Faculty_ShortDesc from [AEAuditDB].[dbo].[tblAllocateCourses] order by Faculty_ShortDesc ";
            dt = u.RunAQry(Query);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                list.Add(new DropdownList()
                {
                    value = dt.Rows[i]["tfaculty_id"].ToString(),
                    text = dt.Rows[i]["Faculty_LongDesc"].ToString()
                });
            }
            ViewBag.departmentddl = list;
        }
        public void getCoursedll()
        {
            List<DropdownlistCourseModel> list = new List<DropdownlistCourseModel>();
            DataTable dt = new DataTable();
            Util u = new Util();
            string Query = "Select distinct sCourse_LongDesc,sCourse_Code from [AEAuditDB].[dbo].[tblAllocateCoursesDetails] order by sCourse_LongDesc";
            dt = u.RunAQry(Query);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                list.Add(new DropdownlistCourseModel()
                {
                    value = dt.Rows[i]["sCourse_Code"].ToString(),
                    text = dt.Rows[i]["sCourse_LongDesc"].ToString()
                });
            }
            ViewBag.CourseList = list;
        }
        public List<DropdownlistCourseModel> GetDropDownYear()
        {
            var tg = new List<DropdownlistCourseModel>();
            for (int i = 0; i < 10; i++)
            {
                tg.Add(new DropdownlistCourseModel
                {
                    value = (DateTime.Now.Year - i).ToString(),
                    text = (DateTime.Now.Year - i).ToString(),
                });
            }
            return tg;
        }

        public void getProgramdll()
        {
            List<DropdownlistCourseModel> list = new List<DropdownlistCourseModel>();
            DataTable dt = new DataTable();
            Util u = new Util();
            string Query = "Select distinct sCourse_LongDesc,sCourse_Code from [AEAuditDB].[dbo].[tblAllocateCoursesDetails] order by sCourse_LongDesc";
            dt = u.RunAQry(Query);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                list.Add(new DropdownlistCourseModel()
                {
                    value = dt.Rows[i]["sCourse_Code"].ToString(),
                    text = dt.Rows[i]["sCourse_LongDesc"].ToString()
                });
            }
            ViewBag.CourseList = list;
        }

        public void ProgramList()
        {
            //int cID = campusID;

            //  var conStringName = db.tblCampus.FirstOrDefault(x => x.tCampus_Id == cID).dbConnectionString;
            using (db=new AEAuditDBEntities())
            {
                try
                {
                    var conStringName = db.tblCampus.FirstOrDefault(x => x.tCampus_Id == 1).dbConnectionString;
                    var connection = ConfigurationManager.ConnectionStrings[conStringName].ToString();
                    //var connection = @"server=10.0.5.91; user id=zabsoltest; password=zabsol.786; database=Zabdesk_Sept;"; 
                    SqlConnection con = new SqlConnection(connection);
                    SqlCommand cmd = new SqlCommand("SELECT tCampus_Id,tDegree_Id,tProgram_Id,sProgram_LongDesc,sProgram_ShortDesc FROM Program", con);
                    con.Open();
                    SqlDataReader idr = cmd.ExecuteReader();
                    List<Programs> program = new List<Programs>();
                    if (idr.HasRows)
                    {
                        while (idr.Read())
                        {
                            program.Add(new Programs
                            {
                                programID = idr["tCampus_Id"].ToString() + "-" + idr["tDegree_Id"].ToString() + "-" + idr["tProgram_Id"].ToString(),
                                programName = Convert.ToString(idr["sProgram_LongDesc"]) + " - " + Convert.ToString(idr["sProgram_ShortDesc"])
                            });
                        }
                        con.Close();
                    }
                    ViewBag.ProgramList = program.OrderBy(x => x.programName).ToList();
                    //ViewBag.ProgramList = program;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public void CampusList()
        {
            List<Campus> list = new List<Campus>();
            DataTable dt = new DataTable();
            Util u = new Util();
            string Query = "Select distinct acd.tCampus_Id,sCampus_ShortDesc " +
                "from [AEAuditDB].[dbo].[tblAllocateCoursesDetails] acd inner join tblCampus c on acd.tCampus_Id = c.tCampus_Id " +
                "order by sCampus_ShortDesc";
            dt = u.RunAQry(Query);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                list.Add(new Campus()
                {
                    tCampus_Id = Convert.ToByte(dt.Rows[i]["tCampus_Id"].ToString()),
                    sCampus_ShortDesc = dt.Rows[i]["sCampus_ShortDesc"].ToString()
                });
            }
            ViewBag.CampusList = list;
        }


        //public DataTable GetProgram()
        //{

        //}
    }
}