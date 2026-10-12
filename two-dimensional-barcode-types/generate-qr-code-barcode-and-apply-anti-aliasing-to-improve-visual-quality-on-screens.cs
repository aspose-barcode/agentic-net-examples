// Title: Generate QR Code with Anti-Aliasing
// Description: Creates a QR Code barcode, enables anti‑aliasing for smoother on‑screen rendering, and saves it as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, demonstrating how to use the BarcodeGenerator class with EncodeTypes.QR to produce QR Code barcodes. Typical use cases include encoding URLs or other data for mobile scanning and improving visual quality on displays by applying anti‑aliasing. Developers often need to adjust rendering parameters such as XDimension and anti‑alias settings to meet UI requirements.
// Prompt: Generate QR Code barcode and apply anti‑aliasing to improve visual quality on screens.
// Tags: qr code, anti-aliasing, barcode generation, png, aspose.barcode, encode types, screen rendering

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code barcode with anti‑aliasing enabled and saving it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates output directory, configures the barcode generator,
    /// enables anti‑aliasing, sets module size, and saves the resulting image.
    /// </summary>
    static void Main()
    {
        // Determine the output folder relative to the current working directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");

        // Ensure the output directory exists
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Full path for the generated QR Code image
        string outputPath = Path.Combine(outputDir, "QrCode_AntiAlias.png");

        // Text to encode in the QR Code (e.g., a URL)
        string codeText = "https://www.example.com";

        // Initialize the barcode generator for QR Code symbology
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Enable anti‑aliasing for smoother rendering on screens
            generator.Parameters.UseAntiAlias = true;

            // Optional: set the size of each QR module (pixel dimension)
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Save the QR Code image in PNG format
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image was saved
        Console.WriteLine($"QR Code with anti-aliasing saved to: {outputPath}");
    }
}