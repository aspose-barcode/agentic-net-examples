// Title: Decode Code39 barcodes while ignoring whitespace
// Description: Demonstrates configuring BarCodeReader to strip spaces from decoded Code39 text, useful when scanning barcodes that contain unintended whitespace.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, illustrating how to use BarCodeReader with DecodeType.Code39 and adjust QualitySettings to tolerate minor barcode imperfections. Developers often need to preprocess decoded strings, such as removing whitespace, to match expected data formats in inventory or tracking systems.
// Prompt: Set BarCodeReader to ignore white space when decoding Code39 barcodes in scanned images.
// Tags: code39, whitespace, barcode reader, decoding, aspose.barcode, recognition

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Shows how to generate a Code39 barcode, read it, and ignore whitespace in the decoded result.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code39 barcode containing spaces,
    /// reads it with BarCodeReader, and outputs the original and whitespace‑removed text.
    /// </summary>
    static void Main()
    {
        // Sample Code39 text containing spaces
        const string originalCodeText = "A B C";

        // Generate a Code39 barcode image in memory
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, originalCodeText))
        {
            // Save barcode to a memory stream as PNG
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading

                // Load the image from the memory stream
                using (var bitmap = new Bitmap(ms))
                {
                    // Initialize BarCodeReader for Code39
                    using (var reader = new BarCodeReader(bitmap, DecodeType.Code39))
                    {
                        // Allow recognition of barcodes with minor issues (e.g., unexpected spaces)
                        reader.QualitySettings.AllowIncorrectBarcodes = true;

                        // Read all detected barcodes
                        foreach (var result in reader.ReadBarCodes())
                        {
                            // Original decoded text (may contain spaces)
                            string decoded = result.CodeText ?? string.Empty;

                            // Ignore whitespace by removing all space characters
                            string cleaned = decoded.Replace(" ", string.Empty);

                            Console.WriteLine($"Original decoded text: \"{decoded}\"");
                            Console.WriteLine($"Whitespace ignored text: \"{cleaned}\"");
                        }
                    }
                }
            }
        }
    }
}