using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LindaniDrivingSchool.Models
{
    public class Car
    {
        [Key]
        public int CarId { get; set; }
        [Display(Name = "Car Model")]
        public int CarModelId { get; set; }
        public virtual CarModel carModel { get; set; }
        [Display(Name = "Car Make")]
        public int CarMakeId { get; set; }
        public virtual CarMake carMake  { get; set; }
        [Display(Name = "Car Transmission")]
        public int TransmissionId { get; set; }
        public virtual Transmission transmission { get; set; }
        [Display(Name = "Car Fuel")]
        public int FuelID { get; set; }
        public virtual Fuel fuel { get; set; }
        [Display(Name = "Car Insurance")]
        public int InsuranceId { get; set; }
        public virtual Insurance insurance { get; set; }
        [Display(Name = "Category Type")]
        public int CarGroupId { get; set; }
        public CarGroup carGroup { get; set; }
        [Display(Name = "Cost Per Day"), DataType(DataType.Currency)]
        public decimal Cost_Per_Day { get; set; }
        [Display(Name = "Kilometer Rate")]
        public decimal KilometerRate { get; set; }
        [Display(Name = "Free Kilometers")]
        public decimal freeKiloMeters { get; set; }
        [Display(Name = "Current Milage")]
        public int CurrentMilage { get; set; }
        public int numTimesBooked { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public string ImageType { get; set; }
    }
}