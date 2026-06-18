using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating Code39 barcodes with and without checksum
/// and comparing the resulting images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates two barcode images (with and without checksum) and
    /// checks whether the resulting byte arrays are identical.
    /// </summary>
    static void Main()
    {
        const string codeText = "ABC-123";

        // Generate barcode image with checksum enabled (explicit)
        byte[] imageWithChecksum = GenerateBarcodeImage(EnableChecksum.Yes, codeText);

        // Generate barcode image with checksum disabled
        byte[] imageWithoutChecksum = GenerateBarcodeImage(EnableChecksum.No, codeText);

        // Compare the two images byte‑by‑byte
        bool areIdentical = AreByteArraysEqual(imageWithChecksum, imageWithoutChecksum);
        Console.WriteLine($"Images are identical: {areIdentical}");
    }

    /// <summary>
    /// Generates a barcode image as a PNG byte array.
    /// </summary>
    /// <param name="checksumSetting">Whether to enable checksum.</param>
    /// <param name="text">The text to encode in the barcode.</param>
    /// <returns>Byte array containing the PNG image.</returns>
    static byte[] GenerateBarcodeImage(EnableChecksum checksumSetting, string text)
    {
        // Create a barcode generator for Code39FullASCII encoding
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, text))
        {
            // Apply the checksum setting
            generator.Parameters.Barcode.IsChecksumEnabled = checksumSetting;

            // Save the generated barcode to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                // Return the image data as a byte array
                return ms.ToArray();
            }
        }
    }

    /// <summary>
    /// Compares two byte arrays for equality.
    /// </summary>
    /// <param name="a">First byte array.</param>
    /// <param name="b">Second byte array.</param>
    /// <returns>True if arrays are non‑null, same length, and contain identical bytes; otherwise false.</returns>
    static bool AreByteArraysEqual(byte[] a, byte[] b)
    {
        // If either array is null, they cannot be equal
        if (a == null || b == null) return false;

        // Different lengths mean arrays are not equal
        if (a.Length != b.Length) return false;

        // Compare each byte sequentially
        for (int i = 0; i < a.Length; i++)
        {
            if (a[i] != b[i]) return false;
        }

        // All bytes match
        return true;
    }
}