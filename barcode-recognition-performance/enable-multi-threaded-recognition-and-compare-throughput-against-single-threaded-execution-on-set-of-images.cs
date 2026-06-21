using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation and recognition using Aspose.BarCode
/// in both single‑threaded and multi‑threaded scenarios.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates sample barcode images, then reads them sequentially
    /// and in parallel while measuring execution time.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Generate sample barcode images and store them in memory.
        // ------------------------------------------------------------
        var barcodeImages = new List<byte[]>();
        for (int i = 0; i < 5; i++)
        {
            // Create a unique code text for each barcode (e.g., CODE001).
            string codeText = $"CODE{i + 1:D3}";

            // Initialize a barcode generator for Code128 format.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Save the generated barcode to a memory stream as PNG.
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    // Store the byte array for later recognition.
                    barcodeImages.Add(ms.ToArray());
                }
            }
        }

        // ------------------------------------------------------------
        // 2. Single‑threaded barcode recognition.
        // ------------------------------------------------------------
        var swSingle = Stopwatch.StartNew(); // Start timing.

        foreach (var imgData in barcodeImages)
        {
            // Load each image from its byte array.
            using (var ms = new MemoryStream(imgData))
            {
                // Initialize a reader that can decode all supported barcode types.
                using (var reader = new BarCodeReader(ms, DecodeType.AllSupportedTypes))
                {
                    // Iterate over all detected barcodes in the image.
                    foreach (var result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"[Single] Type: {result.CodeTypeName}, Text: {result.CodeText}");
                    }
                }
            }
        }

        swSingle.Stop(); // Stop timing.
        Console.WriteLine($"Single‑threaded elapsed: {swSingle.ElapsedMilliseconds} ms");

        // ------------------------------------------------------------
        // 3. Multi‑threaded barcode recognition using all CPU cores.
        // ------------------------------------------------------------
        // Configure the Aspose processor to utilize the full core count.
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Environment.ProcessorCount;

        var swMulti = Stopwatch.StartNew(); // Start timing.

        // Process each image in parallel.
        Parallel.ForEach(barcodeImages, imgData =>
        {
            using (var ms = new MemoryStream(imgData))
            {
                using (var reader = new BarCodeReader(ms, DecodeType.AllSupportedTypes))
                {
                    foreach (var result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"[Parallel] Type: {result.CodeTypeName}, Text: {result.CodeText}");
                    }
                }
            }
        });

        swMulti.Stop(); // Stop timing.
        Console.WriteLine($"Multi‑threaded elapsed: {swMulti.ElapsedMilliseconds} ms");
    }
}