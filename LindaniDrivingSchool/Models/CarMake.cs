using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LindaniDrivingSchool.Models
{
    public class CarMake
    {
        [Key]
        public int CarMakeId { get; set; }
        [Display(Name = "Car Make")]
        public string CarMakeType { get; set; }
        public byte[] Image { get; set; }
        public string ImageType { get; set; }
    }
}