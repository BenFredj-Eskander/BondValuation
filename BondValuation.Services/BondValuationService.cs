using BondValuation.Core;

namespace BondValuation.Services
{
    public abstract class BondValuationService : IBondValuationService
    {
        public abstract bool CanHandle(string bondType);
        public abstract BondValueResult CalculateValue(Bond bond);
    }
}
