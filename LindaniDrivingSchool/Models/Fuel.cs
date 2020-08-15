using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LindaniDrivingSchool.Models
{
    public class Fuel
    {
        [Key]
        public int FuelID { get; set; }
        [Display(Name = "Fuel Type")]
        public string FuelType { get; set; }
    }
}