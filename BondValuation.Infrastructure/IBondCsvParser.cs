using BondValuation.Core;
using System.Collections.Generic;
using System.IO;

namespace BondValuation.Infrastructure
{
    public interface IBondCsvParser
    {
        IEnumerable<Bond> ParseBonds(Stream csvStream);
    }
}
