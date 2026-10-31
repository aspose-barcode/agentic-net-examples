// Title: Generate OneCode barcode with 300 DPI and save as PNG
// Description: Creates a OneCode (Intelligent Mail) barcode with a custom resolution of 300 DPI and writes it to a high‑resolution PNG file.
// Category-Description: This example demonstrates how to use Aspose.BarCode's generation API to produce high‑resolution barcodes. It showcases the BarcodeGenerator class with EncodeTypes.OneCode, configuring the Parameters.Resolution property, and saving the result in PNG format. Typical scenarios include mailing applications, where Intelligent Mail barcodes require precise sizing and clarity for printing and scanning. Developers often need to adjust DPI for print quality and export to common image formats.
// Prompt: Generate a OneCode barcode with custom DPI of 300 and save as high‑resolution PNG.
// Tags: onecode, intelligentmail, barcode, generation, resolution, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a OneCode (Intelligent Mail) barcode at 300 DPI and saving it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, sets resolution, and saves the image.
    /// </summary>
    static void Main()
    {
        // Define a valid OneCode (Intelligent Mail) codetext: 20 digits, second digit between 0‑4
        string codeText = "12345678901234567890";

        // Build the full output file path in the current working directory
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "OneCode_300dpi.png");

        // Initialize the barcode generator with OneCode symbology and the provided codetext
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.OneCode, codeText))
        {
            // Configure the generator to produce a high‑resolution image (300 DPI)
            generator.Parameters.Resolution = 300f;

            // Save the generated barcode as a PNG file at the specified location
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"OneCode barcode saved to: {outputPath}");
    }
}