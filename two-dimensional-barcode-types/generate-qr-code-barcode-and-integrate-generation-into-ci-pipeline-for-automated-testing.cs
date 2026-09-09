// Title: Generate QR Code and Verify in CI Pipeline
// Description: Demonstrates generating a QR Code barcode, saving it as a PNG file, and reading it back to confirm correctness—ideal for automated CI testing scenarios.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator (for creating barcodes) and BarCodeReader (for decoding them). Typical use cases include automated quality checks, CI/CD pipelines, and batch processing where developers need to ensure barcode assets are produced correctly without manual intervention.
// Prompt: Generate QR Code barcode and integrate generation into CI pipeline for automated testing.
// Tags: qr, barcode, generation, recognition, ci, testing, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that creates a QR Code, validates it, and cleans up temporary files.
/// Designed for use in continuous integration pipelines where interactive console input is unavailable.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a QR Code, reads it back for verification, and removes all temporary artifacts.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Create a unique temporary folder for the test to avoid collisions in parallel CI runs
        string tempFolder = Path.Combine(Path.GetTempPath(), "QrTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the text to encode and the output file path
        string barcodeText = "CI Test QR";
        string barcodePath = Path.Combine(tempFolder, "qr.png");

        // ------------------------------------------------------------
        // Generate QR Code
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, barcodeText))
        {
            // Set module size (pixel dimension) for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            // Use the highest error correction level to ensure robustness
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;
            // Define image resolution (DPI) for high‑quality output
            generator.Parameters.Resolution = 300f;
            // Save the generated barcode as a PNG image
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Verify the generated QR Code by reading it back
        // ------------------------------------------------------------
        BaseDecodeType decodeType = DecodeType.QR;
        using (var reader = new BarCodeReader(barcodePath, decodeType))
        {
            var results = reader.ReadBarCodes();
            if (results != null && results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText))
            {
                Console.WriteLine($"SUCCESS: QR code detected. CodeText: {results[0].CodeText}");
            }
            else
            {
                Console.WriteLine("FAILURE: QR code not detected.");
            }
        }

        // ------------------------------------------------------------
        // Clean up temporary files and folder
        // ------------------------------------------------------------
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored – cleanup failures should not affect CI test results
        }
    }
}