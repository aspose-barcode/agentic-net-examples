// Title: Asynchronous Barcode Scanning with Latency Comparison
// Description: Demonstrates generating a Code128 barcode, reading it synchronously and asynchronously, and measuring the time difference.
// Prompt: Implement asynchronous barcode scanning and measure latency reduction compared to synchronous calls.
// Tags: barcode symbology, synchronous, asynchronous, latency measurement, aspose.barcode

using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that creates a barcode, reads it synchronously and asynchronously,
/// and reports the elapsed time for each operation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Executes barcode generation, synchronous read,
    /// asynchronous read, and cleanup while measuring latency for each read method.
    /// </summary>
    static async Task Main()
    {
        // Define the file path for the generated barcode image
        string imagePath = "barcode.png";

        // ------------------------------------------------------------
        // Generate a simple Code128 barcode and save it to a file
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "AsyncTest123"))
        {
            generator.Save(imagePath);
        }

        // ------------------------------------------------------------
        // Synchronous barcode reading and latency measurement
        // ------------------------------------------------------------
        var syncStopwatch = Stopwatch.StartNew(); // Start timing synchronous read
        using (var syncReader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            foreach (var result in syncReader.ReadBarCodes())
            {
                // Output result to ensure processing occurs
                Console.WriteLine($"Sync Read - Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }
        }
        syncStopwatch.Stop(); // Stop timing
        Console.WriteLine($"Synchronous read elapsed: {syncStopwatch.ElapsedMilliseconds} ms");

        // ------------------------------------------------------------
        // Asynchronous barcode reading (wrapped in Task.Run) and latency measurement
        // ------------------------------------------------------------
        var asyncStopwatch = Stopwatch.StartNew(); // Start timing asynchronous read
        BarCodeResult[] asyncResults = await Task.Run(() =>
        {
            using (var asyncReader = new BarCodeReader(imagePath, DecodeType.Code128))
            {
                // Perform the read operation on a background thread
                return asyncReader.ReadBarCodes();
            }
        });
        asyncStopwatch.Stop(); // Stop timing

        // Output asynchronous read results
        foreach (var result in asyncResults)
        {
            Console.WriteLine($"Async Read - Type: {result.CodeTypeName}, Text: {result.CodeText}");
        }
        Console.WriteLine($"Asynchronous read elapsed: {asyncStopwatch.ElapsedMilliseconds} ms");

        // ------------------------------------------------------------
        // Cleanup generated image file
        // ------------------------------------------------------------
        if (File.Exists(imagePath))
        {
            File.Delete(imagePath);
        }
    }
}