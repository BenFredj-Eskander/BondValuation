using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using BondValuation.Core;

namespace BondValuation.Infrastructure
{
    public class BondCsvWriter : IBondCsvWriter
    {
        public async Task WriteBondResultsAsync(IEnumerable<BondValueResult> results, string filePath)
        {
            var csvContent = GenerateCsvContent(results);
            await File.WriteAllTextAsync(filePath, csvContent, Encoding.UTF8);
        }

        public string GenerateCsvContent(IEnumerable<BondValueResult> results)
        {
            var csvBuilder = new StringBuilder();

            // the header of the csv file
            csvBuilder.AppendLine("BondID;Type;PresentValue;Rating");
            // Add the data rows
            foreach (var result in results)
            {
                var bondId = result.BondId;
                var type = result.Type;
                var presentValue = result.PresentValue.ToString();
                var rating = result.Rating;
                csvBuilder.AppendLine($"{bondId};{type};{presentValue};{rating}");
            }

            return csvBuilder.ToString();
        } 
    }
}
