
namespace BondValuation.Core
{
    public class Bond
    {
        public string BondId { get; set; }
        public string Type { get; set; }
        public double Rate { get; set; }
        public double FaceValue { get; set; }
        public int PaymentsPerYear { get; set; }
        public double YearsToMaturity { get; set; }
        public double DiscountFactor { get; set; }
        public string Rating { get; set; }
        public string DeskNotes { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(BondId) && !string.IsNullOrEmpty(Type) && FaceValue > 0 && YearsToMaturity >= 0 && DiscountFactor >= 0 && DiscountFactor <= 1;
        }

        public bool IsIndexingUnclear()
        {
            if (Type?.Equals("Inflation-Linked", System.StringComparison.OrdinalIgnoreCase) == true)
            {
                return DeskNotes.Contains("Indexing details unclear", System.StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }
        
        public bool IsSkipped()
        {
            bool result = !IsValid() || IsIndexingUnclear();
            return result;  
        }
    }

}
