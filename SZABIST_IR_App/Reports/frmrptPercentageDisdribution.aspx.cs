using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace SZABIST_IR_App.Reports
{
    public partial class frmrptPercentageDisdribution : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string programId = string.Empty; int? tCampus_Id = null; string Ac_Year = string.Empty;
                byte? m_ExamType = null; decimal? m_Semster = null;
                string m_semesterName = string.Empty; string m_campusName = string.Empty;
                string m_ExamTypeName = string.Empty;
                string m_ProgramName = string.Empty;

                if (!String.IsNullOrEmpty(Request.QueryString["programId"]))
                    programId = Request.QueryString["programId"].ToString();

                if (!String.IsNullOrEmpty(Request.QueryString["tCampus_Id"]))
                    tCampus_Id = Convert.ToInt32(Request.QueryString["tCampus_Id"].ToString());

                if (!String.IsNullOrEmpty(Request.QueryString["Ac_Year"]))
                    Ac_Year = Request.QueryString["Ac_Year"].ToString();

                if (!String.IsNullOrEmpty(Request.QueryString["m_ExamType"]))
                    m_ExamType = Convert.ToByte(Request.QueryString["m_ExamType"].ToString());

                if(!String.IsNullOrEmpty(Request.QueryString["m_Semster"]))
                    m_Semster= Convert.ToByte(Request.QueryString["m_Semster"].ToString());

                if (!String.IsNullOrEmpty(Request.QueryString["m_semesterName"]))
                    m_semesterName = Request.QueryString["m_semesterName"].ToString();

                if (!String.IsNullOrEmpty(Request.QueryString["m_ExamTypeName"]))
                    m_ExamTypeName = Request.QueryString["m_ExamTypeName"].ToString();

                if (!String.IsNullOrEmpty(Request.QueryString["m_campusName"]))
                    m_campusName = Request.QueryString["m_campusName"].ToString();

                if (!String.IsNullOrEmpty(Request.QueryString["m_ProgramName"]))
                    m_ProgramName = Request.QueryString["m_ProgramName"].ToString();

                GetReport(programId, tCampus_Id, Ac_Year, m_ExamType, m_Semster, m_semesterName, m_ExamTypeName, m_campusName, m_ProgramName);
            }
        }

        public void GetReport(string p_programId, int? tCampus_Id,string Ac_Year, byte? m_ExamType, decimal? m_Semster, string p_semesterName, string p_ExamTypeName, string p_campusName, string p_ProgramName)
        {
            //int? tCampus_Id = null;
            string m_Report_Criteria = string.Empty;
            //string Ac_Year = string.Empty;
            //byte? m_ExamType = 1;
            //decimal? m_Semster = null;
            lblMessage.Visible = false;
            lblMessage.Text = string.Empty;
            lblMessage.ForeColor = System.Drawing.Color.Black;
            rvPercentDistributions.Visible = false;
            rvPercentDistributions.ProcessingMode = ProcessingMode.Local;
            rvPercentDistributions.LocalReport.ReportPath = Server.MapPath(@"~/Reports/rptPercentDistribution.rdlc");

            DataTable dt = new DataTable();
            Util u = new Util();
            string Query = "GetPercentageDistribution";
            SqlCommand cmd = new SqlCommand();
            if (p_programId != string.Empty)
            {
                cmd.Parameters.Add(new SqlParameter("@programType", SqlDbType.VarChar,10)).Value = p_programId;
                m_Report_Criteria += " Program : " + p_ProgramName; //@Nazar Take this value from its dropdownlist
            }
            

            if (tCampus_Id!=null)
            {
                cmd.Parameters.Add(new SqlParameter("@campus", SqlDbType.TinyInt)).Value = tCampus_Id;
                m_Report_Criteria += " Campus : " + p_campusName; //@Nazar Take this value from its dropdownlist
            }
            if (m_Semster!=null)
            {
                cmd.Parameters.Add(new SqlParameter("@semestertype", SqlDbType.Decimal, 18)).Precision = 0;
                cmd.Parameters["@semestertype"].Value = m_Semster;
                m_Report_Criteria += " Semester : " + p_semesterName; //@Nazar Take this value from its dropdownlist
            }
            if (Ac_Year != string.Empty)
            {
                cmd.Parameters.Add(new SqlParameter("@semesteryear", SqlDbType.VarChar, 10)).Value = Ac_Year;
                m_Report_Criteria += " Semester Year : " + Ac_Year; //@Nazar Take this value from its dropdownlist
            }
            if (m_ExamType != null)
            {
                cmd.Parameters.Add(new SqlParameter("@p_ExamType", SqlDbType.TinyInt)).Value = m_ExamType;
                m_Report_Criteria += " Exam Type : " + p_ExamTypeName;
            }

            dt = u.Execute_SP(Query, cmd);
            if (dt.Rows.Count > 0)
            {
                ReportDataSource rds = new ReportDataSource("dsGetPercentageDistribution", dt);
                ReportParameter[] param = new ReportParameter[] {
                    new ReportParameter("prmReportCriteria","Report Criteria: " +m_Report_Criteria)
                };

                if (m_Report_Criteria!=string.Empty)
                    rvPercentDistributions.LocalReport.SetParameters(param);
                rvPercentDistributions.LocalReport.DataSources.Add(rds);
                rvPercentDistributions.Visible = true;
            }
            else
            {
                lblMessage.Visible = true;
                lblMessage.Text = "No record found";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

    }
}
