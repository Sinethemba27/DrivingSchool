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
    public class CarHiringsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CarHirings
        public ActionResult Index()
        {
            var carHirings = db.CarHirings.Include(c => c.car);
            return View(carHirings.ToList());
        }

        // GET: CarHirings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarHiring carHiring = db.CarHirings.Find(id);
            if (carHiring == null)
            {
                return HttpNotFound();
            }
            return View(carHiring);
        }

        // GET: CarHirings/Create
        public ActionResult Create()
        {
            ViewBag.CarId = new SelectList(db.Cars, "CarId", "Description");
            return View();
        }

        // POST: CarHirings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookingId,userId,PickUpDate,ReturnDate,Percentage,BasicPrice,price,ReturnId,Booking_Cost,status,CarId,MilageIn,MilageOut")] CarHiring carHiring)
        {
            if (ModelState.IsValid)
            {
                db.CarHirings.Add(carHiring);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CarId = new SelectList(db.Cars, "CarId", "Description", carHiring.CarId);
            return View(carHiring);
        }

        // GET: CarHirings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarHiring carHiring = db.CarHirings.Find(id);
            if (carHiring == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarId = new SelectList(db.Cars, "CarId", "Description", carHiring.CarId);
            return View(carHiring);
        }

        // POST: CarHirings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookingId,userId,PickUpDate,ReturnDate,Percentage,BasicPrice,price,ReturnId,Booking_Cost,status,CarId,MilageIn,MilageOut")] CarHiring carHiring)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carHiring).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CarId = new SelectList(db.Cars, "CarId", "Description", carHiring.CarId);
            return View(carHiring);
        }

        // GET: CarHirings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarHiring carHiring = db.CarHirings.Find(id);
            if (carHiring == null)
            {
                return HttpNotFound();
            }
            return View(carHiring);
        }

        // POST: CarHirings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CarHiring carHiring = db.CarHirings.Find(id);
            db.CarHirings.Remove(carHiring);
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
