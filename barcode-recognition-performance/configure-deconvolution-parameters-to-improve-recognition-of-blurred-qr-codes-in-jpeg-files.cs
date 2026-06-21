using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a QR code image (if missing) and recognizing it,
/// including handling of blurred images using deconvolution.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR code image if it does not exist,
    /// then reads and decodes the QR code, applying deconvolution to improve detection
    /// of blurred images.
    /// </summary>
    static void Main()
    {
        const string imagePath = "blurred_qr.jpg";

        // ------------------------------------------------------------
        // Step 1: Ensure a QR code image exists.
        // ------------------------------------------------------------
        if (!File.Exists(imagePath))
        {
            // Create a QR code generator with the desired content.
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
            {
                // Set a high error correction level to increase resilience to blur.
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

                // Save the generated QR code as a JPEG file (default quality).
                generator.Save(imagePath, BarCodeImageFormat.Jpeg);
            }

            // Note: In a real scenario the JPEG might already be blurred.
            // The comment about simulating blur is retained for context.
        }

        // ------------------------------------------------------------
        // Step 2: Verify the image file exists before attempting recognition.
        // ------------------------------------------------------------
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // ------------------------------------------------------------
        // Step 3: Read and decode the QR code, using deconvolution to aid
        //         detection of blurred images.
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            // Enable fast deconvolution to attempt restoration of blurred barcodes.
            reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;

            // Allow recognition even if the barcode has minor errors (e.g., checksum issues).
            reader.QualitySettings.AllowIncorrectBarcodes = true;

            // Iterate through all detected barcodes in the image.
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Decoded Text : {result.CodeText}");
                Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                Console.WriteLine($"Confidence    : {result.Confidence}");
            }
        }
    }
}