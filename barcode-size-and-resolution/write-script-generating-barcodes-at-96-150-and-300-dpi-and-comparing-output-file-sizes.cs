// Title: Generate barcodes at multiple DPI settings and compare file sizes
// Description: This example creates a Code128 barcode at 96, 150, and 300 DPI, saves each as a PNG image, and prints the resulting file sizes.
// Category-Description: Demonstrates Aspose.BarCode generation with different resolution settings. The example uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to produce raster images. Developers often need to adjust DPI for print quality, screen display, or file‑size optimization, making this pattern useful for performance testing and output comparison across DPI values.
// Prompt: Write script generating barcodes at 96, 150, and 300 dpi and comparing output file sizes.
// Tags: barcode, code128, generation, resolution, dpi, png, file size, aspose.barcode, aspose.barcode.generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating Code128 barcodes at various DPI settings and comparing the resulting file sizes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes at 96, 150, and 300 DPI, saves them as PNG files, and writes each file's size to the console.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the output images
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeDpiComparison_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Barcode content and symbology
        string codeText = "Sample123";
        BaseEncodeType encodeType = EncodeTypes.Code128;

        // DPI values to test
        int[] dpis = new int[] { 96, 150, 300 };
        foreach (int dpi in dpis)
        {
            // Build the file name for the current DPI
            string filePath = Path.Combine(outputDir, $"barcode_{dpi}dpi.png");

            // Generate the barcode with the specified resolution
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                generator.Parameters.Resolution = dpi;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Retrieve and display the file size
            long fileSize = new FileInfo(filePath).Length;
            Console.WriteLine($"DPI: {dpi}, File: {Path.GetFileName(filePath)}, Size: {fileSize} bytes");
        }

        // Inform the user where the images are stored
        Console.WriteLine($"All barcode images saved to: {outputDir}");
    }
}