// Title: Code39 checksum disabling does not affect image output
// Description: Demonstrates that turning off the checksum for an optional‑checksum symbology (Code 39) yields the same PNG image as the default configuration.
// Prompt: Validate that disabling checksum for an optional‑checksum barcode like Code 39 does not alter the generated image data.
// Tags: barcode, code39, checksum, image, png, aspose.barcode

using System;
using System.IO;
using System.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates Code 39 bar‑codes with and without an explicit checksum
/// setting and verifies that the resulting images are identical.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates two barcode images and compares their binary data.
    /// </summary>
    static void Main()
    {
        // Sample Code39 text (no checksum character)
        const string codeText = "CODE39";

        // Generate image with checksum explicitly disabled
        byte[] imageWithoutChecksum = GenerateBarcodeImage(EncodeTypes.Code39FullASCII, codeText, EnableChecksum.No);

        // Generate image with default settings (checksum not used for Code39)
        byte[] imageDefault = GenerateBarcodeImage(EncodeTypes.Code39FullASCII, codeText, null);

        // Compare the two images byte‑by‑byte
        bool areIdentical = imageWithoutChecksum.SequenceEqual(imageDefault);

        // Output the comparison result
        Console.WriteLine($"Images are identical: {areIdentical}");
    }

    /// <summary>
    /// Creates a barcode image using the specified encoding type, text, and optional checksum setting.
    /// </summary>
    /// <param name="type">The barcode symbology to use.</param>
    /// <param name="text">The data to encode.</param>
    /// <param name="checksumSetting">
    /// Optional flag indicating whether to enable or disable the checksum.
    /// If null, the generator's default behavior is applied.
    /// </param>
    /// <returns>A byte array containing the PNG image data.</returns>
    private static byte[] GenerateBarcodeImage(BaseEncodeType type, string text, EnableChecksum? checksumSetting)
    {
        // Initialize the barcode generator with the requested type and text
        using (var generator = new BarcodeGenerator(type, text))
        {
            // Apply checksum setting only when a value is provided
            if (checksumSetting.HasValue)
            {
                // Disable or enable checksum as requested
                generator.Parameters.Barcode.IsChecksumEnabled = checksumSetting.Value;
            }

            // Write the generated barcode to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                // Return the raw image bytes
                return ms.ToArray();
            }
        }
    }
}