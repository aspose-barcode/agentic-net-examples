// Title: Han Xin Barcode Generation with In-Memory Caching
// Description: Demonstrates generating Han Xin barcodes, caching the PNG images in memory, and saving them to files.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator with EncodeTypes.HanXin, configure encoding mode, and cache generated images for reuse. Developers often need to avoid redundant barcode creation in high‑throughput scenarios, and this pattern illustrates a simple in‑process cache using a dictionary keyed by the input text.
// Prompt: Implement caching layer that stores generated Han Xin barcode images keyed by input text.
// Tags: hanxin, barcode generation, png, caching, aspose.barcode, aspose.drawing

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides an in‑memory cache for Han Xin barcode images keyed by the source text.
/// </summary>
class HanXinBarcodeCache
{
    // Internal dictionary storing barcode image bytes keyed by the original text.
    private readonly Dictionary<string, byte[]> _cache = new Dictionary<string, byte[]>();

    /// <summary>
    /// Retrieves a cached barcode image for the specified text, or generates and caches it if not present.
    /// </summary>
    /// <param name="text">The text to encode into a Han Xin barcode.</param>
    /// <returns>Byte array containing the PNG image of the generated barcode.</returns>
    public byte[] GetOrAdd(string text)
    {
        // Return cached data if it already exists.
        if (_cache.TryGetValue(text, out var data))
            return data;

        // Create a new BarcodeGenerator for Han Xin symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, text))
        {
            // Use automatic encoding mode to let the library choose the best representation.
            generator.Parameters.Barcode.HanXin.EncodeMode = HanXinEncodeMode.Auto;

            // Save the generated barcode to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                data = ms.ToArray();

                // Store the generated image bytes in the cache for future requests.
                _cache[text] = data;
                return data;
            }
        }
    }
}

class Program
{
    /// <summary>
    /// Replaces characters that are invalid in file names with an underscore.
    /// </summary>
    /// <param name="name">Original file name string.</param>
    /// <returns>Sanitized file name safe for use on the file system.</returns>
    static string SanitizeFileName(string name)
    {
        foreach (char c in Path.GetInvalidFileNameChars())
            name = name.Replace(c, '_');
        return name;
    }

    /// <summary>
    /// Entry point that generates sample Han Xin barcodes, caches them, and writes PNG files to a temporary directory.
    /// </summary>
    static void Main()
    {
        // Initialize the barcode cache.
        var cache = new HanXinBarcodeCache();

        // Sample texts to encode as barcodes.
        var samples = new List<string>
        {
            "HelloWorld",
            "1234567890",
            "abc全汉",
            "https://example.com",
            "😀🚀"
        };

        // Determine output directory in the system's temporary folder.
        string outputDir = Path.Combine(Path.GetTempPath(), "HanXinCacheDemo");
        if (!Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Generate or retrieve cached barcodes and write them to files.
        for (int i = 0; i < samples.Count; i++)
        {
            string text = samples[i];
            byte[] imageBytes = cache.GetOrAdd(text);
            string fileName = $"{i + 1}_{SanitizeFileName(text)}.png";
            string filePath = Path.Combine(outputDir, fileName);
            File.WriteAllBytes(filePath, imageBytes);
            Console.WriteLine($"Generated barcode for \"{text}\" at: {filePath}");
        }

        Console.WriteLine("All barcodes generated and cached.");
    }
}