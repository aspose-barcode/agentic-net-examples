// Title: Generate GS1 Code 128 barcode with custom size and save as JPEG
// Description: Demonstrates how to create a GS1 Code 128 barcode, set its dimensions to 200 × 100 pixels, and export it as a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and image parameter settings. Typical use cases include creating product barcodes with specific size requirements for packaging or labeling. Developers often need to control barcode dimensions, resolution, and output format when integrating barcode creation into .NET applications.
// Prompt: Set barcode size to 200 × 100 pixels for a GS1 Code 128 barcode and export as JPEG.
// Tags: gs1code128, barcode generation, size, jpeg, aspnet, aspnetcore, aspose.barcode, image export

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a GS1 Code 128 barcode with a custom size and saving it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the output folder, configures the barcode generator,
    /// sets image dimensions, and saves the barcode as a JPEG file.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define the output directory and ensure it exists.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Full path for the resulting JPEG file.
        string outputPath = Path.Combine(outputDir, "gs1code128.jpg");

        // Initialize the barcode generator for GS1 Code 128 with sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, "(01)12345678901231"))
        {
            // Use nearest auto‑size mode to respect explicit width/height settings.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;

            // Set the desired image dimensions in pixels.
            generator.Parameters.ImageWidth.Pixels = 200f;
            generator.Parameters.ImageHeight.Pixels = 100f;

            // Define the image resolution (DPI).
            generator.Parameters.Resolution = 96f;

            // Save the generated barcode as a JPEG image.
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the file was saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}