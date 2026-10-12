// Title: Generate QR Code without Quiet Zone and Save as BMP
// Description: This example creates a QR Code barcode with the quiet zone (padding) disabled and saves it as a BMP image file.
// Category-Description: Demonstrates Aspose.BarCode barcode generation for QR Code symbology. It showcases the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to customize barcode appearance—specifically disabling the quiet zone—and to export the result in BMP format. Developers often need such examples when integrating QR Code creation into applications for marketing, inventory, or authentication purposes.
// Prompt: Generate a QR Code barcode with quiet zone disabled and save as BMP.
// Tags: qr code, quiet zone, bmp, aspose.barcode, barcode generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates how to generate a QR Code barcode with the quiet zone disabled
/// and save the result as a BMP image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the output directory, configures the QR Code generator,
    /// disables padding (quiet zone), saves the image, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define the output directory and ensure it exists.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Build the full file path for the BMP image.
        string outputPath = Path.Combine(outputDir, "qr_noquietzone.bmp");

        // Initialize the barcode generator for QR Code with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello QR"))
        {
            // Disable the quiet zone by setting all padding sides to zero points.
            generator.Parameters.Barcode.Padding.Left.Point = 0f;
            generator.Parameters.Barcode.Padding.Top.Point = 0f;
            generator.Parameters.Barcode.Padding.Right.Point = 0f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 0f;

            // Save the generated QR Code as a BMP file.
            generator.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        // Inform the user where the file was saved.
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}