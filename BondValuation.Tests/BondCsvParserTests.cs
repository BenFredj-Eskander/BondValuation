using BondValuation.Infrastructure;
using System.Text;
using Xunit;

namespace BondValuation.Tests
{
    public class BondCsvParserTests
    {
        private readonly BondCsvParser _parser;

        public BondCsvParserTests()
        {
            _parser = new BondCsvParser();
        }

        [Fact]
        public void ParseBonds_WithValidCsv_ReturnsCorrectNumberOfBonds()
        {
            // Arrange
            var csvContent = @"BondID;Rate;FaceValue;PaymentFrequency;Rating;Type;YearsToMaturity;DiscountFactor;DeskNotes
B001;3.10%;500;Semi-Annual;AA+;Bond;5.5;0.78498;Corporate bond
B002;4.20%;1000;None;BBB;Zero-Coupon;3.2;0.86862;Issued at discount";

            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(csvContent));

            // Act
            var bonds = _parser.ParseBonds(stream).ToList();

            // Assert
            Assert.Equal(2, bonds.Count);
        }

        [Fact]
        public void ParseBonds_WithInflationLinkedUnclearIndexing_SkipsBond()
        {
            // Arrange
            var csvContent = @"BondID;Rate;FaceValue;PaymentFrequency;Rating;Type;YearsToMaturity;DiscountFactor;DeskNotes
B003;Inflation+0.92%;500;Quarterly;A-;Inflation-Linked;13.7;0.54715;Indexing details unclear
B004;3.10%;500;Semi-Annual;BB+;Bond;5.5;0.78498;Corporate bond";

            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(csvContent));

            // Act
            var bonds = _parser.ParseBonds(stream).ToList();

            // Assert
            Assert.Single(bonds);
            Assert.Equal("B004", bonds[0].BondId);
        }
    }
}
