// Title: Generate QR Code with High Contrast for Accessibility
// Description: Creates a QR Code barcode with black foreground and white background, ensuring it meets accessibility contrast guidelines.
// Category-Description: This example demonstrates how to use Aspose.BarCode's generation API to produce QR Code barcodes. It covers setting error correction levels, customizing foreground and background colors for optimal contrast, and saving the result as a PNG image. Developers working with barcode creation, especially for accessibility‑compliant outputs, will find this pattern useful when integrating Aspose.BarCode into .NET applications.
// Prompt: Generate QR Code barcode and ensure generated image passes accessibility contrast requirements.
// Tags: qr code, barcode generation, accessibility, contrast, png, aspose.barcode, aspose.drawing, qr error correction

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a QR Code barcode with high contrast colors to satisfy accessibility requirements.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR Code, configures contrast settings, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Initialize the barcode generator for QR Code with the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set a high error correction level (Level H) to improve readability under adverse conditions.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Configure maximum contrast: black bars on a white background.
            generator.Parameters.Barcode.BarColor = Color.Black;   // Foreground (bars) color
            generator.Parameters.BackColor = Color.White;          // Background color

            // Save the generated QR Code image to a PNG file.
            generator.Save("qr.png");
        }
    }
}