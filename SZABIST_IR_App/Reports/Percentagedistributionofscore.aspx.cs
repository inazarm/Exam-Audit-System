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
    public partial class Percentagedistributionofscore : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
         {
            if (Request.QueryString["Inst"] != null || Request.QueryString["code"] != null)
            {
                if (!IsPostBack)
                {
                    GetReport(Request.QueryString["Inst"].ToString(), Request.QueryString["code"].ToString(), Convert.ToInt32(Request.QueryString["Semster"]), Convert.ToInt32(Request.QueryString["searchyear"]));
                }
            }
        }
        public void GetReport(string instructor, string code, int semesterId, int semesteryear)
        {
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath(@"~/Reports/Scoredistribution.rdlc");
            DataTable dt = new DataTable();
            Util u = new Util();
            string Query = "select * from GetPercetageDistributionofScore where sCourse_Code = '" + code + "' and Instructor = '" + instructor + "'";
            dt = u.RunAQry(Query);
            //string Query1 = " SELECT TOP 1000 [tGID]  ,[GradeDescription]   ,[complianceLevel]   ,[GradePoint]  FROM [AEAuditDB].[dbo].[tblQuestionGrading]";
            //dt1 = u.RunAQry(Query2);  
            string semesterName = "";  
            if (semesterId == 1)
            {
                semesterName = "Spring";
            }
            if (semesterId == 2)
            {
                semesterName = "Summer";
            }
            if (semesterId == 3)
            {
                semesterName = "Fall";
            }

            ReportDataSource rds = new ReportDataSource("DataSet1", dt);
            ReportParameter[] param = new ReportParameter[] {
                new ReportParameter("SemesterName",semesterName, false),
                  new ReportParameter("SemesterYear",semesteryear.ToString(), false),
                   new ReportParameter("FacultyName",instructor, false),
                   new ReportParameter("CourseName", dt.Rows[0]["sCourse_LongDesc"].ToString(), false)
              };
            ReportViewer1.LocalReport.SetParameters(param);
            ReportViewer1.LocalReport.DataSources.Add(rds);
            ReportViewer1.Visible = true;
        }
    }
}