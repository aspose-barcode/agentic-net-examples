// Title: Generate Code128 barcodes at multiple DPI settings and compare file sizes
// Description: This example creates PNG images of a Code128 barcode at 96, 150, and 300 DPI, then reports each file's size.
// Category-Description: Demonstrates Aspose.BarCode barcode generation with varying resolution settings. It uses the BarcodeGenerator class to encode a Code128 symbology, adjusts the Parameters.Resolution property, and saves the output as PNG files. Developers often need to control DPI for print quality or file size optimization, making this pattern common in image‑based barcode workflows.
// Prompt: Write script generating barcodes at 96, 150, and 300 dpi and comparing output file sizes.
// Tags: code128, barcode generation, resolution, png, aspose.barcode, aspose.barcode.generation, file size comparison

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating barcodes at different DPI values and comparing the resulting file sizes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates barcode images at 96, 150, and 300 DPI, saves them as PNG,
    /// and writes each file's size to the console.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the output images.
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeDpiDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define the DPI values to test.
        float[] dpis = new float[] { 96f, 150f, 300f };
        // Text to encode in the barcode.
        string codeText = "Aspose123";

        // Iterate over each DPI, generate the barcode, save it, and report its size.
        foreach (float dpi in dpis)
        {
            // Build the file path for the current DPI image.
            string filePath = Path.Combine(outputDir, $"barcode_{dpi}dpi.png");

            // Generate the barcode with the specified resolution.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                generator.Parameters.Resolution = dpi; // Set DPI.
                generator.Save(filePath, BarCodeImageFormat.Png); // Save as PNG.
            }

            // Retrieve the file size in bytes.
            long fileSize = new FileInfo(filePath).Length;
            // Output DPI, file name, and size.
            Console.WriteLine($"DPI: {dpi}, File: {Path.GetFileName(filePath)}, Size: {fileSize} bytes");
        }

        // Inform the user where all images have been saved.
        Console.WriteLine($"All barcode images saved to: {outputDir}");
    }
}