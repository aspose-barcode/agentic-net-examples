// Title: GS1 Composite Barcode Generation with Caching Example
// Description: Demonstrates generating a GS1 Composite barcode image and caching the result to avoid redundant processing for identical code texts.
// Category-Description: Shows how to use Aspose.BarCode's BarcodeGenerator to create GS1 Composite barcodes, configure parameters, and implement an in‑memory cache for repeated requests. This example belongs to the barcode generation and performance optimization category, illustrating typical use of EncodeTypes, BarCodeImageFormat, and generator parameters. Developers often need to cache generated images to improve response times in web services or batch processing.
// Prompt: Implement caching of generated GS1 Composite barcode images to improve performance for repeated requests.
// Tags: gs1 composite, barcode generation, caching, aspnet, aspose.barcode, image output, png, encode types

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates GS1 Composite barcode generation with an in‑memory cache to improve performance for repeated requests.
/// </summary>
class Program
{
    // In‑memory cache mapping the barcode text to the generated image bytes.
    static readonly Dictionary<string, byte[]> _barcodeCache = new Dictionary<string, byte[]>();

    /// <summary>
    /// Entry point of the demo. Generates a GS1 Composite barcode twice, showing cache hits on the second iteration.
    /// </summary>
    static void Main()
    {
        // Define the linear and 2D parts of the GS1 Composite code text.
        string linearPart = "(01)01234567890128";
        string twoDPart = "(01)01234567890128";
        string codeText = $"{linearPart}|{twoDPart}";

        // Prepare an output directory in the temporary folder.
        string outputDir = Path.Combine(Path.GetTempPath(), "GS1CompositeCacheDemo");
        Directory.CreateDirectory(outputDir);

        // Generate the same barcode twice to demonstrate caching behavior.
        for (int i = 1; i <= 2; i++)
        {
            // Build the file path for the current iteration.
            string filePath = Path.Combine(outputDir, $"GS1Composite_{i}.png");

            // Retrieve the image bytes from cache or generate a new barcode.
            byte[] imageBytes = GetOrCreateBarcodeImage(codeText);

            // Write the image to disk.
            File.WriteAllBytes(filePath, imageBytes);
            Console.WriteLine($"Barcode image saved to: {filePath} (Iteration {i})");
        }

        Console.WriteLine("Demo completed.");
    }

    /// <summary>
    /// Returns a cached barcode image if available; otherwise generates a new GS1 Composite barcode,
    /// stores it in the cache, and returns the image bytes.
    /// </summary>
    /// <param name="codeText">The combined linear and 2D component text for the GS1 Composite barcode.</param>
    /// <returns>Byte array containing the PNG image of the barcode.</returns>
    static byte[] GetOrCreateBarcodeImage(string codeText)
    {
        // Check if the barcode for the given text is already cached.
        if (_barcodeCache.TryGetValue(codeText, out byte[] cachedBytes))
        {
            Console.WriteLine("Cache hit for code text.");
            return cachedBytes;
        }

        Console.WriteLine("Cache miss - generating barcode.");

        // Create a new BarcodeGenerator for GS1 Composite symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Configure visual and encoding parameters.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_B;
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
            generator.Parameters.Barcode.GS1CompositeBar.AllowOnlyGS1Encoding = false;

            // Save the generated barcode to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] imageBytes = ms.ToArray();

                // Store the generated image in the cache for future requests.
                _barcodeCache[codeText] = imageBytes;
                return imageBytes;
            }
        }
    }
}