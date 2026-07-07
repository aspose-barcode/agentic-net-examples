// Title: In-Memory Barcode Image Caching Example
// Description: Demonstrates how to cache generated barcode images in memory to avoid regenerating identical barcodes, improving performance.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, BaseEncodeType, and image handling classes. Developers often need to generate multiple barcodes with repeated data, and caching reduces redundant processing and resource usage. Ideal for batch processing, reporting, or any scenario where the same barcode may be requested multiple times.
// Prompt: Cache generated barcode images in memory to avoid redundant regeneration for identical field sets.
// Tags: barcode, caching, memory, code128, qr, datamatrix, aspnet, aspose.barcode, image generation

using System;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates in‑memory caching of barcode images to prevent duplicate generation.
/// </summary>
class Program
{
    /// <summary>
    /// Retrieves a barcode image from the cache or generates a new one if it does not exist.
    /// </summary>
    /// <param name="encodeType">The barcode symbology to use.</param>
    /// <param name="codeText">The text or data to encode.</param>
    /// <param name="cache">Dictionary that stores previously generated images keyed by symbology and text.</param>
    /// <returns>A <see cref="Bitmap"/> containing the generated barcode.</returns>
    static Bitmap GetBarcodeImage(BaseEncodeType encodeType, string codeText, Dictionary<string, Bitmap> cache)
    {
        // Build a unique cache key from the encode type and the text.
        string key = $"{encodeType}:{codeText}";

        // Return the cached image if it already exists.
        if (cache.TryGetValue(key, out Bitmap cachedImage))
        {
            Console.WriteLine($"Cache hit for key: {key}");
            return cachedImage;
        }

        // No cached image – generate a new barcode.
        Console.WriteLine($"Generating barcode for key: {key}");
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            Bitmap image = generator.GenerateBarCodeImage();
            cache[key] = image; // Store the newly generated image for future requests.
            return image;
        }
    }

    /// <summary>
    /// Entry point of the example. Generates several barcodes, some of which are duplicates,
    /// to demonstrate caching. Saves each image to disk and disposes resources afterwards.
    /// </summary>
    static void Main()
    {
        // In‑memory cache: maps a unique key to a barcode bitmap.
        var barcodeCache = new Dictionary<string, Bitmap>();

        // Define a set of barcode requests; duplicates are intentional to test caching.
        var requests = new (BaseEncodeType type, string text)[]
        {
            (EncodeTypes.Code128, "123ABC"),
            (EncodeTypes.QR, "https://example.com"),
            (EncodeTypes.Code128, "123ABC"), // duplicate
            (EncodeTypes.DataMatrix, "DataMatrixSample"),
            (EncodeTypes.QR, "https://example.com") // duplicate
        };

        // Process each request, retrieving from cache or generating as needed.
        for (int i = 0; i < requests.Length; i++)
        {
            var (type, text) = requests[i];
            Bitmap barcodeImage = GetBarcodeImage(type, text, barcodeCache);

            // Save each image with a unique filename for verification.
            string fileName = $"barcode_{i + 1}.png";
            using (var fileStream = System.IO.File.OpenWrite(fileName))
            {
                barcodeImage.Save(fileStream, ImageFormat.Png);
            }

            Console.WriteLine($"Saved barcode to {fileName}");
        }

        // Dispose all cached bitmaps before exiting to free unmanaged resources.
        foreach (var kvp in barcodeCache)
        {
            kvp.Value.Dispose();
        }

        Console.WriteLine("All barcodes processed. Press any key to exit.");
        Console.ReadKey();
    }
}