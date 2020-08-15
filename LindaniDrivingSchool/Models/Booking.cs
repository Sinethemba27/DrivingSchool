using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LindaniDrivingSchool.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }
        [DisplayName("Customer Email")]
        public string Customer_Email { get; set; }
        public int BokingPackageId { get; set; }
        public virtual BookingPackage BookingPackage { get; set; }
        public int TimeSlotId { get; set; }
        public virtual TimeSlots TimeSlots { get; set; }

        [DisplayName("Date Booked")]
        public DateTime DateBooked { get; set; }
        [DisplayName("Date Booking For"),DataType(DataType.Date)]
        public DateTime DateBookingFor { get; set; }
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public string Status { get; set; }


    }
}