// Title: Generate auto-sized Code128 barcode image
// Description: This example creates a Code128 barcode image where the bar height is automatically determined based on the content, using default measurement units.
// Category-Description: Demonstrates Aspose.BarCode generation features for automatic sizing of barcodes. It uses the BarcodeGenerator class with EncodeTypes to produce PNG images, a common task for developers needing dynamic barcode creation without manual dimension settings. Suitable for tutorials on barcode rendering, image export, and default configuration usage.
// Prompt: Generate barcode with BarCodeHeight zero to enable auto‑size based on content, using default units.
// Tags: code128, barcode generation, auto-size, png, aspose.barcode, barcodgenerator, encode types

using System;
using System.IO;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode with automatic height sizing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates and saves an auto-sized barcode image.
    /// </summary>
    static void Main()
    {
        // Determine the output directory relative to the current working folder.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDir);

        // Define the full path for the generated barcode image.
        string barcodePath = Path.Combine(outputDir, "AutoSizeBarcode.png");

        // Set the text to be encoded in the barcode.
        string codeText = "Sample123";

        // Create a BarcodeGenerator for Code128 with the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // No explicit BarHeight setting; default behavior auto-sizes based on content.
            // Save the barcode as a PNG image to the specified path.
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Output the location of the saved barcode image.
        Console.WriteLine($"Barcode saved to: {barcodePath}");
    }
}