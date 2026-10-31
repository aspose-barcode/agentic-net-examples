// Title: Validate disabling checksum for Code 39 barcode does not affect image output
// Description: Demonstrates generating a Code 39 barcode with checksum disabled and enabled, then compares the resulting PNG images to confirm they are identical.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to configure optional checksum settings using the BarcodeGenerator class and its Parameters.Barcode.IsChecksumEnabled property. Typical use cases include verifying that disabling checksum for symbologies like Code 39 does not change visual output, which developers often need when optimizing barcode data or complying with specific scanner requirements. The snippet shows image generation, memory stream handling, and byte‑wise comparison, useful for automated testing or CI pipelines.
// Prompt: Validate that disabling checksum for an optional‑checksum barcode like Code 39 does not alter the generated image data.
// Tags: barcode, code39, checksum, image comparison, aspose.barcode, png, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates Code 39 barcodes with and without checksum,
/// then compares the resulting PNG image data to verify they are identical.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates two barcode images and checks for byte‑wise equality.
    /// </summary>
    static void Main()
    {
        // The text to encode in the barcode.
        const string codeText = "CODE39";

        // Generate barcode image with checksum disabled.
        byte[] imageNoChecksum;
        using (var genNo = new BarcodeGenerator(EncodeTypes.Code39FullASCII, codeText))
        {
            // Disable checksum for the optional‑checksum symbology.
            genNo.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;
            using (var ms = new MemoryStream())
            {
                // Save the barcode as PNG into the memory stream.
                genNo.Save(ms, BarCodeImageFormat.Png);
                imageNoChecksum = ms.ToArray();
            }
        }

        // Generate barcode image with checksum enabled.
        byte[] imageWithChecksum;
        using (var genYes = new BarcodeGenerator(EncodeTypes.Code39FullASCII, codeText))
        {
            // Enable checksum for the optional‑checksum symbology.
            genYes.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            using (var ms = new MemoryStream())
            {
                // Save the barcode as PNG into the memory stream.
                genYes.Save(ms, BarCodeImageFormat.Png);
                imageWithChecksum = ms.ToArray();
            }
        }

        // Assume images are identical if their lengths match.
        bool identical = imageNoChecksum.Length == imageWithChecksum.Length;

        // If lengths match, perform a byte‑wise comparison.
        if (identical)
        {
            for (int i = 0; i < imageNoChecksum.Length; i++)
            {
                if (imageNoChecksum[i] != imageWithChecksum[i])
                {
                    identical = false;
                    break;
                }
            }
        }

        // Output the comparison result.
        Console.WriteLine(identical ? "Images are identical." : "Images differ.");
    }
}