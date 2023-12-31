﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SZABIST_IR_App.Models;

namespace SZABIST_IR_App.Controllers
{
    public class AssessmentController : Controller
    {
        AEAuditDBEntities db;
        ProjectClass p;
        // GET: Assessment
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["userID"])))
                return RedirectToAction("Index", "Account");

            using (db = new AEAuditDBEntities())
            {
                try
                {
                    //var list = db.tblAllocateCourses.ToList().Where(h => h.Status == true);
                    //var list = db.vueExamList_Checked_NotAssessed.Where(c => c.isCopyAvailable == null).ToList().OrderByDescending(x => x.CreationDate);
                    //var listOrdered = list.OrderByDescending(x => x.Year);
                    return View(db.uspAllocatedCoursesList(true,false,true).ToList());
                }
                catch (Exception ex)
                {
                    ViewBag.erro = "Exception : " + ex.Message;
                    vueExamList_Checked_NotAssessed obj = new vueExamList_Checked_NotAssessed();
                    return View();

                }

            }
        }

        public ActionResult PendingListBoth()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["userID"])))
                return RedirectToAction("Index", "Account");

            using (db=new AEAuditDBEntities())
            {
                var result = db.uspAllocatedCoursesList(false, false, true).ToList();
                return PartialView("PendingListBoth", result);
            }

        }
        public ActionResult AllocatedCourses(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["userID"])))
                return RedirectToAction("Index", "Account");

            if (id != null)
            {
                using (db = new AEAuditDBEntities())
                {
                    ViewBag.Details = db.tblAllocateCourses.FirstOrDefault(d => d.tAllocateID == id);
                    //Old Code condition : paper checked by Cluster is mandatory then IR can check paper
                    //List<tblAllocateCoursesDetail> crsDetails = db.tblAllocateCoursesDetails.Where(c => c.tAllocateID == id && c.isChecked == true &&c.isCheckedByIR==false && c.isCopyAvailable==null ).OrderBy(x => x.sCourse_LongDesc).ToList();
                    //New Code condition : paper checked by cluster is not mandatory, IR can check their part before as well as after.. isChecked condition removed
                    List<tblAllocateCoursesDetail> crsDetails = db.tblAllocateCoursesDetails.Where(c => c.tAllocateID == id &&c.isCheckedByIR==false && c.isCopyAvailable==null ).OrderBy(x => x.sCourse_LongDesc).ToList();
                    //List<tblCampu> campusDe = db.tblCampus.ToList();
                    //var courseDetails = (from course in crsDetails
                    //                     join campus in campusDe on course.tCampus_Id equals campus.tCampus_Id orderby course.sCourse_LongDesc
                    //                     where course.tAllocateID == id
                    //                     select new vm_AllocatedCrsDe_CamupsName
                    //                     {
                    //                         AllocatedCrsDe = course,
                    //                         CampusDetails = campus
                    //                     }).ToList();

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

        public ActionResult SelectedCourse(int id)
        {
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(Session["userID"])))
                    return RedirectToAction("Index", "Account");

                using (db = new AEAuditDBEntities())
                {
                    //old code cluster checked must be true 
                    //var findCrs = db.tblAllocateCoursesDetails.FirstOrDefault(c => c.tAllocateDetailID == id && c.isChecked == true);
                    //new code cluster isChecked Removed
                    var findCrs = db.tblAllocateCoursesDetails.FirstOrDefault(c => c.tAllocateDetailID == id);
                    if (findCrs == null)
                    {
                        return Json(new { error = "Course Already Examined and Submitted!" }, JsonRequestBehavior.AllowGet);
                        //return Json("Already Submitted!",JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return PartialView("SelectedCourse", findCrs);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }


        }

        public ActionResult QuestionnaireClusterHead(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["userID"])))
                return RedirectToAction("Index", "Account");

            using (AEAuditDBEntities db = new AEAuditDBEntities())
            {
                if (id != null)
                {
                    //int departID = Convert.ToInt16(Session["departID"]);
                    var Q_Standard = db.tblQuestionStandards.ToList().Where(s => s.DepartID == 1);
                    var Q_Content = db.tblQuestionnaires.ToList();
                    ViewBag.Q_Grading = db.tblQuestionGradings.ToList().OrderByDescending(x => x.GradePoint);
                    var result = db.tblAssessmentResults.FirstOrDefault(r => r.tAllocateDetailID == id);
                    ViewBag.Result = db.tblAssessmentResultDetails.Where(r => r.tAResultID == result.tAResultID && r.StandardID == 1).ToList();

                    var Q_Details = (from s in Q_Standard
                                     join sc in Q_Content on s.tSID equals sc.tSID
                                     select new Questionnaire
                                     {
                                         Standards = s,
                                         Questions = sc
                                     });
                    //Html.RenderPartial("QuestionnaireForm", Q_Details);
                    return PartialView("QuestionnaireClusterHead", Q_Details);
                }
                else
                {
                    return RedirectToAction("Index");
                }

            }
        }

        public ActionResult QuestionnaireList()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["userID"])))
                return RedirectToAction("Index", "Account");

            using (AEAuditDBEntities db = new AEAuditDBEntities())
            {
                {
                    int departID = Convert.ToInt16(Session["departID"]);
                    var Q_Standard = db.tblQuestionStandards.ToList().Where(s => s.DepartID == departID);
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
                    return PartialView("QuestionnaireList", Q_Details);
                }
            }
        }

        public ActionResult SaveResult(QuestionaireResult result)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["userID"])))
                //return RedirectToAction("Index", "Account");
                return Json(new { Success = false, message ="Session Expired! Please login again..."});

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

                            //var AssessmentResults = db.tblAssessmentResults.FirstOrDefault(a => a.tAllocateDetailID == tAllocateDetailID);
                            var AssessmentResults = db.tblAssessmentResults.FirstOrDefault(a => a.tAllocateDetailID == tAllocateDetailID);
                            
                            var AssessmentCourseDetails = db.tblAllocateCoursesDetails.Find(tAllocateDetailID);
                            int tAResultID = 0;
                            if (AssessmentResults==null)//if null create new entry else update record...
                            {
                                tblAssessmentResult addResult = new tblAssessmentResult
                                {
                                    tAllocateID = AssessmentCourseDetails.tAllocateID,
                                    tAllocateDetailID = AssessmentCourseDetails.tAllocateDetailID,
                                    CourseID = AssessmentCourseDetails.Course_Id.ToString(),
                                    RemarksIR = result.remarks,
                                    CheckedBy = userID,
                                    CheckedDate = p.currentDateTime(),
                                    Status = true
                                };
                                db.tblAssessmentResults.Add(addResult);
                                db.SaveChanges();
                                tAResultID = addResult.tAResultID;
                            }
                            else
                            {
                                AssessmentResults.RemarksIR = result.remarks;
                                AssessmentResults.CheckedBy = userID;
                                AssessmentResults.CheckedDate = p.currentDateTime();
                                tAResultID = AssessmentResults.tAResultID;
                            }
                            AssessmentCourseDetails.isCheckedByIR = true;
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
                            db.SaveChanges();
                            trans.Commit();
                            success = true;
                            return Json(new { Success = success }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        return Json(new { Success = false, message = ex.Message });
                    }
                }
            }
        }

        public ActionResult ifCopyUnavailable(List<int> data) 
        {
            var idz = data;
            if (idz != null)
            {
                using (db=new AEAuditDBEntities())
                {
                    try
                    {
                        foreach (var item in idz)
                        {
                            var Record = db.tblAllocateCoursesDetails.Find(item);
                            if (Record != null)
                            {
                                Record.isCopyAvailable = false;
                                db.SaveChanges();
                            }
                        }
                        return Json(new { data ="Checked Courses Removed from list" }, JsonRequestBehavior.AllowGet);

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            else
            {
                return Json(false);
            }
        }
    }
}