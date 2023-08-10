using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SZABIST_IR_App.Models
{
    public class Assessments
    {
        public int Completed { get; set; }
        public int Pending { get; set; }
        public int TotalAssigned { get; set; }
        public int TotalCompleted { get; set; }

        public int  RoleId { get; set; }
        public string UserId { get; set; }
    }
}