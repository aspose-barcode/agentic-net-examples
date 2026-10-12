// Title: Batch generation of Aztec barcodes with custom text spacing
// Description: Demonstrates how to generate multiple Aztec barcodes and set the spacing between the barcode and its human‑readable text to 2.5 pixels.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and CodeTextParameters to customize barcode appearance. Typical scenarios include creating batches of barcodes for inventory, ticketing, or product labeling where consistent text positioning improves readability. Developers often need to adjust text spacing, location, and output format when automating barcode production.
// Prompt: Set barcode text spacing to 2.5 pixels for a batch of Aztec barcodes to improve readability.
// Tags: aztec, barcode, text-spacing, batch, generation, png, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a batch of Aztec barcodes with a specific text spacing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a temporary folder, generates barcodes for a set of texts,
    /// configures text spacing, and saves each image as PNG.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the batch
        string batchFolder = Path.Combine(Path.GetTempPath(), "AztecBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // Sample texts for the batch
        string[] texts = new string[] { "Alpha", "Beta", "Gamma", "Delta", "Epsilon" };

        // Generate a barcode for each text value
        foreach (string text in texts)
        {
            // Determine the output file path for the current barcode
            string filePath = Path.Combine(batchFolder, $"{text}.png");

            // Initialize the barcode generator with Aztec symbology and the current text
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Aztec, text))
            {
                // Set spacing between barcode and human‑readable text to 2.5 pixels
                generator.Parameters.Barcode.CodeTextParameters.Space.Pixels = 2.5f;

                // Optional: place the text below the barcode
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

                // Save the barcode image as PNG
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Output the location of the generated barcode
            Console.WriteLine($"Generated Aztec barcode: {filePath}");
        }

        // Indicate that the batch generation process has finished
        Console.WriteLine("Batch generation completed.");
    }
}