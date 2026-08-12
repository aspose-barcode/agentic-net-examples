// Title: Generate QR Code with automatic sizing and save as SVG
// Description: Demonstrates creating a QR Code barcode using Aspose.BarCode, automatically selecting the optimal size, and exporting it to an SVG file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code creation and image export. It showcases the BarcodeGenerator class, EncodeTypes, AutoSizeMode, and BarCodeImageFormat for producing scalable vector graphics. Developers often need to generate QR codes for web links or product information and require SVG output for high‑resolution or responsive designs.
// Prompt: Generate a QR Code barcode with automatic size selection and store as SVG file.
// Tags: qr code, auto size, svg, aspose.barcode, barcode generation, encode types

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code with automatic size selection
/// and saves it as an SVG file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Determine the full path for the output SVG file in the current directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr.svg");

        // Initialize a BarcodeGenerator for QR Code with automatic size selection.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Set the text (URL) that the QR Code will encode.
            generator.CodeText = "https://example.com";

            // Enable automatic sizing using interpolation mode to let the library choose optimal dimensions.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Attempt to save the generated barcode as an SVG file.
            // If the evaluation license does not support SVG export for QR codes, handle the exception gracefully.
            try
            {
                generator.Save(outputPath, BarCodeImageFormat.Svg);
                Console.WriteLine($"QR Code saved successfully to: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to save SVG. A full license is required for SVG export of QR codes.");
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}