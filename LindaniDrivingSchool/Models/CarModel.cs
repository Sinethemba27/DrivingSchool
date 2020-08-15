using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LindaniDrivingSchool.Models
{
    public class CarModel
    {
        [Key]
        public int CarModelId { get; set; }
        [Display(Name = "Model Name")]
        public string CarModelType { get; set; }
    }
}