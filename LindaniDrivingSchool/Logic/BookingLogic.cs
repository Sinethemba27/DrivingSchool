using LindaniDrivingSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
    }
}