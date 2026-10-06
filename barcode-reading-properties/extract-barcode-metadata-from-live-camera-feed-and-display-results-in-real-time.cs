// Title: Real-time Barcode Metadata Extraction from Simulated Camera Feed
// Description: Demonstrates generating a QR code image, reading it with Aspose.BarCode, and displaying barcode metadata such as text, symbology, quality, and orientation.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them, covering typical scenarios like live‑camera scanning, metadata retrieval, and quality assessment. Developers working with barcode imaging often need to generate test images, extract detailed decoding information, and handle various symbologies in real‑time applications.
// Prompt: Extract barcode metadata from live camera feed and display results in real time.
// Tags: barcode, symbology, metadata extraction, real-time, aspose.barcode, generation, recognition, qr, console

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample console application that simulates a live camera feed by generating a QR code,
/// then reads the barcode and prints its metadata to the console.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // NOTE: Live camera feed is not available in this console environment.
        // The example simulates a camera capture by generating a barcode image locally.

        // Create a unique temporary directory to store the generated image.
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define the full path for the sample image and the text to encode.
        string imagePath = Path.Combine(tempDir, "sample.png");
        string codeText = "HelloWorld";

        // ------------------------------------------------------------
        // Generate a QR code image and save it as PNG.
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Read the barcode from the generated image and output metadata.
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Example quality setting (optional) – high performance mode.
            reader.QualitySettings = QualitySettings.HighPerformance;

            // Perform the decoding operation.
            BarCodeResult[] results = reader.ReadBarCodes();

            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
            }
            else
            {
                foreach (var result in results)
                {
                    Console.WriteLine($"Code Text: {result.CodeText}");
                    Console.WriteLine($"Symbology: {result.CodeTypeName}");
                    Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                    Console.WriteLine($"Orientation Angle: {result.Region.Angle}");
                    // Additional metadata can be accessed via result.Extended if needed.
                }
            }
        }

        // ------------------------------------------------------------
        // Clean up temporary files and directory.
        // ------------------------------------------------------------
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignored – cleanup failure should not affect program exit.
        }
    }
}