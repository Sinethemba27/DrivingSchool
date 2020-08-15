using LindaniDrivingSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LindaniDrivingSchool.Logic
{
    public class BusinessLogic
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public decimal GetCostperday(int carId)
        {
            var don = (from c in db.Cars
                       where c.CarId == carId
                       select c.Cost_Per_Day).FirstOrDefault();
            return don;
        }
        public decimal CalcNum_of_Days(CarHiring carHiring)
        {
           
            TimeSpan difference = carHiring.ReturnDate.Subtract(carHiring.PickUpDate);
            var Days = difference.TotalDays;
            return Convert.ToDecimal(Days);
        }     
        public decimal calcPercentage(CarHiring carHiring)
        {
            return (carHiring.BasicPrice - (carHiring.BasicPrice * (carHiring.Percentage / 100)));
        }
        public decimal calcBasicCharge(CarHiring carHiring)
        {
            return CalcNum_of_Days(carHiring) * GetCostperday(carHiring.CarId);
        }
       
        public double getCurrentMilage(int carId)
        {
            var crnt = (from c in db.Cars
                        where c.CarId == carId
                        select c.CurrentMilage).FirstOrDefault();
            return crnt;
        }
        public decimal GetKiloRate(int carId)
        {
            var kilo = (from c in db.Cars
                        where c.CarId == carId
                        select c.KilometerRate).FirstOrDefault();
            return kilo;
        }

        public decimal GetFreeKM(int carId)
        {
            var freeKilos = (from c in db.Cars
                             where c.CarId == carId
                             select c.freeKiloMeters).FirstOrDefault();
            return freeKilos;
        }
        public decimal calcUsedKM(int carId, double MilageIn)
        {
            return Convert.ToDecimal(MilageIn - getCurrentMilage(carId));
        }
        public decimal calcFreeKM(CarHiring carHiring)
        {
            return GetFreeKM(carHiring.CarId) * CalcNum_of_Days(carHiring);
        }
        public decimal calcExtraKMCost(CarHiring carHiring)
        {
            if (calcUsedKM(carHiring.CarId,carHiring.MilageIn) > calcFreeKM(carHiring))
            {
                return (calcUsedKM(carHiring.CarId, carHiring.MilageIn) - calcFreeKM(carHiring)) * GetKiloRate(carHiring.CarId);
            }
            else
            {
                return 0;
            }

        }
        public decimal calcKMRate(CarHiring carHiring)
        {
            var kiloRat = (from c in db.Cars
                           where c.CarId == carHiring.CarId
                           select c.KilometerRate).FirstOrDefault();
            decimal kr = (kiloRat) * calcExtraKMCost(carHiring);
            if (calcUsedKM(carHiring.CarId, carHiring.MilageIn) > calcFreeKM(carHiring))
            {
                return kr;
            }
            else
            {
                return kr = 0;
            }
        }
        public decimal calcGrandTotal(CarHiring carHiring)
        {
            return calcBasicCharge(carHiring) + calcKMRate(carHiring);
        }
    }
}