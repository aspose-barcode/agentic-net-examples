// Title: GS1 Composite Barcode Caching Example
// Description: Demonstrates generating GS1 Composite barcodes and caching the PNG images in memory to avoid redundant processing for repeated requests.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on GS1 Composite symbology. It showcases the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to create linear and 2D components, configure parameters, and improve performance through in‑memory caching—common tasks for developers building high‑throughput barcode services.
// Prompt: Implement caching of generated GS1 Composite barcode images to improve performance for repeated requests.
// Tags: barcode, gs1 composite, caching, image generation, png, aspnet, aspose.barcode

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates GS1 Composite barcode generation with in‑memory caching of PNG images.
/// </summary>
class Program
{
    // Simple in‑memory cache for barcode images keyed by the codetext.
    private static readonly Dictionary<string, byte[]> _barcodeCache = new Dictionary<string, byte[]>();

    /// <summary>
    /// Returns PNG image bytes for the given GS1 Composite codetext.
    /// If the image was generated before, the cached bytes are returned.
    /// </summary>
    /// <param name="codetext">The GS1 Composite codetext to encode.</param>
    /// <returns>Byte array containing the PNG image.</returns>
    private static byte[] GetBarcodeImageBytes(string codetext)
    {
        // Try to retrieve a previously generated image from the cache.
        if (_barcodeCache.TryGetValue(codetext, out var cachedBytes))
        {
            // Cache hit – return the previously generated image.
            return cachedBytes;
        }

        // Cache miss – generate a new barcode image.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codetext))
        {
            // Configure linear component (GS1‑Code128) and 2D component (CC‑A).
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Example component settings – adjust appearance as needed.
            generator.Parameters.Barcode.Pdf417.AspectRatio = 3f;
            generator.Parameters.Barcode.XDimension.Pixels = 3f;
            generator.Parameters.Barcode.BarHeight.Pixels = 100f;

            // Save the generated image to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                var imageBytes = ms.ToArray();

                // Store the bytes in the cache for future requests.
                _barcodeCache[codetext] = imageBytes;
                return imageBytes;
            }
        }
    }

    /// <summary>
    /// Entry point that generates sample barcodes, writes them to files, and shows cache usage.
    /// </summary>
    static void Main()
    {
        // Sample GS1 Composite codetext values.
        var codetexts = new[]
        {
            "(01)03212345678906|(21)A1B2C3D4E5F6G7H8",
            "(01)12345678901234|(21)XYZ1234567890"
        };

        // Ensure the output directory exists.
        var outputDir = "Barcodes";
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Generate each barcode twice to demonstrate caching.
        for (int i = 0; i < codetexts.Length; i++)
        {
            for (int iteration = 0; iteration < 2; iteration++)
            {
                // Retrieve image bytes (cached on second iteration).
                var bytes = GetBarcodeImageBytes(codetexts[i]);

                // Build a unique file name for each iteration.
                var filePath = Path.Combine(outputDir, $"barcode_{i}_{iteration}.png");

                // Write the PNG bytes to disk.
                File.WriteAllBytes(filePath, bytes);

                // Inform the user whether the image was newly generated or retrieved from cache.
                Console.WriteLine($"Saved {(iteration == 0 ? "new" : "cached")} image to {filePath}");
            }
        }

        // Program ends successfully.
    }
}