// Title: DataBar barcode generation with in‑memory caching
// Description: Demonstrates generating DataBar barcodes using Aspose.BarCode and caching the resulting PNG images in memory to avoid redundant processing.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, showcasing how to use BarcodeGenerator, encoding types, and image saving APIs. Developers often need to generate barcodes repeatedly in high‑traffic web scenarios, so caching the byte arrays improves performance and reduces CPU load. The snippet illustrates typical use of BaseEncodeType, BarCodeImageFormat, and in‑memory streams for reusable barcode assets.
// Prompt: Implement caching for generated DataBar barcode images to improve high‑traffic web performance.
// Tags: databar, barcode, caching, image generation, aspnet, aspose.barcode, png, memorycache

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides an in‑memory cache for barcode image byte arrays to avoid regenerating identical barcodes.
/// </summary>
class BarcodeCache
{
    // Internal dictionary storing barcode image bytes keyed by a combination of encode type and text.
    private readonly Dictionary<string, byte[]> _cache = new Dictionary<string, byte[]>();

    /// <summary>
    /// Retrieves a cached barcode image or generates it if not present.
    /// </summary>
    /// <param name="encodeType">The barcode symbology to use.</param>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <returns>Byte array containing the PNG image of the barcode.</returns>
    public byte[] GetOrAdd(BaseEncodeType encodeType, string codeText)
    {
        // Build a unique key for the requested barcode.
        string key = $"{encodeType}_{codeText}";

        // Return cached data if it exists.
        if (_cache.TryGetValue(key, out byte[] cached))
        {
            Console.WriteLine($"Cache hit for {key}");
            return cached;
        }

        // Generate a new barcode image, store it in the cache, and return it.
        Console.WriteLine($"Generating barcode for {key}");
        byte[] data = GenerateBarcodeBytes(encodeType, codeText);
        _cache[key] = data;
        return data;
    }

    // Generates a PNG image for the specified barcode and returns its bytes.
    private byte[] GenerateBarcodeBytes(BaseEncodeType encodeType, string codeText)
    {
        using (var ms = new MemoryStream())
        {
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Example settings for DataBar barcodes.
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.Parameters.Barcode.BarHeight.Pixels = 30f;

                // Save the barcode image to the memory stream in PNG format.
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Return the image data as a byte array.
            return ms.ToArray();
        }
    }
}

/// <summary>
/// Demonstrates usage of <see cref="BarcodeCache"/> by generating several DataBar barcodes and saving them to disk.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo application.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder to store generated barcode images.
        string outputFolder = Path.Combine(Path.GetTempPath(), "BarcodeCacheDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        Console.WriteLine($"Images will be saved to: {outputFolder}");

        // Instantiate the barcode cache.
        var cache = new BarcodeCache();

        // Define a set of sample DataBar barcode requests.
        var requests = new List<(BaseEncodeType encode, string text)>
        {
            (EncodeTypes.DatabarOmniDirectional, "(01)12345678901231"),
            (EncodeTypes.DatabarStackedOmniDirectional, "(01)12345678901231"),
            (EncodeTypes.DatabarTruncated, "(01)12345678901231"),
            (EncodeTypes.DatabarLimited, "(01)08888888888888"),
            (EncodeTypes.DatabarExpanded, "(01)12345678901231(10)ABC123")
        };

        int index = 1;
        // Process each request, using the cache to avoid duplicate generation.
        foreach (var req in requests)
        {
            byte[] imageBytes = cache.GetOrAdd(req.encode, req.text);
            string filePath = Path.Combine(outputFolder, $"barcode_{index}.png");

            // Write the image bytes to a file.
            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                fileStream.Write(imageBytes, 0, imageBytes.Length);
            }

            Console.WriteLine($"Saved barcode #{index} to {filePath}");
            index++;
        }

        Console.WriteLine("Demo completed.");
    }
}