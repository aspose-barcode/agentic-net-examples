// Title: Compress JPEG Barcode Image Using Aspose.BarCode
// Description: Demonstrates how to configure Aspose.BarCode to generate a Code128 barcode and save it as a compressed JPEG by reducing resolution and disabling anti-aliasing.
// Category-Description: This example belongs to the Aspose.BarCode image generation and optimization category. It showcases the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to create barcodes, adjust image parameters, and produce smaller JPEG files. Developers often need to generate barcodes for web or mobile applications where reduced file size improves load times and bandwidth usage.
// Prompt: Provide configuration to enable compression when saving barcode images as JPEG to reduce file size.
// Tags: barcode, code128, jpeg, compression, aspose.barcode, image generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code128 barcode and saves it as a compressed JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode, applies compression settings, and writes the file path to the console.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current directory
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode_compressed.jpg");

        // Create a barcode generator for Code128 with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            // Reduce image resolution to lower JPEG size (72 DPI)
            generator.Parameters.Resolution = 72f;

            // Disable anti-aliasing to further reduce file size
            generator.Parameters.UseAntiAlias = false;

            // Save the barcode as a JPEG image using the configured compression settings
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}