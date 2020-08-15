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
using Microsoft.AspNet.Identity;

namespace LindaniDrivingSchool.Controllers
{
    public class BookingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Bookings
        public ActionResult Index()
        {
            var bookings = db.Bookings.Include(b => b.BookingPackage).Include(b => b.TimeSlots);
            return View(bookings.ToList());
        }

        // GET: Bookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: Bookings/Create
        public ActionResult Create(int? id)
        {
            Session["PackageId"] = id;
            //ViewBag.BokingPackageId = new SelectList(db.BookingPackages, "BokingPackageId", "Descrition");
            ViewBag.TimeSlotId = new SelectList(db.TimeSlots, "TimeSlotId", "SlotTime");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookingId,Customer_Email,BokingPackageId,TimeSlotId,DateBooked,DateBookingFor,Price,Status")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                if (BookingLogic.CheckBooking(booking) == false)
                {
                    if (BookingLogic.CheckDate(booking.DateBookingFor) == false)
                    {
                        booking.BokingPackageId = int.Parse(Session["PackageId"].ToString());
                        booking.DateBooked = DateTime.Now.Date;
                        booking.Customer_Email = User.Identity.GetUserName();
                        db.Bookings.Add(booking);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "You can not pick a date thata has already passed or booking for toda");
                        ViewBag.TimeSlotId = new SelectList(db.TimeSlots, "TimeSlotId", "SlotTime", booking.TimeSlotId);
                        return View(booking);
                    }
                   
                }
                else
                {
                    ModelState.AddModelError("", "Time Slot Is Already Taken For That Dat Please Select Another Time");
                    ViewBag.TimeSlotId = new SelectList(db.TimeSlots, "TimeSlotId", "SlotTime", booking.TimeSlotId);
                    return View(booking);

                }

            }

            //ViewBag.BokingPackageId = new SelectList(db.BookingPackages, "BokingPackageId", "Descrition", booking.BokingPackageId);
            ViewBag.TimeSlotId = new SelectList(db.TimeSlots, "TimeSlotId", "SlotTime", booking.TimeSlotId);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.BokingPackageId = new SelectList(db.BookingPackages, "BokingPackageId", "Descrition", booking.BokingPackageId);
            ViewBag.TimeSlotId = new SelectList(db.TimeSlots, "TimeSlotId", "SlotTime", booking.TimeSlotId);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookingId,Customer_Email,BokingPackageId,TimeSlotId,DateBooked,DateBookingFor,Price,Status")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                if (BookingLogic.CheckDate(booking.DateBookingFor) == false)
                {
                    booking.BokingPackageId = int.Parse(Session["PackageId"].ToString());
                    booking.DateBooked = DateTime.Now.Date;
                    booking.Customer_Email = User.Identity.GetUserName();
                    db.Entry(booking).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.BokingPackageId = new SelectList(db.BookingPackages, "BokingPackageId", "Descrition", booking.BokingPackageId);
                    ViewBag.TimeSlotId = new SelectList(db.TimeSlots, "TimeSlotId", "SlotTime", booking.TimeSlotId);
                    return View(booking);
                }
              
            }
            ViewBag.BokingPackageId = new SelectList(db.BookingPackages, "BokingPackageId", "Descrition", booking.BokingPackageId);
            ViewBag.TimeSlotId = new SelectList(db.TimeSlots, "TimeSlotId", "SlotTime", booking.TimeSlotId);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
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
