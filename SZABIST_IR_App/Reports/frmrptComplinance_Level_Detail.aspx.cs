using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using SZABIST_IR_App.Models;
namespace SZABIST_IR_App.Reports
{
    public partial class frmrptComplinance_Level_Detail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string programId = string.Empty;string programName = string.Empty;
                int? tCampus_Id = null; string Ac_Year = string.Empty;
                byte? m_ExamType = null; decimal? m_Semster = null;
                string m_semesterName = string.Empty; string m_campusName = string.Empty;
                string m_ExamTypeName = string.Empty;
                string m_InstructorID = string.Empty;
                string m_Instructor = string.Empty;
                int? m_Compliance_level = null;


                if (!String.IsNullOrEmpty(Request.QueryString["m_InstructorID"]))
                    m_InstructorID =Request.QueryString["m_InstructorID"].ToString();

                if (!String.IsNullOrEmpty(Request.QueryString["m_Instructor"]))
                    m_Instructor = Request.QueryString["m_Instructor"].ToString();

                if (!String.IsNullOrEmpty(Request.QueryString["m_programId"]))
                    programId = Request.QueryString["m_programId"].ToString();

                if (!String.IsNullOrEmpty(Request.QueryString["m_ProgramName"]))
                    programName = Request.QueryString["m_ProgramName"].ToString();

                if (!String.IsNullOrEmpty(Request.QueryString["tCampus_Id"]))
                    tCampus_Id = Convert.ToInt32(Request.QueryString["tCampus_Id"].ToString());

                if (!String.IsNullOrEmpty(Request.QueryString["Ac_Year"]))
                    Ac_Year = Request.QueryString["Ac_Year"].ToString();

                if (!String.IsNullOrEmpty(Request.QueryString["m_ExamType"]))
                    m_ExamType = Convert.ToByte(Request.QueryString["m_ExamType"]);

                if (!String.IsNullOrEmpty(Request.QueryString["m_ExamTypeName"]))
                    m_ExamTypeName = Request.QueryString["m_ExamTypeName"].ToString();

                if (!String.IsNullOrEmpty(Request.QueryString["m_Semster"]))
                    m_Semster = Convert.ToByte(Request.QueryString["m_Semster"].ToString());

                if (!String.IsNullOrEmpty(Request.QueryString["m_semesterName"]))
                    m_semesterName = Request.QueryString["m_semesterName"].ToString();

                if (!String.IsNullOrEmpty(Request.QueryString["m_Compliance_level"]))
                    m_Compliance_level = Convert.ToInt32(Request.QueryString["m_Compliance_level"]);

                if (!String.IsNullOrEmpty(Request.QueryString["m_campusName"]))
                    m_campusName = Request.QueryString["m_campusName"].ToString();


                GetReport(programId,programName,m_ExamType, m_ExamTypeName, tCampus_Id,m_campusName, Ac_Year, m_Semster, m_semesterName, m_Compliance_level, m_InstructorID,m_Instructor);
            }
        }

        public void GetReport(string programId,string p_programName, byte? m_ExamType,string p_ExamTypeName, int? tCampus_Id,string p_campusName, string Ac_Year, decimal? m_Semster,string p_semesterName, int? p_complianceLevel_max, string p_userID,string p_Instructor)
        {
            //int? tCampus_Id = null;
            string m_Report_Criteria = string.Empty;
            //string Ac_Year = string.Empty;
            //byte? m_ExamType = 1;
            //decimal? m_Semster = null;
            lblMessage.Visible = false;
            lblMessage.Text = string.Empty;
            lblMessage.ForeColor = System.Drawing.Color.Black;
            rvComplinance_Level_Details.Visible = false;
            rvComplinance_Level_Details.ProcessingMode = ProcessingMode.Local;
            rvComplinance_Level_Details.LocalReport.ReportPath = Server.MapPath(@"~/Reports/rptCompl_Level_Detail.rdlc");

            DataTable dt = new DataTable();
            Util u = new Util();
            string Query = "uspGet_Compliance_Level_Detail";
            SqlCommand cmd = new SqlCommand();
            if (p_userID!=string.Empty)
            {
                cmd.Parameters.Add(new SqlParameter("@p_userID", SqlDbType.VarChar, 20)).Value =p_userID;
                m_Report_Criteria += "Instructor : " + p_Instructor;
            }

            if (programId != string.Empty)
            {
                cmd.Parameters.Add(new SqlParameter("@p_Program", SqlDbType.VarChar, 10)).Value = programId;
                m_Report_Criteria += (m_Report_Criteria!=string.Empty?",":"") + 
                    "Program : " + p_programName;
            }

            if (tCampus_Id != null)
            {
                cmd.Parameters.Add(new SqlParameter("@p_tCampus_Id", SqlDbType.TinyInt)).Value = tCampus_Id;
                m_Report_Criteria += (m_Report_Criteria != string.Empty ? "," : "") + 
                    "Campus : " + p_campusName; //@Nazar Take this value from its dropdownlist
            }
            if (m_Semster != null)
            {
                cmd.Parameters.Add(new SqlParameter("@p_Semster", SqlDbType.Decimal, 18)).Precision = 0;
                cmd.Parameters["@p_Semster"].Value = m_Semster;
                m_Report_Criteria += (m_Report_Criteria != string.Empty ? "," : "") + 
                    "Semester : " + p_semesterName; //@Nazar Take this value from its dropdownlist
            }
            if (Ac_Year != string.Empty)
            {
                cmd.Parameters.Add(new SqlParameter("@p_Year", SqlDbType.VarChar, 10)).Value = Ac_Year;
                m_Report_Criteria += (m_Report_Criteria != string.Empty ? "," : "") + 
                    "Semester Year : " + Ac_Year; //@Nazar Take this value from its dropdownlist
            }
            if (m_ExamType != null)
            {
                cmd.Parameters.Add(new SqlParameter("@p_ExamType", SqlDbType.TinyInt)).Value = m_ExamType;
                m_Report_Criteria += (m_Report_Criteria != string.Empty ? "," : "") + 
                    "Exam Type : " + p_ExamTypeName;
            }
            if (p_complianceLevel_max != null)
            {
                cmd.Parameters.Add(new SqlParameter("@p_complianceLevel_max", SqlDbType.TinyInt)).Value = p_complianceLevel_max;
                m_Report_Criteria += (m_Report_Criteria != string.Empty ? "," : "") + 
                    "Compliance Level : " + p_complianceLevel_max.ToString() + "% or below";

            }
            dt = u.Execute_SP(Query, cmd);
            if (dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                dv.Sort = "StandardID,QuestionID";
                DataTable dtQuestionInfo = dv.ToTable(true, "StandardID", "QuestionID", "Question");
                ReportDataSource rds = new ReportDataSource("dsCompliance_Level_Detail", dt);
                ReportDataSource rdsQuestionList = new ReportDataSource("dsQuestionList", dtQuestionInfo);
                ReportParameter[] param = new ReportParameter[] {
                    new ReportParameter("prmReportCriteria","Report Criteria: " +m_Report_Criteria)
                };

                if (m_Report_Criteria!=string.Empty)
                    rvComplinance_Level_Details.LocalReport.SetParameters(param);
                rvComplinance_Level_Details.LocalReport.DataSources.Add(rds);
                rvComplinance_Level_Details.LocalReport.DataSources.Add(rdsQuestionList);
                rvComplinance_Level_Details.Visible = true;
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
