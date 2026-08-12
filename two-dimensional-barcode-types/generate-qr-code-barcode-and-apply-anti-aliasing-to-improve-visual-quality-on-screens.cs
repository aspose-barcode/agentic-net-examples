// Title: Generate QR Code with Anti-Aliasing using Aspose.BarCode
// Description: Demonstrates how to create a QR Code barcode, enable anti‑aliasing for smoother on‑screen rendering, and save it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and rendering parameters such as UseAntiAlias and QR error correction. Developers commonly need to generate high‑quality QR codes for web links, marketing materials, or mobile apps, and this snippet shows the typical steps to configure symbology, set code text, adjust visual settings, and export the image.
// Prompt: Generate QR Code barcode and apply anti‑aliasing to improve visual quality on screens.
// Tags: qr code, anti-aliasing, barcode generation, png output, aspose.barcode, encode types, error correction

using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR Code barcode with anti‑aliasing enabled
/// and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "qr_antialias.png";

        // Initialize a BarcodeGenerator for QR Code symbology.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Set the data (URL) to be encoded in the QR Code.
            generator.CodeText = "https://www.example.com";

            // Enable anti‑aliasing to produce smoother edges when rendered on screens.
            generator.Parameters.UseAntiAlias = true;

            // Optional: increase error correction level to improve readability after damage.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the generated QR Code as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"QR Code with anti‑aliasing saved to: {outputPath}");
    }
}