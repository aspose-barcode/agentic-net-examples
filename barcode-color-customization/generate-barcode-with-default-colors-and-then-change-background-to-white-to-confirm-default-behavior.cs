// Title: Generate Code128 Barcode with Default and White Background
// Description: Demonstrates creating a Code128 barcode image using Aspose.BarCode with the default background and then explicitly setting the background to white.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how to use the BarcodeGenerator class and its Parameters property to control visual aspects such as background color. Typical use cases include generating barcodes for labels, receipts, or inventory systems where default styling is sufficient or a specific background color is required. Developers often need to customize colors, sizes, and formats, and this snippet shows the basic steps for those operations.
// Prompt: Generate a barcode with default colors and then change background to white to confirm default behavior.
// Tags: code128, barcode generation, background color, png, aspose.barcode, aspnet, image generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates two barcode images: one with default colors and one with an explicitly set white background.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates barcode images and saves them to a temporary directory.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for output files
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define file paths for the two output images
        string defaultPath = Path.Combine(outputDir, "barcode_default.png");
        string whiteBgPath = Path.Combine(outputDir, "barcode_whitebg.png");

        // Generate barcode with default colors (background defaults to white)
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

        // Output the locations of the generated files
        Console.WriteLine("Default background barcode saved to: " + defaultPath);
        Console.WriteLine("Explicit white background barcode saved to: " + whiteBgPath);
    }
}