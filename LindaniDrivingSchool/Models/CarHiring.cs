﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LindaniDrivingSchool.Models
{
    public class CarHiring
    {
        [Key]
        public int BookingId { get; set; }
        public string userId { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd - MM - yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Pick up Date")]
        public DateTime PickUpDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Return Date")]
        public DateTime ReturnDate { get; set; }
        public decimal Percentage { get; set; }
        public decimal BasicPrice { get; set; }
        public double price { get; set; }
        public int ReturnId { get; set; }
        [Display(Name = "Booking Cost")]
        public double Booking_Cost { get; set; }
        public decimal Deposit { get; set; }
        public int numOfDays { get; set; }
        public string status { get; set; } = "Pending";
        public int CarId { get; set; }
        public virtual Car car { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true)]
        public double MilageIn { get; set; }
        public double MilageOut { get; set; }
        ApplicationDbContext db = new ApplicationDbContext();
        public decimal getInsuranceFee(int insureId)
        {
            var inFee = (from c in db.Insurances
                         where c.InsuranceId == insureId
                         select c.InsuranceFee).FirstOrDefault();
            return inFee;
        }
        public decimal calcDeposite(int insureId)
        {
            return BasicPrice + getInsuranceFee(insureId);
        }
    }
}