using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LindaniDrivingSchool.Models
{
    public class TimeSlots
    {
        [Key]
        public int TimeSlotId { get; set; }

        [DisplayName("Time Slot"),DataType(DataType.Time)]
        public DateTime SlotTime { get; set; }
    }
}