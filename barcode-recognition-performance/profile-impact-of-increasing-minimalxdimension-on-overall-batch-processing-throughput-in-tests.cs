using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how different MinimalXDimension settings affect the throughput of
/// barcode generation and recognition in a batch scenario.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Executes a profiling loop that generates and
    /// reads a set of barcodes while varying the MinimalXDimension parameter.
    /// </summary>
    static void Main()
    {
        // Define MinimalXDimension values to test (in pixels)
        float[] minimalXDimensions = new float[] { 1f, 2f, 4f, 8f };

        // Number of barcodes to process per test (small batch for runner safety)
        int batchSize = 5;

        // Simple codetext for generated barcodes
        string codeText = "Test123";

        Console.WriteLine("Profiling MinimalXDimension impact on batch processing throughput:");
        Console.WriteLine();

        // Iterate over each MinimalXDimension value
        foreach (float minimalX in minimalXDimensions)
        {
            // Start timing for the current batch
            var stopwatch = Stopwatch.StartNew();

            // Process a batch of barcodes
            for (int i = 0; i < batchSize; i++)
            {
                // Create a barcode generator for Code128 with the specified text
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
                {
                    // Store the generated image in a memory stream
                    using (var ms = new MemoryStream())
                    {
                        // Save the barcode as PNG directly into the stream
                        generator.Save(ms, BarCodeImageFormat.Png);
                        ms.Position = 0; // Reset stream position for reading

                        // Initialize a barcode reader on the generated image
                        using (var reader = new BarCodeReader(ms, DecodeType.AllSupportedTypes))
                        {
                            // Configure the reader to use the MinimalXDimension mode
                            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                            reader.QualitySettings.MinimalXDimension = minimalX;

                            // Perform recognition (results are not used further)
                            var results = reader.ReadBarCodes();

                            // Warn if no barcode was detected (should not happen)
                            if (results.Length == 0)
                            {
                                Console.WriteLine($"Warning: No barcode detected for MinimalXDimension={minimalX}");
                            }
                        }
                    }
                }
            }

            // Stop timing and output the elapsed time for this MinimalXDimension
            stopwatch.Stop();
            Console.WriteLine($"MinimalXDimension = {minimalX} px => Elapsed: {stopwatch.ElapsedMilliseconds} ms for {batchSize} items");
        }

        Console.WriteLine();
        Console.WriteLine("Profiling completed.");
    }
}