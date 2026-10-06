// Title: Generate high‑resolution Code128 barcode and save as JPEG
// Description: This example creates a Code128 barcode, sets the image resolution to 300 DPI, and saves it as a high‑resolution JPEG file.
// Category-Description: Demonstrates Aspose.BarCode barcode generation with custom image resolution. It uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to produce barcodes suitable for printing or high‑quality display. Developers often need to adjust DPI for print media, create images in specific formats, and manage output paths, making this a common pattern in barcode imaging solutions.
// Prompt: Configure barcode resolution to 300 DPI and save the generated image as a high‑resolution JPEG.
// Tags: code128, barcode, resolution, jpeg, generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code128 barcode with a 300 DPI resolution
/// and saves it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output directory relative to the current working directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Build the full path for the resulting JPEG file
        string outputPath = Path.Combine(outputDir, "barcode300dpi.jpg");

        // Initialize the barcode generator with Code128 symbology and sample data
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the image resolution to 300 DPI for high‑quality output
            generator.Parameters.Resolution = 300f;

            // Save the generated barcode as a JPEG image at the specified path
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Output the location of the saved barcode image
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}