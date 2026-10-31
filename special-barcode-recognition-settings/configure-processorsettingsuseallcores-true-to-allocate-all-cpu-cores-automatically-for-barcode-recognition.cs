// Title: Barcode Generation and Multi-Core Recognition Example
// Description: Demonstrates generating a Code128 barcode image and recognizing it using all available CPU cores for faster processing.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader with ProcessorSettings to leverage multi‑core CPUs during recognition. Developers commonly need to generate barcodes for labeling and then read them efficiently in high‑throughput scenarios, making use of the ProcessorSettings API to optimize performance.
// Prompt: Configure ProcessorSettings.UseAllCores true to allocate all CPU cores automatically for barcode recognition.
// Tags: barcode generation, barcode recognition, multithreading, useallcores, code128, aspose.barcode, c#

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates creating a barcode image and reading it using all CPU cores for recognition.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, configures multi‑core processing,
    /// reads the barcode, outputs results, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the sample barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a simple Code128 barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Parameters.Barcode.BarColor = Color.Black;   // Set barcode color
            generator.Parameters.BackColor = Color.White;          // Set background color
            generator.Save(barcodePath, BarCodeImageFormat.Png);   // Save image to file
        }

        // Configure processor settings to use all CPU cores for recognition
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = Environment.ProcessorCount * 2;

        // Read the generated barcode
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            Stopwatch watch = Stopwatch.StartNew();               // Start timing
            var results = reader.ReadBarCodes();                  // Perform recognition
            watch.Stop();                                         // Stop timing

            Console.WriteLine($"Barcodes read: {results.Length}, Recognition time: {watch.ElapsedMilliseconds} ms");
            foreach (var result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // Clean up temporary files
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}