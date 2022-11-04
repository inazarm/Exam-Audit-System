using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SZABIST_IR_App.Models;

namespace SZABIST_IR_App.Controllers
{
    public class ClusterMembersController : Controller
    {
        private AEAuditDBEntities db = new AEAuditDBEntities();
        private ProjectClass p;

        // GET: ClusterMembers
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["userID"])))
                return RedirectToAction("Index", "Account");

            List<tblClusterInfo> clusterInfos = db.tblClusterInfoes.Where(c => c.Status == true).ToList();
            //List<tblClusterMember> clusterMembers = db.tblClusterMembers.Where(c => c.Status == true).ToList();
            List<tblClusterHead> clusterHeads = db.tblClusterHeads.Where(c => c.Status == true).ToList();
            var clusterDetails = (from ci in clusterInfos
                                      //join cm in clusterMembers on ci.tCluster_ID equals cm.tCluster_ID
                                  join ch in clusterHeads on ci.tCluster_ID equals ch.tCluster_ID
                                  where ch.Status==true
                                  select new CreateClusterHead
                                  {
                                      tCluster_Head_Id = ch.tCluster_Head_Id,
                                      ClusterHeadName = ch.ClusterHeadName,
                                      tCluster_ID = ci.tCluster_ID,
                                      ClusterTitle = ci.ClusterTitle,
                                      WEF_From = Convert.ToDateTime(ch.WEF_From)
                                  });
            return View(clusterDetails.OrderBy(x => x.ClusterTitle));
        }

        // GET: ClusterMembers/Details/5
        public ActionResult Details(byte? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["userID"])))
                return RedirectToAction("Index", "Account");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblClusterMember tblClusterMember = db.tblClusterMembers.Find(id);
            if (tblClusterMember == null)
            {
                return HttpNotFound();
            }
            return View(tblClusterMember);
        }

        // GET: ClusterMembers/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["userID"])))
                return RedirectToAction("Index", "Account");

            p = new ProjectClass();
            using (db = new AEAuditDBEntities())
            {
                try
                {
                    var roleMemeber = p.getRoleMember();
                    //Show Userslist into DropdownList...
                    var users = roleMemeber.OrderBy(x => x.Full_Name);
                    ViewBag.clusterHead = new SelectList(users.OrderBy(x => x.Full_Name), "sUser_Id", "Full_Name");
                    //Display Cluster List into DropdownList
                    var clusters = db.tblClusterInfoes.Where(x => x.Status == true).ToList().OrderBy(x => x.ClusterTitle);
                    ViewBag.clustersTitle = new SelectList(clusters, "tCluster_ID", "ClusterTitle");
                    //Display Campus Name
                    var campus = db.tblCampus.ToList();
                    ViewBag.campus = new SelectList(campus, "tCampus_Id", "sCampus_ShortDesc");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tblClusterHead cHead)
        {
            if (ModelState.IsValid)
            {
                var userID = Session["userID"].ToString();
                p = new ProjectClass();
                using (db = new AEAuditDBEntities())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            int tMemberID = 0;
                            var isExistsInMember = db.tblClusterMembers.FirstOrDefault(m => m.ClusterMemberID == cHead.ClusterHeadID);
                            var isUser = db.tblUsers.FirstOrDefault(u => u.UserID == cHead.ClusterHeadID);
                            if (isUser == null)
                            {
                                tblUser addNew = new tblUser
                                {
                                    UserID = cHead.ClusterHeadID,
                                    Name = cHead.ClusterHeadName,
                                    RoleID = 2,
                                    DepartID = 1,
                                    CampusID = cHead.CampusID,
                                    CreationDate = p.currentDateTime(),
                                    Status = true
                                };
                                db.tblUsers.Add(addNew);
                                db.SaveChanges();
                            }
                            if (isExistsInMember != null)
                            {
                                tMemberID = isExistsInMember.tMemberID;
                            }
                            else
                            {
                                tblClusterMember addNew = new tblClusterMember()
                                {
                                    tCluster_ID = cHead.tCluster_ID,
                                    ClusterMemberID = cHead.ClusterHeadID,
                                    ClusterMemberName = cHead.ClusterHeadName,
                                    CreatedBy = userID,
                                    CreatedDate = p.currentDateTime(),
                                    Status = true
                                };
                                db.tblClusterMembers.Add(addNew);
                                db.SaveChanges();
                                tMemberID = addNew.tMemberID;
                            }
                            var find = db.tblClusterHeads.FirstOrDefault(x => x.tCluster_ID ==cHead.tCluster_ID && x.Status == true);
                            if (find == null)
                            {
                                tblClusterHead addNew = new tblClusterHead()
                                {
                                    tCluster_ID = cHead.tCluster_ID,
                                    tMemberID = Convert.ToByte(tMemberID),
                                    ClusterHeadID = cHead.ClusterHeadID,
                                    ClusterHeadName = cHead.ClusterHeadName,
                                    WEF_From = cHead.WEF_From,
                                    CreatedBy = userID,
                                    CreatedDate = p.currentDateTime(),
                                    CampusID = cHead.CampusID,
                                    Status = true
                                };
                                db.tblClusterHeads.Add(addNew);
                                db.SaveChanges();
                                trans.Commit();
                                return RedirectToAction("Index");
                            }
                            else
                            {
                                try
                                {
                                    var roleMemeber = p.getRoleMember();
                                    var users = roleMemeber.OrderBy(x => x.Full_Name);
                                    ViewBag.clusterHead = new SelectList(users, "sUser_Id", "Full_Name");
                                    var clusters = db.tblClusterInfoes.Where(x => x.Status == true).ToList();
                                    ViewBag.clustersTitle = new SelectList(clusters, "tCluster_ID", "ClusterTitle");
                                    trans.Rollback();
                                    ViewBag.error = "Cluster Already Assigned To Cluster Head!";
                                    return View(cHead);
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    throw ex;
                                }
                               
                            }
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            throw ex;
                        }
                    
                    }
                }
            }

            return View(cHead);
        }

        // GET: ClusterMembers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["userID"])))
                return RedirectToAction("Index", "Account");

            using (db = new AEAuditDBEntities())
            {
                p = new ProjectClass();
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var roleMemeber = p.getRoleMember();
                var users = roleMemeber.OrderBy(x => x.Full_Name);
                ViewBag.clusterHead = new SelectList(users, "sUser_Id", "Full_Name");

                var ch = db.tblClusterHeads.Find(id);
                //var cm = db.tblClusterMembers.Find(ch.tMemberID);
                var ci = db.tblClusterInfoes.Find(ch.tCluster_ID);
                CreateClusterHead Details = new CreateClusterHead()
                {
                    tCluster_ID = ci.tCluster_ID,
                    tMemberID = (byte)ch.tMemberID,
                    ClusterTitle = ci.ClusterTitle,
                    tCluster_Head_Id = ch.tCluster_Head_Id,
                    ClusterHeadID = ch.ClusterHeadID,
                    ClusterHeadName = ch.ClusterHeadName,
                    WEF_From = Convert.ToDateTime(ch.WEF_From),
                    CampusID=ch.CampusID  
                };
                if (Details == null)
                {
                    return HttpNotFound();
                }
                return View(Details);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CreateClusterHead cUpdate)
        {
            using (db = new AEAuditDBEntities())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {
                        if (ModelState.IsValid)
                        {
                            int tMember = 0;
                            p = new ProjectClass();
                            var userID = Session["userID"].ToString();
                            var isMemberExists = db.tblClusterMembers.FirstOrDefault(x => x.tCluster_ID == cUpdate.tCluster_ID && x.ClusterMemberID == cUpdate.ClusterHeadID);
                            var chead = db.tblClusterHeads.Find(cUpdate.tCluster_Head_Id);
                            var isUser = db.tblUsers.FirstOrDefault(u => u.UserID == cUpdate.ClusterHeadID);
                            if (cUpdate.ClusterHeadID == chead.ClusterHeadID)
                            {
                                chead.WEF_From = cUpdate.WEF_From;
                                chead.ModifiedBy = userID.ToString();
                                chead.ModifiedDate = p.currentDateTime();
                                chead.Status = true;
                                db.SaveChanges();
                            }
                            else
                            {
                                if (isMemberExists == null)
                                {
                                    tblClusterMember addNewMember = new tblClusterMember()
                                    {
                                        tCluster_ID = cUpdate.tCluster_ID,
                                        ClusterMemberID = cUpdate.ClusterHeadID,
                                        ClusterMemberName = cUpdate.ClusterHeadName,
                                        CreatedBy = userID,
                                        CreatedDate = p.currentDateTime(),
                                        Status = true
                                    };
                                    db.tblClusterMembers.Add(addNewMember);
                                    db.SaveChanges();
                                    tMember = addNewMember.tMemberID;
                                }
                                else
                                    tMember = isMemberExists.tMemberID;
                                if (chead != null)
                                {
                                    chead.WEF_To = p.currentDateTime();
                                    chead.ModifiedBy = userID.ToString();
                                    chead.ModifiedDate = p.currentDateTime();
                                    chead.Status = false;
                                    db.SaveChanges();

                                    tblClusterHead addNewHead = new tblClusterHead()
                                    {
                                        tCluster_ID = cUpdate.tCluster_ID,
                                        tMemberID = tMember,
                                        ClusterHeadID = cUpdate.ClusterHeadID,
                                        ClusterHeadName = cUpdate.ClusterHeadName,
                                        WEF_From = cUpdate.WEF_From,
                                        CreatedBy = userID,
                                        CampusID = cUpdate.CampusID,
                                        Status = true,
                                        CreatedDate = p.currentDateTime(),
                                    };
                                    db.tblClusterHeads.Add(addNewHead);
                                    if (isUser == null)
                                    {
                                        tblUser addNewUser = new tblUser
                                        {
                                            UserID = cUpdate.ClusterHeadID,
                                            Name = cUpdate.ClusterHeadName,
                                            RoleID = 2,
                                            DepartID = 1,
                                            CampusID = cUpdate.CampusID,
                                            CreationDate = p.currentDateTime(),
                                            Status = true
                                        };
                                        db.tblUsers.Add(addNewUser);
                                    }
                                    db.SaveChanges();
                                }
                            }
                            trans.Commit();
                            return RedirectToAction("Index");
                        }
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        return new HttpStatusCodeResult(401, ex.Message);
                    }
                }

            }
            return View(cUpdate);
        }

        // GET: ClusterMembers/Delete/5
        public ActionResult Delete(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblClusterMember ClusterMember = db.tblClusterMembers.Find(id);
            if (ClusterMember == null)
            {
                return HttpNotFound();
            }
            return View(ClusterMember);
        }

        // POST: ClusterMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            tblClusterMember tblClusterMember = db.tblClusterMembers.Find(id);
            db.tblClusterMembers.Remove(tblClusterMember);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}