// Title: Generate QR Code barcode at 300 DPI
// Description: Demonstrates creating a QR Code barcode and saving it as a high‑resolution PNG image suitable for print.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to configure barcode parameters such as resolution and module size. It uses the BarcodeGenerator class with EncodeTypes.QR to produce QR Code symbology, a common requirement for embedding URLs or data in marketing materials, product packaging, and high‑resolution print media. Developers often need to adjust DPI and dimensions to meet print quality standards.
// Prompt: Generate QR Code barcode and scale barcode to three hundred DPI for high‑resolution print.
// Tags: qr code, barcode generation, resolution, dpi, png, aspose.barcode, encode types, high resolution print

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code barcode at 300 DPI and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a QR Code with high resolution and writes the output path to console.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG file.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr_300dpi.png");

        // Initialize the barcode generator with QR symbology and the data to encode.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set the image resolution to 300 DPI for high‑resolution printing.
            generator.Parameters.Resolution = 300f;

            // Optionally adjust the X dimension (module size) in millimeters.
            generator.Parameters.Barcode.XDimension.Millimeters = 1f;

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the location of the saved QR Code image.
        Console.WriteLine($"QR Code saved to {outputPath}");
    }
}