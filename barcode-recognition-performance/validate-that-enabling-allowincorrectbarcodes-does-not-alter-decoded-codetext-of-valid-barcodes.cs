// Title: Validate AllowIncorrectBarcodes does not affect decoded CodeText
// Description: Demonstrates generating a Code128 barcode, decoding it with and without AllowIncorrectBarcodes, and confirming the decoded text remains unchanged.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the BarcodeGenerator for creating barcodes and BarCodeReader for decoding them. Developers often need to validate quality settings such as AllowIncorrectBarcodes to ensure robust scanning in real‑world applications. The snippet highlights typical use cases involving EncodeTypes, DecodeType, and QualitySettings.
// Prompt: Validate that enabling AllowIncorrectBarcodes does not alter the decoded CodeText of valid barcodes.
// Tags: code128, barcode generation, barcode recognition, allowincorrectbarcodes, validation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that verifies the <c>AllowIncorrectBarcodes</c> quality setting does not change the decoded <c>CodeText</c> for a valid barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, reads it twice with different <c>AllowIncorrectBarcodes</c> settings, and validates consistency.
    /// </summary>
    static void Main()
    {
        // Original text to encode into the barcode.
        const string originalCodeText = "Test12345";

        // Create a Code128 barcode generator and produce an image in memory.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, originalCodeText))
        using (var bitmap = generator.GenerateBarCodeImage())
        using (var imageStream = new MemoryStream())
        {
            // Save the generated bitmap as PNG into the memory stream.
            bitmap.Save(imageStream, ImageFormat.Png);
            imageStream.Position = 0; // Reset stream for reading.

            // ---------- First read: default setting (AllowIncorrectBarcodes = false) ----------
            string decodedWithoutAllowance;
            using (var reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
            {
                reader.QualitySettings.AllowIncorrectBarcodes = false;
                decodedWithoutAllowance = ReadFirstCodeText(reader);
            }

            // Reset stream position before the second read.
            imageStream.Position = 0;

            // ---------- Second read: AllowIncorrectBarcodes = true ----------
            string decodedWithAllowance;
            using (var reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
            {
                reader.QualitySettings.AllowIncorrectBarcodes = true;
                decodedWithAllowance = ReadFirstCodeText(reader);
            }

            // Validate that both decodings match the original text.
            bool isConsistent = decodedWithoutAllowance == decodedWithAllowance &&
                                decodedWithoutAllowance == originalCodeText;

            // Output results.
            Console.WriteLine($"Original CodeText: {originalCodeText}");
            Console.WriteLine($"Decoded without AllowIncorrectBarcodes: {decodedWithoutAllowance}");
            Console.WriteLine($"Decoded with AllowIncorrectBarcodes: {decodedWithAllowance}");
            Console.WriteLine(isConsistent
                ? "Success: Decoded CodeText is unchanged by AllowIncorrectBarcodes."
                : "Failure: Decoded CodeText differs.");
        }
    }

    // Reads the first barcode result from the reader and returns its CodeText.
    private static string ReadFirstCodeText(BarCodeReader reader)
    {
        foreach (var result in reader.ReadBarCodes())
        {
            return result.CodeText;
        }
        return null;
    }
}