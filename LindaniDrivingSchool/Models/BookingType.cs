using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LindaniDrivingSchool.Models
{
    public class BookingType
    {
        [Key]
        public int BookingTypeId { get; set; }
        [DisplayName("Booking Type Name"),Required]
        public string BktName { get; set; }
        [DisplayName("Short Description"),Required]
        public string ShortDescription { get; set; }
    }
}