// Title: Generate QR Code with 300 DPI resolution
// Description: Demonstrates creating a QR Code barcode and saving it as a high‑resolution PNG image suitable for print.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to configure barcode parameters such as resolution and module size. It uses the BarcodeGenerator class with EncodeTypes.QR to produce QR Code symbology, and shows saving the result with BarCodeImageFormat. Developers often need to generate high‑resolution barcodes for printing on labels, packaging, or marketing materials, and this snippet provides a concise reference.
// Prompt: Generate QR Code barcode and scale barcode to three hundred DPI for high‑resolution print.
// Tags: qr code, barcode generation, resolution, high dpi, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a QR Code barcode at 300 DPI and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a QR Code with high resolution and writes the output path to console.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG file
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "QRCode_300dpi.png");

        // Text to encode into the QR Code
        string codeText = "https://www.example.com";

        // Initialize the barcode generator for QR Code symbology
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Set the image resolution to 300 DPI for high‑resolution printing
            generator.Parameters.Resolution = 300f;

            // Define the size of a single QR module (X dimension) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR Code image was saved
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}