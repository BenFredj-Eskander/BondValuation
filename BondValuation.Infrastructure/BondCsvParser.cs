using BondValuation.Core;
using System;
using System.Collections.Generic;
using System.IO;

namespace BondValuation.Infrastructure
{
    public class BondCsvParser : IBondCsvParser
    {
        public IEnumerable<Bond> ParseBonds(Stream csvStream)
        {
            var bonds = new List<Bond>();
            using var reader = new StreamReader(csvStream);

            string line;
            bool isHeader = true;
            string[] headers = Array.Empty<string>();

            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var fields = line.Split(';');

                if (isHeader)
                {
                    headers = fields;
                    isHeader = false;
                    continue;
                }

                try
                {
                    var bond = ParseBondLine(headers, fields);
                    if (bond != null) bonds.Add(bond);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error parsing line '{line}': {ex.Message}");
                }
            }

            return bonds;
        }

        private Bond ParseBondLine(string[] headers, string[] fields)
        {
            var fieldDict = new Dictionary<string, string>();
            for (int i = 0; i < headers.Length && i < fields.Length; i++)
            {
                fieldDict[headers[i]] = fields[i];
            }

            var bond = new Bond
            {
                BondId = GetFieldValue(fieldDict, "BondID"),
                Type = GetFieldValue(fieldDict, "Type"),
                Rate = ParseRate(GetFieldValue(fieldDict, "Rate")),
                FaceValue = ParseDouble(GetFieldValue(fieldDict, "FaceValue")),
                PaymentsPerYear = ParsePaymentsPerYear(GetFieldValue(fieldDict, "PaymentFrequency")),
                YearsToMaturity = ParseDouble(GetFieldValue(fieldDict, "YearsToMaturity")),
                DiscountFactor = ParseDouble(GetFieldValue(fieldDict, "DiscountFactor")),
                Rating = GetFieldValue(fieldDict,"Rating"),
                DeskNotes = GetFieldValue(fieldDict, "DeskNotes")
            };

            return bond.IsSkipped() ? null : bond;
        }

        private string GetFieldValue(Dictionary<string, string> fieldDict, string fieldName)
        {
            return fieldDict.TryGetValue(fieldName, out var value) ? value : throw new ArgumentException($"Missing field: {fieldName}");
        }

        private double ParseRate(string rateValue)
        {
            double result;
            if (string.IsNullOrEmpty(rateValue))
            {
                result = 0.0;
            }
            if (rateValue.StartsWith("Inflation+", StringComparison.OrdinalIgnoreCase))
            {
                var percentagePart = rateValue["Inflation+".Length..].TrimEnd('%');
                result = ParseDouble(percentagePart) / 100;
            }
            else if (rateValue.EndsWith("%"))
            {
                result = ParseDouble(rateValue.TrimEnd('%')) / 100;
            }
            else
            {
                result = ParseDouble(rateValue) / 100;
            }
            return result;
        }

        private double ParseDouble(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return 0.0;
            }
            return double.Parse(value, System.Globalization.CultureInfo.InvariantCulture);
        }

        private int ParsePaymentsPerYear(string frequency)
        {
            return frequency?.ToLower() switch
            {
                "annual" => 1,
                "semi-annual" => 2,
                "quarterly" => 4,
                "none" => 0,
                _ => 0
            };
        }
    }
}
