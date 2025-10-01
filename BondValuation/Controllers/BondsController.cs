using BondValuation.Infrastructure;
using BondValuation.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Text;

namespace BondValuation.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BondsController : ControllerBase
    {
        private readonly IBondCsvParser _csvParser;
        private readonly IBondValuationEngine _valuationEngine;
        private readonly IBondCsvWriter _csvWriter;

        public BondsController(
            IBondCsvParser csvParser,
            IBondValuationEngine valuationEngine,
            IBondCsvWriter csvWriter)
        {
            _csvParser = csvParser;
            _valuationEngine = valuationEngine;
            _csvWriter = csvWriter;
        }

        [HttpPost("valuate-upload")]
        public IActionResult ValueBondsFromUpload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            if (Path.GetExtension(file.FileName).ToLower() != ".csv")
                return BadRequest("Only CSV files are supported");

            try
            {
                // Parse the uploaded CSV file
                using var stream = file.OpenReadStream();
                var bonds = _csvParser.ParseBonds(stream);

                // Value the bonds
                var results = _valuationEngine.ValueBonds(bonds);

                // Generate CSV content
                var csvContent = _csvWriter.GenerateCsvContent(results);
                var csvBytes = Encoding.UTF8.GetBytes(csvContent);

                var fileName = $"bond_valuation_output.csv";

                // Return as file download
                return File(csvBytes, "text/csv", fileName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error processing bond valuation: {ex.Message}");
            }
        }
    } 
}

