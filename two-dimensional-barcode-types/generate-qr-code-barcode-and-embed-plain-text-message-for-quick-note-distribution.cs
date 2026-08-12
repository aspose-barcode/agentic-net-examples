// Title: Generate QR Code with Embedded Plain Text Note
// Description: Demonstrates creating a QR Code barcode that encodes a plain‑text message and adds a human‑readable caption, then saves it as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes.QR, configure QR error correction, and customize 2‑D display text. Typical use cases include encoding URLs, contact info, or quick notes for mobile scanning. Developers often need to adjust error levels, add readable annotations, and export the barcode to common image formats.
// Prompt: Generate QR Code barcode and embed plain text message for quick note distribution.
// Tags: qr code, barcode generation, plain text, png, aspose.barcode, encode types, error correction

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a QR Code containing a short note and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the QR Code and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Create a dedicated output folder in the system's temporary directory
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Full file path for the generated QR Code image
        string outputPath = Path.Combine(outputDir, "quicknote_qr.png");

        // Plain text message to encode in the QR code
        string message = "Meet at 10am on 2023-12-01";

        // Initialize the barcode generator for QR Code symbology
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Assign the text that will be encoded into the QR symbol
            generator.CodeText = message;

            // Optional: display a human‑readable caption below the QR symbol
            generator.Parameters.Barcode.CodeTextParameters.TwoDDisplayText = "Quick Note";

            // Set a high error correction level (Level H) for improved resilience to damage
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the generated barcode image as a PNG file
            generator.Save(outputPath);
        }

        // Inform the user where the QR code image has been saved
        Console.WriteLine($"QR code saved to: {outputPath}");
    }
}