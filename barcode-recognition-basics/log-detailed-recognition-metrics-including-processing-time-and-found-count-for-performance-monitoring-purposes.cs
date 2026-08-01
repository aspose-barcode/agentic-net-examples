// Title: Barcode Generation and Recognition with Performance Metrics
// Description: Demonstrates creating a Code128 barcode, saving it as an image, and recognizing it while logging processing time and count of detected barcodes.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of core API classes such as BarcodeGenerator for creating barcodes, BarCodeReader for decoding, and DecodeType for specifying supported symbologies. Typical scenarios include automated testing, batch processing, and performance monitoring where developers need to generate barcodes, read them back, and capture detailed metrics.
// Prompt: Log detailed recognition metrics, including processing time and found count, for performance monitoring purposes.
// Tags: code128, generation, recognition, performance, aspose.barcode, barcodegenerator, barcodereader, decodeType, barcoderesult

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode, saves it to a file,
/// reads it back, and logs detailed recognition metrics for performance monitoring.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output image path for the generated barcode
        string imagePath = "sample.png";

        // Remove any existing file with the same name to ensure a clean run
        if (File.Exists(imagePath))
        {
            File.Delete(imagePath);
        }

        // Create a BarcodeGenerator for Code128 symbology with sample text
        var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123");
        // Save the generated barcode image to the specified path
        generator.Save(imagePath);

        // Verify that the barcode image was successfully created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create the barcode image.");
            return;
        }

        // Initialize a Stopwatch to measure recognition duration
        var stopwatch = new Stopwatch();

        // Open a BarCodeReader for all supported barcode types on the generated image
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Start timing before the recognition process
            stopwatch.Start();

            // Perform barcode detection and retrieve results
            BarCodeResult[] results = reader.ReadBarCodes();

            // Stop timing after recognition completes
            stopwatch.Stop();

            // Output processing time and total number of barcodes detected
            Console.WriteLine($"Processing Time (ms): {stopwatch.ElapsedMilliseconds}");
            Console.WriteLine($"Barcodes Detected: {reader.FoundCount}");

            // Iterate through each detected barcode and display detailed information
            foreach (var result in results)
            {
                Console.WriteLine("----- Barcode -----");
                Console.WriteLine($"Type: {result.CodeTypeName}");
                Console.WriteLine($"Text: {result.CodeText}");
                Console.WriteLine($"Confidence: {result.Confidence}");
                Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                var rect = result.Region.Rectangle;
                Console.WriteLine($"Region - X:{rect.X}, Y:{rect.Y}, Width:{rect.Width}, Height:{rect.Height}");
                Console.WriteLine($"Angle: {result.Region.Angle}");
            }
        }
    }
}