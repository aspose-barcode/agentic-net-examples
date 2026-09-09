// Title: Generate a batch of Aztec barcodes with custom text spacing
// Description: Demonstrates how to create multiple Aztec barcodes and set the distance between the barcode and its human‑readable text to 2.5 pixels, improving readability.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on Aztec symbology and visual appearance customization. It showcases the use of BarcodeGenerator, EncodeTypes, and CodeTextParameters to adjust text spacing, a common requirement when integrating barcodes into UI or printed media. Developers often need to fine‑tune barcode layout for better scanning and aesthetic purposes.
// Prompt: Set barcode text spacing to 2.5 pixels for a batch of Aztec barcodes to improve readability.
// Tags: aztec, barcode, text spacing, generation, png, aspose.barcode, csharp

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch generation of Aztec barcodes with custom text spacing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a temporary folder, generates several Aztec barcodes,
    /// sets the text spacing to 2.5 pixels, saves them as PNG files, and logs the output.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the batch
        string batchFolder = Path.Combine(Path.GetTempPath(), "AztecBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // Sample code texts for the Aztec barcodes
        List<string> codeTexts = new List<string>
        {
            "Hello",
            "World",
            "Aspose",
            "Aztec",
            "12345"
        };

        int index = 1;
        // Iterate over each text value and generate a corresponding barcode image
        foreach (string text in codeTexts)
        {
            // Build the full file path for the current barcode image
            string filePath = Path.Combine(batchFolder, $"Aztec_{index}.png");

            // Initialize the barcode generator with Aztec symbology and the current text
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Aztec, text))
            {
                // Set spacing between barcode and text to 2.5 pixels
                generator.Parameters.Barcode.CodeTextParameters.Space.Pixels = 2.5f;

                // Save the barcode image in PNG format
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Output the location of the generated file
            Console.WriteLine($"Generated: {filePath}");
            index++;
        }

        // Indicate that the batch processing has finished
        Console.WriteLine("Batch generation completed.");
    }
}