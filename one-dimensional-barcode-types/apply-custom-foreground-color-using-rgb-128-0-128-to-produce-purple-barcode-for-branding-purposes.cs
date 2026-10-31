// Title: Generate a purple Code128 barcode image
// Description: Demonstrates how to set a custom foreground color (RGB 128,0,128) for a Code128 barcode and save it as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and color parameters to customize barcode appearance. Typical use cases include branding, product labeling, and creating visually distinct barcodes. Developers often need to adjust barcode colors, background, and output formats using the Parameters API.
// Prompt: Apply a custom foreground color using RGB (128,0,128) to produce a purple barcode for branding purposes.
// Tags: code128, color, png, generation, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a purple Code128 barcode and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory for the generated barcode image.
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Build the full file path for the PNG output.
        string outputPath = Path.Combine(outputDir, "purple_barcode.png");

        // Create a BarcodeGenerator for Code128 with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set the barcode (foreground) color to purple using RGB (128,0,128).
            generator.Parameters.Barcode.BarColor = Color.FromArgb(255, 128, 0, 128);

            // Set the background color to white.
            generator.Parameters.BackColor = Color.White;

            // Save the barcode image as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}