// Title: Validate RM4SCC 4‑State Barcode Encoding for Uppercase Letters
// Description: Generates RM4SCC barcodes for each uppercase alphabet character, reads them back, and verifies that the decoded text matches the original.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It demonstrates using BarcodeGenerator with EncodeTypes.RM4SCC and BarCodeReader with DecodeType.RM4SCC to validate encoding correctness. Developers often need to ensure that 4‑state barcodes such as RM4SCC correctly represent data, especially when integrating barcode scanning in logistics or document processing systems.
// Prompt: Validate that generated 4‑state barcodes correctly encode uppercase letters by comparing expected bit patterns.
// Tags: barcode, rm4scc, generation, recognition, validation, csharp, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation and validation of RM4SCC (4‑state) barcodes for all uppercase letters.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a temporary folder, creates barcodes for A‑Z, reads them back,
    /// compares decoded values with the original characters, reports mismatches, and cleans up.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated barcodes
        string tempFolder = Path.Combine(Path.GetTempPath(), "FourStateValidation_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Build a list of uppercase letters to test (A‑Z)
        List<char> letters = new List<char>();
        for (char c = 'A'; c <= 'Z'; c++) letters.Add(c);

        // Collect any mismatches between expected and decoded values
        List<string> mismatches = new List<string>();

        // Iterate over each letter, generate its barcode, and validate decoding
        foreach (char letter in letters)
        {
            string codeText = letter.ToString();
            string filePath = Path.Combine(tempFolder, $"RM4SCC_{letter}.png");

            // Generate the RM4SCC barcode image
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.RM4SCC, codeText))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 4;   // Set module width
                generator.Parameters.Barcode.BarHeight.Pixels = 50; // Set bar height
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Read back the generated barcode and decode its text
            using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.RM4SCC))
            {
                BarCodeResult[] results = reader.ReadBarCodes();
                if (results.Length == 0)
                {
                    mismatches.Add($"{letter}: No barcode detected.");
                    continue;
                }

                string decodedText = results[0].CodeText;
                if (!string.Equals(decodedText, codeText, StringComparison.Ordinal))
                {
                    mismatches.Add($"{letter}: Expected '{codeText}', got '{decodedText}'.");
                }
            }
        }

        // Output validation results to the console
        if (mismatches.Count == 0)
        {
            Console.WriteLine("All uppercase letters encoded and decoded correctly for RM4SCC.");
        }
        else
        {
            Console.WriteLine("Mismatches found:");
            foreach (string msg in mismatches)
            {
                Console.WriteLine(msg);
            }
        }

        // Attempt to clean up the temporary folder and its contents
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // If cleanup fails, ignore – the OS will eventually remove the files
        }
    }
}