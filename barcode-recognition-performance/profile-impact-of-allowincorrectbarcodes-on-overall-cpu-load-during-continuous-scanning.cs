using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation and recognition timing with different
/// <c>AllowIncorrectBarcodes</c> settings using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a Code128 barcode, then measures recognition time
    /// with <c>AllowIncorrectBarcodes</c> set to false and true.
    /// </summary>
    static void Main()
    {
        // Generate a sample barcode image (Code128) and keep it in memory.
        byte[] barcodeBytes;
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set a reasonable image size (points).
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Save the barcode to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                barcodeBytes = ms.ToArray();
            }
        }

        // Number of repetitions for each timing test.
        const int repetitions = 5;

        // Measure recognition time with AllowIncorrectBarcodes = false (default).
        long elapsedFalse = MeasureRecognition(barcodeBytes, false, repetitions);

        // Measure recognition time with AllowIncorrectBarcodes = true.
        long elapsedTrue = MeasureRecognition(barcodeBytes, true, repetitions);

        // Output the results.
        Console.WriteLine($"Recognition time with AllowIncorrectBarcodes = false: {elapsedFalse} ms");
        Console.WriteLine($"Recognition time with AllowIncorrectBarcodes = true : {elapsedTrue} ms");
    }

    /// <summary>
    /// Measures the time required to recognize a barcode image multiple times.
    /// </summary>
    /// <param name="imageData">Byte array containing the barcode image.</param>
    /// <param name="allowIncorrect">Whether to allow incorrect barcodes during recognition.</param>
    /// <param name="repetitions">Number of times the recognition loop should run.</param>
    /// <returns>Total elapsed time in milliseconds.</returns>
    static long MeasureRecognition(byte[] imageData, bool allowIncorrect, int repetitions)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        // Repeat the recognition process the specified number of times.
        for (int i = 0; i < repetitions; i++)
        {
            // Create a new memory stream for each iteration to avoid stream reuse issues.
            using (var ms = new MemoryStream(imageData))
            {
                // Initialize the barcode reader for all supported decode types.
                using (var reader = new BarCodeReader(ms, DecodeType.AllSupportedTypes))
                {
                    // Apply the quality setting for this test run.
                    reader.QualitySettings.AllowIncorrectBarcodes = allowIncorrect;

                    // Perform recognition; iterate through results to ensure full processing.
                    foreach (var result in reader.ReadBarCodes())
                    {
                        // No further processing needed; iteration forces complete read.
                    }
                }
            }
        }

        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }
}