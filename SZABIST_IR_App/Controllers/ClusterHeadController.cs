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
    public class ClusterHeadController : Controller
    {
        private AEAuditDBEntities db;
        private ProjectClass p;
        // GET: ClusterHead
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["userID"])))
                return RedirectToAction("Index", "Account");

            //using (db = new AEAuditDBEntities())
            //{
            //    var userID = Session["userID"].ToString();
            //    var userDetails = db.tblAllocateCourses.Where(u=>u.ClusterHeadID==userID).ToList();
            //    var campus = db.tblCampus.ToList();
            //    ViewBag.campusName = new SelectList(campus.OrderBy(c => c.sCampus_ShortDesc), "tCampus_Id", "sCampus_ShortDesc").ToList();
            //    ViewBag.userID = Session["userID"].ToString();
            //    ViewBag.year= new SelectList(userDetails.OrderByDescending(u=>u.Year), "Year", "Year").ToList();
            //}
            //return View();
            using (db = new AEAuditDBEntities())
            {
                var userID = Session["userID"].ToString();
                //var list = db.tblAllocateCourses.ToList().Where(h => h.ClusterHeadID == userID && h.Status==true);
                var list = db.uspAssignedCoursesList(userID, false,null).Where(c=>c.Status==true).ToList();
                return View(list.OrderByDescending(x=>x.Year).ToList());
            }
        }

        public ActionResult AllocatedCoursesList(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["userID"])))
                return RedirectToAction("Index", "Account");

            if (id!=null)
            {
                using (db=new AEAuditDBEntities())
                {
                    //id = 2;
                    //ViewBag.Details = db.tblAllocateCourses.FirstOrDefault(d=>d.tAllocateID==id);
                    var crsDetails = db.tblAllocateCoursesDetails.Where(c => c.tAllocateDetailID == id && c.isChecked == false).ToList();
                    var tAllocatedID = db.tblAllocateCoursesDetails.FirstOrDefault(c => c.tAllocateDetailID == id && c.isChecked == false).tAllocateID;
                    ViewBag.Details = db.tblAllocateCourses.FirstOrDefault(d => d.tAllocateID == tAllocatedID);
                    if (crsDetails != null)
                    {
                        return View(crsDetails);
                    }
                    else
                    {
                        TempData["Message"] = "All courses are examined! OR Course not Assigned yet! ";
                        return RedirectToAction("Index");
                    }
                }
            }
            return View();
        }

        public ActionResult SelectedCrsAssessment(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["userID"])))
                return RedirectToAction("Index", "Account");

            if (id != null)
            {
                using (db = new AEAuditDBEntities())
                {
                    //ViewBag.Details = db.tblAllocateCourses.FirstOrDefault(d => d.tAllocateID == id);
                    var crsDetails = db.tblAllocateCoursesDetails.FirstOrDefault(c => c.tAllocateDetailID == id && c.isChecked == false);
                    if (crsDetails != null)
                    {
                        return View(crsDetails);
                    }
                    else
                    {
                        TempData["Message"] = "All courses are examined! OR Course not Assigned yet! ";
                        return RedirectToAction("Index");
                    }
                }
            }
            return View();
        }

        public ActionResult ProgramList(int campusID,string uID)
        {
            try
            {
                using (db=new AEAuditDBEntities())
                {
                    int cID = campusID;
                    var programList = db.tblAllocateCourses.ToList().Where(p => p.tCampus_Id == cID && p.ClusterHeadID == uID && p.Status == true);
                    List<Programs> programDropDown = new List<Programs>();
                    var fromDatabaseEF = new SelectList(db.tblAllocateCourses.ToList(), "Year", "Year");
                    //ViewData["DBMySkills"] = fromDatabaseEF;
                    foreach (var item in programList)
                    {
                        programDropDown.Add(new Programs
                        {
                            programID = item.Program,
                            programName = item.Program_LongDesc.ToString() + " (" + item.Program_ShortDesc.ToString() + ")"
                        });
                    }
                    return Json(programDropDown, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public ActionResult CrsAssignedList(tblAllocateCours tblObj)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["userID"])))
                return RedirectToAction("Index", "Account");

            using (db = new AEAuditDBEntities())
            {
                if (tblObj != null)
                {
                    var cHeadID = Session["userID"].ToString();
                    //var crsAssigned = db.tblAllocateCourses.FirstOrDefault(n => n.ClusterHeadID == cHeadID && n.tCampus_Id == tblObj.tCampus_Id && n.Program == tblObj.Program && n.Year == tblObj.Year && n.Semster == tblObj.Semster && n.ExamType == tblObj.ExamType && n.Status == true);
                    //Static Values for testing....
                    var crsAssigned = db.tblAllocateCourses.FirstOrDefault(n => n.ClusterHeadID == cHeadID && n.tCampus_Id ==tblObj.tCampus_Id && n.Program ==tblObj.Program && n.Year ==tblObj.Year && n.Semster ==tblObj.Semster && n.ExamType ==tblObj.ExamType && n.Status == true);
                    if (crsAssigned != null)
                    {
                        var crsDetails = db.tblAllocateCoursesDetails.Where(c => c.tAllocateID == crsAssigned.tAllocateID && c.isChecked==false).OrderBy(x => x.sCourse_LongDesc).ToList();
                        if (crsDetails != null)
                        {
                            return View(crsDetails);
                        }
                        else
                        {
                            TempData["Message"] = "All courses are examined! OR Course not Assigned yet! ";
                            return RedirectToAction("Index");
                        }

                    }
                    else
                    {
                        TempData["Message"] = "Record Not Found!";
                        return RedirectToAction("Index");
                        //return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                    }
                }
                return RedirectToAction("Index");
            }

        }

        public ActionResult SelectedCrs(int id, int cid)
        {
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(Session["userID"])))
                    return RedirectToAction("Index", "Account");

                using (db = new AEAuditDBEntities())
                {
                    var findCrs = db.tblAllocateCoursesDetails.FirstOrDefault(c => c.tAllocateDetailID == id && c.Course_Id == cid && c.isChecked==false);
                    
                    if (findCrs == null)
                    {
                        return Json(new { error = "Course Already Examined and Submitted!" }, JsonRequestBehavior.AllowGet);
                        //return Json("Already Submitted!",JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return PartialView("SelectedCrs", findCrs);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }


        }


        public ActionResult QuestionnaireForm()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["userID"])))
                return RedirectToAction("Index", "Account");

            using (AEAuditDBEntities db = new AEAuditDBEntities())
            {
                //short departID = Convert.ToInt16(Session["departID"]);
                int departID = Convert.ToInt16(Session["departID"]);
                var Q_Standard = db.tblQuestionStandards.ToList().Where(s => s.DepartID.Value == departID);
                //var Q_Standard = db.tblQuestionStandards.ToList(); //.Where(s => s.DepartID == departID);
                var Q_Content = db.tblQuestionnaires.ToList();
                ViewBag.Q_Grading = db.tblQuestionGradings.ToList().OrderByDescending(x => x.GradePoint);

                var Q_Details = (from s in Q_Standard
                                 join sc in Q_Content on s.tSID equals sc.tSID
                                 select new Questionnaire
                                 {
                                     Standards = s,
                                     Questions = sc
                                 });
                //Html.RenderPartial("QuestionnaireForm", Q_Details);
                return PartialView("QuestionnaireForm", Q_Details);
            }
        }

        public ActionResult SaveResult(QuestionaireResult result)
        {
            
            if (string.IsNullOrEmpty(Convert.ToString(Session["userID"])))
            {
                //var urlBuilder = new UrlHelper(Request.RequestContext);
                //var url = urlBuilder.Action("Index", "Account");
                return Json(new { Success = false, message = "Session Expired! Please login again..." });
            }

            using (db = new AEAuditDBEntities())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    bool success = false;
                    var userID = Session["userID"].ToString();
                    try
                    {
                        if (result.Gradings == null)
                        {
                            return Json(success, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            p = new ProjectClass();
                            var tAllcateID = Convert.ToInt32(result.tAllocateID);
                            var tAllocateDetailID = Convert.ToInt32(result.tAllocateDetailID);
                            var AssessmentResults = db.tblAssessmentResults.FirstOrDefault(a => a.tAllocateDetailID == tAllocateDetailID);
                            //if (AssessmentResults != null)
                            //{
                            //    return Json("Already Submitted!", JsonRequestBehavior.AllowGet);
                            //}
                            var AssessmentCourseDetails = db.tblAllocateCoursesDetails.Find(tAllocateDetailID);
                            int tAResultID = 0;
                            if (AssessmentCourseDetails != null)
                            {
                                if (AssessmentResults == null)
                                {
                                    tblAssessmentResult addResult = new tblAssessmentResult
                                    {
                                        tAllocateID = AssessmentCourseDetails.tAllocateID,
                                        tAllocateDetailID = AssessmentCourseDetails.tAllocateDetailID,
                                        CourseID = AssessmentCourseDetails.Course_Id.ToString(),
                                        CreationDate = p.currentDateTime(),
                                        Remarks = result.remarks,
                                        Status = true
                                    };
                                    db.tblAssessmentResults.Add(addResult);
                                    db.SaveChanges();
                                    tAResultID = addResult.tAResultID;
                                }
                                else
                                {
                                    AssessmentResults.Remarks = result.remarks;
                                    AssessmentResults.ModifiedDate=p.currentDateTime();
                                    tAResultID = AssessmentResults.tAResultID;
                                }

                                AssessmentCourseDetails.isChecked = true;
                                AssessmentCourseDetails.ModifiedDate = p.currentDateTime();
                                //db.SaveChanges();
                                tblAssessmentResultDetail addResultDe = new tblAssessmentResultDetail() { };
                                for (int i = 0; i < result.Questions.Length; i++)
                                {
                                    var qID = Convert.ToByte(result.Questions[i]);
                                    var questions = db.tblQuestionnaires.FirstOrDefault(q => q.tQID == qID);
                                    addResultDe.tAResultID = tAResultID;
                                    addResultDe.StandardID = questions.tSID;
                                    addResultDe.QuestionID = questions.QSNo;
                                    for (int j = 0; j <= i; j++)
                                    {
                                        addResultDe.GradePoint = Convert.ToByte(result.Gradings[j]);
                                    }
                                    db.tblAssessmentResultDetails.Add(addResultDe);
                                    db.SaveChanges();
                                }
                                trans.Commit();
                                success = true;
                            }
                            else
                            {
                                return Json(new { Success = success, message="Course Details not found!" }, JsonRequestBehavior.AllowGet);
                            }
                            if (success == true)
                            {
                                var urlBuilder = new UrlHelper(Request.RequestContext);
                                var url = urlBuilder.Action("Index", "ClusterHead");
                                return Json(new { Success = success, redirectToUrl = url });
                            }
                            else
                            {
                                return Json(new { Success = success }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        return Json(new { Success = false, message = ex.Message });
                        //return Json(success, JsonRequestBehavior.AllowGet);
                    }
                }
            }
        }


    }
}