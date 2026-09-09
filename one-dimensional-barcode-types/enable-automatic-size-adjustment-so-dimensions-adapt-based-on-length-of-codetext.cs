// Title: Automatic Barcode Size Adjustment Based on Code Text Length
// Description: Demonstrates how Aspose.BarCode automatically adjusts barcode dimensions according to the length of the CodeText without manual size settings.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator with EncodeTypes.Code128. It shows typical use cases where developers need barcodes that automatically scale to fit varying data lengths, avoiding manual width/height configuration. The example highlights key classes such as BarcodeGenerator, EncodeTypes, and BarCodeImageFormat, useful for generating PNG barcodes in batch.
// Prompt: Enable automatic size adjustment so dimensions adapt based on the length of CodeText.
// Tags: barcode, code128, autosize, generation, png, aspnet, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a set of Code128 barcodes where each barcode's size automatically adapts to the length of its CodeText.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary output folder, generates barcodes for predefined texts,
    /// and saves them as PNG files with dimensions adjusted automatically.
    /// </summary>
    static void Main()
    {
        // Build a unique temporary directory for the demo output
        string outputDir = Path.Combine(Path.GetTempPath(), "AutoSizeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Sample CodeText values of varying lengths
        string[] codeTexts = { "A", "ABC123", "LongerCodeTextExample12345" };

        // Iterate over each text, generate a barcode, and save it
        foreach (string text in codeTexts)
        {
            // File name includes the length of the text for easy identification
            string filePath = Path.Combine(outputDir, $"barcode_{text.Length}.png");

            // Initialize generator with Code128 symbology and the current text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
            {
                // No explicit size settings; dimensions adapt automatically to CodeText length
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Output the location of the generated barcode
            Console.WriteLine($"Generated barcode for \"{text}\" at {filePath}");
        }
    }
}