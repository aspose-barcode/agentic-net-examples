// Title: GS1 Composite Barcode Generation with In-Memory Caching
// Description: Demonstrates generating a GS1 Composite barcode image, caching the result in memory, and reusing the cached image for subsequent requests.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to create GS1 Composite barcodes using the BarcodeGenerator class. It illustrates typical use cases such as configuring linear and 2D components, adjusting rendering parameters, and implementing a simple in‑memory cache to improve performance for repeated barcode requests. Developers working with barcode rendering, image output, or performance optimization will find this pattern useful.
// Prompt: Implement caching of generated GS1 Composite barcode images to improve performance for repeated requests.
// Tags: gs1 composite, barcode generation, caching, image output, aspose.barcode, png

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Simple thread‑unsafe in‑memory cache for barcode image byte arrays keyed by the barcode's code text.
/// </summary>
class BarcodeCache
{
    // Internal dictionary storing generated barcode images.
    private static readonly Dictionary<string, byte[]> _cache = new Dictionary<string, byte[]>();

    /// <summary>
    /// Retrieves a cached barcode image or generates it using the supplied function and stores it in the cache.
    /// </summary>
    /// <param name="codeText">The barcode code text used as the cache key.</param>
    /// <param name="generatorFunc">A function that generates the barcode image bytes when a cache miss occurs.</param>
    /// <returns>Byte array containing the barcode image.</returns>
    public static byte[] GetOrAdd(string codeText, Func<byte[]> generatorFunc)
    {
        // Return cached data if it exists.
        if (_cache.TryGetValue(codeText, out var cachedData))
        {
            Console.WriteLine($"Cache hit for codeText: {codeText}");
            return cachedData;
        }

        // Cache miss – generate the barcode and store it.
        Console.WriteLine($"Cache miss for codeText: {codeText}. Generating barcode...");
        var data = generatorFunc();
        _cache[codeText] = data;
        return data;
    }
}

/// <summary>
/// Entry point demonstrating GS1 Composite barcode generation with caching and file output.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a GS1 Composite barcode, caches the image, and writes two files to illustrate cache reuse.
    /// </summary>
    static void Main()
    {
        // Sample GS1 Composite barcode code text.
        // AI (01) requires exactly 14 digits; the linear part is followed by a 2D component separated by '|'.
        string linearPart = "(01)00123456789012";
        string twoDPart = "(21)A12345678";
        string codeText = $"{linearPart}|{twoDPart}";

        // First request – generates the barcode and caches the image.
        byte[] imageBytes1 = BarcodeCache.GetOrAdd(codeText, () => GenerateGs1CompositeBarcode(codeText));
        WriteImageToFile("barcode1.png", imageBytes1);

        // Second request with the same code text – retrieves the image from the cache.
        byte[] imageBytes2 = BarcodeCache.GetOrAdd(codeText, () => GenerateGs1CompositeBarcode(codeText));
        WriteImageToFile("barcode2.png", imageBytes2);

        Console.WriteLine("Barcode images have been saved.");
    }

    /// <summary>
    /// Creates a GS1 Composite barcode image in PNG format using Aspose.BarCode.
    /// </summary>
    /// <param name="codeText">The combined linear and 2D component text.</param>
    /// <returns>Byte array containing the PNG image.</returns>
    private static byte[] GenerateGs1CompositeBarcode(string codeText)
    {
        // Initialize the generator with the GS1 Composite symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Configure the linear component to use GS1‑Code128.
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
            // Configure the 2D component to use CC‑A (Composite Component A).
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Optional rendering settings.
            generator.Parameters.Barcode.Pdf417.AspectRatio = 3f;
            generator.Parameters.Barcode.XDimension.Pixels = 3f;
            generator.Parameters.Barcode.BarHeight.Pixels = 100f;

            // Save the barcode to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                return ms.ToArray();
            }
        }
    }

    /// <summary>
    /// Writes a byte array containing image data to a file on disk.
    /// </summary>
    /// <param name="fileName">The target file name.</param>
    /// <param name="imageData">The image data to write.</param>
    private static void WriteImageToFile(string fileName, byte[] imageData)
    {
        using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
        {
            fs.Write(imageData, 0, imageData.Length);
        }
    }
}