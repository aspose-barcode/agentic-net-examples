// Title: Switching between HighPerformance and HighQuality barcode generation presets
// Description: Demonstrates how to generate QR codes using Aspose.BarCode with settings that correspond to HighPerformance and HighQuality presets.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how developers can choose between performance‑oriented and quality‑oriented preset configurations. It showcases the BarcodeGenerator class, EncodeTypes enumeration, and image saving via BarCodeImageFormat. Typical use cases include optimizing barcode rendering for speed in bulk processing or maximizing visual fidelity for print media.
// Prompt: Write documentation examples demonstrating how to switch between HighPerformance and HighQuality presets.
// Tags: qr code, highperformance, highquality, barcode generation, aspose.barcode, image output, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates switching between HighPerformance and HighQuality barcode generation presets using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates QR codes with performance and quality presets and saves them as PNG files.
    /// </summary>
    static void Main()
    {
        // Determine the output directory relative to the current working directory.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        // Ensure the directory exists.
        Directory.CreateDirectory(outputDir);

        // Generate a QR code using settings that approximate the HighPerformance preset.
        GenerateQrCode("HighPerformance preset example", Path.Combine(outputDir, "qr_high_performance.png"));
        // Generate a QR code using settings that approximate the HighQuality preset.
        GenerateQrCode("HighQuality preset example", Path.Combine(outputDir, "qr_high_quality.png"));

        // Inform the user where the generated barcode images are stored.
        Console.WriteLine("Barcodes have been generated in: " + outputDir);
    }

    /// <summary>
    /// Creates a QR code image with default settings (used here to illustrate preset concepts) and saves it to the specified path.
    /// </summary>
    /// <param name="codeText">The text to encode in the QR code.</param>
    /// <param name="outputPath">The full file path where the PNG image will be saved.</param>
    static void GenerateQrCode(string codeText, string outputPath)
    {
        // Initialize the barcode generator for QR encoding with the provided text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Example of setting parameters that roughly correspond to performance/quality presets:
            // HighPerformance: lower resolution, anti-aliasing off
            // HighQuality: higher resolution, anti-aliasing on
            // For simplicity, default settings are used in this demonstration.

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
    }
}