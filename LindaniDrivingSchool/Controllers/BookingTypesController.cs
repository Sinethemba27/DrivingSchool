﻿using System;
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
    public class BookingTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BookingTypes
        public ActionResult Index()
        {
            return View(db.BookingTypes.ToList());
        }

        public ActionResult PackageView()
        {
            return View(db.BookingTypes.ToList());
        }

            // GET: BookingTypes/Details/5
            public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingType bookingType = db.BookingTypes.Find(id);
            if (bookingType == null)
            {
                return HttpNotFound();
            }
            return View(bookingType);
        }

        // GET: BookingTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BookingTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookingTypeId,BktName,ShortDescription")] BookingType bookingType)
        {
            if (ModelState.IsValid)
            {
                db.BookingTypes.Add(bookingType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bookingType);
        }

        // GET: BookingTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingType bookingType = db.BookingTypes.Find(id);
            if (bookingType == null)
            {
                return HttpNotFound();
            }
            return View(bookingType);
        }

        // POST: BookingTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookingTypeId,BktName,ShortDescription")] BookingType bookingType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookingType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bookingType);
        }

        // GET: BookingTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingType bookingType = db.BookingTypes.Find(id);
            if (bookingType == null)
            {
                return HttpNotFound();
            }
            return View(bookingType);
        }

        // POST: BookingTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BookingType bookingType = db.BookingTypes.Find(id);
            db.BookingTypes.Remove(bookingType);
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
