// Title: Apply custom foreground and background colors to a barcode image
// Description: Demonstrates how to set barcode foreground (BarColor) and background colors using Aspose.BarCode generator and save the image as PNG.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize visual appearance of barcodes. It uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to create a barcode, modify its Parameters.Barcode.BarColor and Parameters.BackColor properties, and export the result. Developers often need to match branding colors or improve readability, making color customization a common requirement in barcode generation scenarios.
// Prompt: Apply custom foreground and background colors to the barcode image using generator settings.
// Tags: barcode, color customization, code128, png, aspose.barcode, generation

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
    /// Entry point of the example. Creates an output folder, configures barcode colors, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory for the generated barcode image.
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeColorDemo");
        if (!Directory.Exists(outputDir))
        {
            // Create the directory if it does not already exist.
            Directory.CreateDirectory(outputDir);
        }

        // Full path for the resulting PNG file.
        string outputPath = Path.Combine(outputDir, "custom_color_barcode.png");

        // Initialize the barcode generator with Code128 symbology and sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the barcode's foreground (bars) color to blue.
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Set the image background color to yellow.
            generator.Parameters.BackColor = Color.Yellow;

            // Save the barcode image as a PNG file to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}