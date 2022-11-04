using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SZABIST_IR_App.Models
{
    public class RecapSheet_tbl1
    {
        public int tMarksHead_Id { get; set; }
        public string sMarksHead_ShortDesc { get; set; }
        public string sMarkHead_LongDesc { get; set; }
        public double fCourseMarksDistribution_TotalMarks { get; set; }
        public int bMarksHead_ExemptedForDGS { get; set; }
        public int tCourseMarksDistribution_TotalFrequency { get; set; }
        public int tCourseMarksDistribution_TotalExempted { get; set; }

    }
}