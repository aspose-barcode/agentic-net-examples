// Title: Generate and Verify QR Code Barcode with Aspose.BarCode
// Description: Demonstrates how to generate a QR Code image using Aspose.BarCode, save it as PNG, and verify its readability by decoding the image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, showcasing the use of BarcodeGenerator for creating QR Code symbologies and BarCodeReader for decoding them. Typical use cases include creating scannable QR codes for URLs or data payloads and programmatically confirming their correctness. Developers often need to validate generated barcodes to ensure compatibility with mobile scanning apps and other readers.
// Prompt: Generate QR Code barcode and verify that generated image is scannable by popular mobile apps.
// Tags: qr code, generation, verification, png, aspose.barcode, aspose.barcode.generation, aspose.barcode.recognition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates QR Code generation and verification using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR Code, saves it, reads it back to verify, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Define the text to encode in the QR code.
        string codeText = "https://example.com";

        // Create a unique temporary folder to store the generated image.
        string tempFolder = Path.Combine(Path.GetTempPath(), "QrDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "qr.png");

        // -------------------- QR Code Generation --------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Set optional appearance parameters.
            generator.Parameters.Barcode.BarColor = Color.Black;      // QR code foreground color.
            generator.Parameters.BackColor = Color.White;            // Background color.
            generator.Parameters.Resolution = 300f;                  // Image resolution (dpi).
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM; // Error correction level.

            // Save the generated QR code as a PNG image.
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // -------------------- QR Code Verification --------------------
        BaseDecodeType decodeType = DecodeType.QR;
        using (var reader = new BarCodeReader(imagePath, decodeType))
        {
            bool found = false;

            // Iterate through all detected barcodes (should be only one).
            foreach (var result in reader.ReadBarCodes())
            {
                found = true;
                Console.WriteLine("Decoded Text: " + result.CodeText);

                // Compare the decoded text with the original input.
                if (result.CodeText == codeText)
                {
                    Console.WriteLine("Verification succeeded: decoded text matches original.");
                }
                else
                {
                    Console.WriteLine("Verification failed: decoded text does not match original.");
                }
            }

            // If no barcode was detected, inform the user.
            if (!found)
            {
                Console.WriteLine("No QR code detected in the generated image.");
            }
        }

        // -------------------- Cleanup --------------------
        // Delete the temporary image file and folder (optional).
        try
        {
            if (File.Exists(imagePath))
                File.Delete(imagePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Suppress any exceptions during cleanup.
        }
    }
}