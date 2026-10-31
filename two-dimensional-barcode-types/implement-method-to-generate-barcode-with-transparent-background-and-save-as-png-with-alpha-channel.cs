// Title: Generate Code128 barcode with transparent background and save as PNG
// Description: Demonstrates creating a Code128 barcode, applying a transparent background, and saving it as a PNG image that retains the alpha channel.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how to customize barcode appearance using BarcodeGenerator and its Parameters properties. It shows setting background colors, bar colors, and exporting to common image formats, a typical requirement for embedding barcodes in UI designs or documents where transparency is needed. Developers often use these APIs to produce high‑quality barcodes for web, print, or mobile applications.
// Prompt: Implement method to generate barcode with transparent background and save as PNG with alpha channel.
// Tags: code128, barcode, generate, transparent-background, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode with a transparent background and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary output folder, generates the barcode, and writes the file path to the console.
    /// </summary>
    static void Main()
    {
        // Define a unique temporary directory for the output image
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeTransparent_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Full path for the PNG file
        string outputPath = Path.Combine(outputDir, "barcode.png");

        // Initialize the barcode generator with Code128 symbology and the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set the background to transparent and the bar color to black
            generator.Parameters.BackColor = Color.Transparent;
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save the barcode as a PNG image preserving the alpha channel
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the location of the generated barcode image
        Console.WriteLine("Barcode saved to: " + outputPath);
    }
}