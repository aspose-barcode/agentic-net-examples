// Title: Adjust QR Code Text Gap for High‑Density Barcodes
// Description: Demonstrates how to set a 4‑point gap between a high‑density QR code and its human‑readable text using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR code customization. It showcases the use of BarcodeGenerator, EncodeTypes, QRErrorLevel, and CodeTextParameters to control error correction level, text location, and spacing. Developers often need to fine‑tune QR code appearance for branding or readability, and this snippet provides a concise reference.
// Prompt: Adjust the gap between barcode and its text to 4 points for high‑density QR codes.
// Tags: qr, gap, barcode, generation, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Program demonstrating adjustment of the gap between a QR code and its text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a high‑density QR code with a 4‑point text gap and saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for output files
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define the full path for the resulting PNG image
        string outPath = Path.Combine(outputDir, "HighDensityQR.png");

        // Initialize the barcode generator for a QR code containing the specified data
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set a high error correction level (Level H) for better resilience
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Adjust the spacing (gap) between the barcode and the human‑readable text to 4 points
            generator.Parameters.Barcode.CodeTextParameters.Space.Point = 4f;

            // Ensure the text appears below the barcode (explicitly set for clarity)
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // Save the generated barcode image as a PNG file
            generator.Save(outPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR code image has been saved
        Console.WriteLine($"QR code saved to: {outPath}");
    }
}