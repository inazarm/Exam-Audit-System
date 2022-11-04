using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SZABIST_IR_App.Models
{
    public class Programs
    {
        public string programID { get; set; }

        public Nullable<byte> iFacultyId { get; set; }
        public Nullable<byte> tProgram_Id { get; set; }
        public Nullable<byte> tCampus_Id { get; set; }
        public Nullable<byte> tDegree_Id { get; set; }
        public string programName { get; set; }

        public string Program_ShortDesc { get; set; }

        public string Program_LongDesc { get; set; }
    }
}