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
    public partial class frmrptFaculty_Wise_Questions_Assessment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                byte tFaculty_Id = 3; int? tCampus_Id = null; string Ac_Year = string.Empty;
                byte? m_ExamType = null; decimal? m_Semster = null;
                string m_semesterName = string.Empty; string m_campusName = string.Empty;
                string m_ExamTypeName = string.Empty;

                if (!String.IsNullOrEmpty(Request.QueryString["Faculty_Id"]))
                    tFaculty_Id = Convert.ToByte(Request.QueryString["Faculty_Id"].ToString());

                if (!String.IsNullOrEmpty(Request.QueryString["tCampus_Id"]))
                    tCampus_Id = Convert.ToInt32(Request.QueryString["tCampus_Id"].ToString());

                if (!String.IsNullOrEmpty(Request.QueryString["Ac_Year"]))
                    Ac_Year = Request.QueryString["Ac_Year"].ToString();

                if (!String.IsNullOrEmpty(Request.QueryString["m_ExamType"]))
                    m_ExamType = Convert.ToByte(Request.QueryString["m_ExamType"].ToString());

                if(!String.IsNullOrEmpty(Request.QueryString["m_Semster"]))
                    m_Semster= Convert.ToByte(Request.QueryString["m_Semster"].ToString());

                if (!String.IsNullOrEmpty(Request.QueryString["m_semesterName"].ToString()))
                    m_semesterName = Request.QueryString["m_semesterName"].ToString();

                if (!String.IsNullOrEmpty(Request.QueryString["m_ExamTypeName"].ToString()))
                    m_ExamTypeName = Request.QueryString["m_ExamTypeName"].ToString();

                if (!String.IsNullOrEmpty(Request.QueryString["m_campusName"].ToString()))
                    m_campusName = Request.QueryString["m_campusName"].ToString();


                GetReport(tFaculty_Id, tCampus_Id, Ac_Year, m_ExamType, m_Semster, m_semesterName, m_ExamTypeName, m_campusName);
            }
        }

        public void GetReport(byte p_tFaculty_Id, int? tCampus_Id,string Ac_Year, byte? m_ExamType, decimal? m_Semster, string p_semesterName, string p_ExamTypeName, string p_campusName)
        {
            //int? tCampus_Id = null;
            string m_Report_Criteria = string.Empty;
            //string Ac_Year = string.Empty;
            //byte? m_ExamType = 1;
            //decimal? m_Semster = null;
            lblMessage.Visible = false;
            lblMessage.Text = string.Empty;
            lblMessage.ForeColor = System.Drawing.Color.Black;
            rvFaculty_Wise_Questions_Assessment.Visible = false;
            rvFaculty_Wise_Questions_Assessment.ProcessingMode = ProcessingMode.Local;
            rvFaculty_Wise_Questions_Assessment.LocalReport.ReportPath = Server.MapPath(@"~/Reports/rptFaculty_Question_Assessment.rdlc");

            DataTable dt = new DataTable();
            Util u = new Util();
            string Query = "uspGet_Faculty_Wise_Questions_Assessment";
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Add(new SqlParameter("@p_tFaculty_Id", SqlDbType.TinyInt)).Value = p_tFaculty_Id;

            if (tCampus_Id!=null)
            {
                cmd.Parameters.Add(new SqlParameter("@p_tCampus_Id", SqlDbType.TinyInt)).Value = tCampus_Id;
                m_Report_Criteria += " Campus : " + p_campusName; //@Nazar Take this value from its dropdownlist
            }
            if (m_Semster!=null)
            {
                cmd.Parameters.Add(new SqlParameter("@p_Semster", SqlDbType.Decimal, 18)).Precision = 0;
                cmd.Parameters["@p_Semster"].Value = m_Semster;
                m_Report_Criteria += " Semester : " + p_semesterName; //@Nazar Take this value from its dropdownlist
            }
            if (Ac_Year != string.Empty)
            {
                cmd.Parameters.Add(new SqlParameter("@p_Year", SqlDbType.VarChar, 10)).Value = Ac_Year;
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
                ReportDataSource rds = new ReportDataSource("dsDepartment_Wise_Questions_Assessment", dt);
                ReportParameter[] param = new ReportParameter[] {
                    new ReportParameter("prmReportCriteria","Report Criteria: " +m_Report_Criteria)
                };

                if (m_Report_Criteria!=string.Empty)
                    rvFaculty_Wise_Questions_Assessment.LocalReport.SetParameters(param);
                rvFaculty_Wise_Questions_Assessment.LocalReport.DataSources.Add(rds);
                rvFaculty_Wise_Questions_Assessment.Visible = true;
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
