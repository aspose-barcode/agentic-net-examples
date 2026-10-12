// Title: Generate MaxiCode Mode 2 barcode with unstructured secondary message
// Description: Creates a MaxiCode Mode 2 barcode containing postal information and a free‑text secondary message, then saves it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It demonstrates how to use the ComplexBarcodeGenerator with MaxiCodeCodetextMode2, a key class for creating MaxiCode symbols. Typical use cases include shipping labels and logistics where MaxiCode Mode 2 encodes address data plus an optional unstructured message. Developers often need to combine structured fields (postal code, country, service category) with free‑text secondary data and export the result to common image formats.
// Prompt: Generate a MaxiCode Mode 2 barcode with an unstructured secondary message and save it as PNG.
// Tags: maxicode, mode2, secondary message, unstructured, png, aspose.barcode, complexbarcode, barcode generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a MaxiCode Mode 2 barcode with an unstructured secondary message and saving it as PNG.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Builds the codetext, generates the barcode, and writes the output file path to console.
    /// </summary>
    static void Main()
    {
        // Determine the full path for the output PNG file in the current directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "MaxiCodeMode2Unstructured.png");

        // Create MaxiCode Mode 2 codetext with structured fields and an unstructured secondary message.
        MaxiCodeCodetextMode2 codetext = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",          // 9‑digit postal code
            CountryCode = 56,                  // Numeric ISO country code
            ServiceCategory = 999,             // Service category identifier
            SecondMessage = new MaxiCodeStandardSecondMessage
            {
                Message = "Free text message" // Unstructured free‑text secondary message
            }
        };

        // Initialize the complex barcode generator with the prepared codetext.
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(codetext))
        {
            // Save the generated barcode image as PNG to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"MaxiCode Mode 2 barcode saved to: {outputPath}");
    }
}