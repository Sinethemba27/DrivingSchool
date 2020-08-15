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
    public class CarMakesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CarMakes
        public ActionResult Index()
        {
            return View(db.CarMakes.ToList());
        }

        // GET: CarMakes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarMake carMake = db.CarMakes.Find(id);
            if (carMake == null)
            {
                return HttpNotFound();
            }
            return View(carMake);
        }
       
        // GET: CarMakes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CarMakes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CarMakeId,CarMakeType,Image,ImageType")] CarMake carMake, HttpPostedFileBase img_upload)
        {
            if (img_upload != null && img_upload.ContentLength > 0)
            {
                carMake.ImageType = Path.GetExtension(img_upload.FileName);
                carMake.Image = ConvertToBytes(img_upload);
            }
            if (ModelState.IsValid)
            {
                db.CarMakes.Add(carMake);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(carMake);
        }

        // GET: CarMakes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarMake carMake = db.CarMakes.Find(id);
            if (carMake == null)
            {
                return HttpNotFound();
            }
            return View(carMake);
        }

        // POST: CarMakes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CarMakeId,CarMakeType,Image,ImageType")] CarMake carMake, HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                carMake.ImageType = Path.GetExtension(file.FileName);
                carMake.Image = ConvertToBytes(file);
            }
            if (ModelState.IsValid)
            {
                db.Entry(carMake).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(carMake);
        }

        // GET: CarMakes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarMake carMake = db.CarMakes.Find(id);
            if (carMake == null)
            {
                return HttpNotFound();
            }
            return View(carMake);
        }

        // POST: CarMakes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CarMake carMake = db.CarMakes.Find(id);
            db.CarMakes.Remove(carMake);
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
        //Image
        // Convert file to binary
        public byte[] ConvertToBytes(HttpPostedFileBase file)
        {
            BinaryReader reader = new BinaryReader(file.InputStream);
            return reader.ReadBytes((int)file.ContentLength);
        }
        //Image
        //Display File
        public FileStreamResult RenderImage(int? id)
        {
            MemoryStream ms = null;

            var item = db.CarMakes.FirstOrDefault(x => x.CarMakeId == id);
            if (item != null)
            {
                ms = new MemoryStream(item.Image);
            }
            return new FileStreamResult(ms, item.ImageType);
        }
    }
}
