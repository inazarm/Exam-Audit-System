using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SZABIST_IR_App.Models
{
    public class RecapSheet_tbl2
    {
        public string iStdMain_Id { get; set; }
        public string sStdProfile_FullName { get; set; }
        public string sStdMain_RegistrationNo { get; set; }
        public string hCourseRecapSheet_MarksTypeSerialNo { get; set; }
        public double fCourseRecapSheet_MarksObtained { get; set; }
        public double fCourseRecapSheet_TotalMarks { get; set; }
        public int sMarksHead_Id { get; set; }
        public string bStdRegisteredCourse_Withdrawn { get; set; }
        public string bCourseLectureAttendance_Short { get; set; }
        public string bExamAttendanceFinal_Absent { get; set; }
        public string sExplicitAssignedGrade_Grade { get; set; }
        public string ShowResult { get; set; }
        public string sMarksHead_ShortDesc { get; set; }

        public string sMarksHead_LongDesc { get; set; }



    }
}