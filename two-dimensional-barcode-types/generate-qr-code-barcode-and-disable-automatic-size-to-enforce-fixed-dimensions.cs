// Title: Generate Fixed-Size QR Code with Aspose.BarCode
// Description: Demonstrates how to create a QR Code barcode, disable automatic sizing, and set explicit image dimensions using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category. It showcases the use of the BarcodeGenerator class together with EncodeTypes, AutoSizeMode, and image dimension parameters to produce a QR Code of a predetermined size. Developers often need to generate barcodes that fit exact layout constraints for print or UI designs, and this snippet illustrates the typical workflow for setting fixed width, height, and module size while disabling automatic scaling.
// Prompt: Generate QR Code barcode and disable automatic size to enforce fixed dimensions.
// Tags: qr code, barcode generation, fixed size, autosizemode, aspose.barcode, png output, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code with fixed dimensions using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Creates the output folder, configures the barcode generator, and saves the QR Code image.
    /// </summary>
    static void Main()
    {
        // Determine the output directory relative to the current working folder
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Full path for the generated PNG file
        string outputPath = Path.Combine(outputDir, "FixedSizeQR.png");

        // Initialize the generator for a QR Code with the desired text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            // Disable automatic sizing and enforce a fixed image size (300x300 pixels)
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;
            generator.Parameters.ImageWidth.Pixels = 300f;
            generator.Parameters.ImageHeight.Pixels = 300f;

            // Optional: define the size of a single QR module (pixel dimension)
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Save the barcode as a PNG image to the specified path
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}