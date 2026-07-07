// Title: Generate MaxiCode barcode with 300 DPI resolution
// Description: Demonstrates creating a MaxiCode barcode and setting its image DPI to 300 for high‑quality printing.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator, MaxiCodeCodetextMode2, and related classes to produce MaxiCode symbols. Developers often need to adjust image resolution, embed secondary messages, and export to common formats like PNG for printing or scanning applications.
// Prompt: Set the barcode image DPI to 300 when generating a MaxiCode to improve print quality.
// Tags: maxicode, barcode generation, dpi, image resolution, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that creates a MaxiCode barcode with a 300 DPI image resolution.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a MaxiCode, sets its DPI, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output file name
        string outputPath = "maxicode.png";

        // Prepare MaxiCode codetext (Mode 2 example) with required fields
        var maxiCodeCodetext = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",   // 9‑digit US postal code
            CountryCode = 056,          // USA country code
            ServiceCategory = 999       // Example service category
        };

        // Add a simple second message to the MaxiCode
        var secondMessage = new MaxiCodeStandardSecondMessage
        {
            Message = "Sample MaxiCode"
        };
        maxiCodeCodetext.SecondMessage = secondMessage;

        // Generate the MaxiCode using the complex barcode generator
        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            // Set image resolution to 300 DPI for high‑quality print output
            generator.Parameters.Resolution = 300f;

            // Save the generated barcode image as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"MaxiCode saved to '{Path.GetFullPath(outputPath)}' with 300 DPI.");
    }
}