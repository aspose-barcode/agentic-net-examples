// Title: Generate MaxiCode Mode 3 Barcode and Save as PNG
// Description: Creates a MaxiCode barcode in mode 3 using Aspose.BarCode, then saves it as a PNG image file.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It demonstrates how to use the ComplexBarcodeGenerator with MaxiCodeCodetextMode3 to produce a MaxiCode symbol, a 2‑D barcode used in logistics and shipping. Developers commonly employ these APIs to encode postal information, country codes, and service categories, then export the result to common image formats such as PNG for downstream processing.
// Prompt: Generate a MaxiCode barcode using mode 3 and save the image as PNG file.
// Tags: maxicode, barcode, generation, png, aspose.barcode, complexbarcodegenerator, mode3

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates how to generate a MaxiCode barcode in mode 3 and save it as a PNG file
/// using Aspose.BarCode's ComplexBarcodeGenerator.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Builds the MaxiCode codetext, generates the barcode,
    /// and writes the resulting image to disk.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG file
        string outputPath = Path.Combine(Environment.CurrentDirectory, "maxicode_mode3.png");

        // Build the MaxiCode codetext for Mode 3, including postal code, country code,
        // and service category. These fields are required for this mode.
        var maxiCodeCodetext = new MaxiCodeCodetextMode3
        {
            PostalCode = "B1050",          // 6‑character alphanumeric postal code
            CountryCode = 56,              // Example country code
            ServiceCategory = 999          // Example service category
        };

        // Create a standard second message (plain text) that will be encoded
        // alongside the primary MaxiCode data.
        var secondMessage = new MaxiCodeStandardSecondMessage
        {
            Message = "Sample MaxiCode Mode 3"
        };
        maxiCodeCodetext.SecondMessage = secondMessage;

        // Initialize the complex barcode generator with the prepared codetext.
        using (var complexGenerator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            // Save the generated barcode as a PNG image to the specified path.
            complexGenerator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"MaxiCode barcode (Mode 3) saved to: {outputPath}");
    }
}