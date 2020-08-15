using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LindaniDrivingSchool.Models
{
    public class Transmission
    {
        [Key]
        public int TransmissionId { get; set; }
        [Display(Name = "Transmission Type")]
        public string TransmissionType { get; set; }
    }
}