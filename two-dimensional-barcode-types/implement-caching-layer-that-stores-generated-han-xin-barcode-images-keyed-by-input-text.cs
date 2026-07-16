// Title: Han Xin Barcode Caching Example
// Description: Demonstrates generating Han Xin barcodes and caching the PNG images keyed by the input text to avoid redundant generation.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on the Han Xin symbology. It showcases the use of BarcodeGenerator, EncodeTypes, and HanXin parameters to create barcodes, and implements a simple file‑system cache. Developers often need to generate barcodes repeatedly; caching improves performance and reduces I/O overhead.
// Prompt: Implement caching layer that stores generated Han Xin barcode images keyed by input text.
// Tags: hanxin, barcode, caching, generation, png, aspose.barcode, aspose.drawing

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates Han Xin barcodes for a set of sample texts and caches the resulting PNG images
/// in a local folder to prevent duplicate generation on subsequent runs.
/// </summary>
class Program
{
    // Simple cache directory relative to the executable
    private const string CacheFolder = "HanXinCache";

    /// <summary>
    /// Entry point of the example. Ensures the cache folder exists, iterates over sample texts,
    /// checks for cached images, and generates new barcodes when needed.
    /// </summary>
    static void Main()
    {
        // Ensure the cache folder exists
        if (!Directory.Exists(CacheFolder))
        {
            Directory.CreateDirectory(CacheFolder);
        }

        // Sample texts to encode
        List<string> texts = new List<string>
        {
            "Hello World",
            "Aspose.BarCode",
            "HanXin123",
            "Sample Text",
            "漢字テスト"
        };

        // Process each text, using the cache when possible
        foreach (string text in texts)
        {
            string cachedPath = GetCacheFilePath(text);

            // If a cached image already exists, skip generation
            if (File.Exists(cachedPath))
            {
                Console.WriteLine($"Cache hit for \"{text}\" -> {cachedPath}");
                continue;
            }

            // Generate Han Xin barcode and save to cache
            using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, text))
            {
                // Set error correction level (example: L2)
                generator.Parameters.Barcode.HanXin.ErrorLevel = HanXinErrorLevel.L2;

                // Optional: set version to Auto (default)
                generator.Parameters.Barcode.HanXin.Version = HanXinVersion.Auto;

                // Save as PNG to the cache folder
                generator.Save(cachedPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Generated and cached \"{text}\" -> {cachedPath}");
            }
        }

        // Program ends here
    }

    /// <summary>
    /// Constructs a safe file path for the cached barcode image based on the input text.
    /// Invalid filename characters are replaced, and the name is truncated to a reasonable length.
    /// </summary>
    /// <param name="text">The text to be encoded in the barcode.</param>
    /// <returns>A full file path within the cache folder.</returns>
    private static string GetCacheFilePath(string text)
    {
        // Replace invalid filename characters with underscore
        foreach (char c in Path.GetInvalidFileNameChars())
        {
            text = text.Replace(c, '_');
        }

        // Limit length to avoid overly long filenames
        string safeName = text.Length > 50 ? text.Substring(0, 50) : text;
        return Path.Combine(CacheFolder, safeName + ".png");
    }
}