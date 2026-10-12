// Title: Generate Code128 barcode with custom foreground and background colors
// Description: Demonstrates how to set the barcode foreground to blue and background to white, then save the image as PNG.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Developers commonly need to customize barcode appearance (colors, size, format) for branding or readability before exporting to image files. The snippet shows typical steps for configuring visual properties and saving the result.
// Prompt: Set barcode foreground to blue and background to white before generating the image.
// Tags: barcode, code128, color, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Code128 barcode with custom colors and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the barcode and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG file in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

        // Create a BarcodeGenerator for Code128 symbology with the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the barcode's foreground (bars) color to blue.
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Set the image background color to white.
            generator.Parameters.BackColor = Color.White;

            // Save the generated barcode image as a PNG file to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}