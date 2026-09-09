// Title: Generate Han Xin barcode and save as GIF with web‑friendly palette
// Description: Demonstrates creating a Han Xin 2‑D barcode, configuring error correction, colors, and saving it as a GIF image suitable for web use.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator with EncodeTypes.HanXin, set barcode parameters (error level, encode mode, version), customize colors, and export to common web formats like GIF. Developers working with 2‑D barcodes often need to produce lightweight images for browsers, and this snippet shows the typical API usage for such scenarios.
// Prompt: Save Han Xin barcode as GIF image with limited color palette for web use.
// Tags: hanxin, 2d barcode, gif, color palette, aspose.barcode, generation, image export

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Han Xin barcode and saving it as a GIF image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates output folder, generates the barcode, configures parameters, and saves the image.
    /// </summary>
    static void Main()
    {
        // Determine the output directory and ensure it exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Build the full path for the GIF file
        string outputPath = Path.Combine(outputDir, "HanXinBarcode.gif");

        // Text to encode in the barcode
        string codeText = "Hello World";

        // Initialize the barcode generator for Han Xin symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, codeText))
        {
            // Configure Han Xin specific parameters
            generator.Parameters.Barcode.HanXin.ErrorLevel = HanXinErrorLevel.L2;
            generator.Parameters.Barcode.HanXin.EncodeMode = HanXinEncodeMode.Auto;
            generator.Parameters.Barcode.HanXin.Version = HanXinVersion.Auto;

            // Set barcode (foreground) and background colors
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Save the generated barcode as a GIF image (suitable for web use)
            generator.Save(outputPath, BarCodeImageFormat.Gif);
        }

        // Output the location of the saved barcode image
        Console.WriteLine($"Han Xin barcode saved to: {outputPath}");
    }
}