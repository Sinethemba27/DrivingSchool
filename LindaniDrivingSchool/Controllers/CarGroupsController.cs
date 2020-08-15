using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LindaniDrivingSchool.Models;

namespace LindaniDrivingSchool.Controllers
{
    public class CarGroupsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CarGroups
        public ActionResult Index()
        {
            return View(db.CarGroups.ToList());
        }

        // GET: CarGroups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarGroup carGroup = db.CarGroups.Find(id);
            if (carGroup == null)
            {
                return HttpNotFound();
            }
            return View(carGroup);
        }

        // GET: CarGroups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CarGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CarGroupId,CarGroupType")] CarGroup carGroup)
        {
            if (ModelState.IsValid)
            {
                db.CarGroups.Add(carGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(carGroup);
        }

        // GET: CarGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarGroup carGroup = db.CarGroups.Find(id);
            if (carGroup == null)
            {
                return HttpNotFound();
            }
            return View(carGroup);
        }

        // POST: CarGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CarGroupId,CarGroupType")] CarGroup carGroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(carGroup);
        }

        // GET: CarGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarGroup carGroup = db.CarGroups.Find(id);
            if (carGroup == null)
            {
                return HttpNotFound();
            }
            return View(carGroup);
        }

        // POST: CarGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CarGroup carGroup = db.CarGroups.Find(id);
            db.CarGroups.Remove(carGroup);
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
