// Title: Generate barcode with transparent background for video overlay
// Description: Demonstrates creating a Code128 barcode image with a transparent background, saved as PNG for use as an overlay in video streams.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how to configure barcode appearance using the BarcodeGenerator class. It shows setting background color to transparent and exporting to PNG, a common requirement when developers need to overlay barcodes on video or UI elements without obscuring underlying content. The snippet highlights key API classes such as BarcodeGenerator, EncodeTypes, BarCodeImageFormat, and Aspose.Drawing.Color.
// Prompt: Provide example showing how to generate barcode image with transparent background for overlay on video streams.
// Tags: barcode generation, transparent background, png, code128, aspose.barcode, image export

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a Code128 barcode with a transparent background
/// and saves it as a PNG file suitable for overlaying on video streams.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode image and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Build a unique temporary directory to store the generated barcode image.
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeTransparent_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define the full file path for the PNG output.
        string outputPath = Path.Combine(outputDir, "barcode.png");

        // Initialize the barcode generator with Code128 symbology and the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "VideoOverlay"))
        {
            // Set the background color to transparent so the barcode can be overlaid without a solid box.
            generator.Parameters.BackColor = Color.Transparent;

            // Save the barcode as a PNG image, preserving the transparent background.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}