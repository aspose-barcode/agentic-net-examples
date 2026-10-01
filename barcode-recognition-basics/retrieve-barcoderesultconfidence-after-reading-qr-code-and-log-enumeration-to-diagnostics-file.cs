// Title: Retrieve QR Code Reading Quality and Log to Diagnostics File
// Description: Demonstrates generating a QR code, reading it with Aspose.BarCode, extracting the reading quality (used as confidence) and writing the result to a log file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator to create a QR code, BarCodeReader with QualitySettings to decode it, and how to access BarCodeResult properties such as ReadingQuality for confidence assessment. Developers often need to log barcode detection details for diagnostics or auditing, making this pattern useful for batch processing and CI pipelines.
// Prompt: Retrieve BarCodeResult.Confidence after reading a QR code and log the enumeration to a diagnostics file.
// Tags: qr code,reading quality,confidence,diagnostics,logging,barcode generation,barcode recognition,aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a QR code, reading it, retrieving the reading quality as confidence,
/// and logging the information to a diagnostics file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code image, reads it, extracts confidence,
    /// and writes diagnostic information to a log file.
    /// </summary>
    static void Main()
    {
        // Prepare temporary folder and file paths
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "qr.png");
        string logPath = Path.Combine(tempFolder, "diagnostics.log");

        // Generate a QR code image
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Text"))
        {
            // Save as PNG using the appropriate overload
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was created
        if (!File.Exists(barcodePath))
        {
            File.AppendAllText(logPath, $"[{DateTime.Now}] Error: Barcode image not found at '{barcodePath}'.{Environment.NewLine}");
            return;
        }

        // Read the QR code and retrieve the confidence (using ReadingQuality as a proxy)
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.QR))
        {
            // Optional: set high-performance quality settings
            reader.QualitySettings = QualitySettings.HighPerformance;

            BarCodeResult[] results = reader.ReadBarCodes();
            if (results == null || results.Length == 0)
            {
                // No barcode detected; log the situation
                File.AppendAllText(logPath, $"[{DateTime.Now}] No barcode detected in the image.{Environment.NewLine}");
            }
            else
            {
                // Iterate through detected barcodes
                foreach (BarCodeResult result in results)
                {
                    // Aspose.BarCode does not expose a 'Confidence' property; use ReadingQuality instead
                    double confidence = result.ReadingQuality;
                    string logEntry = $"[{DateTime.Now}] Detected QR Code. CodeText: '{result.CodeText}'. Confidence (ReadingQuality): {confidence:F2}";
                    File.AppendAllText(logPath, logEntry + Environment.NewLine);
                }
            }
        }

        // Output locations for verification (optional)
        Console.WriteLine("Barcode image: " + barcodePath);
        Console.WriteLine("Diagnostics log: " + logPath);
    }
}