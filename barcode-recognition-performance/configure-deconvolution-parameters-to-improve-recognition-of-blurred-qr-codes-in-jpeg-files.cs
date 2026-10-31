// Title: Configure Deconvolution for QR Code Recognition in JPEG Images
// Description: Demonstrates generating a QR code as a JPEG, then reading it with default settings and with deconvolution and low-quality mode to improve detection of blurred images.
// Category-Description: This example belongs to the Aspose.BarCode image processing and recognition category. It showcases the use of BarcodeGenerator for QR code creation, BarCodeReader for decoding, and the QualitySettings API (DeconvolutionMode, BarcodeQualityMode, XDimensionMode) to enhance recognition of low‑quality or blurred barcodes. Developers working with scanned documents, mobile captures, or any scenario where image quality is suboptimal can reference this pattern to boost decoding reliability.
// Prompt: Configure deconvolution parameters to improve recognition of blurred QR codes in JPEG files.
// Tags: qr code, deconvolution, quality settings, barcode recognition, jpeg, aspose.barcode, generation, reading

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR code JPEG and shows how to configure deconvolution
/// and quality settings to improve recognition of blurred QR codes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR code image, reads it with default and enhanced settings,
    /// and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the generated QR code image
        string qrImagePath = Path.Combine(tempFolder, "qr_code.jpg");

        // Generate a QR code containing a URL and save it as a JPEG image
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Generation parameters can be adjusted here if needed
            generator.Save(qrImagePath, BarCodeImageFormat.Jpeg);
        }

        // Verify that the image file was created successfully
        if (!File.Exists(qrImagePath))
        {
            Console.WriteLine("Failed to create QR code image.");
            return;
        }

        // --------------------------------------------------------------------
        // Read the QR code using default reader settings (no quality tweaks)
        // --------------------------------------------------------------------
        Console.WriteLine("Reading QR code with default settings:");
        using (var defaultReader = new BarCodeReader(qrImagePath, DecodeType.QR))
        {
            var defaultResults = defaultReader.ReadBarCodes();
            if (defaultResults.Length > 0)
            {
                Console.WriteLine($"Detected: {defaultResults[0].CodeText}");
            }
            else
            {
                Console.WriteLine("No barcode detected.");
            }
        }

        // --------------------------------------------------------------------
        // Read the QR code with deconvolution and low‑quality mode enabled
        // --------------------------------------------------------------------
        Console.WriteLine("\nReading QR code with deconvolution and low-quality mode:");
        using (var reader = new BarCodeReader(qrImagePath, DecodeType.QR))
        {
            // Apply quality‑enhancing settings to help recognize blurred images
            reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;
            reader.QualitySettings.BarcodeQuality = BarcodeQualityMode.Low;
            // Optionally use minimal X‑dimension mode for finer detection
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;

            var results = reader.ReadBarCodes();
            if (results.Length > 0)
            {
                Console.WriteLine($"Detected: {results[0].CodeText}");
            }
            else
            {
                Console.WriteLine("No barcode detected even after applying quality settings.");
            }
        }

        // -------------------------------------------------
        // Clean up temporary files and directories created
        // -------------------------------------------------
        try
        {
            File.Delete(qrImagePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Suppress any cleanup errors to avoid interrupting the demo flow
        }
    }
}