// Title: Generate Code128 barcode with default colors and white background
// Description: Demonstrates creating a Code128 barcode using Aspose.BarCode with default colors, then explicitly setting a white background to verify default behavior.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes to produce barcode images. Typical use cases include creating product labels, inventory tags, or QR codes where developers need to control visual properties such as background color. The snippet illustrates default rendering and how to modify the BackColor property before saving the image.
// Prompt: Generate a barcode with default colors and then change background to white to confirm default behavior.
// Tags: code128, barcode, generation, background, png, aspose.barcode, colors

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode with default colors
/// and then with an explicitly set white background, saving both images to disk.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates output directory, generates two barcodes,
    /// and writes the file paths to the console.
    /// </summary>
    static void Main()
    {
        // Determine the output folder relative to the current working directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");

        // Ensure the output directory exists
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // File paths for the two generated images
        string defaultPath = Path.Combine(outputDir, "barcode_default.png");
        string whiteBgPath = Path.Combine(outputDir, "barcode_whitebg.png");

        // Generate barcode with default colors (no background explicitly set)
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

        // Output the locations of the saved barcode images
        Console.WriteLine($"Default barcode saved to: {defaultPath}");
        Console.WriteLine($"White background barcode saved to: {whiteBgPath}");
    }
}