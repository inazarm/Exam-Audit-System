using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SZABIST_IR_App.Models
{
    public class RecapSheet_tbl5
    {
        public int iStdProgramBatch_Id { get; set; }

        public string sGradingPlan_Grade { get; set; }

        public string fGradingPlan_MarksFrom { get; set; }

        public double fGradingPlan_MarksTo { get; set; }

        public string fGradingPlan_GPA { get; set; }
        public int tGradingPlan_Id { get; set; }

        public string sGradingPlan_Remarks { get; set; }

        public int sStdMain_Id { get; set; }
        // public int sStdMain_Id { get; set; }
    }
}