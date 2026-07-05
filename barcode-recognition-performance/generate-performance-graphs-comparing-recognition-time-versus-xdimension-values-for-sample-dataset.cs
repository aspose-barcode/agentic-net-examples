// Title: Barcode XDimension performance measurement
// Description: Demonstrates how varying the XDimension of a Code128 barcode affects recognition time, useful for performance analysis.
// Prompt: Generate performance graphs comparing recognition time versus XDimension values for a sample dataset.
// Tags: barcode, cod128, xdimension, performance, recognition, aspose.barcode

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Sample console application that measures barcode recognition time while varying the XDimension.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes with different XDimension values, measures recognition time, and outputs CSV data.
    /// </summary>
    static void Main()
    {
        // Sample barcode text to encode
        const string codeText = "Sample123";

        // XDimension values to test (in points)
        float[] xDimensions = { 0.5f, 1f, 2f, 3f };

        // CSV header for output
        Console.WriteLine("XDimension(Point),RecognitionTime(ms)");

        // Iterate over each XDimension value
        foreach (float xDim in xDimensions)
        {
            // Generate barcode image in memory for the current XDimension
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Apply the XDimension setting (point unit)
                generator.Parameters.Barcode.XDimension.Point = xDim;

                // Optional: fix image size to keep dimensions consistent across tests
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 100f;

                // Store the generated barcode in a memory stream
                using (var imageStream = new MemoryStream())
                {
                    // Save the barcode as PNG into the stream
                    generator.Save(imageStream, BarCodeImageFormat.Png);
                    imageStream.Position = 0; // Reset stream position for reading

                    // Initialize barcode reader for Code128
                    using (var reader = new BarCodeReader(imageStream, DecodeType.Code128))
                    {
                        // Start timing the recognition process
                        var stopwatch = Stopwatch.StartNew();

                        // Perform barcode recognition (could return multiple results)
                        var results = reader.ReadBarCodes();

                        // Stop timing
                        stopwatch.Stop();

                        // Record elapsed time in milliseconds (formatted to two decimal places)
                        double elapsedMs = stopwatch.Elapsed.TotalMilliseconds;
                        Console.WriteLine($"{xDim},{elapsedMs:F2}");
                    }
                }
            }
        }

        // Indicate that all measurements are complete
        Console.WriteLine("Performance measurement completed.");
    }
}