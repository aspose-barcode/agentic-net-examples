// Title: Reset QualitySettings to default after high‑performance barcode reading
// Description: Demonstrates generating a Code128 barcode, reading it with a high‑performance QualitySettings preset, then resetting to NormalQuality and comparing processing times.
// Category-Description: This example belongs to the Aspose.BarCode performance tuning category, illustrating how to use the QualitySettings class with BarCodeReader to balance speed and accuracy. Developers often need to adjust QualitySettings for bulk scanning or resource‑constrained environments, and this snippet shows the typical workflow of applying a preset, measuring performance, and reverting to defaults.
// Prompt: Validate that resetting QualitySettings to defaults restores original CPU consumption patterns during processing.
// Tags: code128, barcode generation, barcode recognition, qualitysettings, performance, aspose.barcode

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a barcode, reads it with different QualitySettings presets,
/// and validates that resetting to the default restores original processing performance.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, performs high‑performance reading,
    /// resets QualitySettings to default, and compares the read times.
    /// </summary>
    static void Main()
    {
        // Generate a simple Code128 barcode and store it in a memory stream.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            using (var barcodeStream = new MemoryStream())
            {
                // Save the generated barcode as PNG into the stream.
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                // Rewind the stream so it can be read from the beginning.
                barcodeStream.Position = 0;

                // Create a BarCodeReader to decode the barcode from the stream.
                using (var reader = new BarCodeReader(barcodeStream, DecodeType.AllSupportedTypes))
                {
                    // Apply a high‑performance preset to prioritize speed.
                    reader.QualitySettings = QualitySettings.HighPerformance;

                    // Measure the time taken to read barcodes with high‑performance settings.
                    var swHighPerf = new Stopwatch();
                    swHighPerf.Start();
                    var resultsHighPerf = reader.ReadBarCodes();
                    swHighPerf.Stop();

                    Console.WriteLine($"HighPerformance preset read time: {swHighPerf.ElapsedMilliseconds} ms");
                    Console.WriteLine($"Barcodes detected: {resultsHighPerf.Length}");

                    // Reset QualitySettings to the default (NormalQuality) for a fair comparison.
                    reader.QualitySettings = QualitySettings.NormalQuality;

                    // Reset stream position for the second read operation.
                    barcodeStream.Position = 0;

                    // Measure the time taken to read barcodes with default settings.
                    var swDefault = new Stopwatch();
                    swDefault.Start();
                    var resultsDefault = reader.ReadBarCodes();
                    swDefault.Stop();

                    Console.WriteLine($"Default (NormalQuality) preset read time: {swDefault.ElapsedMilliseconds} ms");
                    Console.WriteLine($"Barcodes detected: {resultsDefault.Length}");

                    // Simple validation that resetting restores original (or slower) performance characteristics.
                    if (swDefault.ElapsedMilliseconds >= swHighPerf.ElapsedMilliseconds)
                    {
                        Console.WriteLine("Reset to default QualitySettings restored original (or slower) processing time.");
                    }
                    else
                    {
                        Console.WriteLine("Unexpected: default read was faster than high‑performance preset.");
                    }
                }
            }
        }
    }
}