// Title: Compare CPU usage between HighPerformance and MaxQuality presets using Stopwatch
// Description: Demonstrates measuring barcode generation time for a Code128 barcode using Aspose.BarCode. Shows how to capture elapsed milliseconds with Stopwatch for performance comparison.
// Category-Description: This example belongs to the Aspose.BarCode performance tuning category, illustrating how to use the BarcodeGenerator class with different preset options (e.g., HighPerformance, MaxQuality) and measure execution time. Developers often need to benchmark barcode rendering to choose appropriate quality settings for their applications, especially when optimizing CPU usage in high‑throughput scenarios.
// Prompt: Compare CPU usage between HighPerformance and MaxQuality presets using Stopwatch timing measurements.
// Tags: barcode, code128, performance, stopwatch, aspose.barcode, generation, png, highperformance, maxquality

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates measuring barcode generation time using Stopwatch for performance comparison.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode twice and outputs the elapsed time for each run.
    /// </summary>
    static void Main()
    {
        // Define the text to encode and the barcode symbology.
        string codeText = "1234567890";
        BaseEncodeType encodeType = EncodeTypes.Code128;

        // Measure the time taken for the first generation.
        long firstRunMs = MeasureGeneration(encodeType, codeText);
        // Measure the time taken for the second generation.
        long secondRunMs = MeasureGeneration(encodeType, codeText);

        // Output the measured times.
        Console.WriteLine($"First generation time: {firstRunMs} ms");
        Console.WriteLine($"Second generation time: {secondRunMs} ms");
    }

    /// <summary>
    /// Generates a barcode using the specified encoding type and text, measures the elapsed time, and returns it in milliseconds.
    /// </summary>
    /// <param name="encodeType">The barcode symbology to use.</param>
    /// <param name="codeText">The text to encode into the barcode.</param>
    /// <returns>Elapsed time in milliseconds for the generation operation.</returns>
    static long MeasureGeneration(BaseEncodeType encodeType, string codeText)
    {
        // Start timing.
        Stopwatch sw = new Stopwatch();
        sw.Start();

        // Create the barcode generator and save the image to a memory stream.
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
            }
        }

        // Stop timing and return the elapsed milliseconds.
        sw.Stop();
        return sw.ElapsedMilliseconds;
    }
}