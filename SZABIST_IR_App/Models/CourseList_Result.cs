using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SZABIST_IR_App.Models
{
    public class CourseList_Result
    {
        public int iCourse_Id { get; set; }
        public string sUser_Id { get; set; }
        public int iSemester_Id { get; set; }
        public int iSemesterSection_Id { get; set; }
        public int tCampus_Id { get; set; }
        public int tProgram_Id { get; set; }
        public string sCourse_Code { get; set; }
        public string sCourse_LongDesc { get; set; }
        public string Instructor { get; set; }
        public string Class { get; set; }

    }
}