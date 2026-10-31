// Title: PDF417 barcode with custom gray colors
// Description: Demonstrates how to generate a PDF417 barcode with a light gray background and dark gray bars, saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize barcode appearance using the BarcodeGenerator class. Typical use cases include branding, UI integration, and printing where specific color schemes are required. Developers often need to adjust background and bar colors to match design guidelines, and this snippet shows the essential API calls.
// Prompt: Set the background color to light gray and bar color to dark gray for a PDF417 barcode.
// Tags: pdf417, barcode, color, background, barcolor, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a PDF417 barcode with a light gray background and dark gray bars, and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates output directory, configures barcode colors, and saves the image.
    /// </summary>
    static void Main()
    {
        // Determine the output directory path relative to the current working directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");

        // Ensure the output directory exists; create it if it does not
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Build the full file path for the resulting PNG image
        string outputPath = Path.Combine(outputDir, "Pdf417_Gray.png");

        // Initialize the barcode generator with PDF417 symbology and the data to encode
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "1234567890"))
        {
            // Set the background color to light gray
            generator.Parameters.BackColor = Color.LightGray;

            // Set the bar (foreground) color to dark gray
            generator.Parameters.Barcode.BarColor = Color.DarkGray;

            // Save the generated barcode as a PNG image to the specified path
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the location of the saved barcode image
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}