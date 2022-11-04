using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SZABIST_IR_App.Models
{
    public class QuestionaireResult
    {
        public string[] Standards { get; set; }
        public string[] Questions { get; set; }
        public string[] Gradings { get; set; }
        public string tAllocateID { get; set; }
        public string tAllocateDetailID { get; set; }
        public string remarks { get; set; }
        public tblAllocateCoursesDetail CourseDetails { get; set; }
    }
}