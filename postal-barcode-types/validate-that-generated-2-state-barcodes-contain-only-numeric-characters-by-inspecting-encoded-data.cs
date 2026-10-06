// Title: Validate Numeric Content of 2‑State Postal Barcodes
// Description: Demonstrates generating 2‑state postal barcodes (Planet and Postnet) and verifying that the decoded data consists solely of numeric characters.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for extracting encoded data. Developers often need to validate barcode content after generation, especially for postal symbologies where numeric-only data is required.
// Prompt: Validate that generated 2‑state barcodes contain only numeric characters by inspecting the encoded data.
// Tags: barcode symbology, validation, numeric, generation, recognition, aspose.barcode, csharp

using System;
using System.IO;
using System.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates 2‑state postal barcodes (Planet and Postnet), reads them back,
/// and validates that the decoded text contains only numeric characters.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary files, generates barcodes,
    /// reads them, checks for numeric-only content, and cleans up.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeValidate_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define sample numeric data for 2‑state postal barcodes
        var samples = new[]
        {
            new { Symbology = EncodeTypes.Planet, CodeText = "1234567890", FileName = "planet.png", Decode = DecodeType.Planet },
            new { Symbology = EncodeTypes.Postnet, CodeText = "1159628792", FileName = "postnet.png", Decode = DecodeType.Postnet }
        };

        // Process each sample: generate, read, and validate
        foreach (var sample in samples)
        {
            string filePath = Path.Combine(tempFolder, sample.FileName);

            // Generate barcode image using BarcodeGenerator
            using (var generator = new BarcodeGenerator(sample.Symbology, sample.CodeText))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 4; // Set module size
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Validate that the decoded data contains only numeric characters
            bool isNumeric = false;
            using (var reader = new BarCodeReader(filePath, sample.Decode))
            {
                var results = reader.ReadBarCodes();
                if (results != null && results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText))
                {
                    string code = results[0].CodeText;
                    isNumeric = code.All(char.IsDigit);
                    Console.WriteLine($"{sample.Symbology} barcode read text: \"{code}\"");
                }
                else
                {
                    Console.WriteLine($"No barcode detected in file: {filePath}");
                }
            }

            Console.WriteLine($"{sample.Symbology} barcode contains only numeric characters: {isNumeric}");
        }

        // Clean up generated files (optional)
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}