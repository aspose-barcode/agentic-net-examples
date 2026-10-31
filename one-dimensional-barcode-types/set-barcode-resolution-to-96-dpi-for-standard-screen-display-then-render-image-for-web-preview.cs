// Title: Set barcode resolution to 96 DPI and save as PNG
// Description: Demonstrates how to configure a barcode image to 96 DPI for standard screen display and generate a PNG file for web preview.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to create barcodes. Typical scenarios include producing screen‑optimized barcode images for websites, emails, or mobile apps. Developers often need to adjust resolution, choose symbology, and export to common image formats.
// Prompt: Set barcode resolution to 96 DPI for standard screen display, then render image for web preview.
// Tags: code128, barcode generation, set resolution, png output, barcodegenerator, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a Code128 barcode, sets its resolution to 96 DPI,
/// and saves the result as a PNG image suitable for web preview.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Build a unique temporary directory to store the generated barcode image.
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define the full file path for the PNG output.
        string outputPath = Path.Combine(outputDir, "barcode.png");

        // Initialize the barcode generator with Code128 symbology and sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the image resolution to 96 DPI for optimal screen display.
            generator.Parameters.Resolution = 96f;

            // Save the barcode as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine("Barcode image saved to: " + outputPath);
    }
}