// Title: Validate numeric-only content of generated 2‑state barcodes
// Description: Demonstrates generating Code128 barcodes, recognizing them, and checking that the decoded data contains only digits.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, illustrating how to use BarcodeGenerator, BarCodeReader, and related classes to create, read, and validate barcodes. Typical use cases include automated verification of barcode data integrity in inventory, shipping, and document processing systems. Developers often need to ensure that encoded information meets format constraints such as numeric‑only content.
// Prompt: Validate that generated 2‑state barcodes contain only numeric characters by inspecting the encoded data.
// Tags: code128, barcode generation, barcode recognition, numeric validation, png, barcodegenerator, barcodereader

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating Code128 barcodes, recognizing them, and validating that the decoded text consists only of numeric characters.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes from sample texts, saves them as PNG, reads them back, and reports whether each decoded value is numeric‑only.
    /// </summary>
    static void Main()
    {
        // Sample code texts: some numeric, some containing letters or other characters
        var codeTexts = new List<string>
        {
            "1234567890",          // numeric only
            "ABC12345",            // contains letters
            "987654321",           // numeric only
            "12-34-56",            // contains non‑digit characters
            "20231130"             // numeric only (date)
        };

        // Create a temporary directory to store generated barcode images
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        if (!Directory.Exists(tempDir))
        {
            Directory.CreateDirectory(tempDir);
        }

        int index = 0;
        foreach (string text in codeTexts)
        {
            // Build the file path for the current barcode image
            string filePath = Path.Combine(tempDir, $"barcode_{index}.png");

            // Generate barcode image (using Code128 as a 2‑state barcode example)
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
            {
                // Save the barcode to a PNG file
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Recognize the barcode and retrieve the encoded text
            using (var reader = new BarCodeReader(filePath, DecodeType.Code128))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    string decodedText = result.CodeText ?? string.Empty;
                    bool isNumeric = IsAllDigits(decodedText);
                    Console.WriteLine($"Barcode {index}: Original=\"{text}\", Decoded=\"{decodedText}\", NumericOnly={isNumeric}");
                }
            }

            index++;
        }

        // Cleanup: optionally delete temporary files
        // foreach (var file in Directory.GetFiles(tempDir, "*.png"))
        // {
        //     File.Delete(file);
        // }
        // Directory.Delete(tempDir);
    }

    // Returns true if the string consists solely of decimal digits
    static bool IsAllDigits(string s)
    {
        return s.All(char.IsDigit);
    }
}