// Title: Generate QR Code with Custom Quiet Zone
// Description: Demonstrates how to create a QR Code barcode using Aspose.BarCode and set a quiet zone of eight modules to improve scanner tolerance.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to configure barcode parameters such as module size and padding. It uses the BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to produce a PNG image. Developers often need to adjust quiet zone dimensions to meet scanner requirements, making this pattern common in QR Code creation workflows.
/// Prompt: Generate QR Code barcode and configure quiet zone size to eight modules for scanner tolerance.
/// Tags: qr code, quiet zone, barcode generation, aspose.barcode, png output, encode types, barcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a QR Code image with a custom quiet zone using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates the output directory, configures the QR Code generator,
    /// sets a quiet zone of eight modules, and saves the result as a PNG file.
    /// </summary>
    static void Main()
    {
        // Determine and create the output folder
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "QRCode.png");

        // Initialize the barcode generator for QR Code with the desired text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            // Define the size of a single module (XDimension) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Calculate quiet zone size: eight modules on each side
            float quietZonePixels = 8f * generator.Parameters.Barcode.XDimension.Pixels;

            // Apply the calculated quiet zone to all padding sides
            generator.Parameters.Barcode.Padding.Left.Pixels = quietZonePixels;
            generator.Parameters.Barcode.Padding.Right.Pixels = quietZonePixels;
            generator.Parameters.Barcode.Padding.Top.Pixels = quietZonePixels;
            generator.Parameters.Barcode.Padding.Bottom.Pixels = quietZonePixels;

            // Save the generated QR Code as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR Code image was saved
        Console.WriteLine($"QR Code generated at: {outputPath}");
    }
}