using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using SZABIST_IR_App.Models;

namespace SZABIST_IR_App.Controllers
{
    public class CourseAssignController : Controller
    {
        private AEAuditDBEntities db;
        private ProjectClass p;

        // GET: CourseAssign
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["userID"])))
                return RedirectToAction("Index", "Account");

            using (db = new AEAuditDBEntities())
            {
                var clusterList = db.vue_clusterHead_clusterInfo.Where(c => c.HeadStatus == true && c.ClusterStatus == true).ToList();
                var campus = db.tblCampus.ToList();
                ViewBag.clusters = new SelectList(clusterList.OrderBy(c => c.ClusterTitle), "tCluster_ID", "ClusterTitle");
                ViewBag.campus = new SelectList(campus.OrderBy(c => c.sCampus_ShortDesc), "tCampus_Id", "sCampus_ShortDesc");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(tblAllocateCours addNew, string courseData)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(Convert.ToString(Session["userID"])))
                    return RedirectToAction("Index", "Account");

                using (db = new AEAuditDBEntities())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var userID = Session["userID"].ToString();
                            p = new ProjectClass();
                            int tAllocateID = 0;
                            string[] program = addNew.Program.Split('-');
                            //string[] numbers = number.Split(',');
                            int campusID =Convert.ToInt32(program[0]);
                            int degreeID =Convert.ToInt32(program[1]);
                            int programID =Convert.ToInt32(program[2]);
                            var programRow = p.getProgramList(addNew.tCampus_Id).FirstOrDefault(x => x.tCampus_Id ==campusID && x.tDegree_Id == degreeID && x.tProgram_Id == programID);
                            var facultyRow = p.getFacultyList(addNew.tCampus_Id).FirstOrDefault(f => f.tFaculty_Id == programRow.iFacultyId);
                            int ExamType = Convert.ToInt16(addNew.ExamType);
                            var HeadDetails = db.tblAllocateCourses.FirstOrDefault(h => h.ClusterHeadID == addNew.ClusterHeadID && h.tCampus_Id==addNew.tCampus_Id && h.Year == addNew.Year && h.Semster == addNew.Semster && h.Program == addNew.Program && h.ExamType == ExamType);
                            if (HeadDetails == null)
                            {
                                addNew.Program_ShortDesc = programRow.Program_ShortDesc;
                                addNew.Program_LongDesc = programRow.Program_LongDesc;
                                addNew.tFaculty_Id = facultyRow.tFaculty_Id;
                                addNew.Faculty_ShortDesc = facultyRow.sFaculty_ShortDesc;
                                addNew.Faculty_LongDesc = facultyRow.sFaculty_LongDesc;
                                addNew.CreationDate = p.currentDateTime();
                                addNew.CreatedBy = userID;
                                addNew.Status = true;
                                db.tblAllocateCourses.Add(addNew);
                                db.SaveChanges();
                                tAllocateID = addNew.tAllocateID;
                            }
                            else
                            {
                                HeadDetails.ModifiedDate = p.currentDateTime();
                                HeadDetails.ModifiedBy = userID;
                                HeadDetails.Status = true;
                                db.SaveChanges();
                                tAllocateID = HeadDetails.tAllocateID;
                            }
                            var courseID = "";
                            var CourseCode = "";
                            var CourseName = "";
                            List<tblAllocateCoursesDetail> crsAsgnDetails = new List<tblAllocateCoursesDetail>();
                            var c = courseData;
                            List<string> listCrs = c.Split('^').ToList();
                            for (int i = 0; i < listCrs.Count - 1; i++)
                            {
                                List<string> singleleCrs = listCrs[i].Split('~').ToList();
                                courseID = singleleCrs[0];
                                CourseCode = singleleCrs[1];
                                CourseName = singleleCrs[2];
                                tblAllocateCoursesDetail crsAsgnDtls = new tblAllocateCoursesDetail();

                                crsAsgnDtls.tAllocateID = tAllocateID;
                                crsAsgnDtls.Course_Id = Convert.ToDecimal(singleleCrs[0]);
                                crsAsgnDtls.sCourse_Code = singleleCrs[1].ToString();
                                crsAsgnDtls.sCourse_LongDesc = singleleCrs[2].ToString();
                                crsAsgnDtls.Instructor = singleleCrs[3].ToString();
                                crsAsgnDtls.Class = singleleCrs[4].ToString();
                                crsAsgnDtls.userID = singleleCrs[5].ToString();
                                crsAsgnDtls.iSemester_Id = Convert.ToDecimal(singleleCrs[6]);
                                crsAsgnDtls.iSemesterSection_Id = Convert.ToByte(singleleCrs[7]);
                                crsAsgnDtls.tCampus_Id = addNew.tCampus_Id;
                                crsAsgnDtls.tProgram_Id = Convert.ToByte(singleleCrs[9]);
                                crsAsgnDtls.isChecked = false;
                                crsAsgnDtls.isCheckedByIR = false;
                                crsAsgnDtls.CreationDate = p.currentDateTime();
                                db.tblAllocateCoursesDetails.Add(crsAsgnDtls);
                                db.SaveChanges();
                            }
                            trans.Commit();
                            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            return new HttpStatusCodeResult(401, ex.Message);
                        }
                    }
                }
            }
            return View();
        }



        public ActionResult ProgramList(byte campusID)
        {
            using (db = new AEAuditDBEntities())
            {
                try
                {
                    p = new ProjectClass();
                    List<Programs> programDropDown = new List<Programs>();
                    var programList = p.getProgramList(campusID).OrderBy(x=>x.Program_LongDesc);
                    //programDropDown = programList.ToList();
                    foreach (var item in programList)
                    {
                        programDropDown.Add(new Programs
                        {
                            programID = item.tCampus_Id + "-" + item.tDegree_Id + "-" + item.tProgram_Id,
                            programName = item.Program_LongDesc.ToString() + " (" + item.Program_ShortDesc.ToString()+")"
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


        public ActionResult CourseList(string prgmId, int? year, int? sId, int? campusID)
        {
            if (year == null || sId == null || campusID == null)
            {
                return RedirectToAction("Index", "Account");
            }
            using (db = new AEAuditDBEntities())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        List<CourseList_Result> result = new List<CourseList_Result>(); ;
                        var xcon = db.tblCampus.FirstOrDefault(c => c.tCampus_Id == campusID).dbConnectionString;
                        var connection = ConfigurationManager.ConnectionStrings[xcon].ToString();
                        SqlConnection con = new SqlConnection(connection);
                        SqlCommand cmd = new SqlCommand("sp_GetCourse_Instructor_ByYearSemsProg", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Year", year);
                        cmd.Parameters.AddWithValue("@SemsterID", sId);
                        cmd.Parameters.AddWithValue("@ProgramID", prgmId);
                        con.Open();
                        SqlDataReader xdr = cmd.ExecuteReader();
                        if (xdr.HasRows)
                        {

                            while (xdr.Read())
                            {
                                CourseList_Result add = new CourseList_Result()
                                {
                                    iCourse_Id = Convert.ToInt32(xdr["Course_Id"]),
                                    sCourse_Code = xdr["sCourse_Code"].ToString(),
                                    sUser_Id = xdr["sUser_Id"].ToString(),
                                    iSemester_Id = Convert.ToInt32(xdr["iSemester_Id"]),
                                    iSemesterSection_Id = Convert.ToInt32(xdr["iSemesterSection_Id"]),
                                    tCampus_Id = Convert.ToInt32(xdr["tCampus_Id"]),
                                    tProgram_Id = Convert.ToInt32(xdr["tProgram_Id"]),
                                    Instructor = xdr["Instructor"].ToString(),
                                    Class = xdr["Class"].ToString(),
                                    sCourse_LongDesc = xdr["sCourse_LongDesc"].ToString()
                                };
                                result.Add(add);
                            }
                        }
                        //var courseList = db.sp_GetCourseByYearSemsProg(year, sId, prgmId);
                        if (result.Count > 0)
                        {
                            return PartialView("CourseList", result);

                        }
                        else
                        {
                            return Json(new { msg = "Courses Not Available" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }
                else
                {
                    return View();
                }

            }
        }

        public JsonResult ClusterHead(int id)
        {
            AEAuditDBEntities db = new AEAuditDBEntities();
            db.Configuration.ProxyCreationEnabled = false;
            var cHead = db.tblClusterHeads.FirstOrDefault(h => h.tCluster_ID == id && h.Status==true);
            return Json(cHead, JsonRequestBehavior.AllowGet);
        }
    }
}