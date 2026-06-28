using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a QR code, saving it to a file, and then reading it back to verify the content.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a QR code, saves it as an image, and validates the encoded text.
    /// </summary>
    static void Main()
    {
        const string codeText = "Hello Aspose";   // Text to encode in the QR code
        const string imagePath = "qr.png";       // Output file path for the QR code image

        // -------------------------------------------------
        // Generate QR Code
        // -------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Optional: set error correction level to Medium (Level M)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the generated QR code image to the specified path
            generator.Save(imagePath);
        }

        // -------------------------------------------------
        // Verify that the image file was created successfully
        // -------------------------------------------------
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Failed to create barcode image at '{imagePath}'.");
            return;
        }

        // -------------------------------------------------
        // Read and validate the QR Code from the saved image
        // -------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            // Attempt to read all barcodes present in the image
            var results = reader.ReadBarCodes();

            // If no barcodes were detected, report and exit
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
                return;
            }

            // Retrieve the decoded text from the first detected barcode
            var decodedText = results[0].CodeText;

            // Compare the decoded text with the original input and report the outcome
            if (decodedText == codeText)
            {
                Console.WriteLine("Test passed: decoded text matches original.");
            }
            else
            {
                Console.WriteLine($"Test failed: expected '{codeText}', got '{decodedText}'.");
            }
        }
    }
}