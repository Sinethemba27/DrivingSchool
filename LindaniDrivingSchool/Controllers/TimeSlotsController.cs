using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LindaniDrivingSchool.Logic;
using LindaniDrivingSchool.Models;

namespace LindaniDrivingSchool.Controllers
{
    public class TimeSlotsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TimeSlots
        public ActionResult Index()
        {
            return View(db.TimeSlots.ToList());
        }

        // GET: TimeSlots/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeSlots timeSlots = db.TimeSlots.Find(id);
            if (timeSlots == null)
            {
                return HttpNotFound();
            }
            return View(timeSlots);
        }

        // GET: TimeSlots/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TimeSlots/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TimeSlotId,SlotTime")] TimeSlots timeSlots)
        {
            if (ModelState.IsValid)
            {
                if (BookingLogic.CheckITimeSlot(timeSlots) == false)
                {
                    // TODO Check if time slot exists
                    db.TimeSlots.Add(timeSlots);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "You can not add the same slot more than once");
                    return View(timeSlots);
                }
               
            }

            return View(timeSlots);
        }

        // GET: TimeSlots/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeSlots timeSlots = db.TimeSlots.Find(id);
            if (timeSlots == null)
            {
                return HttpNotFound();
            }
            return View(timeSlots);
        }

        // POST: TimeSlots/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TimeSlotId,SlotTime")] TimeSlots timeSlots)
        {
            if (ModelState.IsValid)
            {
                db.Entry(timeSlots).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(timeSlots);
        }

        // GET: TimeSlots/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeSlots timeSlots = db.TimeSlots.Find(id);
            if (timeSlots == null)
            {
                return HttpNotFound();
            }
            return View(timeSlots);
        }

        // POST: TimeSlots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TimeSlots timeSlots = db.TimeSlots.Find(id);
            db.TimeSlots.Remove(timeSlots);
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
