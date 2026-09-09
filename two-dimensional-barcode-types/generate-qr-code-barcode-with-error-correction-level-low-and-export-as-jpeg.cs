// Title: Generate QR Code with Low Error Correction and Save as JPEG
// Description: This example creates a QR Code barcode with low error correction level and saves it as a JPEG image.
// Category-Description: Demonstrates Aspose.BarCode barcode generation for QR Code symbology, focusing on error correction settings and image export. Uses BarcodeGenerator, EncodeTypes, QRErrorLevel, and BarCodeImageFormat classes. Ideal for developers needing to create QR codes with specific error correction levels and output them in common image formats.
// Prompt: Generate a QR Code barcode with error correction level low and export as JPEG.
// Tags: qr code, error correction, jpeg, barcode generation, aspose.barcode, encode types, qrcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code with low error correction and saves it as a JPEG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory and ensure it exists.
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Full path for the resulting JPEG image.
        string outputPath = Path.Combine(outputDir, "qr_low_error_correction.jpg");

        // Text to encode in the QR Code.
        string codeText = "Hello, QR!";

        // Create a QR Code generator with the specified text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Set the QR Code error correction level to low (Level L).
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelL;

            // Save the generated barcode as a JPEG image.
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the image was saved.
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}