using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a QR code image and verifying it by reading the barcode back.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a QR code, saves it as a PNG, then reads it back to verify the content.
    /// </summary>
    static void Main()
    {
        // Define the text to encode in the QR code (a sample URL).
        string qrText = "https://example.com";

        // Define the output file path for the generated QR code image.
        string outputPath = "qr.png";

        // -------------------------------------------------
        // Generate QR code image
        // -------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, qrText))
        {
            // Set a high error correction level (Level H) to improve scanning reliability.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Set the image resolution to 300 DPI (dots per inch).
            generator.Parameters.Resolution = 300f;

            // Save the generated QR code as a PNG file.
            generator.Save(outputPath);
        }

        // -------------------------------------------------
        // Verify the generated QR code by reading it back
        // -------------------------------------------------
        using (var reader = new BarCodeReader(outputPath, DecodeType.QR))
        {
            bool found = false; // Tracks whether any QR code was detected.

            // Iterate through all detected barcodes in the image.
            foreach (var result in reader.ReadBarCodes())
            {
                found = true;
                Console.WriteLine($"Detected QR Code Text: {result.CodeText}");

                // Compare the decoded text with the original input.
                if (result.CodeText == qrText)
                {
                    Console.WriteLine("Verification succeeded: decoded text matches original.");
                }
                else
                {
                    Console.WriteLine("Verification failed: decoded text does not match original.");
                }
            }

            // If no barcode was found, inform the user.
            if (!found)
            {
                Console.WriteLine("No QR code detected. The image may not be scannable.");
            }
        }
    }
}