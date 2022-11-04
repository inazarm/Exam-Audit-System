using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using SZABIST_IR_App.Models;

namespace SZABIST_IR_App.Models
{
    public class ProjectClass
    {
        private AEAuditDBEntities db;

        public DateTime currentDateTime()
        {
            using (db = new AEAuditDBEntities())
            {
                DateTime dateTime = db.Database.SqlQuery<DateTime>("Select dbo.funcCurrentDateTime()").FirstOrDefault();
                return dateTime;
            }
        }

        public List<UserRoleMember> getRoleMember()
        {
            using (db = new AEAuditDBEntities())
            {
                List<UserRoleMember> roleMemeber = new List<UserRoleMember>();
                var connection_string = db.tblCampus.FirstOrDefault(c => c.tCampus_Id == 1).dbConnectionString;
                var connection = ConfigurationManager.ConnectionStrings[connection_string].ToString();
                SqlConnection xcon = new SqlConnection(connection);
                SqlCommand xcmd = new SqlCommand("Select * from vUserRoleMember", xcon);
                xcmd.CommandType = CommandType.Text;
                xcon.Open();
                SqlDataReader xdr = xcmd.ExecuteReader();
                if (xdr.HasRows)
                {
                    while (xdr.Read())
                    {
                        roleMemeber.Add(new UserRoleMember
                        {
                            sUser_Id = xdr["sUser_Id"].ToString(),
                            Full_Name = xdr["Full_Name"].ToString()
                        });
                    }
                }
                xcon.Close();
                return roleMemeber;
            }
        }

        //public List<Programs> getProgramList(byte? cID)
        public List<Programs> getProgramList(byte? cID)
        {
            using (db = new AEAuditDBEntities())
            {
                
                var conStringName = db.tblCampus.FirstOrDefault(x => x.tCampus_Id == cID).dbConnectionString;
                try
                {
                        var connection = ConfigurationManager.ConnectionStrings[conStringName].ToString();
                        SqlConnection con = new SqlConnection(connection);
                        SqlCommand cmd = new SqlCommand("SELECT tCampus_Id,tDegree_Id,tProgram_Id,sProgram_LongDesc,sProgram_ShortDesc,iFacultyId FROM Program", con);
                        con.Open();
                        SqlDataReader idr = cmd.ExecuteReader();
                        List<Programs> program = new List<Programs>();
                        if (idr.HasRows)
                        {
                            while (idr.Read())
                            {
                                byte? m_FacultyId = null;
                                if (!string.IsNullOrEmpty(idr["iFacultyId"].ToString()))
                                {
                                    m_FacultyId = Convert.ToByte(idr["iFacultyId"]);
                                }
                                program.Add(new Programs
                                {
                                    tCampus_Id = Convert.ToByte(idr["tCampus_Id"]),
                                    tDegree_Id = Convert.ToByte(idr["tDegree_Id"]),
                                    tProgram_Id = Convert.ToByte(idr["tProgram_Id"]),
                                    iFacultyId = m_FacultyId,
                                    Program_ShortDesc = idr["sProgram_ShortDesc"].ToString(),
                                    Program_LongDesc = idr["sProgram_LongDesc"].ToString()
                                    //programID = idr["tCampus_Id"].ToString() + "-" + idr["tDegree_Id"].ToString() + "-" + idr["tProgram_Id"].ToString(),
                                    //programName = Convert.ToString(idr["sProgram_LongDesc"]) + " - " + Convert.ToString(idr["sProgram_ShortDesc"]),
                                });
                            }
                            con.Close();
                        }
                        var proramList = program.OrderBy(p => p.programName).ToList();
                        return proramList;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public List<FacultyList_Result> getFacultyList(byte? cID)
        {
            using (db = new AEAuditDBEntities())
            {
                var conStringName = db.tblCampus.FirstOrDefault(x => x.tCampus_Id == cID).dbConnectionString;
                try
                {
                    var connection = ConfigurationManager.ConnectionStrings[conStringName].ToString();
                    SqlConnection con = new SqlConnection(connection);
                    SqlCommand cmd = new SqlCommand("SELECT TOP 1000 tFaculty_Id,sFaculty_ShortDesc,sFaculty_LongDesc FROM Faculty", con);
                    con.Open();
                    SqlDataReader idr = cmd.ExecuteReader();
                    List<FacultyList_Result> faculties = new List<FacultyList_Result>();
                    if (idr.HasRows)
                    {
                        while (idr.Read())
                        {
                            faculties.Add(new FacultyList_Result
                            {
                                tFaculty_Id = Convert.ToByte(idr["tFaculty_Id"]),
                                sFaculty_ShortDesc = idr["sFaculty_ShortDesc"].ToString(),
                                sFaculty_LongDesc = idr["sFaculty_LongDesc"].ToString(),
                            });
                        }
                        con.Close();
                    }
                    var facultyList = faculties.OrderBy(f =>f.sFaculty_LongDesc).ToList();
                    return facultyList;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataSet getRecapSheet(int p_tAllocateDetailID, string spName)
        {
            DataSet dset = new DataSet();
            using (db = new AEAuditDBEntities())
            {
                var courseDe = db.tblAllocateCoursesDetails.Find(p_tAllocateDetailID);
                var uID = courseDe.userID.Trim();
                int sID = Convert.ToInt32(courseDe.iSemester_Id);
                int semSecID = Convert.ToInt16(courseDe.iSemesterSection_Id);
                int courseID = Convert.ToInt32(courseDe.Course_Id);
                var conStringName = db.tblCampus.Find(courseDe.tCampus_Id).dbConnectionString;
                var connection = ConfigurationManager.ConnectionStrings[conStringName].ToString();
                SqlConnection con = new SqlConnection(connection);
                SqlCommand command = new SqlCommand(spName, con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@piSemester_Id", SqlDbType.Int).Value = sID;
                command.Parameters.Add("@piSemesterSection_Id", SqlDbType.TinyInt).Value = semSecID;
                command.Parameters.Add("@psUser_Id", SqlDbType.VarChar, 20).Value = uID;
                command.Parameters.Add("@piOfferedCourses_Course_Id", SqlDbType.Int).Value = courseID;
                //command.Parameters.Add("@piRetVal", SqlDbType.Int).Direction = ParameterDirection.Output;
                con.Open();
                dset = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter(command);
                adp.Fill(dset);
            }

            return dset;
        }

    }
}