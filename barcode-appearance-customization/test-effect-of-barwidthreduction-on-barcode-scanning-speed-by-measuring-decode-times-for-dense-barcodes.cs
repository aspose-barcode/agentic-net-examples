// Title: BarWidthReduction Impact on Code128 Decode Speed
// Description: Demonstrates how varying BarWidthReduction affects scanning time for dense Code128 barcodes.
// Prompt: Test the effect of BarWidthReduction on barcode scanning speed by measuring decode times for dense barcodes.
// Tags: code128, barwidthreduction, performance, decoding, aspnet.barcode, generation, recognition

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Program to generate dense Code128 barcodes with different BarWidthReduction values and measure decode performance.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes, decodes them, and reports timing for each BarWidthReduction setting.
    /// </summary>
    static void Main()
    {
        // Sample dense Code128 text (long numeric string)
        string codeText = "12345678901234567890123456789012345678901234567890";

        // Different BarWidthReduction values to test (in points)
        float[] reductions = new float[] { 0f, 0.5f, 1f };

        // Ensure output directory exists
        string outputDir = "BarWidthReductionSamples";
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Iterate over each reduction value, generate barcode, and measure decode time
        foreach (float reduction in reductions)
        {
            // Build file path for the generated barcode image
            string filePath = Path.Combine(outputDir, $"barcode_{reduction}.png");

            // Generate barcode image with specific BarWidthReduction
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Use fixed size (no auto-sizing) to keep dimensions comparable
                generator.Parameters.AutoSizeMode = AutoSizeMode.None;
                generator.Parameters.Barcode.BarHeight.Point = 50f;          // Fixed bar height
                generator.Parameters.Barcode.XDimension.Point = 2f;        // Module size
                generator.Parameters.Barcode.BarWidthReduction.Point = reduction; // Test value

                // Save the generated image to disk
                generator.Save(filePath);
            }

            // Measure decoding time for the generated barcode
            using (var reader = new BarCodeReader(filePath, DecodeType.Code128))
            {
                var stopwatch = Stopwatch.StartNew();
                var results = reader.ReadBarCodes();
                stopwatch.Stop();

                // Expect exactly one result for this test
                string decodedText = results.Length > 0 ? results[0].CodeText : "No result";

                // Output reduction value, decode time, and decoded text
                Console.WriteLine($"Reduction: {reduction} pt | Decode time: {stopwatch.ElapsedMilliseconds} ms | Decoded: {decodedText}");
            }
        }

        // Cleanup: optional removal of generated files
        // foreach (var file in Directory.GetFiles(outputDir))
        // {
        //     File.Delete(file);
        // }
        // Directory.Delete(outputDir);
    }
}