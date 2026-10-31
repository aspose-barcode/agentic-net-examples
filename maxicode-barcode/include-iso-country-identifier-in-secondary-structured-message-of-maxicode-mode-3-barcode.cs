// Title: Generate MaxiCode Mode 3 barcode with ISO country identifier in secondary message
// Description: Demonstrates creating a MaxiCode Mode 3 barcode, setting postal code, country code, service category, and adding an ISO country identifier in the structured secondary message. The barcode is saved as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, focusing on MaxiCode symbology. It shows how to use the MaxiCodeCodetextMode3, MaxiCodeStructuredSecondMessage, and ComplexBarcodeGenerator classes to build a MaxiCode with custom data fields. Developers working with shipping, logistics, or inventory systems often need to generate MaxiCode barcodes that include structured address information and ISO country codes.
// Prompt: Include an ISO country identifier in the secondary structured message of a MaxiCode Mode 3 barcode.
// Tags: maxicode, barcode generation, complex barcode, iso country code, structured message, png output, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Provides the entry point for the MaxiCode generation example.
/// </summary>
class Program
{
    /// <summary>
    /// Creates a MaxiCode Mode 3 barcode with address data and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define output directory and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "MaxiCodeDemo");
        Directory.CreateDirectory(outputDir);

        // Full path for the generated barcode image
        string outputPath = Path.Combine(outputDir, "MaxiCodeMode3.png");

        // Initialize MaxiCode codetext for Mode 3 and set required fields
        MaxiCodeCodetextMode3 codetext = new MaxiCodeCodetextMode3
        {
            PostalCode = "B1050",
            CountryCode = 56,
            ServiceCategory = 999
        };

        // Build the structured secondary message (address lines, ISO country code, year)
        MaxiCodeStructuredSecondMessage secondMessage = new MaxiCodeStructuredSecondMessage();
        secondMessage.Add("634 ALPHA DRIVE"); // Street address
        secondMessage.Add("PITTSBURGH");      // City
        secondMessage.Add("PA");              // State/Province
        secondMessage.Add("US");              // ISO country identifier
        secondMessage.Year = 99;              // Year component

        // Assign the secondary message to the codetext
        codetext.SecondMessage = secondMessage;

        // Generate the barcode using ComplexBarcodeGenerator
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(codetext))
        {
            // Set the X-dimension (module size) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 5f;

            // Save the barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"MaxiCode barcode saved to: {outputPath}");
    }
}