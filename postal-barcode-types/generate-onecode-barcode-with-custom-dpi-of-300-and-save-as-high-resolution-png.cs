// Title: Generate OneCode barcode with 300 DPI and save as PNG
// Description: Demonstrates creating a OneCode (Intelligent Mail) barcode using Aspose.BarCode, setting a custom resolution of 300 DPI, and exporting it as a high‑resolution PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing the use of BarcodeGenerator with EncodeTypes.OneCode. It illustrates configuring image resolution, defining output paths, and saving the barcode in PNG format—common tasks for developers needing high‑quality barcode images for printing or digital use.
// Prompt: Generate a OneCode barcode with custom DPI of 300 and save as high‑resolution PNG.
// Tags: onecode, barcode, generation, png, resolution, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a OneCode barcode with a custom DPI of 300
/// and saves it as a high‑resolution PNG file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Sample valid OneCode (Intelligent Mail) codetext: 20 digits, second digit between 0-4
        string codeText = "12345678901234567890";

        // Initialize the barcode generator for the OneCode symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.OneCode, codeText))
        {
            // Set the image resolution to 300 DPI for high‑resolution output
            generator.Parameters.Resolution = 300f;

            // Build the full output file path in the current working directory
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "OneCode.png");

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user that the barcode has been generated
        Console.WriteLine("OneCode barcode generated successfully.");
    }
}