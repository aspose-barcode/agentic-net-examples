// Title: Verify MaxiCode Mode 2 Codetext Generation
// Description: Demonstrates how to construct and validate the formatted codetext for a MaxiCode Mode 2 barcode, ensuring it matches the expected string.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, focusing on MaxiCode symbology. It shows how to use MaxiCodeCodetextMode2, MaxiCodeStandardSecondMessage, and ComplexBarcodeGenerator to build codetext and render a barcode image. Developers working with shipping and logistics often need to generate MaxiCode Mode 2 barcodes for parcel tracking, requiring precise codetext formatting.
// Prompt: Create a unit test that verifies the generated MaxiCode Mode 2 codetext matches the expected formatted string.
// Tags: maxicode, mode2, codetext, validation, aspose.barcode, complexbarcode, unit-test, barcode-generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates verification of MaxiCode Mode 2 codetext and optional barcode image generation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that builds expected codetext, constructs a MaxiCode codetext object, validates it,
    /// and optionally saves a barcode image.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare test data for the MaxiCode Mode 2 payload
        // ------------------------------------------------------------
        string postalCode = "524032140"; // 9‑digit postal code
        int countryCode = 56;            // Numeric ISO country code
        int serviceCategory = 999;       // Service category identifier
        string secondMessageText = "TestMessage";

        // ------------------------------------------------------------
        // Build the expected formatted codetext according to the MaxiCode spec
        // ------------------------------------------------------------
        string gs = "\u001d"; // Group Separator
        string rs = "\u001e"; // Record Separator
        string eot = "\u0004"; // End Of Transmission
        string expectedCodetext = $"[)>{rs}01{gs}{postalCode}{gs}{countryCode:D3}{gs}{serviceCategory:D3}{gs}{secondMessageText}{eot}";

        // ------------------------------------------------------------
        // Create a MaxiCode codetext object (Mode 2) and populate its fields
        // ------------------------------------------------------------
        var maxiCodeCodetext = new MaxiCodeCodetextMode2
        {
            PostalCode = postalCode,
            CountryCode = countryCode,
            ServiceCategory = serviceCategory,
            SecondMessage = new MaxiCodeStandardSecondMessage { Message = secondMessageText }
        };

        // ------------------------------------------------------------
        // Retrieve the constructed codetext from the object for verification
        // ------------------------------------------------------------
        string actualCodetext = maxiCodeCodetext.GetConstructedCodetext();

        // ------------------------------------------------------------
        // Verify that the generated codetext matches the expected format
        // ------------------------------------------------------------
        if (actualCodetext == expectedCodetext)
        {
            Console.WriteLine("Test Passed: Codetext matches expected format.");
        }
        else
        {
            Console.WriteLine("Test Failed:");
            Console.WriteLine($"Expected: {expectedCodetext}");
            Console.WriteLine($"Actual:   {actualCodetext}");
        }

        // ------------------------------------------------------------
        // Optional: generate a barcode image and save it to a temporary folder
        // ------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "MaxiCodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "MaxiCodeMode2.png");

        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            // Set the MaxiCode mode to 2 (required for the complex generator)
            generator.Parameters.Barcode.MaxiCode.Mode = MaxiCodeMode.Mode2;
            generator.Save(imagePath);
        }

        Console.WriteLine($"Barcode image saved to: {imagePath}");
    }
}