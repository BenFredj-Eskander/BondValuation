using BondValuation.Core;
using System;

namespace BondValuation.Services
{
    public class ZeroCouponBondValuationService : BondValuationService
    {
        public override bool CanHandle(string bondType) => bondType?.ToLower() == "zero-coupon";

        public override BondValueResult CalculateValue(Bond bond)
        {
            double coefficient = 1 + bond.Rate;
            double presentValue = (Math.Pow(coefficient, bond.YearsToMaturity) * bond.FaceValue) * bond.DiscountFactor;

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
