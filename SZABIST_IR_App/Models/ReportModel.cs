using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SZABIST_IR_App.Models
{
    public class ReportModel
    {
        public int semster { get; set; }
 
        public int year { get; set; }

        public string faculty { get; set; }

        public string Program { get; set; }

        public int Department { get; set; }

        public int Instructor { get; set; }

        public string Course_Name { get; set; }

        public string ReportType { get; set; }

        public int Semster { get; set; }

        public byte ExamType { get; set; }

        public byte Campus_Id { get; set; }
        public string sCampus_ShortDesc { get; set; }


        [MaxLength(3)]
        public int compliance_Level { get; set; }
    }
}