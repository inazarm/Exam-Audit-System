using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SZABIST_IR_App.Reports
{
    public partial class IRReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["Inst"] != null || Request.QueryString["code"] != null)
            {
                if (!IsPostBack)
                {
                    GetReport(Request.QueryString["Inst"].ToString(), Request.QueryString["code"].ToString(), Convert.ToInt32(Request.QueryString["Semster"].ToString()), Convert.ToInt32(Request.QueryString["searchyear"].ToString()));
                }
            }
        }


        public void GetReport(string instructor,string code,int semesterId,int searchyear)
        {
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath(@"~/Reports/Report1.rdlc");

            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            Util u = new Util();
            string Query = "select st.tSID, st.StandardDescription,st.StandardPecentage,Q.Question,Q.QSNo,Q.tQID,tBd.GradePoint,Tb.Remarks, d.Instructor,d.sCourse_Code,c.Year as searchyear, c.semster from tblQuestionnaire Q inner join tblQuestionStandard st "+
           " on Q.tSID = st.tSID inner join tblAssessmentREsultDetail tBd on tBd.QuestionID = Q.tQID "+
           " inner join tblAssessmentResult Tb on tb.tAResultID = tBd.tAResultID "+
            " inner join tblAllocateCoursesDetails d on tb.tAllocateDetailID = d.tAllocateDetailID "+
            " inner join dbo.tblAllocateCourses c on c.tAllocateID = d.tAllocateID "+
           " where tBd.StandardID = 1 and  d.Instructor = '" + instructor + "' and d.sCourse_Code ='" + code + "' and c.Year  = "+searchyear+ " and c.semster = "+ semesterId +"";
            dt =  u.RunAQry(Query);
            string Query2 = " select st.tSID as tSID1, st.StandardDescription as StandardDescription1 ,st.StandardPecentage as StandardPecentage1,Q.Question as Question1,Q.QSNo as QSNo1,Q.tQID as tQID,tBd.GradePoint as GradePoint1, d.Instructor,d.sCourse_Code, c.Year as searchyear, c.semster from tblQuestionnaire Q inner join tblQuestionStandard st " +
            " on Q.tSID = st.tSID inner join tblAssessmentREsultDetail tBd on tBd.QuestionID = Q.tQID "+
             " inner join tblAssessmentResult Tb on tb.tAResultID =  tBd.tAResultID " +
            " inner join tblAllocateCoursesDetails d on tb.tAllocateDetailID = d.tAllocateDetailID " +
             " inner join dbo.tblAllocateCourses c on c.tAllocateID = d.tAllocateID " +
         " where tBd.StandardID = 1 and  d.Instructor = '" + instructor + "' and d.sCourse_Code ='" + code + "' and c.Year  = " + searchyear + " and c.semster = " + semesterId + "";
            dt1 = u.RunAQry(Query2);
            string Query3 = "  select d.sCourse_LongDesc as Course_Name,d.sCourse_Code as Course_Code,d.Instructor,case when c.Semster = 1 then 'Spring'  when c.Semster = 2 then 'Summer'  else 'Fall' end as semeter_Type, " +
    " case when c.ExamType = 1 then 'Midterm Exams'  when c.ExamType = 2 then 'Final Exams' else 'Both Exams'  end as Exam_type,  cam.sCampus_ShortDesc,d.CreationDate " +
    "    from tblAllocateCoursesDetails d inner join tblAllocateCourses c on d.tAllocateID = c.tAllocateID " +
    "   inner join tblCampus cam on c.tCampus_Id = cam.tCampus_Id where d.Instructor = '"+ instructor +"' and d.sCourse_Code ='"+ code + "'";
            dt2 = u.RunAQry(Query3);

            //string Query1 = " SELECT TOP 1000 [tGID]  ,[GradeDescription]   ,[complianceLevel]   ,[GradePoint]  FROM [AEAuditDB].[dbo].[tblQuestionGrading]";
            //dt1 = u.RunAQry(Query2);

            //  ReportDataSource rds2 = new ReportDataSource("DataSet2", dt2);
            ReportDataSource rds2 = new ReportDataSource("DataSet3", dt2);
            ReportDataSource rds1 = new ReportDataSource("DataSet2", dt1);
            ReportDataSource rds = new ReportDataSource("DataSet1", dt);
            ReportViewer1.LocalReport.DataSources.Add(rds);
            ReportViewer1.LocalReport.DataSources.Add(rds1);
            ReportViewer1.LocalReport.DataSources.Add(rds2);
            ReportViewer1.Visible = true;
        }
    }
}