// Title: Reset barcode background color to default white
// Description: This example generates a Code128 barcode with a gray background, then resets the background to white and saves both images.
// Category-Description: Demonstrates Aspose.BarCode generation features, focusing on the Parameters.BackColor property to control barcode background colors. Common scenarios include customizing barcode appearance for branding or UI themes and then reverting to default colors for standard printing. Developers working with barcode image creation, color customization, and format export will find this pattern useful.
// Prompt: Reset the barcode background to default white after previously setting it to gray.
// Tags: barcode, code128, background color, reset, aspose.barcode, image generation, png, color customization

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Illustrates resetting the barcode background color from gray to white using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode with a gray background, then changes the background to white and saves both images.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for output files
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeBgDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define file paths for the gray and white background images
        string grayPath = Path.Combine(outputDir, "barcode_gray.png");
        string whitePath = Path.Combine(outputDir, "barcode_white.png");

        // Generate barcode with gray background
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set background color to gray
            generator.Parameters.BackColor = Color.Gray;
            // Save the gray-background barcode image
            generator.Save(grayPath, BarCodeImageFormat.Png);

            // Reset background to default white
            generator.Parameters.BackColor = Color.White;
            // Save the white-background barcode image
            generator.Save(whitePath, BarCodeImageFormat.Png);
        }

        // Output the locations of the generated images
        Console.WriteLine($"Gray background barcode saved to: {grayPath}");
        Console.WriteLine($"White background barcode saved to: {whitePath}");
    }
}