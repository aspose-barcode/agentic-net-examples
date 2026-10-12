// Title: Generate QR Code with Green Background and Black Bars
// Description: Demonstrates creating a QR code using Aspose.BarCode, setting a green background and black bars, and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to produce customized barcode images. Typical use cases include applying color schemes, exporting to common image formats, and integrating QR codes into applications. Developers often need to adjust visual properties like background and bar colors before saving the result.
// Prompt: Create a QR code with a green background and black bars, then save as PNG.
// Tags: qr code, barcode generation, png output, aspose.barcode, color customization

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR code with a green background and black bars,
/// then saves the image as a PNG file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output directory relative to the current working directory.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDir);

        // Build the full path for the resulting PNG file.
        string outputPath = Path.Combine(outputDir, "QrCode_GreenBackground.png");

        // Initialize the barcode generator for QR code with the desired text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Code"))
        {
            // Set the background color to green.
            generator.Parameters.BackColor = Color.Green;

            // Set the barcode (bar) color to black.
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save the generated QR code as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the location of the saved QR code image.
        Console.WriteLine($"QR code saved to: {outputPath}");
    }
}