// Title: Generate High‑Resolution Code128 Barcode and Save as TIFF
// Description: This example creates a Code128 barcode, sets the resolution to 300 DPI, and saves it as a TIFF image suitable for printing.
// Category-Description: Demonstrates Aspose.BarCode barcode generation with high‑resolution settings. It uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to configure resolution and output format. Developers often need to produce print‑ready barcode images in TIFF for high‑quality print workflows.
// Prompt: Configure the barcode generator for high resolution (300 DPI) and produce TIFF images for printing.
// Tags: code128, barcode generation, high resolution, tiff, aspose.barcode, image format

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a high‑resolution Code128 barcode and saves it as a TIFF file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory and ensure it exists.
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeOutput");
        Directory.CreateDirectory(outputDir);

        // Build the full file path for the resulting TIFF image.
        string filePath = Path.Combine(outputDir, "barcode.tiff");

        // Create a barcode generator for Code128 with the specified data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            // Set the image resolution to 300 DPI for high‑quality printing.
            generator.Parameters.Resolution = 300f;

            // Save the generated barcode as a TIFF image.
            generator.Save(filePath, BarCodeImageFormat.Tiff);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {filePath}");
    }
}