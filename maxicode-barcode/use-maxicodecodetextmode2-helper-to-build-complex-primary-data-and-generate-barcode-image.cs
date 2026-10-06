// Title: Generate MaxiCode Mode 2 Barcode with Structured Secondary Message
// Description: Demonstrates how to use the MaxiCodeCodetextMode2 helper to build complex primary data and a structured secondary message, then generate a MaxiCode barcode image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of MaxiCodeCodetextMode2, MaxiCodeStructuredSecondMessage, and ComplexBarcodeGenerator classes to create MaxiCode (Mode 2) symbols, a common requirement for shipping and logistics applications where detailed address information must be encoded. Developers often need to combine primary and secondary data fields to meet industry standards, and this snippet provides a clear, reusable pattern.
// Prompt: Use the MaxiCodeCodetextMode2 helper to build complex primary data and generate the barcode image.
// Tags: maxicode, barcode generation, complex barcode, structured secondary message, png output, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a MaxiCode Mode 2 barcode with a structured secondary message
/// and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Builds the codetext, generates the barcode, and writes the file path to the console.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary output directory for the generated image
        string outputDir = Path.Combine(Path.GetTempPath(), "MaxiCodeDemo");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "MaxiCodeMode2Structured.png");

        // Build complex primary data for MaxiCode Mode 2
        MaxiCodeCodetextMode2 codetext = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140", // 9‑digit postal code
            CountryCode = 56,         // Numeric ISO country code
            ServiceCategory = 999     // Service category identifier
        };

        // Create a structured secondary message (address lines, city, state, year)
        MaxiCodeStructuredSecondMessage secondMessage = new MaxiCodeStructuredSecondMessage();
        secondMessage.Add("634 ALPHA DRIVE");
        secondMessage.Add("PITTSBURGH");
        secondMessage.Add("PA");
        secondMessage.Year = 99; // Two‑digit year

        // Attach the secondary message to the primary codetext
        codetext.SecondMessage = secondMessage;

        // Generate the MaxiCode barcode and save it as PNG
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(codetext))
        {
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image was saved
        Console.WriteLine($"MaxiCode barcode saved to: {outputPath}");
    }
}