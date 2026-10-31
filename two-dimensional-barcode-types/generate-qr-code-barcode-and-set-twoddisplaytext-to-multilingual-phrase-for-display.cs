// Title: Generate QR Code with Multilingual Display Text
// Description: Creates a QR code containing a multilingual phrase and sets the TwoDDisplayText property so the text is shown when the barcode is scanned.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class to produce QR Code barcodes. It demonstrates setting the code text with UTF‑8 encoding, customizing the QR error correction level, and assigning TwoDDisplayText for multilingual display. Developers working with barcode creation, especially QR codes for internationalized content, commonly need these APIs to embed readable text alongside the machine‑readable symbol.
// Prompt: Generate QR Code barcode and set TwoDDisplayText to multilingual phrase for display.
// Tags: qr, barcode, generation, multilingual, twoddisplaytext, png

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR code with multilingual display text using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates QR code, sets TwoDDisplayText, saves as PNG.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory and ensure it exists.
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Full path for the generated PNG image.
        string outputPath = Path.Combine(outputDir, "qr_multilingual.png");

        // Multilingual phrase to encode and display (Japanese, emoji, Russian).
        string multilingualText = "こんにちは世界 🌍 Привет мир";

        // Initialize the barcode generator for QR code symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Set the code text using UTF‑8 encoding to support multilingual characters.
            generator.SetCodeText(multilingualText, Encoding.UTF8);

            // Assign the same text to TwoDDisplayText so it appears when scanned.
            generator.Parameters.Barcode.CodeTextParameters.TwoDDisplayText = multilingualText;

            // Use high error correction level (Level H) for better resilience.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR code image was saved.
        Console.WriteLine($"QR code saved to: {outputPath}");
    }
}