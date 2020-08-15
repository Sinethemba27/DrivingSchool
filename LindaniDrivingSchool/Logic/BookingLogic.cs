using LindaniDrivingSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace LindaniDrivingSchool.Logic
{
    public class BookingLogic
    {
        private static readonly ApplicationDbContext _db = new ApplicationDbContext();

        public static bool CheckITimeSlot(TimeSlots timeSlots)
        {
            bool result = false;
            var dbRecord = _db.TimeSlots.Where(x => x.SlotTime == timeSlots.SlotTime).FirstOrDefault();
            if (dbRecord != null)
            {
                result = true;
            }
            return result;
        }

        public static  bool CheckBooking(Booking booking)
        {
            bool result = false;
            var dbRecords = _db.Bookings.Where(x => x.DateBookingFor == booking.DateBookingFor).ToList() ;
            foreach (var item in dbRecords)
            {
                if (booking.TimeSlotId == booking.TimeSlotId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public static bool CheckDate(DateTime subDate)
        {
            bool result = false;
            if (subDate< DateTime.Now.Date)
            {
                result = true;
            }
            return result;
        }
    }
}