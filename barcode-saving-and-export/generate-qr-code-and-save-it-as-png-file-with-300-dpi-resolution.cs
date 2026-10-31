// Title: Generate QR Code PNG with 300 DPI using Aspose.BarCode
// Description: This example creates a QR code that encodes a URL and saves it as a PNG image with a resolution of 300 DPI.
// Category-Description: Demonstrates the Aspose.BarCode generation API for creating QR codes. It showcases the BarcodeGenerator class, EncodeTypes enumeration, and resolution settings, which are commonly used to produce high‑resolution printable barcodes for marketing, product labeling, and authentication scenarios. Developers looking for quick QR code generation and image export will find this pattern useful.
// Prompt: Generate a QR code and save it as a PNG file with 300 DPI resolution.
// Tags: qr code, png, 300dpi, barcode generation, aspose.barcode, encode types

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR code and saving it as a high‑resolution PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates output folder, generates QR code, sets resolution, and saves the image.
    /// </summary>
    static void Main()
    {
        // Determine output directory path and ensure it exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Build full file path for the PNG image
        string filePath = Path.Combine(outputDir, "qr_code_300dpi.png");

        // Initialize BarcodeGenerator with QR encoding and the desired data
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set image resolution to 300 DPI
            generator.Parameters.Resolution = 300f;

            // Save the generated QR code as a PNG file
            generator.Save(filePath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"QR code saved to: {filePath}");
    }
}