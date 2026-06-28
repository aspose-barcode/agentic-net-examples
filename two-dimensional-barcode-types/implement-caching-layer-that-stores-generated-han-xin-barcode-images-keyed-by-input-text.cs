using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating Han Xin barcodes with caching.
/// </summary>
class Program
{
    /// <summary>
    /// Simple in‑memory cache mapping barcode text to the file path of the generated image.
    /// </summary>
    private static readonly Dictionary<string, string> _barcodeCache = new Dictionary<string, string>(StringComparer.Ordinal);

    /// <summary>
    /// Entry point of the application. Generates barcodes for a set of sample texts,
    /// reusing cached images when possible, and writes the file locations to the console.
    /// </summary>
    static void Main()
    {
        // Define sample texts to encode as Han Xin barcodes.
        var texts = new[]
        {
            "HelloWorld",
            "Aspose123",
            "HanXinBarcode",
            "HelloWorld" // duplicate to demonstrate caching
        };

        // Determine the output directory and ensure it exists.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Process each text, generating or retrieving the barcode image.
        foreach (var text in texts)
        {
            string imagePath = GetOrCreateBarcode(text, outputDir);
            Console.WriteLine($"Barcode for \"{text}\" saved at: {imagePath}");
        }
    }

    /// <summary>
    /// Retrieves a cached barcode image path for the specified text, or creates a new
    /// barcode image, saves it to the output directory, caches the path, and returns it.
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="outputDirectory">The directory where the barcode image will be saved.</param>
    /// <returns>The full file path of the generated or cached barcode image.</returns>
    private static string GetOrCreateBarcode(string codeText, string outputDirectory)
    {
        // Check if the barcode for this text is already cached.
        if (_barcodeCache.TryGetValue(codeText, out string cachedPath))
        {
            // Return the existing cached image path.
            return cachedPath;
        }

        // Create a unique, safe file name for the new barcode image.
        string safeFileName = $"{Guid.NewGuid():N}.png";
        string fullPath = Path.Combine(outputDirectory, safeFileName);

        // Configure the barcode generator for Han Xin symbology.
        BaseEncodeType encodeType = EncodeTypes.HanXin;
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Optional: set the error correction level for Han Xin.
            generator.Parameters.Barcode.HanXin.ErrorLevel = HanXinErrorLevel.L2;

            // Save the generated barcode as a PNG file.
            generator.Save(fullPath, BarCodeImageFormat.Png);
        }

        // Cache the newly created image path for future reuse.
        _barcodeCache[codeText] = fullPath;
        return fullPath;
    }
}