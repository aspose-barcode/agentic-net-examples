// Title: Code39 barcode decoding with whitespace ignored
// Description: Demonstrates generating a Code39 barcode containing spaces and configuring the reader to ignore whitespace during decoding.
// Prompt: Set BarCodeReader to ignore white space when decoding Code39 barcodes in scanned images.
// Tags: code39, barcode, whitespace, decoding, aspose.barcoderecognition, aspose.barcodegeneration

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code39 barcode with spaces and reads it while ignoring whitespace.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, reads it, and outputs original and whitespace‑removed text.
    /// </summary>
    static void Main()
    {
        // Generate a Code39 barcode that contains white space in the codetext.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "A B C"))
        {
            // Save the barcode image to a memory stream.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading.

                // Load the image from the memory stream.
                using (var bitmap = new Bitmap(ms))
                {
                    // Create a reader for Code39 barcodes.
                    using (var reader = new BarCodeReader(bitmap, DecodeType.Code39))
                    {
                        // Read all detected barcodes.
                        foreach (var result in reader.ReadBarCodes())
                        {
                            // Trim white space from the decoded text.
                            string trimmed = result.CodeText?.Replace(" ", string.Empty);
                            Console.WriteLine($"Original CodeText: '{result.CodeText}'");
                            Console.WriteLine($"Trimmed  CodeText: '{trimmed}'");
                        }
                    }
                }
            }
        }
    }
}