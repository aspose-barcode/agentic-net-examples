// Title: Generate QR Code with Fixed Dimensions
// Description: Demonstrates how to create a QR Code barcode using Aspose.BarCode, disable automatic sizing, and enforce specific image dimensions.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and image parameter settings. Developers often need to produce QR codes of exact pixel size for UI layout, printing, or embedding in documents, requiring control over AutoSizeMode, ImageWidth, ImageHeight, and XDimension. The snippet shows typical steps for configuring these properties and saving the barcode as a PNG image.
// Prompt: Generate QR Code barcode and disable automatic size to enforce fixed dimensions.
// Tags: qr code, barcode generation, fixed dimensions, autosizemode, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code barcode with fixed dimensions using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the output folder, configures the barcode generator, and saves the QR Code image.
    /// </summary>
    static void Main()
    {
        // Define and create the output directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Full path for the generated PNG file
        string outputPath = Path.Combine(outputDir, "qr_fixed.png");

        // Initialize the barcode generator for a QR Code with the desired text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Disable automatic sizing and enforce fixed dimensions
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;
            generator.Parameters.ImageWidth.Pixels = 300f;
            generator.Parameters.ImageHeight.Pixels = 300f;

            // Optional: set the module (dot) size for the QR Code
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Save the barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}