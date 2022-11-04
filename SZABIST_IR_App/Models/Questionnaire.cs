using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SZABIST_IR_App.Models
{
    public class Questionnaire
    {
        public tblQuestionStandard Standards { get; set; }
        public tblQuestionnaire Questions { get; set; }
        public tblQuestionGrading  Gradings { get; set; }
        public List<tblAllocateCoursesDetail> aCourseDetails { get; set; }
    }
}