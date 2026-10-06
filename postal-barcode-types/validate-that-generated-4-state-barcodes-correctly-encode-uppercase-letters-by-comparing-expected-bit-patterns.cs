// Title: Validate RM4SCC 4‑state barcode encoding of uppercase letters
// Description: Generates RM4SCC barcodes for each uppercase alphabet character, saves them as PNG files, then reads them back to verify that the decoded text matches the original.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It demonstrates how to use BarcodeGenerator (EncodeTypes.RM4SCC) to create 4‑state barcodes, and BarCodeReader (DecodeType.RM4SCC) to decode them. Typical use cases include automated testing of barcode symbologies, batch processing of inventory codes, and validation of encoding logic. Developers often need to generate barcodes, persist them in image formats, and later verify correctness via the recognition API.
/// Prompt: Validate that generated 4‑state barcodes correctly encode uppercase letters by comparing expected bit patterns.
/// Tags: barcode symbology, generation, recognition, validation, rm4scc, png, csharp, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generation and validation of RM4SCC (4‑state) barcodes for all uppercase letters.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes, saves them, then reads each back to ensure the encoded text matches the original letter.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "FourStateTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Build a list containing all uppercase letters A‑Z
        List<char> letters = new List<char>();
        for (char c = 'A'; c <= 'Z'; c++)
        {
            letters.Add(c);
        }

        // -----------------------------------------------------------------
        // Generate RM4SCC barcodes for each letter and save them as PNG files
        // -----------------------------------------------------------------
        foreach (char letter in letters)
        {
            string codeText = letter.ToString();
            string filePath = Path.Combine(tempFolder, $"RM4SCC_{letter}.png");

            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.RM4SCC, codeText))
            {
                // Optional visual parameters: set module size and bar height
                generator.Parameters.Barcode.XDimension.Pixels = 4;
                generator.Parameters.Barcode.BarHeight.Pixels = 50;

                // Save the generated barcode image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // ---------------------------------------------------------------
        // Validate each generated barcode by decoding it and comparing text
        // ---------------------------------------------------------------
        bool allValid = true;
        foreach (char letter in letters)
        {
            string expected = letter.ToString();
            string filePath = Path.Combine(tempFolder, $"RM4SCC_{letter}.png");

            // Ensure the file was created
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File missing: {filePath}");
                allValid = false;
                continue;
            }

            string decoded = null;

            // Decode the barcode using the RM4SCC symbology
            using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.RM4SCC))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    decoded = result.CodeText;
                    break; // Take the first decoded result
                }
            }

            // Evaluate decoding outcome
            if (decoded == null)
            {
                Console.WriteLine($"Failed to decode barcode for '{expected}'.");
                allValid = false;
            }
            else if (!decoded.Equals(expected, StringComparison.Ordinal))
            {
                Console.WriteLine($"Mismatch for '{expected}': decoded as '{decoded}'.");
                allValid = false;
            }
            else
            {
                Console.WriteLine($"Success: '{expected}' encoded and decoded correctly.");
            }
        }

        // Summarize validation results
        Console.WriteLine(allValid ? "All uppercase letters validated successfully." : "Some validations failed.");
    }
}