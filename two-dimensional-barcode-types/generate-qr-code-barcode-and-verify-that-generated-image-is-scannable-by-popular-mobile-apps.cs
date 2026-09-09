// Title: Generate and Verify QR Code Barcode
// Description: Demonstrates generating a QR Code image with Aspose.BarCode and then reading it back to confirm the encoded data matches the original text.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator to create a QR Code, configure its appearance and resolution, save it as PNG, and then employ BarCodeReader to decode the image. Developers working with QR codes for URLs, product links, or authentication often need to programmatically generate scannable images and verify them, making this pattern a common task in mobile app integration and automated testing.
// Prompt: Generate QR Code barcode and verify that generated image is scannable by popular mobile apps.
// Tags: qr code, barcode generation, barcode recognition, png, aspose.barcode, encode, decode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a QR Code image and validates it by decoding the saved file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR Code, saves it as PNG, then reads it back to verify the encoded text.
    /// </summary>
    static void Main()
    {
        // Text to encode in the QR Code
        string codeText = "https://example.com";

        // Path for the generated PNG image (temporary folder)
        string outputPath = Path.Combine(Path.GetTempPath(), "qr.png");

        // -------------------- QR Code Generation --------------------
        // Create a BarcodeGenerator for QR type with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Set error correction level to Medium (Level M)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Define foreground (barcode) and background colors
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Set image resolution (dpi) for higher quality
            generator.Parameters.Resolution = 300f;

            // Save the generated barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // -------------------- Verification (Decoding) --------------------
        // Ensure the image file was created successfully
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to generate QR code image.");
            return;
        }

        // Specify that we expect to decode a QR Code
        BaseDecodeType decodeType = DecodeType.QR;

        // Initialize a BarCodeReader for the saved image
        using (var reader = new BarCodeReader(outputPath, decodeType))
        {
            bool found = false;

            // Iterate through all detected barcodes (should be one)
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Decoded text: {result.CodeText}");

                // Compare decoded text with the original input
                if (result.CodeText == codeText)
                {
                    Console.WriteLine("Verification succeeded: decoded text matches original.");
                }
                else
                {
                    Console.WriteLine("Verification failed: decoded text does not match original.");
                }

                found = true;
            }

            // If no barcode was detected, inform the user
            if (!found)
            {
                Console.WriteLine("No QR code detected in the generated image.");
            }
        }
    }
}