using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demo program that generates and decodes Planet barcodes for benchmarking purposes.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a set of barcode images, then measures decoding performance.
    /// </summary>
    static void Main()
    {
        // Number of barcodes to process in this demo.
        // In a real benchmark you would increase this to 5000.
        const int sampleCount = 10;

        // Prepare a list that will hold the generated barcode images as byte arrays.
        var barcodeImages = new List<byte[]>(sampleCount);
        for (int i = 0; i < sampleCount; i++)
        {
            // Planet barcodes use a numeric code (e.g., a 5‑digit zip code).
            const string codeText = "12345";

            // Create a barcode generator for the Planet symbology with the specified text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Planet, codeText))
            {
                // Use a memory stream to capture the generated image.
                using (var ms = new MemoryStream())
                {
                    // Save the barcode image to the memory stream in PNG format.
                    generator.Save(ms, BarCodeImageFormat.Png);
                    // Store the image bytes for later decoding.
                    barcodeImages.Add(ms.ToArray());
                }
            }
        }

        // Benchmark the decoding phase.
        var stopwatch = new Stopwatch();
        int totalDecoded = 0;

        stopwatch.Start();
        // Iterate over each generated image and attempt to decode it.
        foreach (var imageData in barcodeImages)
        {
            // Load the image bytes into a memory stream for the reader.
            using (var ms = new MemoryStream(imageData))
            {
                // Create a barcode reader configured for Planet barcodes.
                using (var reader = new BarCodeReader(ms, DecodeType.Planet))
                {
                    // Read all barcodes found in the image.
                    foreach (var result in reader.ReadBarCodes())
                    {
                        // For the benchmark we only need to count successful reads.
                        totalDecoded++;
                        // Optionally, you could verify result.CodeText here.
                    }
                }
            }
        }
        stopwatch.Stop();

        // Output the benchmark results.
        Console.WriteLine($"Decoded {totalDecoded} Planet barcodes in {stopwatch.ElapsedMilliseconds} ms.");
        Console.WriteLine($"Average time per barcode: {stopwatch.Elapsed.TotalMilliseconds / sampleCount:F2} ms.");
    }
}