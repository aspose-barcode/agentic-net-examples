// Title: Generate semi‑transparent Code128 barcode PNG using Aspose.BarCode
// Description: Demonstrates how to set a custom semi‑transparent bar color with System.Drawing.Color.FromArgb and save the barcode as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to customize barcode appearance. Developers often need to adjust colors, transparency, and output formats when integrating barcodes into UI or reports. The snippet shows typical steps: configure parameters, generate, and save the image.
// Prompt: Use a custom System.Drawing.Color.FromArgb value for semi‑transparent bar color and generate PNG.
// Tags: code128, barcode, color, transparency, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Code128 barcode with a semi‑transparent bar color and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define the output directory relative to the current working directory.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDir);

        // Build the full file path for the generated PNG image.
        string filePath = Path.Combine(outputDir, "barcode.png");

        // Initialize the barcode generator with Code128 symbology and sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set a semi‑transparent red bar color (alpha 128, red 255, green 0, blue 0).
            generator.Parameters.Barcode.BarColor = Color.FromArgb(128, 255, 0, 0);

            // Save the generated barcode as a PNG image.
            generator.Save(filePath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to {filePath}");
    }
}