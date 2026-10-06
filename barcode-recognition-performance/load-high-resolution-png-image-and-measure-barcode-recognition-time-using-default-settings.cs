// Title: Measure barcode recognition time for a high‑resolution PNG image
// Description: Demonstrates loading a high‑resolution PNG barcode image, recognizing it with Aspose.BarCode, and measuring the time taken using default settings.
// Category-Description: This example belongs to the Aspose.BarCode image recognition category, illustrating how to use BarCodeReader with default decoding options. It shows typical steps such as loading an image, initializing the reader, and timing the recognition process. Developers working with barcode scanning, performance benchmarking, or high‑resolution image handling will find this pattern useful.
// Prompt: Load a high‑resolution PNG image and measure barcode recognition time using default settings.
// Tags: barcode, recognition, performance, png, high-resolution, aspose.barcode, reader, decode

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates loading a high‑resolution PNG barcode image, recognizing it, and measuring the recognition time.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code image if missing, reads barcodes, and outputs timing information.
    /// </summary>
    static void Main()
    {
        // Define the temporary file path for the high‑resolution PNG image.
        string imagePath = Path.Combine(Path.GetTempPath(), "high_res.png");

        // If the image does not exist, generate a QR code with 300 dpi resolution.
        if (!File.Exists(imagePath))
        {
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Code"))
            {
                generator.Parameters.Resolution = 300; // Set high resolution.
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }
        }

        // Verify that the image was successfully created.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Initialize the barcode reader for all supported symbologies.
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Start timing the recognition process.
            Stopwatch sw = Stopwatch.StartNew();

            // Perform barcode detection.
            BarCodeResult[] results = reader.ReadBarCodes();

            // Stop the timer.
            sw.Stop();

            // Output performance and detection results.
            Console.WriteLine($"Recognition time: {sw.ElapsedMilliseconds} ms");
            Console.WriteLine($"Barcodes found: {results?.Length ?? 0}");
            foreach (var result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }
    }
}