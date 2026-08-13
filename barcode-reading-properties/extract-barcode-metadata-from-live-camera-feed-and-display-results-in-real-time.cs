// Title: Extract barcode metadata from generated image (simulated live feed)
// Description: Generates a QR code, reads it, and outputs metadata such as type, text, confidence, and region.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It demonstrates using BarcodeGenerator to create barcodes and BarCodeReader to extract metadata, a common task for developers building scanning applications, inventory systems, or real‑time camera processing pipelines. The snippet shows key API classes (BarcodeGenerator, BarCodeReader, QualitySettings) and typical usage patterns for extracting barcode information.
// Prompt: Extract barcode metadata from live camera feed and display results in real time.
// Tags: barcode, qr, metadata, generation, recognition, realtime

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how to generate a QR code, read it, and display its metadata.
/// This simulates the extraction logic that would be applied to each frame of a live camera feed.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode image, reads it, and prints metadata to the console.
    /// </summary>
    static void Main()
    {
        // NOTE: Real‑time live camera feed processing would require continuous monitoring,
        // which is not possible in a self‑contained console example without external input.
        // This sample generates a barcode image, reads it, and displays metadata,
        // demonstrating the extraction logic that would be applied to each frame.

        // Create a BarcodeGenerator for a QR code with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Code"))
        {
            // Optional: configure visual appearance of the generated barcode
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Generate the barcode image in memory
            using (Bitmap barcodeImage = generator.GenerateBarCodeImage())
            {
                // Initialize a BarCodeReader to decode any supported barcode type from the image
                using (var reader = new BarCodeReader(barcodeImage, DecodeType.AllSupportedTypes))
                {
                    // Set recognition quality (default is NormalQuality)
                    reader.QualitySettings = QualitySettings.NormalQuality;

                    // Iterate through all detected barcodes and output their metadata
                    foreach (var result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"BarCode Type: {result.CodeTypeName}");
                        Console.WriteLine($"BarCode CodeText: {result.CodeText}");
                        Console.WriteLine($"Confidence: {result.Confidence}");
                        Console.WriteLine($"Reading Quality: {result.ReadingQuality}");

                        // Retrieve the bounding rectangle of the detected barcode region
                        var bounds = result.Region.Rectangle;
                        Console.WriteLine($"Region: X={bounds.X}, Y={bounds.Y}, Width={bounds.Width}, Height={bounds.Height}");
                        Console.WriteLine(new string('-', 40));
                    }
                }
            }
        }
    }
}