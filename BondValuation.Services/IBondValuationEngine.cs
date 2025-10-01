using BondValuation.Core;
using System.Collections.Generic;

namespace BondValuation.Services
{
    public interface IBondValuationEngine
    {
        IEnumerable<BondValueResult> ValueBonds(IEnumerable<Bond> bonds);
        BondValueResult ValueBond(Bond bond);
    }
}
