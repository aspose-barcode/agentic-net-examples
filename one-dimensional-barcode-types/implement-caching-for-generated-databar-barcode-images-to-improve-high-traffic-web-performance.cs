// Title: DataBar barcode generation with in‑memory caching
// Description: Demonstrates generating DataBar barcode images using Aspose.BarCode and caching the resulting PNG bytes in memory to avoid redundant processing.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to create various DataBar symbologies, configure basic rendering parameters, and store the generated images in a simple in‑memory cache. Developers building high‑traffic web services often need to generate barcodes on demand while minimizing CPU and I/O overhead; caching the image bytes is a common technique to improve response times and scalability.
// Prompt: Implement caching for generated DataBar barcode images to improve high‑traffic web performance.
// Tags: databar, barcode, caching, aspose.barcode, image generation, png, in-memory cache

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Simple thread‑unsafe in‑memory cache for barcode image byte arrays.
/// </summary>
class BarcodeCache
{
    // Dictionary uses case‑insensitive keys to store generated image data.
    private readonly Dictionary<string, byte[]> _cache = new Dictionary<string, byte[]>(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// Retrieves a cached entry by key or generates and stores it if missing.
    /// </summary>
    /// <param name="key">Unique identifier for the cached barcode (e.g., symbology + data).</param>
    /// <param name="generate">Delegate that creates the barcode image bytes when needed.</param>
    /// <returns>Byte array containing the PNG image.</returns>
    public byte[] GetOrAdd(string key, Func<byte[]> generate)
    {
        // Return cached data if it already exists.
        if (_cache.TryGetValue(key, out var data))
            return data;

        // Generate the image, store it in the cache, then return it.
        data = generate();
        _cache[key] = data;
        return data;
    }
}

/// <summary>
/// Demonstrates generating several DataBar barcodes, caching the images, and writing them to disk.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo application.
    /// </summary>
    static void Main()
    {
        // Define a collection of DataBar symbologies with associated GS1 code texts and output file names.
        var items = new List<(BaseEncodeType encodeType, string codeText, string fileName)>
        {
            (EncodeTypes.DatabarOmniDirectional, "(01)12345678901231", "DatabarOmniDirectional.png"),
            (EncodeTypes.DatabarStacked, "(01)12345678901231", "DatabarStacked.png"),
            (EncodeTypes.DatabarLimited, "(01)08888888888888", "DatabarLimited.png"),
            (EncodeTypes.DatabarExpanded, "(01)12345678901231", "DatabarExpanded.png"),
            (EncodeTypes.DatabarExpandedStacked, "(01)12345678901231", "DatabarExpandedStacked.png")
        };

        // Initialize the in‑memory cache.
        var cache = new BarcodeCache();

        // Create an output directory in the system temporary folder.
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeCacheDemo");
        Directory.CreateDirectory(outputDir);

        // Iterate over each barcode definition, generate or retrieve from cache, and save to file.
        foreach (var (encodeType, codeText, fileName) in items)
        {
            // Build a unique cache key based on symbology type name and the encoded text.
            string cacheKey = $"{encodeType.TypeName}_{codeText}";

            // Retrieve the image bytes from cache or generate them if not present.
            byte[] imageBytes = cache.GetOrAdd(cacheKey, () => GenerateBarcodeImage(encodeType, codeText));

            // Write the PNG bytes to the designated file.
            string filePath = Path.Combine(outputDir, fileName);
            File.WriteAllBytes(filePath, imageBytes);
            Console.WriteLine($"Saved barcode to: {filePath}");
        }

        Console.WriteLine("All barcodes generated and cached.");
    }

    /// <summary>
    /// Generates a PNG image for the specified DataBar barcode.
    /// </summary>
    /// <param name="encodeType">The DataBar symbology to use.</param>
    /// <param name="codeText">The GS1 code text to encode.</param>
    /// <returns>Byte array containing the PNG image.</returns>
    private static byte[] GenerateBarcodeImage(BaseEncodeType encodeType, string codeText)
    {
        // Create a barcode generator with the requested symbology and data.
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Configure rendering parameters for better visibility.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Barcode.BarHeight.Pixels = 30f;

            // Generate the barcode as a bitmap.
            using (var bitmap = generator.GenerateBarCodeImage())
            {
                // Save the bitmap to a memory stream in PNG format.
                using (var ms = new MemoryStream())
                {
                    bitmap.Save(ms, ImageFormat.Png);
                    return ms.ToArray();
                }
            }
        }
    }
}