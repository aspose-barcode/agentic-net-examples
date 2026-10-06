// Title: Generate a Code128 barcode with custom foreground and background colors
// Description: Demonstrates how to create a Code128 barcode image with a blue foreground and light‑yellow background using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize barcode appearance through the Parameters.Barcode.BarColor and Parameters.BackColor properties. Developers often need to match corporate branding by adjusting colors, resolution, and output format when generating barcodes for packaging, labels, or digital media.
// Prompt: Implement method to generate barcode with custom foreground and background colors for branding purposes.
// Tags: code128, barcode, color, generation, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode with custom colors and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates the output directory, configures the barcode generator,
    /// applies custom foreground and background colors, sets resolution, saves the image, and writes the result path.
    /// </summary>
    static void Main()
    {
        // Define and create a temporary output directory for the generated barcode image.
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Build the full file path for the PNG image.
        string outputPath = Path.Combine(outputDir, "custom_color_barcode.png");

        // Initialize the barcode generator with Code128 symbology and sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set the barcode (foreground) color to blue.
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Set the background color to light yellow.
            generator.Parameters.BackColor = Color.LightYellow;

            // Define the image resolution (dots per inch).
            generator.Parameters.Resolution = 300;

            // Save the generated barcode as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the location of the saved barcode image.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}