// Title: Generate Code128 barcode PNG with 250 DPI resolution
// Description: Demonstrates setting barcode image resolution to 250 DPI, saving as PNG, and checking file size for storage optimization.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how to configure resolution, choose output format, and evaluate generated file size. It uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes, common tasks for developers needing high‑resolution barcodes for printing or digital storage while managing file size.
// Prompt: Set barcode resolution to 250 DPI, generate PNG, and evaluate file size for storage optimization.
// Tags: code128, resolution, png, file-size, barcode-generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a Code128 barcode, sets a high image resolution,
/// saves it as a PNG file, and reports the resulting file size.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and evaluates its file size.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define the output file path for the generated PNG image
        string outputPath = "barcode.png";

        // Initialize a BarcodeGenerator for Code128 with the sample text "Sample123"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Configure the image resolution to 250 DPI for higher quality output
            generator.Parameters.Resolution = 250f;

            // Save the generated barcode as a PNG image to the specified path
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Verify that the file was created and output its size for storage analysis
        if (File.Exists(outputPath))
        {
            var fileInfo = new FileInfo(outputPath);
            Console.WriteLine($"Generated barcode saved to {outputPath}");
            Console.WriteLine($"File size: {fileInfo.Length} bytes");
        }
        else
        {
            Console.WriteLine("Failed to generate barcode image.");
        }
    }
}