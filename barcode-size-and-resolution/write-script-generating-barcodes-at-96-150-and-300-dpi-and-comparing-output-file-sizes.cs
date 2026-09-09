// Title: Generate barcodes at multiple DPI settings and compare file sizes
// Description: Demonstrates how to create Code128 barcodes at 96, 150, and 300 dpi, save them as PNG files, and display each file’s size.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, its Parameters.Resolution property, and image export via BarCodeImageFormat. Developers often need to adjust barcode resolution for printing or screen display and evaluate resulting file sizes for storage or transmission considerations. The snippet serves as a quick reference for DPI‑based barcode creation in .NET.
// Prompt: Write script generating barcodes at 96, 150, and 300 dpi and comparing output file sizes.
// Tags: barcode, code128, dpi, file size, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates Code128 barcodes at different DPI settings, saves them as PNG files,
/// and prints each file’s size to the console.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary output folder,
    /// iterates over specified DPI values, generates barcodes, saves them,
    /// and reports the resulting file sizes.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the output files.
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeDpiComparison_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define the DPI values to test.
        float[] dpis = new float[] { 96f, 150f, 300f };
        // Barcode content and symbology.
        string codeText = "1234567890";
        BaseEncodeType encodeType = EncodeTypes.Code128;

        // Generate a barcode for each DPI setting.
        foreach (float dpi in dpis)
        {
            // Build the file name that includes the DPI value.
            string filePath = Path.Combine(outputDir, $"barcode_{dpi}dpi.png");

            // Create and configure the barcode generator.
            using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Set the resolution (DPI) for the generated image.
                generator.Parameters.Resolution = dpi;
                // Save the barcode as a PNG file.
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Retrieve and display the file size.
            long fileSize = new FileInfo(filePath).Length;
            Console.WriteLine($"DPI: {dpi}, File: {Path.GetFileName(filePath)}, Size: {fileSize} bytes");
        }

        // Inform the user where the files were saved.
        Console.WriteLine($"Barcodes saved to: {outputDir}");
    }
}