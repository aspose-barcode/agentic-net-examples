// Title: Validate disabling checksum for Code 39 barcode does not affect image output
// Description: Demonstrates generating a Code 39 barcode with default settings and with checksum disabled, then compares the resulting PNG byte arrays to confirm they are identical.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to control optional checksum settings using the BarcodeGenerator class. Developers often need to enable or disable checksums for symbologies like Code 39 without affecting visual output, and this snippet shows the typical API usage and byte‑level comparison for validation.
// Prompt: Validate that disabling checksum for an optional‑checksum barcode like Code 39 does not alter the generated image data.
// Tags: barcode, code39, checksum, image comparison, aspose.barcode, generation, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates disabling checksum for a Code 39 barcode and verifying that the generated image remains unchanged.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates two barcode images—one with default checksum handling and one with checksum explicitly disabled—then compares their byte arrays.
    /// </summary>
    static void Main()
    {
        // Text to encode in the barcode
        string codeText = "CODE39";

        // Temporary folder for any intermediate files (not used directly here)
        string tempPath = Path.GetTempPath();

        // ------------------------------------------------------------
        // Generate barcode with default checksum setting (optional checksum not enabled)
        // ------------------------------------------------------------
        byte[] defaultImageBytes;
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, codeText))
        {
            using (var ms = new MemoryStream())
            {
                // Save the barcode as PNG into the memory stream
                generator.Save(ms, BarCodeImageFormat.Png);
                defaultImageBytes = ms.ToArray();
            }
        }

        // ------------------------------------------------------------
        // Generate barcode with checksum explicitly disabled
        // ------------------------------------------------------------
        byte[] disabledImageBytes;
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, codeText))
        {
            // Disable checksum for the barcode
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;

            using (var ms = new MemoryStream())
            {
                // Save the barcode as PNG into the memory stream
                generator.Save(ms, BarCodeImageFormat.Png);
                disabledImageBytes = ms.ToArray();
            }
        }

        // ------------------------------------------------------------
        // Compare the two images byte by byte
        // ------------------------------------------------------------
        bool areEqual = AreByteArraysEqual(defaultImageBytes, disabledImageBytes);
        if (areEqual)
        {
            Console.WriteLine("Images are identical: disabling checksum does not alter the generated image data.");
        }
        else
        {
            Console.WriteLine("Images differ: disabling checksum altered the generated image data.");
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
        if (a == null || b == null) return false;
        if (a.Length != b.Length) return false;
        for (int i = 0; i < a.Length; i++)
        {
            if (a[i] != b[i]) return false;
        }
        return true;
    }
}