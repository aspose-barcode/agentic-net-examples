// Title: Validate ReadingQuality based on barcode image resolution
// Description: Demonstrates how the ReadingQuality property of a detected QR code reaches 100 only when the image resolution meets a defined DPI threshold.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create barcodes and BarCodeReader to decode them, focusing on image quality assessment. Developers often need to verify that barcode images meet minimum resolution requirements for reliable scanning; this snippet illustrates how to programmatically evaluate that using the ReadingQuality metric.
// Prompt: Validate that ReadingQuality reaches 100 only when the barcode image meets a minimum resolution threshold.
// Tags: qr code, readingquality, resolution, barcode generation, barcode recognition, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that validates the ReadingQuality of a QR code based on image resolution.
/// </summary>
class Program
{
    // Minimum DPI required for a perfect reading quality (100)
    const float MinimumResolutionDpi = 200f;

    /// <summary>
    /// Entry point. Generates QR codes at two different DPI values and checks ReadingQuality.
    /// </summary>
    static void Main()
    {
        // Sample QR code text
        string codeText = "https://example.com";

        // Test with low resolution (e.g., 100 DPI)
        TestResolution(codeText, 100f);

        // Test with high resolution (e.g., 300 DPI)
        TestResolution(codeText, 300f);
    }

    /// <summary>
    /// Generates a QR code at the specified DPI, reads it back, and evaluates the ReadingQuality.
    /// </summary>
    /// <param name="codeText">The text to encode in the QR code.</param>
    /// <param name="resolutionDpi">The DPI resolution to apply when generating the image.</param>
    static void TestResolution(string codeText, float resolutionDpi)
    {
        // Create a barcode generator for QR code with the provided text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Set the image resolution (DPI) for the generated barcode
            generator.Parameters.Resolution = resolutionDpi;

            using (var ms = new MemoryStream())
            {
                // Save the generated barcode to a memory stream in PNG format
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading

                // Initialize a barcode reader to decode the QR code from the stream
                using (var reader = new BarCodeReader(ms, DecodeType.QR))
                {
                    BarCodeResult[] results = reader.ReadBarCodes();

                    if (results.Length == 0)
                    {
                        Console.WriteLine($"[Resolution {resolutionDpi} DPI] No barcode detected.");
                        return;
                    }

                    // Iterate through all detected barcodes (should be one in this case)
                    foreach (BarCodeResult result in results)
                    {
                        double quality = result.ReadingQuality; // Value ranges from 0 to 100
                        Console.WriteLine($"[Resolution {resolutionDpi} DPI] Detected: {result.CodeText}");
                        Console.WriteLine($"ReadingQuality: {quality}");

                        // Verify expected quality based on the DPI threshold
                        if (resolutionDpi >= MinimumResolutionDpi)
                        {
                            if (quality == 100.0)
                            {
                                Console.WriteLine("✅ Quality is 100 as expected for sufficient resolution.");
                            }
                            else
                            {
                                Console.WriteLine("⚠️ Expected quality 100 for sufficient resolution, but got lower.");
                            }
                        }
                        else
                        {
                            if (quality < 100.0)
                            {
                                Console.WriteLine("✅ Quality is below 100 as expected for insufficient resolution.");
                            }
                            else
                            {
                                Console.WriteLine("⚠️ Unexpected quality 100 for insufficient resolution.");
                            }
                        }
                    }
                }
            }
        }
    }
}