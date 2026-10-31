// Title: Resetting Barcode Background Color to Default White
// Description: Demonstrates how to generate a barcode with a custom gray background and then reset the background to the default white using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to manipulate the barcode appearance via the BarcodeGenerator class. It covers setting the background color, a common requirement for branding or visual integration, and resetting it to the default white. Developers often need to customize colors for UI consistency and then revert to defaults for standard outputs.
// Prompt: Reset the barcode background to default white after previously setting it to gray.
// Tags: barcode symbology, background color, reset, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates two barcode images: one with a gray background
/// and another with the default white background, demonstrating how to reset
/// the background color using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates barcode images with different
    /// background colors and saves them to a temporary directory.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the output files
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeBgDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define file paths for the gray and white barcode images
        string grayPath = Path.Combine(outputDir, "barcode_gray.png");
        string whitePath = Path.Combine(outputDir, "barcode_white.png");

        // Generate barcode with a custom gray background
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            generator.Parameters.BackColor = Color.Gray; // set background to gray
            generator.Save(grayPath, BarCodeImageFormat.Png); // save as PNG
        }

        // Generate barcode with the default white background (reset)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            generator.Parameters.BackColor = Color.White; // reset to default white
            generator.Save(whitePath, BarCodeImageFormat.Png); // save as PNG
        }

        // Output the locations of the generated images
        Console.WriteLine("Generated barcode images:");
        Console.WriteLine(grayPath);
        Console.WriteLine(whitePath);
    }
}