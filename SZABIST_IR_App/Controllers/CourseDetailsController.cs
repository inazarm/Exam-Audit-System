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
    public class CourseDetailsController : Controller
    {
        AEAuditDBEntities db;
        ProjectClass p;
        // GET: CourseDetails
        public ActionResult Index(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["userID"])))
                return RedirectToAction("Index", "Account");

            using (db = new AEAuditDBEntities())
            {
                try
                {
                    var details = db.tblAllocateCoursesDetails.Find(id);
                    if (details != null)
                    {
                        ViewBag.zabdestURL = db.tblCampus.FirstOrDefault(x => x.tCampus_Id == details.tCampus_Id).zabDeskURL;
                        return View(details);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }


        public ActionResult CourseOutLine(int? tAllocateDetailID)
        {
            if (tAllocateDetailID == null)
            {
                return HttpNotFound();
            }
            using (db = new AEAuditDBEntities())
            {
                try
                {
                    var courseDe = db.tblAllocateCoursesDetails.Find(tAllocateDetailID);
                    var uID = courseDe.userID.Trim();
                    int sID = Convert.ToInt32(courseDe.iSemester_Id);
                    int semSecID = Convert.ToInt16(courseDe.iSemesterSection_Id);
                    int courseID = Convert.ToInt32(courseDe.Course_Id);

                    var conStringName = db.tblCampus.Find(courseDe.tCampus_Id).dbConnectionString;
                    var connection = ConfigurationManager.ConnectionStrings[conStringName].ToString();
                    SqlConnection con = new SqlConnection(connection);
                    SqlCommand command = new SqlCommand("spFacCourseOutline_Show", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@piSemester_Id", SqlDbType.Int).Value = sID;
                    command.Parameters.Add("@piSemesterSection_Id", SqlDbType.TinyInt).Value = semSecID;
                    command.Parameters.Add("@psUser_Id", SqlDbType.VarChar, 20).Value = uID;
                    command.Parameters.Add("@piCourse_Id", SqlDbType.Int).Value = courseID;

                    con.Open();
                    DataSet dset = new DataSet();
                    SqlDataAdapter adp = new SqlDataAdapter(command);
                    adp.Fill(dset);
                    var table1 = dset.Tables[0];
                    var facCourseOutline = dset.Tables[1];
                    var facCourseOutline_Detail = dset.Tables[2];
                    List<courseOutLine> courseOutLine = new List<courseOutLine>();

                    if (facCourseOutline.Rows.Count > 0)
                    {
                        for (int i = 0; i < facCourseOutline_Detail.Rows.Count; i++)
                        {
                            courseOutLine.Add(new courseOutLine
                            {
                                learningOutcomes = facCourseOutline.Rows[0]["sFacCourseOutline_LearningOutcomes"].ToString(),
                                //courseObjective = facCourseOutline.Rows[0]["sCourseObjectives"].ToString(),
                                Week = facCourseOutline_Detail.Rows[i]["tFacCourseOutlineDetail_LectureNo"].ToString(),
                                lectureNo = facCourseOutline_Detail.Rows[i]["tFacCourseOutlineDetail_LectureNo"].ToString(),
                                lectureDetails = facCourseOutline_Detail.Rows[i]["sFacCourseOutlineDetail_LectureDetail"].ToString()
                                
                            });
                        }
                    }

                    //if (idr.HasRows)
                    //{
                    //    while (idr.Read())
                    //    {
                    //        //program.Add(new Programs
                    //        //{
                    //        //    programID = idr["tProgram_Id"].ToString(),
                    //        //    programName = Convert.ToString(idr["sProgram_LongDesc"]) + " - " + Convert.ToString(idr["sProgram_ShortDesc"])
                    //        //});
                    //    }

                    //}
                    con.Close();
                    return Json(courseOutLine, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }


        public ActionResult CoursePortFolio(int? tAllocateDetailID)
        {
            if (tAllocateDetailID == null)
            {
                return HttpNotFound();
            }
            using (db = new AEAuditDBEntities())
            {
                try
                {
                    var courseDe = db.tblAllocateCoursesDetails.Find(tAllocateDetailID);
                    var uID = courseDe.userID.Trim();
                    int sID = Convert.ToInt32(courseDe.iSemester_Id);
                    int semSecID = Convert.ToInt16(courseDe.iSemesterSection_Id);
                    int courseID = Convert.ToInt32(courseDe.Course_Id);
                    var conStringName = db.tblCampus.Find(courseDe.tCampus_Id).dbConnectionString;
                    var connection = ConfigurationManager.ConnectionStrings[conStringName].ToString();
                    SqlConnection con = new SqlConnection(connection);
                    SqlCommand command = new SqlCommand("spFacCoursePortFolio_Check", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@piSemester_Id", SqlDbType.Int).Value = sID;
                    command.Parameters.Add("@piSemesterSection_Id", SqlDbType.TinyInt).Value = semSecID;
                    command.Parameters.Add("@psUser_Id", SqlDbType.VarChar, 20).Value = uID;
                    command.Parameters.Add("@piCourse_Id", SqlDbType.Int).Value = courseID;
                    command.Parameters.Add("@piRetVal", SqlDbType.Int).Direction = ParameterDirection.Output;
                    con.Open();
                    DataSet dset = new DataSet();
                    SqlDataAdapter adp = new SqlDataAdapter(command);
                    adp.Fill(dset);
                    var vJOffCouTea_Cou_Sem_SemSec_Pro = dset.Tables[0];
                    var FacCoursePortfolio = dset.Tables[1];
                    List<CoursePortFolio_Result> coursePortfolio = new List<CoursePortFolio_Result>();

                    if (FacCoursePortfolio.Rows.Count > 0)
                    {
                        for (int i = 0; i < FacCoursePortfolio.Rows.Count; i++)
                        {
                            coursePortfolio.Add(new CoursePortFolio_Result
                            {
                                WeekNumber = FacCoursePortfolio.Rows[i]["WeekNumber"].ToString(),
                                Description = FacCoursePortfolio.Rows[i]["Description"].ToString(),
                                Title = FacCoursePortfolio.Rows[i]["Title"].ToString(),
                                FileLocation = FacCoursePortfolio.Rows[i]["FileLocation"].ToString(),
                                DateCreated = FacCoursePortfolio.Rows[i]["DateCreated"].ToString(),
                                FacCoursePortfolio_Id = FacCoursePortfolio.Rows[i]["FacCoursePortfolio_Id"].ToString()
                            });
                        }
                    }
                    con.Close();
                    return Json(coursePortfolio, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }

        public ActionResult CourseRecapSheet(int tAllocateDetailID)
        {
            if (tAllocateDetailID == null)
            {
                return HttpNotFound();
            }
            using (db = new AEAuditDBEntities())
            {
                try
                {
                    p = new ProjectClass();
                    int v = 1;
                    DataSet xdset = p.getRecapSheet(tAllocateDetailID, "spCourseRecapeSheet_Show");

                    RecapSheet_Result recapSheet_result = new RecapSheet_Result();
                    if (xdset.Tables.Count > 0)
                    {
                        if (xdset.Tables[2].Rows.Count == 0)
                        {
                            xdset = p.getRecapSheet(tAllocateDetailID, "spCoorStdRecapSheet_Show");
                            v++;
                        }
                        //var tbl0 = xdset.Tables[0];
                        var tbl1 = xdset.Tables[1];
                        var tbl2 = xdset.Tables[2];
                        //var tbl3 = xdset.Tables[3];
                        //var tbl4 = xdset.Tables[4];
                        var tbl5 = xdset.Tables[5];
                        var tbl6 = xdset.Tables[6];
                        //var tbl7 = xdset.Tables[7];
                        List<RecapSheet_tbl1> headermodel = new List<RecapSheet_tbl1>();
                        List<RecapSheet_tbl2> detail = new List<RecapSheet_tbl2>();
                        List<RecapSheet_tbl5> grade = new List<RecapSheet_tbl5>();
                        List<RecapSheet_tbl6> grading = new List<RecapSheet_tbl6>();
                        if (tbl1.Rows.Count > 0)
                        {
                            for (int i = 0; i < tbl1.Rows.Count; i++)
                            {
                                headermodel.Add(new RecapSheet_tbl1
                                {
                                    tMarksHead_Id = Convert.ToInt32(tbl1.Rows[i]["tMarksHead_Id"]),
                                    sMarksHead_ShortDesc = tbl1.Rows[i]["sMarksHead_ShortDesc"].ToString(),
                                    sMarkHead_LongDesc = tbl1.Rows[i]["sMarksHead_LongDesc"].ToString(),
                                    fCourseMarksDistribution_TotalMarks =Convert.ToDouble(tbl1.Rows[i]["fCourseMarksDistribution_TotalMarks"]),
                                    tCourseMarksDistribution_TotalFrequency =Convert.ToInt32(tbl1.Rows[i]["tCourseMarksDistribution_TotaLFrequency"]),
                                    tCourseMarksDistribution_TotalExempted = Convert.ToInt32(tbl1.Rows[i]["tCourseMarksDistribution_TotalExempted"]),
                                    bMarksHead_ExemptedForDGS =Convert.ToInt32(tbl1.Rows[i]["bMarksHead_ExemptedForDGs"]),
                                });
                            }
                            for (int i = 0; i < tbl2.Rows.Count; i++)
                            {
                                detail.Add(new RecapSheet_tbl2
                                {
                                iStdMain_Id=tbl2.Rows[i]["iStdMain_Id"].ToString(),
                                    sStdProfile_FullName=tbl2.Rows[i]["sStdProfile_FullName"].ToString(),
                                    sStdMain_RegistrationNo = tbl2.Rows[i]["sStdMain_RegistrationNo"].ToString(),
                                    sMarksHead_Id =Convert.ToByte(tbl2.Rows[i]["tMarksHead_Id"]),
                                    hCourseRecapSheet_MarksTypeSerialNo =Convert.ToString(tbl2.Rows[i]["hCourseRecapSheet_MarksTypeSerialNo"]),
                                    fCourseRecapSheet_MarksObtained =Convert.ToDouble(tbl2.Rows[i]["fCourseRecapSheet_MarksObtained"]),
                                    fCourseRecapSheet_TotalMarks =Convert.ToDouble(tbl2.Rows[i]["fCourseRecapSheet_TotalMarks"]),
                                    bStdRegisteredCourse_Withdrawn =Convert.ToString(tbl2.Rows[i]["bStdRegisteredCourse_Withdrawn"]),
                                    bCourseLectureAttendance_Short =Convert.ToString(tbl2.Rows[i]["bCourseLectureAttendance_Short"]),
                                    bExamAttendanceFinal_Absent =Convert.ToString(tbl2.Rows[i]["bExamAttendanceFinal_Absent"]),
                                    sExplicitAssignedGrade_Grade =Convert.ToString(tbl2.Rows[i]["sExplicitAssignedGrade_Grade"])
                                });
                            }
                            for (int i = 0; i < tbl5.Rows.Count; i++)
                            {
                                grade.Add(new RecapSheet_tbl5
                                {
                                    iStdProgramBatch_Id=Convert.ToInt32(tbl5.Rows[i]["iStdProgramBatch_Id"]),
                                    sGradingPlan_Grade=tbl5.Rows[i]["sGradingPlan_Grade"].ToString(),
                                    fGradingPlan_MarksFrom =Convert.ToString(tbl5.Rows[i]["fGradingPlan_MarksFrom"]),
                                    fGradingPlan_MarksTo =Convert.ToDouble(tbl5.Rows[i]["fGradingPlan_MarksTo"]),
                                    fGradingPlan_GPA =Convert.ToString(tbl5.Rows[i]["fGradingPlan_GPA"]),
                                    sStdMain_Id =Convert.ToInt32(tbl5.Rows[i]["iStdMain_Id"]),
                                    tGradingPlan_Id =Convert.ToByte(tbl5.Rows[i]["tGradingPlan_Id"]),
                                    sGradingPlan_Remarks = tbl5.Rows[i]["sGradingPlan_Remarks"].ToString(),
                                });
                            }
                            for (int i = 0; i < tbl6.Rows.Count; i++)
                            {
                                grading.Add(new RecapSheet_tbl6
                                {
                                    tGradingPlan_Id =Convert.ToInt32(tbl6.Rows[i]["tGradingPlan_Id"]),
                                    sGradingPlan_Grade = tbl6.Rows[i]["sGradingPlan_Grade"].ToString(),
                                    fGradingPlan_MarksFrom =Convert.ToString(tbl6.Rows[i]["fGradingPlan_MarksFrom"]),
                                    fGradingPlan_MarksTo =Convert.ToDouble(tbl6.Rows[i]["fGradingPlan_MarksTo"]),
                                    fGradingPlan_GPA =Convert.ToString(tbl6.Rows[i]["fGradingPlan_GPA"]),

                                }) ;
                            }
                        }
                        recapSheet_result.headermodel = headermodel;
                        recapSheet_result.detail = detail;
                        recapSheet_result.grade = grade;
                        recapSheet_result.grading = grading;

                    }
                    return PartialView("CourseRecapSheet", recapSheet_result);
                    //return Json(recapSheet_result, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }


        public ActionResult fetchRecapSheet(int p_tAllocateDetailID)
        {
            if (p_tAllocateDetailID == null)
            {
                return HttpNotFound();
            }
            using (db = new AEAuditDBEntities())
            {

                try
                {
                    p = new ProjectClass();
                    bool isFirst = true;
                    DataSet xdset = p.getRecapSheet(p_tAllocateDetailID, "spCourseRecapeSheet_Show");

                    RecapSheet_Result recapSheet_result = new RecapSheet_Result();
                    if (xdset.Tables.Count > 0)
                    {
                        if (xdset.Tables[2].Rows.Count == 0)
                        {
                            xdset = p.getRecapSheet(p_tAllocateDetailID, "spCoorStdRecapSheet_Show");
                            isFirst = false;
                        }

                    }
                    var courseDe = db.tblAllocateCoursesDetails.Find(p_tAllocateDetailID);
                    courseDe.isChecked = isFirst;
                    return Json(courseDe, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }
    }
}