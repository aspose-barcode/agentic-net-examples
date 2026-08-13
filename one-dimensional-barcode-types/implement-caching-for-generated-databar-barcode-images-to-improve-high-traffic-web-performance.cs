// Title: DataBar Barcode Image Caching Example
// Description: Demonstrates generating DataBar Expanded and Limited barcodes and caching the resulting PNG images in memory to avoid redundant generation.
// Category-Description: Shows how to use Aspose.BarCode's BarcodeGenerator with EncodeTypes to create DataBar symbologies, configure barcode parameters, and implement a simple in‑memory cache for high‑traffic scenarios. This example belongs to the barcode generation and image handling category, illustrating typical use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat for web applications that need fast repeated barcode rendering.
// Prompt: Implement caching for generated DataBar barcode images to improve high‑traffic web performance.
// Tags: databar, barcode generation, caching, png, aspose.barcode, encode types, web performance

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating DataBar barcodes and caching the PNG image bytes in memory.
/// </summary>
class Program
{
    // Simple in‑memory cache: key = symbology|codetext, value = PNG bytes
    private static readonly Dictionary<string, byte[]> _cache = new Dictionary<string, byte[]>(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// Entry point of the example. Generates barcodes for a set of symbologies and code texts,
    /// writes image sizes to the console, and saves the PNG files to disk.
    /// </summary>
    static void Main()
    {
        // Example usage: generate DataBar Expanded and Limited barcodes
        string[] symbologies = { "DatabarExpanded", "DatabarLimited" };
        string[] codeTexts = { "(01)12345678901231", "(01)08888888888888" };

        foreach (var sym in symbologies)
        {
            foreach (var text in codeTexts)
            {
                // Retrieve barcode image bytes, using cache when possible
                byte[] imageBytes = GetBarcodeImage(sym, text);

                // Output image size for demonstration purposes
                Console.WriteLine($"{sym} | {text} => Image bytes: {imageBytes.Length}");

                // Save the image to a file (in a real web app this would be sent to the client)
                string fileName = $"{sym}_{text.Replace('(', '_').Replace(')', '_')}.png";
                File.WriteAllBytes(fileName, imageBytes);
            }
        }
    }

    // Returns PNG image bytes for the requested barcode, using cache when possible
    private static byte[] GetBarcodeImage(string symbologyName, string codeText)
    {
        string cacheKey = $"{symbologyName}|{codeText}";

        // Check if the image is already cached
        if (_cache.TryGetValue(cacheKey, out byte[] cachedBytes))
        {
            // Cache hit
            Console.WriteLine($"Cache hit for key: {cacheKey}");
            return cachedBytes;
        }

        // Resolve symbology name to BaseEncodeType via reflection
        var field = typeof(EncodeTypes).GetField(symbologyName);
        if (field == null)
        {
            Console.WriteLine($"Unknown symbology: {symbologyName}");
            return Array.Empty<byte>();
        }
        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // Create generator and configure basic parameters
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Set XDimension and padding for consistent size
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Parameters.Barcode.Padding.Left.Point = 5f;
            generator.Parameters.Barcode.Padding.Top.Point = 5f;
            generator.Parameters.Barcode.Padding.Right.Point = 5f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 5f;

            // Generate image into a memory stream as PNG
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] imageBytes = ms.ToArray();

                // Store in cache for future requests
                _cache[cacheKey] = imageBytes;
                Console.WriteLine($"Cache miss – generated and cached key: {cacheKey}");
                return imageBytes;
            }
        }
    }
}