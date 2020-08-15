using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LindaniDrivingSchool.Models
{
    public class BookingPackage
    {
        [Key]
        public int BokingPackageId { get; set; }
        public int BookingTypeId { get; set; }
        public virtual BookingType BookingType { get; set; }
        [DisplayName("Description"),Required]
        public string Descrition { get; set; }
        [DisplayName("Extra Service")]
        public bool IncludeExtra { get; set; }
        [DisplayName("Package Price")]
        public decimal PackagePrice { get; set; }


    }
}