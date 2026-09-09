// Title: Resetting Barcode Background Color to Default White
// Description: Demonstrates how to generate a barcode with a custom gray background and then reset the background to the default white using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to manipulate the barcode appearance via the Parameters property. It shows setting the BackColor property to a custom color and then reverting it to the default white. Developers working with barcode creation often need to customize colors for branding or UI integration, and this pattern demonstrates the typical API usage for such scenarios.
// Prompt: Reset the barcode background to default white after previously setting it to gray.
// Tags: barcode, background color, reset, code128, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates two Code128 barcodes: one with a gray background
/// and another with the background reset to the default white color.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates barcode images with different background colors.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for output files
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define the barcode text and output file paths
        string codeText = "1234567890";
        string grayPath = Path.Combine(outputDir, "barcode_gray.png");
        string whitePath = Path.Combine(outputDir, "barcode_white.png");

        // Generate barcode with a custom gray background
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Parameters.BackColor = Color.Gray; // Set background to gray
            generator.Save(grayPath, BarCodeImageFormat.Png); // Save as PNG
        }

        // Generate barcode and reset background to the default white
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Parameters.BackColor = Color.White; // Reset background to white
            generator.Save(whitePath, BarCodeImageFormat.Png); // Save as PNG
        }

        // Output the locations of the generated images
        Console.WriteLine("Generated barcode images:");
        Console.WriteLine(grayPath);
        Console.WriteLine(whitePath);
    }
}