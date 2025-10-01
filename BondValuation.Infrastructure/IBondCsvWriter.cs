using System.Collections.Generic;
using System.Threading.Tasks;
using BondValuation.Core;

namespace BondValuation.Infrastructure
{
    public interface IBondCsvWriter
    {
        Task WriteBondResultsAsync(IEnumerable<BondValueResult> results, string filePath);
        string GenerateCsvContent(IEnumerable<BondValueResult> results); 
    }
}
