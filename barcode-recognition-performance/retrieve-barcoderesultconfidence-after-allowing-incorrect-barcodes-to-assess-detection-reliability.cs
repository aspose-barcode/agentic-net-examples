// Title: Retrieve Barcode Confidence with AllowIncorrectBarcodes
// Description: Demonstrates generating a Code128 barcode, reading it while allowing potentially incorrect barcodes, and retrieving the confidence value for each detection.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create barcodes and BarCodeReader with QualitySettings.AllowIncorrectBarcodes to detect even doubtful barcodes. Developers often need to assess detection reliability, especially in noisy or low‑quality scans, by examining BarCodeResult.Confidence and ReadingQuality.
// Prompt: Retrieve BarCodeResult.Confidence after allowing incorrect barcodes to assess detection reliability.
// Tags: barcode, code128, confidence, allowincorrectbarcodes, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Sample program that generates a barcode, reads it with relaxed validation,
/// and outputs detection confidence information.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode image, reads it while allowing incorrect barcodes,
    /// and prints each result's type, text, confidence, and reading quality.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the sample barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a simple Code128 barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify the generated file exists before attempting to read it
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Initialize the barcode reader for all supported types
        using (var reader = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
        {
            // Enable detection of potentially incorrect barcodes
            reader.QualitySettings.AllowIncorrectBarcodes = true;

            // Read all barcodes from the image
            BarCodeResult[] results = reader.ReadBarCodes();

            Console.WriteLine($"Barcodes read (AllowIncorrectBarcodes=true): {results.Length}");
            foreach (BarCodeResult result in results)
            {
                // Output detailed information for each detected barcode
                Console.WriteLine($"Type: {result.CodeTypeName}");
                Console.WriteLine($"Text: {result.CodeText}");
                Console.WriteLine($"Confidence: {result.Confidence}");
                Console.WriteLine($"ReadingQuality: {result.ReadingQuality}");
                Console.WriteLine();
            }
        }

        // Clean up temporary files
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program outcome
        }
    }
}