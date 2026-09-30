// Title: Apply Custom Text Color to a Barcode (Code128)
// Description: Demonstrates how to set a custom color for the human‑readable text of a barcode while keeping the bar and background colors at their defaults.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to customize barcode appearance using the BarcodeGenerator class. It covers setting colors for specific barcode elements (e.g., code text) without altering the default bar and background colors. Developers often need to adjust visual aspects such as text color, font size, or style to match branding or UI requirements, and this snippet illustrates the typical API usage for those scenarios.
// Prompt: Apply a custom text color to a barcode while leaving bar and background colors at defaults.
// Tags: barcode, code128, text-color, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode with a custom text color.
/// </summary>
class Program
{
    /// <summary>
    /// Generates the barcode image and saves it to the Output folder.
    /// </summary>
    static void Main()
    {
        // Define the output directory and ensure it exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Build the full file path for the resulting PNG image
        string outputPath = Path.Combine(outputDir, "customTextColor.png");

        // Initialize the barcode generator with Code128 symbology and sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set a custom color (red) for the human‑readable text only
            generator.Parameters.Barcode.CodeTextParameters.Color = Color.Red;

            // Increase the font size of the code text for better visibility
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 14f;

            // Save the generated barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image was saved
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}