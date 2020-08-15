using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using LindaniDrivingSchool.Logic;
using LindaniDrivingSchool.Models;
using Microsoft.AspNet.Identity;
using PayFast;
using PayFast.AspNet;

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
        public ActionResult MyHirings()
        {
            var userName = User.Identity.GetUserName();
            var carHirings = db.CarHirings.Include(c => c.car);
            return View(carHirings.ToList().Where(x=>x.userId == userName));
        }
        public ActionResult ConfrimHire(int? id)
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
        public ActionResult ConfirmPickUp(int? id)
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
        public ActionResult ConfirmCollection(int? id)
        {
            CarHiring carHiring = db.CarHirings.Find(id);
            carHiring.status = "Picked Up";
            db.Entry(carHiring).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult ReturnVehicle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarHiring carHiring = db.CarHirings.Find(id);
            ViewBag.pick = carHiring.PickUpDate.Date;
            ViewBag.retur = carHiring.ReturnDate.Date;
            ViewBag.userID = carHiring.userId;
            ViewBag.status = carHiring.status;
            ViewBag.bookID = carHiring.BookingId;
            ViewBag.Deposit = carHiring.Deposit;
            ViewBag.BasicPrice = carHiring.BasicPrice;
            ViewBag.numOfDays = carHiring.numOfDays;
            ViewBag.Percentage = carHiring.Percentage;
            ViewBag.CustomerName = carHiring.CustomerName;
            ViewBag.CustomerSurname = carHiring.CustomerSurname;

            if (carHiring == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarId = new SelectList(db.Cars, "CarId", "Description", carHiring.CarId);
            return View(carHiring);
        }
        public ActionResult AvailableCar(int? id)
        {
            CarHiring carHiring = db.CarHirings.Find(id);
            carHiring.status = "Complete";
            carHiring.Booking_Cost = logic.calcGrandTotal(carHiring);
            Car car = db.Cars.ToList().Find(x => x.CarId == carHiring.CarId);
            car.Status = "Available";
            db.Entry(carHiring).State = EntityState.Modified;
            db.Entry(car).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Cars");
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
        public ActionResult Create(int? id)
        {
            ViewBag.Id = id;
            ViewBag.CarId = new SelectList(db.Cars, "CarId", "Description");
            return View();
        }
        BusinessLogic logic =new  BusinessLogic();
        // POST: CarHirings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookingId,userId,PickUpDate,ReturnDate,Percentage,BasicPrice,price,ReturnId,Booking_Cost,status,CarId,MilageIn,MilageOut")] CarHiring carHiring)
        {
            var userName = User.Identity.GetUserName();
            carHiring.userId = userName;
            var customers = db.Customers.Where(p => p.Email == userName).FirstOrDefault();
            if (ModelState.IsValid)
            {
                carHiring.CustomerName = customers.FirstName;
                carHiring.CustomerSurname = customers.LastName;
                carHiring.MilageOut = carHiring.car.CurrentMilage;
                carHiring.numOfDays = Convert.ToInt32(logic.CalcNum_of_Days(carHiring));
                carHiring.BasicPrice = logic.calcBasicCharge(carHiring);
                carHiring.Deposit = carHiring.calcDeposite();
                carHiring.Percentage = logic.calcPercentage(carHiring);
                db.CarHirings.Add(carHiring);
                db.SaveChanges();
                return RedirectToAction("ConfrimHire", "CarHirings", new { id = carHiring.BookingId });
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
                return RedirectToAction("ReturnVehicle", "CarHirings", new { id = carHiring.BookingId });
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
        public CarHiringsController()
        {
            this.payFastSettings = new PayFastSettings();
            this.payFastSettings.MerchantId = ConfigurationManager.AppSettings["MerchantId"];
            this.payFastSettings.MerchantKey = ConfigurationManager.AppSettings["MerchantKey"];
            this.payFastSettings.PassPhrase = ConfigurationManager.AppSettings["PassPhrase"];
            this.payFastSettings.ProcessUrl = ConfigurationManager.AppSettings["ProcessUrl"];
            this.payFastSettings.ValidateUrl = ConfigurationManager.AppSettings["ValidateUrl"];
            this.payFastSettings.ReturnUrl = ConfigurationManager.AppSettings["ReturnUrl"];
            this.payFastSettings.CancelUrl = ConfigurationManager.AppSettings["CancelUrl"];
            this.payFastSettings.NotifyUrl = ConfigurationManager.AppSettings["NotifyUrl"];
        }
        //Payment
        #region Fields

        private readonly PayFastSettings payFastSettings;

        #endregion Fields

        #region Constructor

        //public ApprovedOwnersController()
        //{

        //}

        #endregion Constructor

        #region Methods



        public ActionResult Recurring()
        {
            var recurringRequest = new PayFastRequest(this.payFastSettings.PassPhrase);

            // Merchant Details
            recurringRequest.merchant_id = this.payFastSettings.MerchantId;
            recurringRequest.merchant_key = this.payFastSettings.MerchantKey;
            recurringRequest.return_url = this.payFastSettings.ReturnUrl;
            recurringRequest.cancel_url = this.payFastSettings.CancelUrl;
            recurringRequest.notify_url = this.payFastSettings.NotifyUrl;

            // Buyer Details
            recurringRequest.email_address = "sbtu01@payfast.co.za";

            // Transaction Details
            recurringRequest.m_payment_id = "8d00bf49-e979-4004-228c-08d452b86380";
            recurringRequest.amount = 20;
            recurringRequest.item_name = "Recurring Option";
            recurringRequest.item_description = "Some details about the recurring option";

            // Transaction Options
            recurringRequest.email_confirmation = true;
            recurringRequest.confirmation_address = "drnendwandwe@gmail.com";

            // Recurring Billing Details
            recurringRequest.subscription_type = SubscriptionType.Subscription;
            recurringRequest.billing_date = DateTime.Now;
            recurringRequest.recurring_amount = 20;
            recurringRequest.frequency = BillingFrequency.Monthly;
            recurringRequest.cycles = 0;

            var redirectUrl = $"{this.payFastSettings.ProcessUrl}{recurringRequest.ToString()}";

            return Redirect(redirectUrl);
        }
        //public ActionResult pay(int? id)
        //{
        //    StudentApplication studentApplication = db.Studentapplications.Find(id);
        //    var priceId = db.ClassFees.Where(p => p.ClassNameId == studentApplication.ClassNameId).Select(p => p.FeeTypeId).FirstOrDefault();
        //    var price = db.FeeTypes.Where(p => p.Id == priceId).Select(p => p.FeeAmount).FirstOrDefault();
        //    studentApplication.Status = "Paid";
        //    db.Entry(studentApplication).State = EntityState.Modified;

        //    // db.Studentapplications.Add(studentApplication);
        //    db.SaveChanges();
        //    return RedirectToAction("Index2");

        //}
        public ActionResult OnceOff(int? id)
        {
            var onceOffRequest = new PayFastRequest(this.payFastSettings.PassPhrase);

            // Merchant Details
            onceOffRequest.merchant_id = this.payFastSettings.MerchantId;
            onceOffRequest.merchant_key = this.payFastSettings.MerchantKey;
            onceOffRequest.return_url = this.payFastSettings.ReturnUrl;
            onceOffRequest.cancel_url = this.payFastSettings.CancelUrl;
            onceOffRequest.notify_url = this.payFastSettings.NotifyUrl;

            // Buyer Details

            onceOffRequest.email_address = "sbtu01@payfast.co.za";
            //onceOffRequest.email_address = "sbtu01@payfast.co.za";

            // Transaction Details
            CarHiring carHiring = db.CarHirings.Find(id);
            carHiring.status = "Paid";
            db.Entry(carHiring).State = EntityState.Modified;
            db.SaveChanges();

            Car car = db.Cars.ToList().Find(x => x.CarId == carHiring.CarId);
            car.Status = "Not Available";
            db.Entry(car).State = EntityState.Modified;
            db.SaveChanges();

            onceOffRequest.m_payment_id = "";
            onceOffRequest.amount = Convert.ToDouble(carHiring.Deposit);
            onceOffRequest.item_name = "Car Hire Payment";
            onceOffRequest.item_description = "Some details about the once off payment";
            var userName = User.Identity.GetUserName();

            var mailTo = new List<MailAddress>();
            mailTo.Add(new MailAddress(userName, carHiring.CustomerName));
            var body = $"Dear {carHiring.CustomerName} {carHiring.CustomerSurname} <br/><br/>" +
                $"Your car hiring was successful, please see details below: <br/><br/>" +
                $"Hire Date:{System.DateTime.Now.Date} \n" +
                $"Pick-Up Date: {carHiring.PickUpDate}\n" +
                $"Return-Date: {carHiring.ReturnDate}\n" +
                $"Number Of Days: {carHiring.numOfDays}\n" +
                $"Deposit: {carHiring.Deposit}\n" +
                $"Daily Cost: {carHiring.car.Cost_Per_Day}\n" +
                $"Vehicle: {carHiring.car.carMake.CarMakeType} {carHiring.car.carModel.CarModelType}\n" +
                $"Transmission: {carHiring.car.transmission.TransmissionType}\n" +
                $"Fuel: {carHiring.car.fuel.FuelType}<br/>" +
                $"Regards,<br/><br/> Bright Ideas <br/> .";

            LindaniDrivingSchool.Logic.EmailService emailService = new LindaniDrivingSchool.Logic.EmailService();
            emailService.SendEmail(new EmailContent()
            {
                mailTo = mailTo,
                mailCc = new List<MailAddress>(),
                mailSubject = "Application Statement | Ref No.:" + carHiring.BookingId,
                mailBody = body,
                mailFooter = "<br/> Many Thanks, <br/> <b>Bright Ideas</b>",
                mailPriority = MailPriority.High,
                mailAttachments = new List<Attachment>()

            });
            // Transaction Options
            onceOffRequest.email_confirmation = true;
            onceOffRequest.confirmation_address = "sbtu01@payfast.co.za";

            var redirectUrl = $"{this.payFastSettings.ProcessUrl}{onceOffRequest.ToString()}";
            return Redirect(redirectUrl);
        }

        public ActionResult AdHoc()
        {
            var adHocRequest = new PayFastRequest(this.payFastSettings.PassPhrase);

            // Merchant Details
            adHocRequest.merchant_id = this.payFastSettings.MerchantId;
            adHocRequest.merchant_key = this.payFastSettings.MerchantKey;
            adHocRequest.return_url = this.payFastSettings.ReturnUrl;
            adHocRequest.cancel_url = this.payFastSettings.CancelUrl;
            adHocRequest.notify_url = this.payFastSettings.NotifyUrl;

            // Buyer Details
            adHocRequest.email_address = "sbtu01@payfast.co.za";

            // Transaction Details
            adHocRequest.m_payment_id = "";
            adHocRequest.amount = 70;
            adHocRequest.item_name = "Adhoc Agreement";
            adHocRequest.item_description = "Some details about the adhoc agreement";

            // Transaction Options
            adHocRequest.email_confirmation = true;
            adHocRequest.confirmation_address = "sbtu01@payfast.co.za";

            // Recurring Billing Details
            adHocRequest.subscription_type = SubscriptionType.AdHoc;

            var redirectUrl = $"{this.payFastSettings.ProcessUrl}{adHocRequest.ToString()}";

            return Redirect(redirectUrl);
        }

        public ActionResult Return()
        {
            return View();
        }

        public ActionResult Cancel()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Notify([ModelBinder(typeof(PayFastNotifyModelBinder))] PayFastNotify payFastNotifyViewModel)
        {
            payFastNotifyViewModel.SetPassPhrase(this.payFastSettings.PassPhrase);

            var calculatedSignature = payFastNotifyViewModel.GetCalculatedSignature();

            var isValid = payFastNotifyViewModel.signature == calculatedSignature;

            System.Diagnostics.Debug.WriteLine($"Signature Validation Result: {isValid}");

            // The PayFast Validator is still under developement
            // Its not recommended to rely on this for production use cases
            var payfastValidator = new PayFastValidator(this.payFastSettings, payFastNotifyViewModel, IPAddress.Parse(this.HttpContext.Request.UserHostAddress));

            var merchantIdValidationResult = payfastValidator.ValidateMerchantId();

            System.Diagnostics.Debug.WriteLine($"Merchant Id Validation Result: {merchantIdValidationResult}");

            var ipAddressValidationResult = payfastValidator.ValidateSourceIp();

            System.Diagnostics.Debug.WriteLine($"Ip Address Validation Result: {merchantIdValidationResult}");

            // Currently seems that the data validation only works for successful payments
            if (payFastNotifyViewModel.payment_status == PayFastStatics.CompletePaymentConfirmation)
            {
                var dataValidationResult = await payfastValidator.ValidateData();

                System.Diagnostics.Debug.WriteLine($"Data Validation Result: {dataValidationResult}");
            }

            if (payFastNotifyViewModel.payment_status == PayFastStatics.CancelledPaymentConfirmation)
            {
                System.Diagnostics.Debug.WriteLine($"Subscription was cancelled");
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public ActionResult Error()
        {
            return View();
        }

        #endregion Methods
    }
}

