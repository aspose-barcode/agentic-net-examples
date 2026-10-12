// Title: DataBar Barcode Image Caching Example
// Description: Demonstrates generating DataBar barcodes and caching the resulting PNG images in a temporary directory to improve performance by reusing previously generated images.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on DataBar symbologies (Omni‑Directional, Stacked, Limited, Expanded). It showcases the use of BarcodeGenerator, BaseEncodeType, and related parameter settings to create barcodes, and implements a simple file‑based cache to reduce processing overhead in high‑traffic web scenarios. Developers often need to generate barcodes on‑the‑fly and benefit from caching strategies to serve images quickly.
// Prompt: Implement caching for generated DataBar barcode images to improve high‑traffic web performance.
// Tags: databar, barcode generation, caching, png, aspose.barcode, barcodelibrary, web performance

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides an example of generating DataBar barcodes with a file‑based cache to improve performance.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a cache folder, generates or retrieves cached DataBar barcode images, and writes their paths to the console.
    /// </summary>
    static void Main()
    {
        // Create a persistent cache directory in the system temporary folder
        string cacheDir = Path.Combine(Path.GetTempPath(), "DataBarCache");
        Directory.CreateDirectory(cacheDir);

        // Define the DataBar symbologies that will be generated
        BaseEncodeType[] dataBarTypes = new BaseEncodeType[]
        {
            EncodeTypes.DatabarOmniDirectional,
            EncodeTypes.DatabarStackedOmniDirectional,
            EncodeTypes.DatabarLimited,
            EncodeTypes.DatabarExpanded
        };

        // Iterate over each symbology, generate (or retrieve) the barcode image, and output its location
        foreach (BaseEncodeType type in dataBarTypes)
        {
            string codeText = GetSampleCodeText(type);
            string cachedPath = GenerateDataBarBarcode(type, codeText, cacheDir);
            Console.WriteLine($"Generated/ cached barcode for {type.GetType().Name}.{type} saved at: {cachedPath}");
        }
    }

    /// <summary>
    /// Returns sample code text appropriate for the specified DataBar symbology.
    /// </summary>
    static string GetSampleCodeText(BaseEncodeType type)
    {
        // Limited type requires a valid GTIN; other types can use a generic example
        if (type == EncodeTypes.DatabarLimited)
        {
            // Valid GTIN for Limited type
            return "(01)08888888888888";
        }

        // Default sample for other DataBar types
        return "(01)12345678901231";
    }

    /// <summary>
    /// Generates a DataBar barcode image or returns a cached version if it already exists.
    /// </summary>
    /// <param name="type">The DataBar symbology to generate.</param>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="cacheDir">The directory used for caching generated images.</param>
    /// <returns>The full file path of the generated or cached PNG image.</returns>
    static string GenerateDataBarBarcode(BaseEncodeType type, string codeText, string cacheDir)
    {
        // Compute a deterministic cache file name based on symbology and encoded text
        string hash = ComputeHash(type.ToString() + "|" + codeText);
        string fileName = $"{type.GetType().Name}_{hash}.png";
        string filePath = Path.Combine(cacheDir, fileName);

        // Return the cached file if it already exists
        if (File.Exists(filePath))
        {
            return filePath;
        }

        // Create a new barcode generator and configure common parameters
        using (var generator = new BarcodeGenerator(type, codeText))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Barcode.BarHeight.Pixels = 30f;

            // Adjust aspect ratio for stacked DataBar types
            if (type == EncodeTypes.DatabarStackedOmniDirectional || type == EncodeTypes.DatabarExpandedStacked)
            {
                generator.Parameters.Barcode.DataBar.AspectRatio = 15;
            }

            // Save the generated barcode directly to the cache folder in PNG format
            generator.Save(filePath, BarCodeImageFormat.Png);
        }

        return filePath;
    }

    /// <summary>
    /// Computes a SHA‑256 hash of the given input string and returns it as a hexadecimal string.
    /// </summary>
    static string ComputeHash(string input)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = sha256.ComputeHash(bytes);
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hashBytes)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}