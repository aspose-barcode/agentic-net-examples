using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generation and caching of GS1 Composite barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    // In-memory cache: key = codetext, value = PNG image bytes
    private static readonly Dictionary<string, byte[]> _barcodeCache = new Dictionary<string, byte[]>();

    /// <summary>
    /// Entry point of the application. Generates barcodes for a set of sample GS1 Composite codetexts,
    /// utilizing an in‑memory cache to avoid duplicate generation.
    /// </summary>
    static void Main()
    {
        // Sample GS1 Composite codetexts (linear|2D parts)
        var requests = new[]
        {
            "(01)03212345678906|(21)A1B2C3D4E5F6G7H8",
            "(01)03212345678906|(21)A1B2C3D4E5F6G7H8", // duplicate to test cache
            "(01)12345678901231|(21)XYZ1234567890",
            "(01)12345678901231|(21)XYZ1234567890", // duplicate
            "(01)99999999999999|(21)TESTCODE"
        };

        // Determine output directory (relative to current working directory)
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputDir))
        {
            // Create the directory if it does not exist
            Directory.CreateDirectory(outputDir);
        }

        int index = 1;
        foreach (var codetext in requests)
        {
            // Build file name for each barcode image
            string filePath = Path.Combine(outputDir, $"barcode_{index}.png");

            // Generate a new barcode or retrieve it from the cache
            GenerateOrRetrieveGs1CompositeBarcode(codetext, filePath);

            Console.WriteLine($"Saved barcode #{index} to: {filePath}");
            index++;
        }
    }

    /// <summary>
    /// Generates a GS1 Composite barcode for the specified <paramref name="codetext"/> and saves it to <paramref name="outputPath"/>.
    /// If the barcode has been generated previously, the cached image bytes are written directly to the file.
    /// </summary>
    /// <param name="codetext">The GS1 Composite codetext (linear|2D parts).</param>
    /// <param name="outputPath">The full file path where the PNG image will be saved.</param>
    private static void GenerateOrRetrieveGs1CompositeBarcode(string codetext, string outputPath)
    {
        // Attempt to retrieve a cached image for the given codetext
        if (_barcodeCache.TryGetValue(codetext, out byte[] cachedBytes))
        {
            // Write cached image bytes to the output file
            using (var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                fileStream.Write(cachedBytes, 0, cachedBytes.Length);
            }

            Console.WriteLine("Cache hit for codetext.");
            return;
        }

        // Cache miss: generate a new barcode using Aspose.BarCode
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codetext))
        {
            // Configure GS1 Composite parameters
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Example additional settings (adjust as needed)
            generator.Parameters.Barcode.Pdf417.AspectRatio = 3f;
            generator.Parameters.Barcode.XDimension.Pixels = 3f;
            generator.Parameters.Barcode.BarHeight.Pixels = 100f;

            // Save the generated barcode to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] imageBytes = ms.ToArray();

                // Store the generated image bytes in the cache for future reuse
                _barcodeCache[codetext] = imageBytes;

                // Write the image bytes to the specified output file
                using (var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    fileStream.Write(imageBytes, 0, imageBytes.Length);
                }
            }
        }

        Console.WriteLine("Generated new barcode and cached it.");
    }
}