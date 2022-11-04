using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SZABIST_IR_App.Models;

namespace SZABIST_IR_App.Controllers
{
    public class ClusterInfoController : Controller
    {
        private AEAuditDBEntities db = new AEAuditDBEntities();
        ProjectClass p = new ProjectClass();

        // GET: ClusterInfo
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["userID"])))
                return RedirectToAction("Index", "Account");

            return View(db.tblClusterInfoes.Where(c=>c.Status==true).ToList().OrderBy(x => x.ClusterTitle));
        }

        // GET: ClusterInfo/Details/5
        public ActionResult Details(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblClusterInfo tblClusterInfo = db.tblClusterInfoes.Find(id);
            if (tblClusterInfo == null)
            {
                return HttpNotFound();
            }
            return View(tblClusterInfo);
        }

        // GET: ClusterInfo/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["userID"])))
                return RedirectToAction("Index", "Account");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tblClusterInfo ClusterInfo)
        {
            if (ModelState.IsValid)
            {
               
                ClusterInfo.CreatedBy = Session["userID"].ToString();
                ClusterInfo.CreatedDate =p.currentDateTime();
                ClusterInfo.Status = true;
                db.tblClusterInfoes.Add(ClusterInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ClusterInfo);
        }

        // GET: ClusterInfo/Edit/5
        public ActionResult Edit(byte? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["userID"])))
                return RedirectToAction("Index", "Account");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblClusterInfo tblClusterInfo = db.tblClusterInfoes.Find(id);
            if (tblClusterInfo == null)
            {
                return HttpNotFound();
            }
            return View(tblClusterInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tblClusterInfo ClusterInfo)
        {
            if (ModelState.IsValid)
            {
                ClusterInfo.ModifiedBy = Session["userID"].ToString();
                ClusterInfo.ModifiedDate = p.currentDateTime();
                db.Entry(ClusterInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ClusterInfo);
        }

        // GET: ClusterInfo/Delete/5
        public ActionResult Delete(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblClusterInfo tblClusterInfo = db.tblClusterInfoes.Find(id);
            if (tblClusterInfo == null)
            {
                return HttpNotFound();
            }
            return View(tblClusterInfo);
        }

        // POST: ClusterInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            tblClusterInfo tblClusterInfo = db.tblClusterInfoes.Find(id);
            tblClusterInfo.Status = false;
            tblClusterInfo.ModifiedBy = Session["userID"].ToString();
            tblClusterInfo.ModifiedDate = p.currentDateTime();
            db.Entry(tblClusterInfo).State = EntityState.Modified;
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
