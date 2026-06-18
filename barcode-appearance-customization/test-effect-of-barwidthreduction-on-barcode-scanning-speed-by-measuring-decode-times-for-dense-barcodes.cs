using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how different BarWidthReduction values affect barcode generation and recognition performance.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a Code128 barcode with varying BarWidthReduction values,
    /// saves it to a memory stream, and measures the time required to decode it.
    /// </summary>
    static void Main()
    {
        // Dense barcode text (50 numeric characters) to test decoding performance.
        string codeText = "12345678901234567890123456789012345678901234567890";

        // Array of BarWidthReduction values (in points) to evaluate.
        float[] reductions = new float[] { 0f, 0.1f, 0.2f };

        // Iterate over each reduction value and perform generation + decoding.
        foreach (float reduction in reductions)
        {
            // Create a barcode generator for Code128 with the specified text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Apply the current BarWidthReduction setting.
                generator.Parameters.Barcode.BarWidthReduction.Point = reduction;

                // Use a memory stream to hold the generated PNG image.
                using (var ms = new MemoryStream())
                {
                    // Save the barcode image into the memory stream.
                    generator.Save(ms, BarCodeImageFormat.Png);
                    ms.Position = 0; // Reset stream position for reading.

                    // Load the image from the stream into a Bitmap for decoding.
                    using (var bitmap = new Bitmap(ms))
                    {
                        // Initialize a barcode reader configured for Code128.
                        using (var reader = new BarCodeReader(bitmap, DecodeType.Code128))
                        {
                            // Start timing the decode operation.
                            var stopwatch = Stopwatch.StartNew();

                            // Perform the barcode reading.
                            var results = reader.ReadBarCodes();

                            // Stop timing after reading completes.
                            stopwatch.Stop();

                            // Output the decode results and elapsed time.
                            if (results.Length > 0)
                            {
                                Console.WriteLine(
                                    $"BarWidthReduction: {reduction}, DecodeTime: {stopwatch.ElapsedMilliseconds} ms, DetectedText: {results[0].CodeText}");
                            }
                            else
                            {
                                Console.WriteLine(
                                    $"BarWidthReduction: {reduction}, DecodeTime: {stopwatch.ElapsedMilliseconds} ms, No barcode detected.");
                            }
                        }
                    }
                }
            }
        }
    }
}