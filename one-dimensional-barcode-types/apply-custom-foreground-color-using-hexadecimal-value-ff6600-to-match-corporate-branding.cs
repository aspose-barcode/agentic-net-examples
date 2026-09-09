// Title: Apply custom foreground color to a Code128 barcode
// Description: Demonstrates how to generate a Code128 barcode and set its bar color using a hexadecimal value to match corporate branding.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize visual appearance of barcodes. It uses the BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to create a barcode image, a common task for developers needing branded scanning solutions. Typical use cases include generating printable labels, receipts, or product tags with company-specific colors.
// Prompt: Apply custom foreground color using hexadecimal value #FF6600 to match corporate branding.
// Tags: barcode, code128, color, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a Code128 barcode, applies a custom foreground color, and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates an output directory, configures the barcode generator,
    /// sets the bar color to #FF6600, saves the image, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define a temporary folder to store the generated barcode image.
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Build the full file path for the PNG output.
        string outputPath = Path.Combine(outputDir, "barcode.png");

        // Initialize the barcode generator with Code128 symbology and the desired data.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set custom foreground (bar) color to #FF6600 (RGB: 255, 102, 0).
            generator.Parameters.Barcode.BarColor = Color.FromArgb(255, 102, 0);

            // Save the generated barcode as a PNG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}