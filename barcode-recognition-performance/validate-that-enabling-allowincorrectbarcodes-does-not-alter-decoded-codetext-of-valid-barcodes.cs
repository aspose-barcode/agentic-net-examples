// Title: Validate AllowIncorrectBarcodes does not affect decoded CodeText
// Description: Demonstrates generating a Code128 barcode, decoding it with default settings and with AllowIncorrectBarcodes enabled, and verifies that the decoded text remains unchanged.
// Prompt: Validate that enabling AllowIncorrectBarcodes does not alter the decoded CodeText of valid barcodes.
// Tags: code128, barcode generation, barcode recognition, allowincorrectbarcodes, validation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that validates the effect of <c>AllowIncorrectBarcodes</c> on decoding valid barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, decodes it with default and altered settings, and compares the results.
    /// </summary>
    static void Main()
    {
        // Define the text to encode in the barcode.
        const string originalText = "Test12345";

        // Create a memory stream to hold the generated barcode image.
        using (var memoryStream = new MemoryStream())
        {
            // Generate a Code128 barcode and save it as PNG into the memory stream.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, originalText))
            {
                generator.Save(memoryStream, BarCodeImageFormat.Png);
            }

            // Reset the stream position so it can be read from the beginning.
            memoryStream.Position = 0;

            // Decode the barcode using default settings (AllowIncorrectBarcodes = false).
            string decodedDefault;
            using (var reader = new BarCodeReader(memoryStream, DecodeType.Code128))
            {
                var results = reader.ReadBarCodes();
                decodedDefault = results.Length > 0 ? results[0].CodeText : null;
            }

            // Reset the stream position again for the second decoding pass.
            memoryStream.Position = 0;

            // Decode the same barcode with AllowIncorrectBarcodes enabled.
            string decodedAllow;
            using (var reader = new BarCodeReader(memoryStream, DecodeType.Code128))
            {
                reader.QualitySettings.AllowIncorrectBarcodes = true;
                var results = reader.ReadBarCodes();
                decodedAllow = results.Length > 0 ? results[0].CodeText : null;
            }

            // Compare the two decoded values to ensure they are identical.
            bool isEqual = string.Equals(decodedDefault, decodedAllow, StringComparison.Ordinal);
            Console.WriteLine($"Original CodeText: {originalText}");
            Console.WriteLine($"Decoded (default settings): {decodedDefault ?? "null"}");
            Console.WriteLine($"Decoded (AllowIncorrectBarcodes = true): {decodedAllow ?? "null"}");
            Console.WriteLine(isEqual
                ? "Success: AllowIncorrectBarcodes does not alter the decoded CodeText."
                : "Failure: Decoded CodeText differs when AllowIncorrectBarcodes is enabled.");
        }
    }
}