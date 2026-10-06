// Title: Generate QR Code barcode and save as SVG with automatic size selection
// Description: Demonstrates creating a QR Code barcode using Aspose.BarCode, automatically selecting module size, and saving the result as an SVG file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.QR to produce scalable vector graphics. Typical use cases include generating QR codes for web URLs, product information, or contact data, where developers need high‑quality, resolution‑independent output. The snippet shows setting optional parameters, handling file paths, and error handling, which are common tasks when integrating barcode creation into .NET applications.
// Prompt: Generate a QR Code barcode with automatic size selection and store as SVG file.
// Tags: qr code, barcode generation, svg output, aspose.barcode, encode types, automatic size selection

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code barcode and saving it as an SVG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the QR Code and writes it to the file system.
    /// </summary>
    static void Main()
    {
        // Determine the full path for the output SVG file
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr_code.svg");

        // Initialize the barcode generator with QR encoding and the desired data
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Optional: set the module (X) dimension in pixels; Aspose.BarCode can auto‑select size if omitted
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            try
            {
                // Save the generated QR Code as an SVG image
                generator.Save(outputPath, BarCodeImageFormat.Svg);
                Console.WriteLine($"QR Code saved to: {outputPath}");
            }
            catch (Exception ex)
            {
                // Report any errors that occur during the save operation
                Console.WriteLine($"Failed to save QR Code as SVG: {ex.Message}");
            }
        }
    }
}