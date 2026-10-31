// Title: Barcode generation with in-memory caching
// Description: Demonstrates generating barcodes using Aspose.BarCode and caching the resulting images in memory to avoid duplicate generation for identical parameters.
// Category-Description: This example belongs to the Aspose.BarCode generation and caching category. It shows how to use BarcodeGenerator, set basic parameters, and store generated PNG images in a Dictionary for reuse. Developers working with high‑volume barcode creation often need to cache results to improve performance and reduce CPU load.
// Prompt: Cache generated barcode images in memory to avoid redundant regeneration for identical field sets.
// Tags: barcode generation, caching, memory cache, aspnet, aspose.barcode, png, code128, qr, datamatrix

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Provides an in‑memory cache for barcode images generated with Aspose.BarCode.
/// </summary>
class BarcodeCache
{
    // Internal dictionary that maps a unique key (encode type + text) to the generated PNG bytes.
    private readonly Dictionary<string, byte[]> _cache = new Dictionary<string, byte[]>();

    /// <summary>
    /// Retrieves a cached barcode image or generates a new one if it does not exist.
    /// </summary>
    /// <param name="encodeType">The barcode symbology to use (e.g., Code128, QR).</param>
    /// <param name="codeText">The text or data to encode in the barcode.</param>
    /// <returns>A byte array containing the PNG image of the barcode.</returns>
    public byte[] GetOrCreateBarcode(BaseEncodeType encodeType, string codeText)
    {
        // Build a deterministic cache key from the encode type and the text.
        string key = $"{encodeType.TypeName}|{codeText}";

        // Return the cached image if it already exists.
        if (_cache.TryGetValue(key, out byte[] cachedData))
        {
            Console.WriteLine($"Cache hit for key: {key}");
            return cachedData;
        }

        // Cache miss – generate a new barcode.
        Console.WriteLine($"Cache miss for key: {key}. Generating barcode.");
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Set a simple visual property (black barcode on default background).
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

            // Save the generated barcode to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
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
    /// Entry point of the demo application. Generates several barcodes, some of which are duplicates,
    /// and saves them to a temporary directory while leveraging the in‑memory cache.
    /// </summary>
    static void Main()
    {
        // Prepare an output directory in the system's temporary folder.
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeCacheDemo");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Instantiate the cache manager.
        var cache = new BarcodeCache();

        // Define a list of field sets (encode type + code text) to process.
        var fieldSets = new List<(BaseEncodeType encodeType, string codeText)>
        {
            (EncodeTypes.Code128, "ABC123"),
            (EncodeTypes.QR, "https://example.com"),
            (EncodeTypes.Code128, "ABC123"), // duplicate to test caching
            (EncodeTypes.DataMatrix, "DataMatrixSample"),
            (EncodeTypes.QR, "https://example.com") // duplicate
        };

        // Iterate over each field set, retrieve or generate the barcode, and write it to disk.
        for (int i = 0; i < fieldSets.Count; i++)
        {
            var (encodeType, codeText) = fieldSets[i];
            byte[] imageData = cache.GetOrCreateBarcode(encodeType, codeText);
            string filePath = Path.Combine(outputDir, $"barcode_{i + 1}.png");

            // Write the PNG bytes to a file.
            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                fileStream.Write(imageData, 0, imageData.Length);
            }

            Console.WriteLine($"Saved barcode to: {filePath}");
        }

        Console.WriteLine("Barcode generation and caching completed.");
    }
}