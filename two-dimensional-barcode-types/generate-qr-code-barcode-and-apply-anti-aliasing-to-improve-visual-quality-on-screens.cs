// Title: Generate QR Code with Anti-Aliasing for Screen Display
// Description: Demonstrates how to create a QR Code barcode, enable anti‑aliasing, and save it as a PNG image for high‑quality on‑screen rendering.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code creation and visual enhancement. It showcases key API classes such as BarcodeGenerator, EncodeTypes, and QRErrorLevel, illustrating typical use cases like setting error correction, resolution, and anti‑aliasing to improve readability on digital displays. Developers looking for quick QR Code generation with optimal screen quality will find this pattern useful.
// Prompt: Generate QR Code barcode and apply anti‑aliasing to improve visual quality on screens.
// Tags: qr code, anti-aliasing, barcode generation, png, aspose.barcode, aspose.drawing

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR Code barcode, applies anti‑aliasing,
/// and saves the result as a PNG image suitable for screen display.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a QR Code with anti‑aliasing
    /// and writes the image to the file system.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated QR code image.
        string outputPath = "qr.png";

        // Initialize the QR code generator with the desired text/content.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Enable anti‑aliasing to produce smoother edges on screen.
            generator.Parameters.UseAntiAlias = true;

            // Set a high error correction level (Level H) to improve readability
            // even if the code is partially damaged or obscured.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Configure the image resolution (e.g., 150 DPI) appropriate for screen display.
            generator.Parameters.Resolution = 150f;

            // Save the generated QR code as a PNG file at the specified path.
            generator.Save(outputPath);
        }

        // Inform the user where the QR code image has been saved.
        Console.WriteLine($"QR code saved to {outputPath}");
    }
}