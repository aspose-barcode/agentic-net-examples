// Title: Validate checksum disabling for Code 39 barcode does not affect image output
// Description: The example generates a Code 39 barcode with checksum enabled and disabled, then compares the resulting PNG byte arrays to confirm they are identical.
// Category-Description: This sample belongs to the Aspose.BarCode generation category, demonstrating how to work with optional‑checksum symbologies such as Code 39. It showcases the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to create barcodes, adjust checksum settings, and compare output images—common tasks for developers integrating barcode creation into reporting, labeling, or inventory systems.
// Prompt: Validate that disabling checksum for an optional‑checksum barcode like Code 39 does not alter the generated image data.
// Tags: barcode symbology, checksum, code39, image generation, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates how to generate a Code 39 barcode with and without checksum
/// and verifies that the resulting image data remains unchanged.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates two barcode images, compares them,
    /// and writes the results to the console.
    /// </summary>
    static void Main()
    {
        // Generate barcode image with checksum enabled
        byte[] withChecksum = GenerateBarcode(EnableChecksum.Yes);

        // Generate barcode image with checksum disabled
        byte[] withoutChecksum = GenerateBarcode(EnableChecksum.No);

        // Compare the two byte arrays for equality
        bool identical = AreArraysEqual(withChecksum, withoutChecksum);

        // Output the sizes and comparison result
        Console.WriteLine("Checksum enabled image size: {0} bytes", withChecksum.Length);
        Console.WriteLine("Checksum disabled image size: {0} bytes", withoutChecksum.Length);
        Console.WriteLine("Images are identical: {0}", identical);
    }

    /// <summary>
    /// Generates a Code 39 barcode image using the specified checksum setting.
    /// </summary>
    /// <param name="checksumSetting">Whether to enable or disable the checksum.</param>
    /// <returns>Byte array containing the PNG image data.</returns>
    private static byte[] GenerateBarcode(EnableChecksum checksumSetting)
    {
        // Sample barcode text; Code 39 checksum is optional
        const string codeText = "ABC123";

        // Initialize the barcode generator for Code 39 Full ASCII symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, codeText))
        {
            // Apply the requested checksum setting
            generator.Parameters.Barcode.IsChecksumEnabled = checksumSetting;

            // Render the barcode to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                return ms.ToArray();
            }
        }
    }

    /// <summary>
    /// Compares two byte arrays for exact equality.
    /// </summary>
    /// <param name="a">First byte array.</param>
    /// <param name="b">Second byte array.</param>
    /// <returns>True if arrays are non‑null, same length, and contain identical bytes; otherwise false.</returns>
    private static bool AreArraysEqual(byte[] a, byte[] b)
    {
        if (a == null || b == null) return false;
        if (a.Length != b.Length) return false;
        for (int i = 0; i < a.Length; i++)
        {
            if (a[i] != b[i]) return false;
        }
        return true;
    }
}