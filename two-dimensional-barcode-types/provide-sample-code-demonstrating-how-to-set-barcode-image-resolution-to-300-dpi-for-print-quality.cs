// Title: Set barcode image resolution to 300 DPI for high-quality printing
// Description: Demonstrates how to configure the barcode generator to produce a 300 DPI PNG image, suitable for print.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how to adjust rendering parameters such as resolution. It uses the BarcodeGenerator class and its Parameters property to control output quality. Developers often need to set DPI for print-ready barcodes, ensuring sharpness and compliance with printing standards.
// Prompt: Provide sample code demonstrating how to set barcode image resolution to 300 DPI for print quality.
// Tags: barcode symbology, resolution, image generation, png, aspose.barcode, code128

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates setting barcode image resolution to 300 DPI using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode saved as a 300 DPI PNG file.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory for the generated barcode image
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeResolutionDemo");
        // Ensure the directory exists
        Directory.CreateDirectory(outputDir);
        // Build the full file path for the PNG output
        string outputPath = Path.Combine(outputDir, "barcode_300dpi.png");

        // Create a barcode generator for Code128 with the specified data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the image resolution to 300 DPI for print-quality output
            generator.Parameters.Resolution = 300f;
            // Save the barcode as a PNG file at the defined path
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"Barcode saved at: {outputPath}");
    }
}