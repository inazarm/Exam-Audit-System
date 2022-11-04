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
    public partial class StandardWise : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["Inst"] != null || Request.QueryString["code"] != null)
            {
                if (!IsPostBack)
                {
                    GetReport(Request.QueryString["Inst"].ToString(), Request.QueryString["code"].ToString(), Convert.ToInt32(Request.QueryString["Semster"].ToString()), Convert.ToInt32(Request.QueryString["searchyear"].ToString()),Request.QueryString["section"].ToString());
                }
            }
        }

        public void GetReport(string instructor, string code, int semesterId,int semesteryear, string section)
        {
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath(@"~/Reports/StandarWiseReport.rdlc");
            DataTable dt = new DataTable();          
            Util u = new Util();
            string Query = "select * from GetStandardWisePicChart where sCourse_Code = '"+ code + "' and Instructor = '"+ instructor + "' and Semster = "+semesterId+ " and searchyear = "+ semesteryear + " and Class = '"+ section + "'";
            dt = u.RunAQry(Query);
            if (dt.Rows.Count>0)
            {
                //string Query1 = " SELECT TOP 1000 [tGID]  ,[GradeDescription]   ,[complianceLevel]   ,[GradePoint]  FROM [AEAuditDB].[dbo].[tblQuestionGrading]";
                //dt1 = u.RunAQry(Query2);   
                string semester = "";
                if (semesterId == 1)
                {
                    semester = "Spring";
                }
                if (semesterId == 2)
                {
                    semester = "Summer";
                }
                if (semesterId == 3)
                {
                    semester = "Fall";
                }
                ReportDataSource rds = new ReportDataSource("DataSet2", dt);
                ReportParameter[] param = new ReportParameter[] {
                new ReportParameter("SemesterName",semester, false),
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
}