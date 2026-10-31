// Title: Generate barcode with automatic height sizing
// Description: Demonstrates creating a Code128 barcode where BarCodeHeight is left at zero, allowing the library to auto‑size the barcode based on its content.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class to produce barcodes with default sizing. Developers often need to generate barcodes without manually specifying dimensions, relying on auto‑sizing for consistent visual results across different symbologies and output formats.
// Prompt: Generate barcode with BarCodeHeight zero to enable auto‑size based on content, using default units.
// Tags: barcode, code128, auto-size, generation, png, aspose.barcode, barcodheight

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Code128 barcode with automatic height sizing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a temporary output folder, generates the barcode, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the output file
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeExample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Build the full path for the PNG image to be saved
        string outputPath = Path.Combine(outputDir, "barcode.png");

        // Initialize the barcode generator for Code128 with the desired text
        // BarCodeHeight is not set, so the generator will auto-size the barcode height
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Save the generated barcode image in PNG format
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine("Barcode generated at: " + outputPath);
    }
}