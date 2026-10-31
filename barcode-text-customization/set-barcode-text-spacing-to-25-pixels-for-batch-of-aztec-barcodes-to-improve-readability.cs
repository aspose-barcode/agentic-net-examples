// Title: Set text spacing for Aztec barcodes in batch
// Description: Demonstrates how to generate multiple Aztec barcodes with a custom text spacing of 2.5 pixels, improving readability of the encoded text.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and CodeTextParameters to customize barcode appearance. Typical use cases include batch creation of barcodes with consistent visual settings for printing or display. Developers often need to adjust text spacing, dimensions, and image formats when generating barcodes programmatically.
// Prompt: Set barcode text spacing to 2.5 pixels for a batch of Aztec barcodes to improve readability.
// Tags: aztec, barcode, text spacing, batch generation, png, aspose.barcode, csharp

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a batch of Aztec barcodes with a specific text spacing setting.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary folder, generates Aztec barcodes with 2.5‑pixel text spacing, and outputs the file paths.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the batch
        string batchFolder = Path.Combine(Path.GetTempPath(), "AztecBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // Define sample texts to encode in the Aztec barcodes
        List<string> codeTexts = new List<string>
        {
            "Sample1",
            "HelloWorld",
            "Aspose.BarCode",
            "Aztec12345",
            "BarcodeDemo"
        };

        // Collect the paths of generated barcode images
        List<string> generatedFiles = new List<string>();

        // Generate a barcode for each text entry
        foreach (string text in codeTexts)
        {
            string filePath = Path.Combine(batchFolder, $"Aztec_{text}.png");
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Aztec, text))
            {
                // Set text spacing to 2.5 pixels to improve readability
                generator.Parameters.Barcode.CodeTextParameters.Space.Pixels = 2.5f;

                // Optional: set a reasonable XDimension for visibility
                generator.Parameters.Barcode.XDimension.Pixels = 4;

                // Save the barcode image in PNG format
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            generatedFiles.Add(filePath);
        }

        // Output the list of generated barcode files
        Console.WriteLine("Generated Aztec barcodes with 2.5‑pixel text spacing:");
        foreach (string file in generatedFiles)
        {
            Console.WriteLine(file);
        }
    }
}