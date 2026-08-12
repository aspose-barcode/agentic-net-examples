// Title: Generate QR Code with Square Aspect Ratio
// Description: Demonstrates how to generate a QR Code barcode using Aspose.BarCode and enforce a square aspect ratio for consistent module sizing.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Typical scenarios include creating QR codes for URLs, product information, or authentication purposes where uniform module dimensions are required. Developers often need to control visual appearance, such as aspect ratio, to ensure consistent rendering across different media.
// Prompt: Generate QR Code barcode and set aspect ratio to square for consistent sizing.
// Tags: qr code, barcode generation, aspect ratio, square, aspose.barcode, png, c#

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

/// <summary>
/// Example program that creates a QR Code barcode with a square aspect ratio
/// and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates the QR Code and writes the output file path to the console.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define the full path for the output PNG file.
        string outputPath = Path.Combine(Environment.CurrentDirectory, "qr_square.png");

        // Initialize the barcode generator for QR Code symbology.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Set the data to be encoded in the QR Code.
            generator.CodeText = "https://example.com";

            // AspectRatio = Height / Width; setting to 1 makes each module square.
            generator.Parameters.Barcode.QR.AspectRatio = 1f;

            // Save the generated barcode image in PNG format.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR Code image has been saved.
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}