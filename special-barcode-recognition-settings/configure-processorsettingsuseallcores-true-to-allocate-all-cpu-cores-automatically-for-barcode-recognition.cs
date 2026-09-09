// Title: Barcode Generation and Multi-Core Recognition Example
// Description: Demonstrates generating a Code128 barcode image and recognizing it using all CPU cores for faster processing.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader with ProcessorSettings to enable multi‑core processing. Typical use cases include high‑throughput scanning applications where performance is critical, and developers often need to configure processor settings, select decode types, and handle image I/O.
// Prompt: Configure ProcessorSettings.UseAllCores true to allocate all CPU cores automatically for barcode recognition.
// Tags: barcode generation, barcode recognition, multithreading, useallcores, code128, aspose.barcode, image processing

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates creating a barcode image and recognizing it using all available CPU cores.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, configures multi‑core processing,
    /// reads the barcode, outputs results, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the sample barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a simple Code128 barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Enable processor settings to use all available CPU cores for recognition
        BarCodeReader.ProcessorSettings.UseAllCores = true;

        // Read the generated barcode using all supported decode types
        using (var reader = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
        {
            Stopwatch watch = Stopwatch.StartNew(); // Start timing the recognition process
            var results = reader.ReadBarCodes();     // Perform barcode recognition
            watch.Stop();                            // Stop timing

            // Output the number of barcodes found and the elapsed time
            Console.WriteLine($"Barcodes found: {results.Length}");
            Console.WriteLine($"Recognition time: {watch.ElapsedMilliseconds} ms");

            // List each recognized barcode's type and text
            foreach (var result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // Clean up temporary files and directory
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program exit
        }
    }
}