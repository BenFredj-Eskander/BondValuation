using BondValuation.Core;

namespace BondValuation.Services
{
    public interface IBondValuationService
    {
        BondValueResult CalculateValue(Bond bond);
        bool CanHandle(string bondType);
    }
}
