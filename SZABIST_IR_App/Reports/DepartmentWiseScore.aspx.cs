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
    public partial class DepartmentWiseScore : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetReport(Convert.ToInt32(Request.QueryString["SemesterId"].ToString()), Convert.ToInt32(Request.QueryString["SemesterYear"].ToString()));

             //   GetReport(Convert.ToInt32(Request.QueryString["SemesterId"].ToString()), Convert.ToInt32(Request.QueryString["SemesterYear"].ToString()), Request.QueryString["depart"].ToString());
            }
        }

        public void GetReport(int SemesterId, int SemesterYear)
        {
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath(@"~/Reports/DepartmentWiseScore.rdlc");
            DataTable dt = new DataTable();
            Util u = new Util();
            string Query = "select * from departmentWiseScore where searchyear = "+ SemesterYear + " and semster =" + SemesterId+"";
            dt = u.RunAQry(Query);
            //string Query1 = " SELECT TOP 1000 [tGID]  ,[GradeDescription]   ,[complianceLevel]   ,[GradePoint]  FROM [AEAuditDB].[dbo].[tblQuestionGrading]";
            //dt1 = u.RunAQry(Query2);     
            string SemesterName = "";
            if (SemesterId == 1)
            {
                SemesterName = "Spring";
            }
            if (SemesterId == 2)
            {
                SemesterName = "Summer";
            }
            if (SemesterId == 3)
            {
                SemesterName = "Fall";
            }
            ReportDataSource rds = new ReportDataSource("DataSet1", dt);
            ReportParameter[] param = new ReportParameter[] {
                new ReportParameter("SemesterName",SemesterName, false),
                  new ReportParameter("SemesterYear",SemesterYear.ToString(), false)
              };
            ReportViewer1.LocalReport.SetParameters(param);
            ReportViewer1.LocalReport.DataSources.Add(rds);
            ReportViewer1.Visible = true;
        }
    }
}