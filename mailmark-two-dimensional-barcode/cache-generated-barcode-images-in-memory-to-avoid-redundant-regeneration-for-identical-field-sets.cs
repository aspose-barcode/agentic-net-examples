// Title: In-Memory Barcode Image Caching Example
// Description: Demonstrates how to cache generated barcode images in memory to avoid regenerating identical barcodes.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, BarCodeImageFormat, and common .NET collections to improve performance. Developers often need to generate the same barcode multiple times (e.g., for reports or labels) and benefit from caching the image bytes in memory to reduce CPU and I/O overhead.
// Prompt: Cache generated barcode images in memory to avoid redundant regeneration for identical field sets.
// Tags: barcode, caching, memory, aspose.barcode, generation, png

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Simple in‑memory cache for barcode images generated with Aspose.BarCode.
/// </summary>
class BarcodeCache
{
    // Stores barcode image bytes keyed by a combination of encode type and text.
    private static readonly Dictionary<string, byte[]> _cache = new Dictionary<string, byte[]>();

    /// <summary>
    /// Retrieves a barcode image from the cache or generates it if not present.
    /// </summary>
    /// <param name="encodeType">The barcode symbology to use.</param>
    /// <param name="codeText">The text or data to encode.</param>
    /// <returns>Byte array containing the PNG image of the barcode.</returns>
    public static byte[] GetBarcodeImage(BaseEncodeType encodeType, string codeText)
    {
        // Build a unique key for the requested barcode.
        string key = encodeType.ToString() + "|" + codeText;

        // Return cached image if it exists.
        if (_cache.TryGetValue(key, out byte[] cachedBytes))
        {
            Console.WriteLine($"Cache hit for [{key}]");
            return cachedBytes;
        }

        // Generate a new barcode image and store it in the cache.
        Console.WriteLine($"Generating barcode for [{key}]");
        using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, codeText))
        {
            using (MemoryStream ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] bytes = ms.ToArray();
                _cache[key] = bytes;
                return bytes;
            }
        }
    }
}

/// <summary>
/// Demonstrates generating barcodes, using the cache, and saving images to a temporary folder.
/// </summary>
class Program
{
    static void Main()
    {
        // Create a temporary folder for the output images.
        string outputFolder = Path.Combine(Path.GetTempPath(), "BarcodeCacheDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        Console.WriteLine($"Output folder: {outputFolder}");

        // Define sample barcodes (symbology and text).
        var samples = new List<(BaseEncodeType encodeType, string text)>
        {
            (EncodeTypes.Code128, "ABC123"),
            (EncodeTypes.QR, "https://example.com"),
            (EncodeTypes.DataMatrix, "DataMatrixSample")
        };

        // Generate each barcode twice to illustrate caching behavior.
        int index = 1;
        foreach (var (encodeType, text) in samples)
        {
            for (int repeat = 1; repeat <= 2; repeat++)
            {
                // Retrieve the barcode image (cached on second iteration).
                byte[] imageBytes = BarcodeCache.GetBarcodeImage(encodeType, text);

                // Save the image to a file.
                string filePath = Path.Combine(outputFolder, $"barcode_{index}_{repeat}.png");
                using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(imageBytes, 0, imageBytes.Length);
                }
                Console.WriteLine($"Saved image to {filePath}");
            }
            index++;
        }

        Console.WriteLine("Barcode generation and caching completed.");
    }
}