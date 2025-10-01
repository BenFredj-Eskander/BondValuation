using BondValuation.Core;
using System;

namespace BondValuation.Services
{
    public class CouponBondValuationService : BondValuationService
    {
        public override bool CanHandle(string bondType) => bondType?.ToLower() == "bond";

        public override BondValueResult CalculateValue(Bond bond)
        {
            double couponPerPeriod = bond.Rate / bond.PaymentsPerYear;
            double periods = bond.YearsToMaturity * bond.PaymentsPerYear;
            double presentValue = ((Math.Pow(1 + couponPerPeriod, periods)) * bond.FaceValue) * bond.DiscountFactor;

            return new BondValueResult
            {
                BondId = bond.BondId,
                Type = bond.Type,
                PresentValue = Math.Round(presentValue,2),
                Rating = bond.Rating
            };
        }
    }
}
