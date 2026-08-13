// Title: BarWidthReduction Impact on Code128 Decode Speed
// Description: Demonstrates how varying BarWidthReduction affects decoding time for dense Code128 barcodes.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator (generation) and BarCodeReader (recognition) to create dense barcodes, adjust the BarWidthReduction property, and measure decode performance. Developers often need to fine‑tune barcode rendering parameters for optimal scanning speed in high‑density scenarios.
// Prompt: Test the effect of BarWidthReduction on barcode scanning speed by measuring decode times for dense barcodes.
// Tags: code128, barwidthreduction, performance, benchmark, generation, recognition, aspnet, aspose.barcode

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that benchmarks the impact of the BarWidthReduction property on
/// decoding speed for dense Code128 barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a dense Code128 barcode with different BarWidthReduction values,
    /// decodes each version multiple times, and reports the average decode time.
    /// </summary>
    static void Main()
    {
        // Sample dense Code128 barcode text (50 characters)
        const string codeText = "12345678901234567890123456789012345678901234567890";

        // BarWidthReduction values to test (in points)
        float[] reductions = { 0f, 0.5f, 1f };

        // Number of repetitions for each setting (kept small for CI)
        const int repetitions = 5;

        Console.WriteLine("BarWidthReduction benchmark (dense Code128)");
        Console.WriteLine($"CodeText length: {codeText.Length}");
        Console.WriteLine();

        // Iterate over each BarWidthReduction setting
        foreach (float reduction in reductions)
        {
            long totalTicks = 0;

            // Perform multiple runs to obtain an average decode time
            for (int i = 0; i < repetitions; i++)
            {
                // Generate barcode with the current BarWidthReduction
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
                {
                    generator.Parameters.Barcode.BarWidthReduction.Point = reduction;

                    // Save generated barcode to a memory stream in PNG format
                    using (var ms = new MemoryStream())
                    {
                        generator.Save(ms, BarCodeImageFormat.Png);
                        ms.Position = 0; // Reset stream position for reading

                        // Measure decoding time using a stopwatch
                        var stopwatch = Stopwatch.StartNew();
                        using (var reader = new BarCodeReader(ms, DecodeType.Code128))
                        {
                            // Read all barcodes (there will be only one)
                            foreach (var result in reader.ReadBarCodes())
                            {
                                // Access result to ensure full processing
                                var _ = result.CodeText;
                            }
                        }
                        stopwatch.Stop();

                        // Accumulate elapsed ticks
                        totalTicks += stopwatch.ElapsedTicks;
                    }
                }
            }

            // Calculate average decode time in milliseconds
            double avgMs = (totalTicks * 1000.0) / Stopwatch.Frequency / repetitions;
            Console.WriteLine($"BarWidthReduction = {reduction} pt => Average decode time: {avgMs:F3} ms over {repetitions} runs");
        }

        Console.WriteLine();
        Console.WriteLine("Benchmark completed.");
    }
}