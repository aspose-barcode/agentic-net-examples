// Title: Validate numeric content of generated 2‑state barcodes
// Description: Demonstrates how to generate Planet and Postnet 2‑state barcodes, save them as PNG images, and verify that the decoded data consists solely of numeric characters.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating 2‑state postal symbologies and BarCodeReader for decoding them. Developers often need to programmatically validate barcode data integrity, especially for numeric‑only postal codes, using the API classes in Aspose.BarCode.Generation and Aspose.BarCode.BarCodeRecognition.
// Prompt: Validate that generated 2‑state barcodes contain only numeric characters by inspecting the encoded data.
// Tags: barcode, two-state, validation, png, generation, recognition, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates 2‑state barcodes, saves them as PNG files,
/// and validates that the decoded text contains only numeric characters.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes, decodes them, checks for numeric‑only content,
    /// and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeValidate_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define test cases for 2‑state postal barcodes (Planet and Postnet)
        var testCases = new List<(string Name, BaseEncodeType Encode, string CodeText)>
        {
            ("Planet", EncodeTypes.Planet, "1234567890"),
            ("Postnet", EncodeTypes.Postnet, "987654321")
        };

        var generatedFiles = new List<string>();

        // Generate barcode images and store their file paths
        foreach (var (name, encode, codeText) in testCases)
        {
            string filePath = Path.Combine(tempFolder, $"{name}.png");
            using (var generator = new BarcodeGenerator(encode, codeText))
            {
                // Optional: set module size for better visibility
                generator.Parameters.Barcode.XDimension.Pixels = 4;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            generatedFiles.Add(filePath);
            Console.WriteLine($"Generated {name} barcode at: {filePath}");
        }

        // Prepare a decoder that supports all barcode types
        BaseDecodeType decodeAll = DecodeType.AllSupportedTypes;

        // Validate each generated barcode by decoding its image
        foreach (string file in generatedFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            using (var reader = new BarCodeReader(file, decodeAll))
            {
                BarCodeResult[] results = reader.ReadBarCodes();
                if (results.Length == 0)
                {
                    Console.WriteLine($"No barcode detected in file: {Path.GetFileName(file)}");
                    continue;
                }

                // Assume the first result corresponds to the generated barcode
                string decodedText = results[0].CodeText ?? string.Empty;

                // Check that every character in the decoded text is a digit
                bool isNumeric = true;
                foreach (char c in decodedText)
                {
                    if (!char.IsDigit(c))
                    {
                        isNumeric = false;
                        break;
                    }
                }

                if (isNumeric)
                {
                    Console.WriteLine($"Validation passed for {Path.GetFileName(file)}: decoded text \"{decodedText}\" is numeric.");
                }
                else
                {
                    Console.WriteLine($"Validation failed for {Path.GetFileName(file)}: decoded text \"{decodedText}\" contains non‑numeric characters.");
                }
            }
        }

        // Cleanup: delete temporary folder and its contents
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // If cleanup fails, ignore – the folder will be removed by the OS eventually
        }
    }
}