// Title: Han Xin Barcode Image Caching Example
// Description: Demonstrates generating Han Xin barcodes, caching the PNG images in memory, and saving them to disk.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator (EncodeTypes.HanXin) and BarCodeReader (DecodeType.HanXin) to create and decode barcodes, while introducing an in‑memory caching layer (Dictionary<string, byte[]>) to avoid redundant image generation. Developers working with high‑throughput barcode creation or needing repeatable outputs will find this pattern useful for performance optimization.
// Prompt: Implement caching layer that stores generated Han Xin barcode images keyed by input text.
// Tags: hanxin, barcode, caching, generation, recognition, png, aspose.barcode, c#

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides an in‑memory cache for Han Xin barcode images.
/// </summary>
class HanXinBarcodeCache
{
    // Internal dictionary that maps the input text to the generated PNG byte array.
    private readonly Dictionary<string, byte[]> _cache = new Dictionary<string, byte[]>();

    /// <summary>
    /// Retrieves a cached barcode image for the specified text, or generates and caches it if not present.
    /// </summary>
    /// <param name="text">The text to encode into a Han Xin barcode.</param>
    /// <returns>A byte array containing the PNG image of the barcode.</returns>
    public byte[] GetOrAdd(string text)
    {
        // Return the cached image if it already exists.
        if (_cache.TryGetValue(text, out var data))
            return data;

        // Create a new barcode generator for Han Xin symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, text))
        {
            // Optional: configure error correction level (L2 provides moderate redundancy).
            generator.Parameters.Barcode.HanXin.ErrorLevel = HanXinErrorLevel.L2;
            // EncodeMode defaults to Auto; set explicitly for clarity.
            generator.Parameters.Barcode.HanXin.EncodeMode = HanXinEncodeMode.Auto;

            // Save the generated barcode to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                data = ms.ToArray();

                // Store the generated image in the cache for future requests.
                _cache[text] = data;
                return data;
            }
        }
    }
}

/// <summary>
/// Demonstrates the use of <see cref="HanXinBarcodeCache"/> to generate, cache, save, and verify Han Xin barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo application.
    /// </summary>
    static void Main()
    {
        // Sample texts to encode as Han Xin barcodes.
        var texts = new[]
        {
            "HelloWorld",
            "1234567890",
            "abc全漢",
            "https://example.com",
            "ΑΒΓΔΕ"
        };

        // Initialize the caching helper.
        var cache = new HanXinBarcodeCache();

        // Prepare a temporary directory for output files.
        string tempDir = Path.Combine(Path.GetTempPath(), "HanXinCacheDemo");
        Directory.CreateDirectory(tempDir);

        // Process each text, generate (or retrieve) the barcode image, and write it to disk.
        for (int i = 0; i < texts.Length; i++)
        {
            string txt = texts[i];
            byte[] imgBytes = cache.GetOrAdd(txt);
            string filePath = Path.Combine(tempDir, $"barcode_{i}.png");
            File.WriteAllBytes(filePath, imgBytes);
            Console.WriteLine($"Saved barcode for \"{txt}\" to {filePath}");

            // Verify the generated barcode by decoding it from the in‑memory image.
            using (var ms = new MemoryStream(imgBytes))
            using (var reader = new BarCodeReader(ms, DecodeType.HanXin))
            {
                var results = reader.ReadBarCodes();
                foreach (var result in results)
                {
                    Console.WriteLine($"  Decoded text: {result.CodeText}");
                }
            }
        }

        Console.WriteLine("Demo completed.");
    }
}