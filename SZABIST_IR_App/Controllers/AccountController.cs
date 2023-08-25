using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SZABIST_IR_App.Models;

namespace SZABIST_IR_App.Controllers
{
    public class AccountController : Controller
    {
        AEAuditDBEntities db;
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public JsonResult ValidateUser(string userid, string password)
        {
            using (db = new AEAuditDBEntities())
            {
                int RoleId = 0;
                string uID = "";
                string uFullName = "";
                string uDesignation = "";
                byte Selected_cID = 1;
                bool userAvailable = false;
                bool flag = false;
                try
                {
                    var connection_string = db.tblCampus.FirstOrDefault(c => c.tCampus_Id == Selected_cID);
                    var connection = ConfigurationManager.ConnectionStrings[connection_string.dbConnectionString].ToString();
                    SqlConnection con = new SqlConnection(connection);
                    SqlCommand cmd = new SqlCommand("spVerifyUserUsingUserId", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userid);
                    cmd.Parameters.AddWithValue("@Password", password);
                    con.Open();
                    SqlDataReader idr = cmd.ExecuteReader();
                    if (idr.HasRows)
                    {
                        //while (idr.Read())
                        //{
                        //    uID = idr["sUser_Id"].ToString();
                        //    uFullName = idr["sUserProfile_FirstName"] + " " + idr["sUserProfile_MiddleName"] + " " + idr["sUserProfile_LastName"];
                        //    uDesignation = idr["sUserProfile_Designation"].ToString();
                        //}
                        userAvailable = true;
                        con.Close();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                if (userAvailable == true)
                {
                    var isUserExists = db.tblUsers.FirstOrDefault(u=>u.UserID==userid && u.Status==true);
                    if (isUserExists != null)
                    {
                        Session["userID"] = isUserExists.UserID;
                        Session["userName"] = isUserExists.Name;
                        Session["roleID"] = isUserExists.RoleID;
                        Session["departID"] = isUserExists.DepartID;
                        RoleId =Convert.ToInt16(isUserExists.RoleID);
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                }
                else
                {
                    Session.Clear();
                    Session.Abandon();
                    flag = false;
                    //ViewBag.msg = "Invalid UserName / Password";
                    //ViewBag.campus = new SelectList(Pub_db.tbl_Campus.ToList(), "tCampus_Id", "sCampus_ShortDesc");
                    //ViewBag.campus = new SelectList(Pub_db.tbl_Campus.ToList().OrderBy(x => x.sCampus_ShortDesc), "tCampus_Id", "sCampus_ShortDesc");
                    
                }
                return Json(new { Success = flag,Role= RoleId}, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Logout()
        {
            Session["userID"] = string.Empty;
            FormsAuthentication.SignOut();
            Session.Remove(Session["userID"].ToString());
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();

            return RedirectToAction("Index");
        }
    }
}