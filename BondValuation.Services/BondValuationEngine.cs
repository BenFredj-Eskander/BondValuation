using BondValuation.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BondValuation.Services
{
    public class BondValuationEngine : IBondValuationEngine
    {
        private readonly IEnumerable<IBondValuationService> _valuationServices;

        public BondValuationEngine(IEnumerable<IBondValuationService> valuationServices)
        {
            _valuationServices = valuationServices;
        }

        public BondValueResult ValueBond(Bond bond)
        {
            var service = _valuationServices.FirstOrDefault(s => s.CanHandle(bond.Type));
            if (service == null)
            {
                throw new InvalidOperationException($"No valuation service found for bond type: {bond.Type}");
            }

            return service.CalculateValue(bond);
        }

        public IEnumerable<BondValueResult> ValueBonds(IEnumerable<Bond> bonds)
        {
            var results = new List<BondValueResult>();

            foreach (var bond in bonds)
            {
                try
                {
                    results.Add(ValueBond(bond));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to value bond {bond.BondId}: {ex.Message}");
                }
            }

            return results;
        }
    }
}
