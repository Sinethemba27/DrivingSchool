using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LindaniDrivingSchool.Models
{
    public class CarGroup
    {
        [Key]
        public int CarGroupId { get; set; }
        [Display(Name = "Car Group")]
        public string CarGroupType { get; set; }
    }
}