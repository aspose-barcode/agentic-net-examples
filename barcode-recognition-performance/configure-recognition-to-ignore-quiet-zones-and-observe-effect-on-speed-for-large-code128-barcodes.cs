// Title: Code128 Barcode Generation and Recognition Speed Test with Quality Settings
// Description: Generates a long Code128 barcode and measures recognition time using different quality presets to illustrate performance impact.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, demonstrating how to use BarcodeGenerator for creating barcodes and BarCodeReader with QualitySettings for optimizing recognition speed and accuracy. Developers often need to balance performance and quality when processing large or high‑volume barcodes, and this snippet shows typical API usage for such scenarios.
// Prompt: Configure recognition to ignore quiet zones and observe effect on speed for large Code128 barcodes.
// Tags: code128, barcode, generation, recognition, performance, qualitysettings, aspose.barcode

using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a large Code128 barcode and measuring recognition performance
/// using different <see cref="QualitySettings"/> presets.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, then reads it with high‑performance
    /// and max‑quality settings while timing the operations.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Build a long string (200 characters) to create a large Code128 barcode.
        var sb = new StringBuilder();
        for (int i = 0; i < 20; i++)
        {
            sb.Append("1234567890");
        }
        string codeText = sb.ToString();

        // Create a barcode generator for Code128 with the long text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Store the generated barcode image in a memory stream.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading.

                // Define two quality presets to compare: high performance vs. max quality.
                var presets = new (string Name, QualitySettings Settings)[]
                {
                    ("HighPerformance", QualitySettings.HighPerformance),
                    ("MaxQuality", QualitySettings.MaxQuality)
                };

                // Iterate over each preset, read the barcode, and measure elapsed time.
                foreach (var preset in presets)
                {
                    ms.Position = 0; // Ensure the stream is at the beginning for each read.

                    using (var reader = new BarCodeReader(ms, DecodeType.Code128))
                    {
                        // Apply the selected quality preset.
                        reader.QualitySettings = preset.Settings;

                        // For the high‑performance preset, reduce deconvolution effort to speed up processing.
                        if (preset.Name == "HighPerformance")
                        {
                            reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;
                        }

                        // Start timing, read barcodes, then stop timing.
                        var stopwatch = Stopwatch.StartNew();
                        BarCodeResult[] results = reader.ReadBarCodes();
                        stopwatch.Stop();

                        // Output the results and elapsed time.
                        Console.WriteLine($"{preset.Name} preset: read {results.Length} barcode(s) in {stopwatch.ElapsedMilliseconds} ms");
                        foreach (var result in results)
                        {
                            Console.WriteLine($"  CodeText: {result.CodeText}");
                        }
                    }
                }
            }
        }

        // Note: Aspose.BarCode does not expose an API to ignore quiet zones during recognition.
        // The observed speed differences are due to the selected QualitySettings presets.
    }
}