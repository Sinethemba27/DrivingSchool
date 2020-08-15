using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LindaniDrivingSchool.Models;

namespace LindaniDrivingSchool.Controllers
{
    public class CarsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cars
        public ActionResult Index()
        {
            var cars = db.Cars.Include(c => c.carGroup).Include(c => c.carMake).Include(c => c.carModel).Include(c => c.fuel).Include(c => c.insurance).Include(c => c.transmission);
            return View(cars.ToList());
        }
        public ActionResult HireCar()
        {
            var cars = db.Cars.Include(c => c.carGroup).Include(c => c.carMake).Include(c => c.carModel).Include(c => c.fuel).Include(c => c.insurance).Include(c => c.transmission);
            return View(cars.ToList());
        }
        // GET: Cars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }
        // Convert file to binary
        public byte[] ConvertToBytes(HttpPostedFileBase file)
        {
            BinaryReader reader = new BinaryReader(file.InputStream);
            return reader.ReadBytes((int)file.ContentLength);
        }

        // Display File
        public FileStreamResult RenderImage(int id)
        {
            MemoryStream ms = null;

            var item = db.Cars.FirstOrDefault(x => x.CarId == id);
            if (item != null)
            {
                ms = new MemoryStream(item.Image);
            }
            return new FileStreamResult(ms, item.ImageType);
        }
        // GET: Cars/Create
        public ActionResult Create()
        {
            ViewBag.CarGroupId = new SelectList(db.CarGroups, "CarGroupId", "CarGroupType");
            ViewBag.CarMakeId = new SelectList(db.CarMakes, "CarMakeId", "CarMakeType");
            ViewBag.CarModelId = new SelectList(db.CarModels, "CarModelId", "CarModelType");
            ViewBag.FuelID = new SelectList(db.Fuels, "FuelID", "FuelType");
            ViewBag.InsuranceId = new SelectList(db.Insurances, "InsuranceId", "InsuranceType");
            ViewBag.TransmissionId = new SelectList(db.Transmissions, "TransmissionId", "TransmissionType");
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CarId,CarModelId,CarMakeId,TransmissionId,FuelID,InsuranceId,CarGroupId,Cost_Per_Day,KilometerRate,freeKiloMeters,CurrentMilage,numTimesBooked,Description,Image,ImageType")] Car car, HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                car.ImageType = Path.GetExtension(file.FileName);
                car.Image = ConvertToBytes(file);
            }
            if (ModelState.IsValid)
            {
                db.Cars.Add(car);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CarGroupId = new SelectList(db.CarGroups, "CarGroupId", "CarGroupType", car.CarGroupId);
            ViewBag.CarMakeId = new SelectList(db.CarMakes, "CarMakeId", "CarMakeType", car.CarMakeId);
            ViewBag.CarModelId = new SelectList(db.CarModels, "CarModelId", "CarModelType", car.CarModelId);
            ViewBag.FuelID = new SelectList(db.Fuels, "FuelID", "FuelType", car.FuelID);
            ViewBag.InsuranceId = new SelectList(db.Insurances, "InsuranceId", "InsuranceType", car.InsuranceId);
            ViewBag.TransmissionId = new SelectList(db.Transmissions, "TransmissionId", "TransmissionType", car.TransmissionId);
            return View(car);
        }

        // GET: Cars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarGroupId = new SelectList(db.CarGroups, "CarGroupId", "CarGroupType", car.CarGroupId);
            ViewBag.CarMakeId = new SelectList(db.CarMakes, "CarMakeId", "CarMakeType", car.CarMakeId);
            ViewBag.CarModelId = new SelectList(db.CarModels, "CarModelId", "CarModelType", car.CarModelId);
            ViewBag.FuelID = new SelectList(db.Fuels, "FuelID", "FuelType", car.FuelID);
            ViewBag.InsuranceId = new SelectList(db.Insurances, "InsuranceId", "InsuranceType", car.InsuranceId);
            ViewBag.TransmissionId = new SelectList(db.Transmissions, "TransmissionId", "TransmissionType", car.TransmissionId);
            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CarId,CarModelId,CarMakeId,TransmissionId,FuelID,InsuranceId,CarGroupId,Cost_Per_Day,KilometerRate,freeKiloMeters,CurrentMilage,numTimesBooked,Description,Image,ImageType")] Car car, HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                car.ImageType = Path.GetExtension(file.FileName);
                car.Image = ConvertToBytes(file);
            }
            if (ModelState.IsValid)
            {
                db.Entry(car).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CarGroupId = new SelectList(db.CarGroups, "CarGroupId", "CarGroupType", car.CarGroupId);
            ViewBag.CarMakeId = new SelectList(db.CarMakes, "CarMakeId", "CarMakeType", car.CarMakeId);
            ViewBag.CarModelId = new SelectList(db.CarModels, "CarModelId", "CarModelType", car.CarModelId);
            ViewBag.FuelID = new SelectList(db.Fuels, "FuelID", "FuelType", car.FuelID);
            ViewBag.InsuranceId = new SelectList(db.Insurances, "InsuranceId", "InsuranceType", car.InsuranceId);
            ViewBag.TransmissionId = new SelectList(db.Transmissions, "TransmissionId", "TransmissionType", car.TransmissionId);
            return View(car);
        }

        // GET: Cars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Car car = db.Cars.Find(id);
            db.Cars.Remove(car);
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
