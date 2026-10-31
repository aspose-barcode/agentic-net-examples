// Title: Generate QR Code with Anti-Aliasing
// Description: Creates a QR Code barcode, enables anti‑aliasing, and saves it as a PNG image for high‑quality screen display.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, demonstrating how to configure rendering options such as anti‑aliasing and resolution using the BarcodeGenerator class. Typical use cases include generating QR codes for web URLs, product information, or authentication, where clear on‑screen rendering is essential. Developers often need to adjust image quality parameters to meet UI design requirements.
// Prompt: Generate QR Code barcode and apply anti‑aliasing to improve visual quality on screens.
// Tags: qr code, barcode generation, anti-aliasing, png, aspose.barcode, encode types, image quality

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code barcode with anti‑aliasing enabled and saving it as a PNG image.
/// </summary>
static class Program
{
    /// <summary>
    /// Entry point of the example. Creates an output folder, generates the QR Code, applies rendering settings,
    /// saves the image, and writes the result path to the console.
    /// </summary>
    static void Main()
    {
        // Define a temporary directory for the output image.
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        if (!Directory.Exists(outputDir))
        {
            // Create the directory if it does not already exist.
            Directory.CreateDirectory(outputDir);
        }

        // Build the full file path for the generated PNG image.
        string outputPath = Path.Combine(outputDir, "qr_anti_alias.png");
        // The text to encode in the QR Code (e.g., a URL).
        string codeText = "https://www.example.com";

        // Initialize the barcode generator for QR Code with the specified text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Enable anti‑aliasing to improve visual quality on screens.
            generator.Parameters.UseAntiAlias = true;
            // Set the image resolution (dots per inch).
            generator.Parameters.Resolution = 300f;
            // Save the generated barcode as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR code image has been saved.
        Console.WriteLine($"QR code saved to: {outputPath}");
    }
}