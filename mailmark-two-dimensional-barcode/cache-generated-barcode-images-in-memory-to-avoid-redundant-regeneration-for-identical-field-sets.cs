// Title: In-Memory Barcode Caching Example
// Description: Demonstrates how to cache generated barcode images in memory to avoid regenerating identical barcodes.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, BaseEncodeType, and BarCodeImageFormat. It shows typical use cases such as reducing processing time and resource consumption when the same barcode data is needed multiple times. Developers working with barcode creation often cache images to improve performance in web services, batch processing, or UI rendering scenarios.
// Prompt: Cache generated barcode images in memory to avoid redundant regeneration for identical field sets.
// Tags: barcode, caching, memory, code128, png, aspnet, aspose.barcode, generation

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides an in‑memory cache for barcode images generated with Aspose.BarCode.
/// </summary>
class BarcodeCache
{
    // Internal dictionary stores barcode byte arrays keyed by a unique string.
    private readonly Dictionary<string, byte[]> _cache = new Dictionary<string, byte[]>();

    /// <summary>
    /// Retrieves a barcode image as a byte array, using the cache when possible.
    /// </summary>
    /// <param name="encodeType">The barcode symbology to use.</param>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <returns>PNG image data representing the barcode.</returns>
    public byte[] GetBarcode(BaseEncodeType encodeType, string codeText)
    {
        // Build a unique cache key from the encode type and the text.
        string key = $"{encodeType}_{codeText}";

        // Return cached data if it exists.
        if (_cache.TryGetValue(key, out var data))
        {
            Console.WriteLine("Cache hit for key: " + key);
            return data;
        }

        // Cache miss – generate a new barcode.
        Console.WriteLine("Generating barcode for key: " + key);
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            using (var ms = new MemoryStream())
            {
                // Save the barcode as PNG into the memory stream.
                generator.Save(ms, BarCodeImageFormat.Png);
                data = ms.ToArray();

                // Store the generated image in the cache for future requests.
                _cache[key] = data;
                return data;
            }
        }
    }
}

/// <summary>
/// Entry point demonstrating barcode generation and caching.
/// </summary>
class Program
{
    static void Main()
    {
        // Instantiate the cache helper.
        var cache = new BarcodeCache();

        // Define barcode parameters.
        BaseEncodeType encode = EncodeTypes.Code128;
        string text = "12345678";

        // First request – generates the barcode and caches it.
        byte[] bytes1 = cache.GetBarcode(encode, text);

        // Second request – retrieves the barcode from the cache.
        byte[] bytes2 = cache.GetBarcode(encode, text);

        // Determine temporary file paths for the output images.
        string outPath1 = Path.Combine(Path.GetTempPath(), "barcode1.png");
        string outPath2 = Path.Combine(Path.GetTempPath(), "barcode2.png");

        // Write the PNG data to disk.
        File.WriteAllBytes(outPath1, bytes1);
        File.WriteAllBytes(outPath2, bytes2);

        // Inform the user where the files were saved.
        Console.WriteLine($"Barcodes saved to:\n{outPath1}\n{outPath2}");
    }
}