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
    public partial class MidtoFinalCamparision : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           // if (Request.QueryString[""] != null)
          //  {
               if (!IsPostBack)
                {
                GetReport(Convert.ToInt32(Request.QueryString["SemesterId"].ToString()), Convert.ToInt32(Request.QueryString["SemesterYear"].ToString()), Request.QueryString["program"].ToString());
              
            }
           // }
        }
        public void GetReport(int semesterId, int semesteryear,string programname)
        {
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath(@"~/Reports/MidtoFinal.rdlc");
            DataTable dt = new DataTable();
            Util u = new Util();
            string Query = "select * from MidtoFinal where Searchyear = "+ semesteryear + " and semster = "+semesterId+ " and Program='" + programname+"'";
            dt = u.RunAQry(Query);
            //string Query1 = " SELECT TOP 1000 [tGID]  ,[GradeDescription]   ,[complianceLevel]   ,[GradePoint]  FROM [AEAuditDB].[dbo].[tblQuestionGrading]";
            //dt1 = u.RunAQry(Query2);     
            ReportDataSource rds = new ReportDataSource("DataSet1", dt);
            ReportViewer1.LocalReport.DataSources.Add(rds);
            ReportViewer1.Visible = true;
        }
    }
}