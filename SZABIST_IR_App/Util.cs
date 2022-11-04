using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Configuration;

/// <summary>
/// Summary description for Util
/// </summary>
public class Util
{
    public Util()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    // string constr = @"server=10.0.5.55; user id=sa; password=Hptel-Mobilink#224; database=zabdesk;";
    //string constr = @"server=10.0.5.91; user id=zabsoltest; password=zabsol.786; database=AEAuditDB;";
    string constr = ConfigurationManager.AppSettings["ReportConStr"];
    // string constr = @"server=(local); user id=sa; password=123; database=zabdesk_sept;";
    public static string SemetserTypeId = "3";
    public static string SemetserType = "Fall";
    public static string SemesterYear = "2020";
    public static string MasterPassword = "szabist.90.100";      
    public bool SqlCheckPass(string input)
    {
        if (input.Contains("select") || input.Contains("delete") || input.Contains("update") || input.Contains("insert") || input.Contains(";") || input.Contains("union") || input.Contains(" * ") || input.Contains("--") || input.Contains("drop") || input.Contains("table") || input.Contains("=") || input.Contains("<") || input.Contains(">") || input.Contains("+"))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public void SendEmail(string ToEmail, string Subject, string Body)
    {
        string mySMTP = "alpha.szabist.edu.pk";
            SmtpClient mySmtpClient = new SmtpClient(mySMTP);
            mySmtpClient.UseDefaultCredentials = false;
            System.Net.NetworkCredential basicAuthenticationInfo = new
               System.Net.NetworkCredential("noreply1@szabist.edu.pk", "Szab!00#90");
            mySmtpClient.Credentials = basicAuthenticationInfo;
            MailAddress from = new MailAddress("noreply1@szabist.edu.pk", "SZABIST");
            MailAddress to = new MailAddress(ToEmail);
            MailMessage myMail = new System.Net.Mail.MailMessage(from, to);
            MailAddress replyto = new MailAddress("noreply1@szabist.edu.pk");
            myMail.Subject = Subject;
            myMail.SubjectEncoding = System.Text.Encoding.UTF8;
            myMail.Body = Body.Replace("\r\n", "<br />");
            myMail.BodyEncoding = System.Text.Encoding.UTF8;
            myMail.IsBodyHtml = true;
            mySmtpClient.Send(myMail);           
    }

    public void RunANonQry(string qry) 
    {
        SqlConnection con = new SqlConnection(constr);
        SqlCommand cmd = new SqlCommand(qry,con);
        if (con.State != ConnectionState.Open) 
        {
            con.Open();
        }
        cmd.ExecuteNonQuery();       
        if (con.State != ConnectionState.Closed)
        {
            con.Close();
        }
    }
    public DataTable RunAQry(string qry)

    {
        SqlConnection con = new SqlConnection(constr);
        SqlCommand cmd = new SqlCommand(qry,con);
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        if (con.State != ConnectionState.Open)
        {
            con.Open();
        }
        DataTable dt = new  DataTable();
        adpt.Fill(dt);
        if (con.State != ConnectionState.Closed)
        {
            con.Close();
        }
        return dt;
    }

    public DataTable Execute_SP(string p_SP_Name, SqlCommand p_cmd = null)
    {
        SqlConnection con = new SqlConnection(constr);
        SqlCommand cmd;
        if (p_cmd == null)
            cmd = new SqlCommand();
        else
            cmd = p_cmd;
        cmd.CommandText = p_SP_Name;
        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        if (con.State != ConnectionState.Open)
            con.Open();
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        if (con.State != ConnectionState.Closed)
            con.Close();

        return dt;
    }

}