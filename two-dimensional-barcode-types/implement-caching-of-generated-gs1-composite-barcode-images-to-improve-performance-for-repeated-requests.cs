// Title: GS1 Composite Barcode Caching Example
// Description: Demonstrates how to cache generated GS1 Composite barcode images using Aspose.BarCode to avoid redundant generation for duplicate inputs.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on GS1 Composite symbology. It showcases the use of BarcodeGenerator, encoding settings, and in‑memory caching to improve performance in scenarios where the same barcode data is requested multiple times. Developers working with high‑throughput barcode creation or web services can reuse this pattern to reduce CPU load and I/O.
// Prompt: Implement caching of generated GS1 Composite barcode images to improve performance for repeated requests.
// Tags: gs1 composite, barcode generation, caching, png, aspose.barcode, aspose.barcode.generation

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates caching of GS1 Composite barcode images using Aspose.BarCode.
/// </summary>
class Program
{
    // In‑memory cache: key = barcode text, value = PNG byte array
    static readonly Dictionary<string, byte[]> _cache = new Dictionary<string, byte[]>();

    /// <summary>
    /// Entry point. Generates barcodes, caches duplicates, and saves PNG files.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary folder for output images
        string outputFolder = Path.Combine(Path.GetTempPath(), "GS1CompositeCacheDemo");
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Sample GS1 Composite code texts (some duplicates to test caching)
        List<string> codeTexts = new List<string>
        {
            "(01)01234567890128|HelloWorld",
            "(01)01234567890128|HelloWorld", // duplicate
            "(01)01234567890128|SampleData",
            "(01)01234567890128|SampleData", // duplicate
            "(01)01234567890128|AnotherTest"
        };

        int index = 1;
        foreach (string codeText in codeTexts)
        {
            // Retrieve barcode image bytes, using cache when possible
            byte[] imageBytes = GetBarcodeImageBytes(codeText);

            // Save the PNG file to the output folder
            string filePath = Path.Combine(outputFolder, $"barcode_{index}.png");
            File.WriteAllBytes(filePath, imageBytes);
            Console.WriteLine($"Saved barcode {index} to: {filePath}");
            index++;
        }

        Console.WriteLine("Processing completed.");
    }

    /// <summary>
    /// Generates a GS1 Composite barcode image or returns a cached version.
    /// </summary>
    /// <param name="codeText">The GS1 Composite code text to encode.</param>
    /// <returns>PNG image bytes representing the barcode.</returns>
    static byte[] GetBarcodeImageBytes(string codeText)
    {
        // Return cached image if it already exists
        if (_cache.TryGetValue(codeText, out byte[] cachedBytes))
        {
            Console.WriteLine("Cache hit for code text.");
            return cachedBytes;
        }

        Console.WriteLine("Cache miss - generating barcode.");

        // Create a new barcode generator for GS1 Composite symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Configure GS1 Composite parameters
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_C;
            generator.Parameters.Barcode.GS1CompositeBar.AllowOnlyGS1Encoding = false;

            // For CC_C (full PDF417) set column count
            generator.Parameters.Barcode.Pdf417.Columns = 30;

            // Optional visual settings
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Save barcode to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] bytes = ms.ToArray();

                // Store generated image in cache for future requests
                _cache[codeText] = bytes;
                return bytes;
            }
        }
    }
}