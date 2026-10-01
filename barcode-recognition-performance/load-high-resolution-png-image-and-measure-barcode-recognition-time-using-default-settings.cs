// Title: Measure barcode recognition time for a high‑resolution PNG image
// Description: This example generates a high‑resolution QR code PNG, saves it, and measures how long the Aspose.BarCode reader takes to recognize the barcode using default settings.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition performance scenarios. It uses BarcodeGenerator to create a high‑DPI image and BarCodeReader to detect all supported symbologies, a common workflow for developers who need to benchmark barcode scanning speed or validate image quality. These examples help when integrating barcode processing into imaging pipelines, quality‑control systems, or mobile capture apps.
// Prompt: Load a high‑resolution PNG image and measure barcode recognition time using default settings.
// Tags: qr, barcode, recognition, performance, png, high-resolution, aspose.barcode, generation, reading

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates loading a high‑resolution PNG barcode image and measuring recognition time using Aspose.BarCode default settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code PNG at 600 DPI, saves it to a temporary file, and times the barcode detection process.
    /// </summary>
    static void Main()
    {
        // Define a temporary path for the sample high‑resolution PNG image
        string imagePath = Path.Combine(Path.GetTempPath(), "highres_barcode.png");

        // Generate a sample QR barcode with high resolution (600 DPI) and save as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "SampleBarcode"))
        {
            // Set a high resolution for the generated image
            generator.Parameters.Resolution = 600f;

            // Save the barcode image to the specified path
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file exists before attempting recognition
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Start measuring the time taken to recognize barcodes in the image using default settings
        Stopwatch stopwatch = Stopwatch.StartNew();

        // Create a BarCodeReader for all supported symbologies
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Perform the recognition
            BarCodeResult[] results = reader.ReadBarCodes();

            // Output the recognized barcodes (if any)
            foreach (var result in results)
            {
                Console.WriteLine($"Detected: {result.CodeText} (Type: {result.CodeTypeName})");
                Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
            }
        }

        // Stop the timer after recognition completes
        stopwatch.Stop();

        // Output the elapsed time in milliseconds
        Console.WriteLine($"Barcode recognition time: {stopwatch.ElapsedMilliseconds} ms");
    }
}