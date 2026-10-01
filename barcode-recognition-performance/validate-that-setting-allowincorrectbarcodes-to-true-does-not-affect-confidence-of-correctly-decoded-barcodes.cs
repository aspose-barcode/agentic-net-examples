// Title: Validate that AllowIncorrectBarcodes does not affect barcode reading quality
// Description: This example generates a Code128 barcode, reads it twice—once with AllowIncorrectBarcodes disabled and once enabled—and verifies that the ReadingQuality (confidence) remains the same.
// Category-Description: Demonstrates Aspose.BarCode reading quality settings, focusing on the QualitySettings.AllowIncorrectBarcodes property. Typical use cases include handling imperfect scans while preserving confidence metrics. Developers working with barcode recognition, especially when needing to tolerate minor errors without skewing quality scores, will find this pattern useful.
/// Prompt: Validate that setting AllowIncorrectBarcodes to true does not affect confidence of correctly decoded barcodes.
/// Tags: barcode, code128, readingquality, allowincorrectbarcodes, qualitysettings, aspose.barcode, barcodegeneration, barcoderecognition, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;
using Aspose.Drawing;

/// <summary>
/// Demonstrates validation that AllowIncorrectBarcodes does not change the reading quality of correctly decoded barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, reads it with different AllowIncorrectBarcodes settings, and compares the confidence scores.
    /// </summary>
    static void Main()
    {
        // Sample barcode data to encode
        const string codeText = "1234567890";

        // Create a barcode generator for Code128 and write the image to a memory stream
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            using (var barcodeStream = new MemoryStream())
            {
                // Save the generated barcode as PNG into the stream
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                barcodeStream.Position = 0; // Reset stream for reading

                // ------------------------------------------------------------
                // First read: AllowIncorrectBarcodes = false (default)
                // ------------------------------------------------------------
                double qualityWithoutAllow;
                using (var reader = new BarCodeReader(barcodeStream, DecodeType.Code128))
                {
                    // Explicitly ensure the default setting
                    reader.QualitySettings.AllowIncorrectBarcodes = false;

                    // Perform the read operation
                    var results = reader.ReadBarCodes();

                    // Verify that a barcode was decoded
                    if (results.Length == 0)
                    {
                        Console.WriteLine("Failed to decode barcode without AllowIncorrectBarcodes.");
                        return;
                    }

                    // Capture the confidence (reading quality) of the decoded barcode
                    qualityWithoutAllow = results[0].ReadingQuality;
                    Console.WriteLine($"ReadingQuality without AllowIncorrectBarcodes: {qualityWithoutAllow}");
                }

                // Reset stream position for the second read
                barcodeStream.Position = 0;

                // ------------------------------------------------------------
                // Second read: AllowIncorrectBarcodes = true
                // ------------------------------------------------------------
                double qualityWithAllow;
                using (var reader = new BarCodeReader(barcodeStream, DecodeType.Code128))
                {
                    // Enable tolerance for incorrect barcodes
                    reader.QualitySettings.AllowIncorrectBarcodes = true;

                    // Perform the read operation
                    var results = reader.ReadBarCodes();

                    // Verify that a barcode was decoded
                    if (results.Length == 0)
                    {
                        Console.WriteLine("Failed to decode barcode with AllowIncorrectBarcodes.");
                        return;
                    }

                    // Capture the confidence (reading quality) of the decoded barcode
                    qualityWithAllow = results[0].ReadingQuality;
                    Console.WriteLine($"ReadingQuality with AllowIncorrectBarcodes: {qualityWithAllow}");
                }

                // ------------------------------------------------------------
                // Validation: confidence should be unchanged regardless of the setting
                // ------------------------------------------------------------
                const double epsilon = 0.0001; // Tolerance for floating‑point comparison
                if (Math.Abs(qualityWithoutAllow - qualityWithAllow) <= epsilon)
                {
                    Console.WriteLine("Validation passed: AllowIncorrectBarcodes does not affect confidence of correctly decoded barcodes.");
                }
                else
                {
                    Console.WriteLine("Validation failed: Confidence differs when AllowIncorrectBarcodes is enabled.");
                }
            }
        }
    }
}