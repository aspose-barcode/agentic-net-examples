// Title: In-Memory Barcode Image Caching Example
// Description: Demonstrates caching of generated barcode images in memory to avoid redundant regeneration for identical barcode specifications.
// Category-Description: This example belongs to the Aspose.BarCode generation and caching category, showcasing how to use BarcodeGenerator, BaseEncodeType, and BarCodeImageFormat to create barcodes, store them in a dictionary, and reuse them. Developers often need to improve performance when generating many barcodes with repeated parameters, and this pattern provides a simple in‑process cache.
// Prompt: Cache generated barcode images in memory to avoid redundant regeneration for identical field sets.
// Tags: barcode, caching, memory, code128, qr, datamatrix, generation, aspnet, aspose.barcode

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Provides an in‑memory cache for barcode images generated with Aspose.BarCode.
/// </summary>
class BarcodeCache
{
    // Internal dictionary that maps a unique key (encode type + text) to the generated PNG bytes.
    private static readonly Dictionary<string, byte[]> _cache = new Dictionary<string, byte[]>();

    /// <summary>
    /// Retrieves a cached barcode image or generates a new one if it does not exist.
    /// </summary>
    /// <param name="encodeType">The barcode symbology to use (e.g., Code128, QR).</param>
    /// <param name="codeText">The text or data to encode in the barcode.</param>
    /// <returns>A byte array containing the PNG image of the barcode.</returns>
    public static byte[] GetOrCreate(BaseEncodeType encodeType, string codeText)
    {
        // Build a unique cache key based on the encode type's full name, its enum value, and the text.
        string key = $"{encodeType.GetType().FullName}:{encodeType}:{codeText}";

        // Return the cached image if it already exists.
        if (_cache.TryGetValue(key, out byte[] cachedData))
        {
            Console.WriteLine($"Cache hit for [{encodeType}] \"{codeText}\"");
            return cachedData;
        }

        // Cache miss – generate a new barcode image.
        Console.WriteLine($"Generating barcode for [{encodeType}] \"{codeText}\"");
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Example: set a higher resolution for better image quality.
            generator.Parameters.Resolution = 300;

            using (var ms = new MemoryStream())
            {
                // Save the barcode to the memory stream in PNG format.
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] data = ms.ToArray();

                // Store the generated image in the cache for future requests.
                _cache[key] = data;
                return data;
            }
        }
    }
}

/// <summary>
/// Demonstrates the use of <see cref="BarcodeCache"/> to generate and cache barcode images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a series of barcodes, some of which are duplicates,
    /// to illustrate caching behavior, and writes the images to disk.
    /// </summary>
    static void Main()
    {
        // Define a list of barcode generation requests; duplicates test the cache.
        var requests = new List<(BaseEncodeType type, string text)>
        {
            (EncodeTypes.Code128, "ABC123"),
            (EncodeTypes.QR, "https://example.com"),
            (EncodeTypes.Code128, "ABC123"), // duplicate
            (EncodeTypes.DataMatrix, "DataMatrixSample"),
            (EncodeTypes.QR, "https://example.com") // duplicate
        };

        int index = 1;
        foreach (var (type, text) in requests)
        {
            // Retrieve the barcode image, using the cache when possible.
            byte[] imageData = BarcodeCache.GetOrCreate(type, text);

            // Construct a file name that includes the request order and barcode type.
            string fileName = $"barcode_{index}_{type}.png";

            // Write the PNG bytes to disk.
            File.WriteAllBytes(fileName, imageData);
            Console.WriteLine($"Saved barcode to {fileName}");
            index++;
        }

        Console.WriteLine("Processing completed.");
    }
}