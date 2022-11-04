using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SZABIST_IR_App.Models;

namespace SZABIST_IR_App.Controllers
{
    public class ClusterSetupController : Controller
    {
        AEAuditDBEntities db;
        ProjectClass p;
        // GET: ClusterSetup
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["userID"])))
                return RedirectToAction("Index", "Account");

            using (db = new AEAuditDBEntities())
            {
                List<tblClusterHead> heads = db.tblClusterHeads.Where(c => c.Status == true).ToList();
                //List<tblHeadDetail> headDetails = db.tblHeadDetails.ToList();
                //var clusterList = (from c in heads
                //                   join cd in headDetails on c.tCluster_ID equals cd.tCluster_ID
                //                   select new Cluster_ClusterDetailsVM
                //                   {
                //                       head = c,
                //                       headDetail = cd
                //                   });
                return View(heads);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Index(ClusterHeadSetupVM NewCluster)
        {
            return View();
        }

        public ActionResult CreateCluster()
        {
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(Session["userID"])))
                    return RedirectToAction("Index", "Account");

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
                    var users = roleMemeber.OrderBy(x => x.Full_Name);
                    ViewBag.clusterHead = new SelectList(users, "sUser_Id", "Full_Name");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCluster(ClusterHeadSetupVM NewCluster)
        {
            try
            {
                using (db = new AEAuditDBEntities())
                {
                    p = new ProjectClass();
                    var ifExists = db.tblClusterMembers.FirstOrDefault(c => c.ClusterMemberID == NewCluster.ClusterHead);
                    var FullName = "";
                    var userId = Session["userID"].ToString();
                    if (ifExists == null)
                    {
                        var connection_string = db.tblCampus.FirstOrDefault(c => c.tCampus_Id == 1).dbConnectionString;
                        var connection = ConfigurationManager.ConnectionStrings[connection_string].ToString();
                        SqlConnection xcon = new SqlConnection(connection);
                        SqlCommand xcmd = new SqlCommand("Select * from vUserRoleMember where sUser_Id='" + NewCluster.ClusterHead + "'", xcon);
                        xcmd.CommandType = CommandType.Text;
                        xcon.Open();
                        SqlDataReader xdr = xcmd.ExecuteReader();
                        if (xdr.HasRows)
                        {
                            while (xdr.Read())
                            {
                                FullName = xdr["Full_Name"].ToString();
                            }
                        }
                        //var ClusterHead = db.vUserRoleMembers.FirstOrDefault(h => h.sUser_Id == NewCluster.ClusterHead);
                        tblClusterInfo tblCluster = new tblClusterInfo()
                        {
                            ClusterTitle = NewCluster.ClusterTitle,
                            ClusterDescription = NewCluster.ClusterDescription,
                            CreatedBy = userId,
                            CreatedDate = p.currentDateTime(),
                            Status=true,
                        };
                        db.tblClusterInfoes.Add(tblCluster);
                        db.SaveChanges();

                    tblClusterMember clusterMember = new tblClusterMember() {
                        tCluster_ID = tblCluster.tCluster_ID,
                        ClusterMemberID = NewCluster.ClusterMemberID,
                        ClusterMemberName = NewCluster.ClusterMemberName,
                        CreatedBy = userId,
                        CreatedDate = p.currentDateTime(),
                           Status=true
                        };
                        db.tblClusterMembers.Add(clusterMember);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.error = "Cluster";
                    }
                }
            }
            catch (Exception ex)
            {

                string exc = "Exception" + ex.InnerException.Message;
            }

            return View();
        }


        public ActionResult EditCluster(int? id)
        {
            using (db = new AEAuditDBEntities())
            {
                try
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    var clusters = db.tblClusterHeads.FirstOrDefault(x => x.tCluster_Head_Id == id);
                    if (clusters == null)
                    {
                        return HttpNotFound();
                    }
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
                    var users = roleMemeber.OrderBy(x => x.Full_Name);
                    ViewBag.clusterHead = new SelectList(users, "sUser_Id", "Full_Name");
                    return View(clusters);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCluster(tblClusterHead EditCluster)
        {
            try
            {
                using (db = new AEAuditDBEntities())
                {
                    if (ModelState.IsValid)
                    {
                        p = new ProjectClass();
                        var userId = Session["userID"].ToString();
                        var HeadDetails = db.tblClusterHeads.Find(EditCluster.tCluster_Head_Id);
                        if (HeadDetails != null)
                        {
                            HeadDetails.tCluster_ID = EditCluster.tCluster_ID;
                            //HeadDetails.tMemberID = EditCluster.tMemberID;
                            HeadDetails.ClusterHeadName = EditCluster.ClusterHeadName;
                            HeadDetails.WEF_From = EditCluster.WEF_From;
                            HeadDetails.WEF_To = EditCluster.WEF_To;
                            HeadDetails.ModifiedBy = userId;
                            HeadDetails.ModifiedDate = p.currentDateTime();
                            db.Entry(HeadDetails).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        return RedirectToAction("Index");
                    }
                    return View(EditCluster);
                }
            }
            catch (Exception ex)
            {
                //nothing to do ...just enjoy with exception...
                string exp = ex.Message;
                return View(EditCluster);
            }
        }
    }
}