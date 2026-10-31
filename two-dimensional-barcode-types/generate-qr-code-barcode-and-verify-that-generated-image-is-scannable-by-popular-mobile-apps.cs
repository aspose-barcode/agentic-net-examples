// Title: Generate and Validate QR Code Barcode with Aspose.BarCode
// Description: This example creates a QR Code image from a URL and then reads it back to confirm it is correctly encoded and scannable.
// Category-Description: Demonstrates Aspose.BarCode's barcode generation and recognition capabilities. The example uses BarcodeGenerator to produce a QR Code image (PNG) and BarCodeReader to decode it, showcasing typical workflows for creating machine‑readable data and verifying its integrity—common tasks for developers integrating QR codes into applications, marketing materials, or inventory systems.
// Prompt: Generate QR Code barcode and verify that generated image is scannable by popular mobile apps.
// Tags: qr code, barcode generation, barcode recognition, png, aspose.barcode, encode, decode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR Code image and validates it by decoding the same image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR Code, saves it as PNG, and verifies its content.
    /// </summary>
    static void Main()
    {
        // Define output file path in the temporary folder
        string outputPath = Path.Combine(Path.GetTempPath(), "qr.png");
        // Text to encode in the QR Code (e.g., a URL)
        string codeText = "https://example.com";

        // -------------------------
        // Generate QR Code image
        // -------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Set visual appearance
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;
            // High resolution for better readability on screens and prints
            generator.Parameters.Resolution = 300f;
            // Use the highest error correction level to improve scan reliability
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;
            // Save the generated barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was created
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to generate QR code image.");
            return;
        }

        // -------------------------
        // Read and validate QR Code
        // -------------------------
        using (var reader = new BarCodeReader(outputPath, DecodeType.QR))
        {
            bool found = false;
            // Iterate through all detected barcodes (should be only one)
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Decoded text: {result.CodeText}");
                Console.WriteLine($"Match original: {result.CodeText == codeText}");
                found = true;
            }

            if (!found)
            {
                Console.WriteLine("No QR code detected in the generated image.");
            }
        }
    }
}