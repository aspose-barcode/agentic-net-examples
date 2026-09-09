// Title: Validate 4‑State RM4SCC Barcodes for Uppercase Letters
// Description: Generates RM4SCC (4‑state) barcodes for letters A‑Z, reads them back, and verifies that the decoded text matches the original character.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It demonstrates using BarcodeGenerator to create 4‑state barcodes, BarCodeReader to decode them, and typical validation logic. Developers working with RM4SCC symbology can use this pattern to ensure correct encoding/decoding in automated tests or batch processing scenarios.
// Prompt: Validate that generated 4‑state barcodes correctly encode uppercase letters by comparing expected bit patterns.
// Tags: barcode, rm4scc, generation, recognition, validation, csharp, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generation and validation of RM4SCC (4‑state) barcodes for uppercase letters.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes, decodes them, and reports success or mismatches.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "Barcode4State_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        List<string> files = new List<string>();

        // Generate RM4SCC barcodes for uppercase letters A-Z
        for (char ch = 'A'; ch <= 'Z'; ch++)
        {
            string text = ch.ToString();
            string filePath = Path.Combine(tempFolder, $"RM4SCC_{ch}.png");
            using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.RM4SCC, text))
            {
                // Set the X-dimension (module width) to 4 pixels for better readability
                gen.Parameters.Barcode.XDimension.Pixels = 4;
                // Save the barcode image as PNG
                gen.Save(filePath, BarCodeImageFormat.Png);
            }
            files.Add(filePath);
        }

        // Prepare a reader that can decode any supported barcode type
        BaseDecodeType decodeType = DecodeType.AllSupportedTypes;

        // Read and validate each generated barcode
        foreach (string file in files)
        {
            using (BarCodeReader reader = new BarCodeReader(file, decodeType))
            {
                BarCodeResult[] results = reader.ReadBarCodes();
                // Expected text is the character part of the file name (e.g., "A" from "RM4SCC_A.png")
                string expected = Path.GetFileNameWithoutExtension(file).Split('_')[1];

                if (results.Length == 0)
                {
                    Console.WriteLine($"No barcode detected in {Path.GetFileName(file)}");
                }
                else
                {
                    string decoded = results[0].CodeText;
                    if (decoded != expected)
                    {
                        Console.WriteLine($"Mismatch for {expected}: decoded '{decoded}'");
                    }
                    else
                    {
                        Console.WriteLine($"Success for {expected}");
                    }
                }
            }
        }

        // Cleanup temporary files and folder
        try
        {
            foreach (string file in files)
            {
                File.Delete(file);
            }
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect validation result
        }
    }
}