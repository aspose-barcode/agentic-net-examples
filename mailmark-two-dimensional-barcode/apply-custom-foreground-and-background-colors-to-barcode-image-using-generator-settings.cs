// Title: Custom Foreground and Background Colors for a Code128 Barcode
// Description: Demonstrates how to set custom foreground (barcode) and background colors when generating a barcode image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and Parameters to customize visual appearance. Developers often need to match branding or UI themes by adjusting barcode colors and saving to common image formats like PNG.
// Prompt: Apply custom foreground and background colors to the barcode image using generator settings.
// Tags: code128, color, png, barcodegenerator, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a Code128 barcode with custom foreground and background colors and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary output folder, configures barcode colors, saves the image, and writes the output path.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the output file
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeColorDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define the full path for the generated PNG image
        string outputPath = Path.Combine(outputDir, "custom_color_barcode.png");

        // Initialize the barcode generator with Code128 symbology and sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the barcode (foreground) color to blue
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Set the background color to light gray
            generator.Parameters.BackColor = Color.LightGray;

            // Save the barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the location of the saved barcode image
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}