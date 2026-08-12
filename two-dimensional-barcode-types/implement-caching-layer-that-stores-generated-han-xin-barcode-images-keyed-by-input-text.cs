// Title: Han Xin Barcode Caching Example
// Description: Demonstrates generating Han Xin barcodes and caching the resulting PNG images keyed by the input text.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing how to use BarcodeGenerator with EncodeTypes.HanXin, configure error correction, and implement a simple file‑based cache. Developers often need to avoid regenerating identical barcodes, so caching improves performance in web services, batch processing, or desktop apps.
// Prompt: Implement caching layer that stores generated Han Xin barcode images keyed by input text.
// Tags: hanxin, barcode, caching, image, png, aspose.barcode, generation

using System;
using System.IO;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Provides a file‑based cache for Han Xin barcode images.
/// The cache stores generated PNG files keyed by the original text, avoiding duplicate generation.
/// </summary>
class HanXinBarcodeCache
{
    // Directory where cached images are stored.
    private readonly string _cacheDirectory;

    // In‑memory map of text to cached file path for quick lookup.
    private readonly Dictionary<string, string> _cacheMap = new Dictionary<string, string>(StringComparer.Ordinal);

    /// <summary>
    /// Initializes a new instance of <see cref="HanXinBarcodeCache"/> with the specified cache folder.
    /// </summary>
    /// <param name="cacheDirectory">Path to the folder used for storing cached barcode images.</param>
    public HanXinBarcodeCache(string cacheDirectory)
    {
        if (string.IsNullOrWhiteSpace(cacheDirectory))
            throw new ArgumentException("Cache directory path must be provided.", nameof(cacheDirectory));

        _cacheDirectory = cacheDirectory;

        // Ensure the cache directory exists.
        if (!Directory.Exists(_cacheDirectory))
        {
            Directory.CreateDirectory(_cacheDirectory);
        }
    }

    /// <summary>
    /// Returns the file path of a cached barcode image for the given text.
    /// If the image does not exist, it is generated, saved, and cached.
    /// </summary>
    /// <param name="text">The text to encode in the Han Xin barcode.</param>
    /// <returns>Full file path to the PNG image representing the barcode.</returns>
    public string GetOrCreate(string text)
    {
        if (text == null)
            throw new ArgumentNullException(nameof(text));

        // Return cached path if we already have it and the file still exists.
        if (_cacheMap.TryGetValue(text, out string existingPath) && File.Exists(existingPath))
        {
            return existingPath;
        }

        // Compute a deterministic file name based on a SHA‑256 hash of the input text.
        string fileName = ComputeHash(text) + ".png";
        string filePath = Path.Combine(_cacheDirectory, fileName);

        // Generate the barcode image only if the file is missing.
        if (!File.Exists(filePath))
        {
            GenerateHanXinBarcode(text, filePath);
        }

        // Update the in‑memory map and return the path.
        _cacheMap[text] = filePath;
        return filePath;
    }

    // Generates a Han Xin barcode PNG file for the supplied text.
    private static void GenerateHanXinBarcode(string codeText, string outputPath)
    {
        using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, codeText))
        {
            // Example: set error correction level to L2.
            generator.Parameters.Barcode.HanXin.ErrorLevel = HanXinErrorLevel.L2;

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath);
        }
    }

    // Computes a SHA‑256 hash of the input string and returns it as a hex string.
    private static string ComputeHash(string input)
    {
        using (var sha256 = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            byte[] hash = sha256.ComputeHash(bytes);
            var sb = new StringBuilder(hash.Length * 2);
            foreach (byte b in hash)
                sb.Append(b.ToString("x2"));
            return sb.ToString();
        }
    }
}

/// <summary>
/// Demonstrates usage of <see cref="HanXinBarcodeCache"/> by generating barcodes for sample texts.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary cache folder, generates barcodes for a set of sample strings,
    /// and writes the resulting file paths to the console.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the cache.
        string cacheFolder = Path.Combine(Path.GetTempPath(), "HanXinCache_" + Guid.NewGuid().ToString("N"));
        var cache = new HanXinBarcodeCache(cacheFolder);

        // Sample texts to encode.
        var samples = new List<string>
        {
            "Sample123",
            "Hello World!",
            "漢字テスト",
            "Sample123" // duplicate to demonstrate cache hit
        };

        // Generate or retrieve cached barcodes and output their locations.
        foreach (var text in samples)
        {
            string path = cache.GetOrCreate(text);
            Console.WriteLine($"Barcode for \"{text}\" stored at: {path}");
        }

        // Cleanup comment: In a real application you might keep the cache persistent.
        // The temporary folder will be removed by the OS eventually.
    }
}