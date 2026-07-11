// Title: Validate Numeric Content of Generated Code128 Barcodes
// Description: Generates Code128 barcodes from sample texts, saves them as PNG files, then reads them back to verify that the encoded data consists only of numeric characters.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It demonstrates how to use BarcodeGenerator (for creating barcodes) and BarCodeReader (for decoding them) with Code128 symbology. Typical scenarios include automated barcode creation, batch processing, and data validation where developers need to ensure that barcodes contain only expected character sets.
// Prompt: Validate that generated 2‑state barcodes contain only numeric characters by inspecting the encoded data.
// Tags: barcode symbology, validation, code128, generation, recognition, csharp, aspose.barcode

using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating Code128 barcodes, saving them to files, and validating that the encoded data contains only numeric characters.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcodes from sample texts, saves them, and validates their content.
    /// </summary>
    static void Main()
    {
        // Sample codetexts to generate barcodes for
        string[] samples = { "123456", "ABC123", "9876543210" };

        // Process each sample text
        foreach (string text in samples)
        {
            // Build a file name based on the sample text
            string fileName = $"barcode_{text}.png";

            // Generate a Code128 barcode with the given codetext and save it as PNG
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
            {
                generator.Save(fileName);
            }

            // Verify that the barcode file was successfully created
            if (!File.Exists(fileName))
            {
                Console.WriteLine($"Failed to create barcode file: {fileName}");
                continue;
            }

            // Read the barcode from the file and validate the encoded data
            using (var reader = new BarCodeReader(fileName, DecodeType.Code128))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    // Retrieve the decoded text; use empty string if null
                    string decodedText = result.CodeText ?? string.Empty;

                    // Check if the decoded text consists solely of digits
                    bool isNumeric = Regex.IsMatch(decodedText, @"^\d+$");

                    if (isNumeric)
                    {
                        Console.WriteLine($"Barcode '{fileName}' contains numeric data: {decodedText}");
                    }
                    else
                    {
                        Console.WriteLine($"Barcode '{fileName}' contains non-numeric data: {decodedText}");
                    }
                }
            }
        }
    }
}