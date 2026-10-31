// Title: Generate barcode with default colors and white background
// Description: Demonstrates creating a Code128 barcode using Aspose.BarCode with default colors, then explicitly setting the background to white and saving both images.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class to produce barcodes, configure visual parameters such as background color, and export to PNG. Typical use cases include creating product labels, inventory tags, or any scenario where barcode images are needed. Developers often need to customize appearance while relying on default rendering for quick generation.
// Prompt: Generate a barcode with default colors and then change background to white to confirm default behavior.
// Tags: barcode, code128, background color, png, aspose.barcode, generation, image export

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Code128 barcode with default colors
/// and then with an explicitly set white background, saving both images to disk.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates two barcode images and writes their file paths to the console.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the output files
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define file paths for the default and white‑background barcodes
        string defaultPath = Path.Combine(outputDir, "barcode_default.png");
        string whiteBgPath = Path.Combine(outputDir, "barcode_whitebg.png");

        // Generate barcode with default colors (no visual parameters modified)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(defaultPath, BarCodeImageFormat.Png);
        }

        // Generate barcode with background explicitly set to white
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Parameters.BackColor = Color.White;
            generator.Save(whiteBgPath, BarCodeImageFormat.Png);
        }

        // Output the locations of the generated images
        Console.WriteLine($"Default barcode saved to: {defaultPath}");
        Console.WriteLine($"White background barcode saved to: {whiteBgPath}");
    }
}