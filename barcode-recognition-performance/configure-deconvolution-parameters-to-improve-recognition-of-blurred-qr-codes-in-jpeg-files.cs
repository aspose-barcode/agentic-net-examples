// Title: QR Code Recognition with Deconvolution in JPEG Images
// Description: Demonstrates configuring deconvolution and quality settings to improve detection of blurred QR codes stored in JPEG files.
// Prompt: Configure deconvolution parameters to improve recognition of blurred QR codes in JPEG files.
// Tags: qr, deconvolution, barcode recognition, jpeg, aspose.barcode, qualitysettings

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that reads a blurred QR code from a JPEG image
/// using Aspose.BarCode with deconvolution and high‑quality settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Configures the barcode reader, applies deconvolution, and outputs detection results.
    /// </summary>
    static void Main()
    {
        // Path to the JPEG image containing a blurred QR code.
        string imagePath = "blurred_qr.jpg";

        // Verify that the image file exists before attempting to read it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Initialize the barcode reader for QR codes using the specified image.
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            // Apply a high‑quality preset that is suitable for damaged or blurred barcodes.
            reader.QualitySettings = QualitySettings.HighQuality;

            // Enable fast deconvolution to help restore blurred image details.
            reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;

            // Allow recognition of barcodes with minor errors to increase detection chances.
            reader.QualitySettings.AllowIncorrectBarcodes = true;

            // Iterate through all detected barcodes and output their details.
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected QR Code: {result.CodeText}");
                Console.WriteLine($"Confidence: {result.Confidence}");
                Console.WriteLine($"Reading Quality: {result.ReadingQuality}");

                // Output the detected barcode region (bounding rectangle).
                var bounds = result.Region.Rectangle;
                Console.WriteLine($"Region - X:{bounds.X}, Y:{bounds.Y}, Width:{bounds.Width}, Height:{bounds.Height}");
            }
        }
    }
}