using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LindaniDrivingSchool.Models
{
    public class Insurance
    {
        [Key]
        public int InsuranceId { get; set; }
        [Display(Name ="Insurance Type")]
        public string InsuranceType { get; set; }
        [Display(Name = "Insurance Excess Fee"), Range(0,10000)]
        public decimal InsuranceFee { get; set; }
    }
}