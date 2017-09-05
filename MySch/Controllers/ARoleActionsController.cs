using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MySch.Dal;
using MySch.Models;

namespace MySch.Controllers
{
    public class ARoleActionsController : Controller
    {
        private BaseContext db = new BaseContext();

        // GET: ARoleActions
        public ActionResult Index()
        {
            return View(db.ARoleActions.ToList());
        }

        // GET: ARoleActions/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ARoleAction aRoleAction = db.ARoleActions.Find(id);
            if (aRoleAction == null)
            {
                return HttpNotFound();
            }
            return View(aRoleAction);
        }

        // GET: ARoleActions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ARoleActions/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,IDS,Name,IsMenu,RoleTypeIDS")] ARoleAction aRoleAction)
        {
            if (ModelState.IsValid)
            {
                db.ARoleActions.Add(aRoleAction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aRoleAction);
        }

        // GET: ARoleActions/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ARoleAction aRoleAction = db.ARoleActions.Find(id);
            if (aRoleAction == null)
            {
                return HttpNotFound();
            }
            return View(aRoleAction);
        }

        // POST: ARoleActions/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,IDS,Name,IsMenu,RoleTypeIDS")] ARoleAction aRoleAction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aRoleAction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aRoleAction);
        }

        // GET: ARoleActions/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ARoleAction aRoleAction = db.ARoleActions.Find(id);
            if (aRoleAction == null)
            {
                return HttpNotFound();
            }
            return View(aRoleAction);
        }

        // POST: ARoleActions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ARoleAction aRoleAction = db.ARoleActions.Find(id);
            db.ARoleActions.Remove(aRoleAction);
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
