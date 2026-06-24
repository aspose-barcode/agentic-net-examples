using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates in‑memory caching of barcode images generated with Aspose.BarCode.
/// </summary>
class Program
{
    // Simple in‑memory cache: key = symbology + code text, value = PNG bytes
    private static readonly Dictionary<string, byte[]> _barcodeCache = new Dictionary<string, byte[]>();

    /// <summary>
    /// Entry point. Generates barcodes and shows cache behavior.
    /// </summary>
    static void Main()
    {
        // First request – should generate a new image
        byte[] img1 = GetBarcodeImage(EncodeTypes.Code128, "Sample123");
        Console.WriteLine($"Generated image size: {img1.Length} bytes");

        // Second request with identical parameters – should hit the cache
        byte[] img2 = GetBarcodeImage(EncodeTypes.Code128, "Sample123");
        Console.WriteLine($"Cached image size: {img2.Length} bytes");

        // Different barcode – new generation
        byte[] img3 = GetBarcodeImage(EncodeTypes.QR, "https://example.com");
        Console.WriteLine($"Generated QR image size: {img3.Length} bytes");
    }

    // Returns PNG bytes for the requested barcode, using the cache when possible
    private static byte[] GetBarcodeImage(BaseEncodeType encodeType, string codeText)
    {
        // Build a unique cache key from the encode type and the text to encode
        string cacheKey = $"{encodeType.GetHashCode()}|{codeText}";

        // Try to retrieve a cached image
        if (_barcodeCache.TryGetValue(cacheKey, out byte[] cachedBytes))
        {
            Console.WriteLine("Cache hit for key: " + cacheKey);
            return cachedBytes;
        }

        // Cache miss – generate a new barcode image
        Console.WriteLine("Cache miss – generating barcode for key: " + cacheKey);
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Set a modest image size and resolution
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;
            generator.Parameters.Resolution = 300f;

            using (var ms = new MemoryStream())
            {
                // Save directly to the stream in PNG format
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] imageBytes = ms.ToArray();

                // Store the generated image in the cache for future requests
                _barcodeCache[cacheKey] = imageBytes;
                return imageBytes;
            }
        }
    }
}