// Title: Verify barcode preset switching does not affect earlier results
// Description: Demonstrates generating a batch of barcodes, changing generator presets mid‑batch, and confirming that previously generated barcodes remain decodable.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, illustrating how to use BarcodeGenerator, adjust Parameters, and employ BarCodeReader for validation. Developers often need to modify barcode appearance or settings on the fly while processing multiple items; this snippet shows safe preset changes without corrupting earlier outputs.
// Prompt: Verify that switching presets mid‑batch does not corrupt previously obtained results.
// Tags: barcode generation, barcode recognition, preset switching, batch processing, csharp, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch barcode generation with mid‑batch preset changes and validates decoding.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes, alters presets after the third item, saves them, and verifies decoding.
    /// </summary>
    static void Main()
    {
        // Prepare output folder for generated barcode images
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Define a small batch of barcodes with their symbology and data
        var batch = new (BaseEncodeType Encode, string CodeText)[]
        {
            (EncodeTypes.Code128, "ABC123"),
            (EncodeTypes.QR, "https://example.com"),
            (EncodeTypes.DataMatrix, "DM12345"),
            (EncodeTypes.Pdf417, "PDF417_SAMPLE"),
            (EncodeTypes.Aztec, "AZTEC_TEXT")
        };

        // Generate each barcode, switching presets after the third item (index >= 3)
        for (int i = 0; i < batch.Length; i++)
        {
            var (encode, text) = batch[i];
            string filePath = Path.Combine(outputFolder, $"barcode_{i}.png");

            using (var generator = new BarcodeGenerator(encode, text))
            {
                // Apply custom preset only to the later barcodes
                if (i >= 3)
                {
                    // Example preset change: increase X-dimension and set bar color to red
                    generator.Parameters.Barcode.XDimension.Point = 3f;
                    generator.Parameters.Barcode.BarColor = Color.Red;
                }

                // Save the generated barcode image as PNG
                generator.Save(filePath, BarCodeImageFormat.Png);
                Console.WriteLine($"Saved barcode {i} to {filePath}");
            }
        }

        // Verify that each saved barcode decodes back to its original text
        bool allMatch = true;
        for (int i = 0; i < batch.Length; i++)
        {
            var expectedText = batch[i].CodeText;
            string filePath = Path.Combine(outputFolder, $"barcode_{i}.png");

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Error: File not found - {filePath}");
                allMatch = false;
                continue;
            }

            using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
            {
                var results = reader.ReadBarCodes();
                if (results.Length == 0)
                {
                    Console.WriteLine($"Failed to read barcode {i} from {filePath}");
                    allMatch = false;
                    continue;
                }

                // Use the first detected result for comparison
                var result = results[0];
                if (result.CodeText != expectedText)
                {
                    Console.WriteLine($"Mismatch for barcode {i}: expected '{expectedText}', got '{result.CodeText}'");
                    allMatch = false;
                }
                else
                {
                    Console.WriteLine($"Barcode {i} decoded correctly: '{result.CodeText}'");
                }
            }
        }

        // Summarize verification outcome
        Console.WriteLine(allMatch
            ? "All barcodes decoded correctly. Switching presets did not corrupt results."
            : "Some barcodes did not decode as expected. Check the output above.");
    }
}