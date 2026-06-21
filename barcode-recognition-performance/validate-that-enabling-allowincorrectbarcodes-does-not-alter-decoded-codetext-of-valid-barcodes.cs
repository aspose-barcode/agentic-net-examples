using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode, then reading it twice:
/// once with default settings and once with <c>AllowIncorrectBarcodes</c> enabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, reads it under two different quality settings,
    /// and compares the decoded results.
    /// </summary>
    static void Main()
    {
        const string originalCodeText = "1234567890";

        // Create a barcode generator for Code128 with the original text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, originalCodeText))
        {
            // Enable checksum calculation required for Code128.
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Save the generated barcode to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading.

                // ------------------------------------------------------------
                // First read: default quality settings (AllowIncorrectBarcodes = false)
                // ------------------------------------------------------------
                string decodedDefault;
                using (var bitmap = new Bitmap(ms))
                {
                    using (var reader = new BarCodeReader(bitmap, DecodeType.Code128))
                    {
                        // Read all barcodes found in the image.
                        var results = reader.ReadBarCodes();
                        // Take the first result if any; otherwise null.
                        decodedDefault = results.Length > 0 ? results[0].CodeText : null;
                    }
                }

                // Reset stream position to reuse the same image for the second read.
                ms.Position = 0;

                // ------------------------------------------------------------
                // Second read: enable AllowIncorrectBarcodes to true
                // ------------------------------------------------------------
                string decodedAllowIncorrect;
                using (var bitmap = new Bitmap(ms))
                {
                    using (var reader = new BarCodeReader(bitmap, DecodeType.Code128))
                    {
                        // Allow reading barcodes that may not meet strict quality criteria.
                        reader.QualitySettings.AllowIncorrectBarcodes = true;
                        var results = reader.ReadBarCodes();
                        decodedAllowIncorrect = results.Length > 0 ? results[0].CodeText : null;
                    }
                }

                // Compare the two decoded strings for equality.
                bool isEqual = string.Equals(decodedDefault, decodedAllowIncorrect, StringComparison.Ordinal);

                // Output the original and decoded values along with the comparison result.
                Console.WriteLine($"Original CodeText: {originalCodeText}");
                Console.WriteLine($"Decoded (default settings): {decodedDefault ?? "null"}");
                Console.WriteLine($"Decoded (AllowIncorrectBarcodes = true): {decodedAllowIncorrect ?? "null"}");
                Console.WriteLine($"Result: {(isEqual ? "Pass – CodeText unchanged" : "Fail – CodeText differs")}");
            }
        }
    }
}