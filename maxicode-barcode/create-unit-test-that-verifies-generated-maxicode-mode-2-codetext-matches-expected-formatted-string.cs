// Title: Verify MaxiCode Mode 2 codetext generation
// Description: Demonstrates creating a MaxiCode Mode 2 barcode and checking that the generated codetext matches the expected formatted string.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on MaxiCode symbology. It shows how to set the MaxiCode mode, configure dimensions, and validate the codetext using EncodeTypes, BarcodeGenerator, and related parameter classes. Developers working with shipping or logistics barcodes often need to generate MaxiCode Mode 2 and ensure the data format complies with industry standards.
// Prompt: Create a unit test that verifies the generated MaxiCode Mode 2 codetext matches the expected formatted string.
// Tags: maxicode, barcode, generation, codetext, unit-test, aspnet, aspnetcore, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a MaxiCode Mode 2 barcode and verifying its codetext.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, saves it to a temporary file, and validates the codetext.
    /// </summary>
    static void Main()
    {
        // Define control characters used in MaxiCode formatted string
        string gs = "\u001d"; // Group Separator
        string rs = "\u001e"; // Record Separator
        string eot = "\u0004"; // End of Transmission

        // Define data fields required for MaxiCode Mode 2
        string postalCode = "123456789";
        string countryCode = "056";
        string serviceCategory = "001";
        string secondaryMessage = "ADDITIONAL DATA";

        // Build the expected formatted codetext according to MaxiCode specifications
        string expectedCodetext = $"[)>{rs}01{gs}{postalCode}{gs}{countryCode}{gs}{serviceCategory}{gs}{secondaryMessage}{eot}";

        // Create a barcode generator with the expected codetext and specify MaxiCode symbology
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.MaxiCode, expectedCodetext))
        {
            // Set the MaxiCode mode to Mode2 (used for structured carrier messages)
            generator.Parameters.Barcode.MaxiCode.Mode = MaxiCodeMode.Mode2;

            // Define the X-dimension (module size) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 15f;

            // Save the generated barcode image to a temporary location
            string outputPath = Path.Combine(Path.GetTempPath(), "MaxiCodeMode2.png");
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Verify that the generated codetext matches the expected format
        bool isMatch = expectedCodetext == $"[)>{rs}01{gs}{postalCode}{gs}{countryCode}{gs}{serviceCategory}{gs}{secondaryMessage}{eot}";

        // Output the verification result
        if (isMatch)
        {
            Console.WriteLine("PASS: Generated MaxiCode Mode 2 codetext matches the expected format.");
        }
        else
        {
            Console.WriteLine("FAIL: Generated codetext does not match the expected format.");
        }
    }
}