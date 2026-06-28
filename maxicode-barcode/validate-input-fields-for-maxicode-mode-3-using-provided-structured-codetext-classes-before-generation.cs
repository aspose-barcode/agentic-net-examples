using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation of a MaxiCode Mode 3 barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a MaxiCode Mode 3 barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Sample input values for MaxiCode Mode 3
        string postalCode = "B1050A"; // 6 alphanumeric characters
        int countryCode = 56;         // 3‑digit numeric code
        int serviceCategory = 999;    // 3‑digit numeric code
        string secondMessageText = "Test message";

        try
        {
            // Validate the input fields before barcode generation
            ValidateMaxiCodeMode3(postalCode, countryCode, serviceCategory, secondMessageText);

            // Build the structured codetext object required by ComplexBarcodeGenerator
            var codetext = new MaxiCodeCodetextMode3
            {
                PostalCode = postalCode,
                CountryCode = countryCode,
                ServiceCategory = serviceCategory,
                SecondMessage = new MaxiCodeStandardSecondMessage { Message = secondMessageText }
            };

            // Determine a temporary file path for the output PNG image
            string outputPath = Path.Combine(Path.GetTempPath(), "maxicode_mode3.png");

            // Generate the barcode and save it directly to the file system
            using (var generator = new ComplexBarcodeGenerator(codetext))
            {
                // ComplexBarcodeGenerator supports the same Save method as BarcodeGenerator
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"MaxiCode Mode 3 barcode generated successfully: {outputPath}");
        }
        catch (ArgumentException ex)
        {
            // Handle validation errors
            Console.WriteLine($"Input validation error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Handle any unexpected errors
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
    }

    /// <summary>
    /// Validates the input parameters for a MaxiCode Mode 3 barcode.
    /// Throws <see cref="ArgumentException"/> if any parameter is invalid.
    /// </summary>
    /// <param name="postalCode">Six‑character alphanumeric postal code.</param>
    /// <param name="countryCode">Three‑digit numeric country code (0‑999).</param>
    /// <param name="serviceCategory">Three‑digit numeric service category (0‑999).</param>
    /// <param name="secondMessage">Secondary message text; must not be empty.</param>
    static void ValidateMaxiCodeMode3(string postalCode, int countryCode, int serviceCategory, string secondMessage)
    {
        // PostalCode must be exactly 6 alphanumeric characters
        if (string.IsNullOrWhiteSpace(postalCode) ||
            postalCode.Length != 6 ||
            !Regex.IsMatch(postalCode, @"^[A-Za-z0-9]{6}$"))
        {
            throw new ArgumentException("PostalCode must be exactly 6 alphanumeric characters.");
        }

        // CountryCode must be between 0 and 999 (inclusive)
        if (countryCode < 0 || countryCode > 999)
        {
            throw new ArgumentException("CountryCode must be a 3‑digit number between 0 and 999.");
        }

        // ServiceCategory must be between 0 and 999 (inclusive)
        if (serviceCategory < 0 || serviceCategory > 999)
        {
            throw new ArgumentException("ServiceCategory must be a 3‑digit number between 0 and 999.");
        }

        // SecondMessage must be provided and not consist solely of whitespace
        if (string.IsNullOrWhiteSpace(secondMessage))
        {
            throw new ArgumentException("SecondMessage cannot be empty.");
        }
    }
}