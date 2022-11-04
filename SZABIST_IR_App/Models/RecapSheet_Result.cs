using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SZABIST_IR_App.Models
{
    public class RecapSheet_Result
    {
        public List<RecapSheet_tbl1> headermodel { get; set; }
        public List<RecapSheet_tbl2> detail { get; set; }
        public List<RecapSheet_tbl5> grade { get; set; }
        public List<RecapSheet_tbl6> grading { get; set; }

        public string sCourse_Code { get; set; }

        public string sCourse_LongDesc { get; set; }

        public string Class { get; set; }

        public string sUserProfile_FullName { get; set; }

        public int bOffeeredCourseTea_RecapPosted { get; set; }

        public int CHours { get; set; }

        public string dtmodify { get; set; }

        public int tMarksHead_Id { get; set; }

        public string sMarksHead_ShortDesc { get; set; }

        public string sMarkHead_LongDesc { get; set; }

        public double fCourseMarksDistribution_TotalMarks { get; set; }

        public string tCourseMarksDistribution_TotalFrequency { get; set; }

        public string tCourseMarksDistribution_TotalExempted { get; set; }

        public string bMarksHead_ExemptedForDGs { get; set; }

        public int iStdMain_Id { get; set; }

        public string sStdProfile_FullName { get; set; }

        public string sStdMain_RegistrationNo { get; set; }

        public string hCourseRecapSheet_MarksTypeSerialNo { get; set; }

        public double fCourseRecapSheet_MarksObtained { get; set; }

        public double fCourseRecapSheet_TotalMarks { get; set; }

        public string bStdRegisteredCourse_Withdrawn { get; set; }

        public string bCourseLectureAttendance_Short { get; set; }

        public string bExamAttendanceFinal_Absent { get; set; }

        public string sExplicitAssignedGrade_Grade { get; set; }

        public int iStdProgramBatch_Id { get; set; }

        public string sGradingPlan_Grade { get; set; }

        public string fGradingPlan_MarksFrom { get; set; }

        public double fGradingPlan_MarksTo { get; set; }

        public string fGradingPlan_GPA { get; set; }

        public int tGradingPlan_Id { get; set; }

        public string sGradingPlan_Remarks { get; set; }

        public int tDegree_id { get; set; }
        public int icourse_id { get; set; }
        public int tProgram_id { get; set; }
        public int iSemesterSection_id { get; set; }
        public string ProgramSemester { get; set; }
        public string CourseName { get; set; }
        public string SectionName { get; set; }
        public string sFullName { get; set; }
        public string tSemester_No { get; set; }
        public int isTaught { get; set; }
        public string sUser_id { get; set; }
        public int iOfferedCourses_Course_id { get; set; }
        public int bOfferedCourses_Course_id { get; set; }
        public bool bOfferedCourseTea_isAdjusted { get; set; }
        public string bIsRecapPostedByTeacher { get; set; }
        public string ProgramDesc { get; set; }
        public int iSemester_id { get; set; }
        public int iReturnValue { get; set; }
        public int tCourseMarksDistribution_Totalfrequency { get; set; }
        public string tCourseMarksDistribution_TotalExemted { get; set; }
        public string sUser_Id { get; set; }
        public int iOfferedCourses_Course_Id { get; set; }
        public int tMarkHead_Id { get; set; }
        public int iSemestersection_Id { get; set; }
        public int ISemester_Id { get; set; }
        public string sMarksHead_LongDesc { get; set; }
        public int bMarksHead_ExemtedForDGs { get; set; }

        public string sCourse_ShortDesc { get; set; }

        public string PstRecapShtData { get; set; }

        public string mUserId { get; set; }

        public int mCourse_Id { get; set; }

        public int mSemester_id { get; set; }

        public int mSemesterSection_Id { get; set; }


        public string sStdRegisteredCourse_Grade { get; set; }

        public string sExplicitAssignedGrade_Remarks { get; set; }

        public int IsDGrader { get; set; }
    }
}