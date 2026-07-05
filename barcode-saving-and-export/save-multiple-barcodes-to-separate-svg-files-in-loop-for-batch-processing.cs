// Title: Batch Barcode Generation to Separate Files
// Description: Demonstrates generating multiple Code128 barcodes and saving each to an individual file in a loop for batch processing.
// Prompt: Save multiple barcodes to separate SVG files in a loop for batch processing.
// Tags: code128, barcode, batch, svg, generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a series of barcodes and saves each to a separate file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates an output directory, iterates over sample texts,
    /// generates a Code128 barcode for each, saves it to a file, and logs the operation.
    /// </summary>
    static void Main()
    {
        // Define the output directory for generated barcode files
        string outputDir = "Barcodes";

        // Ensure the output directory exists
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Sample texts to encode into barcodes
        string[] sampleTexts = new string[]
        {
            "Sample001",
            "Sample002",
            "Sample003",
            "Sample004",
            "Sample005"
        };

        // Loop through each sample text, generate a barcode, and save it
        for (int i = 0; i < sampleTexts.Length; i++)
        {
            // Current text to encode
            string codeText = sampleTexts[i];

            // Build the output file name (e.g., barcode_1.png)
            string fileName = Path.Combine(outputDir, $"barcode_{i + 1}.png");

            // Create a barcode generator for Code128 with the current text
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Save the generated barcode to the specified file
                generator.Save(fileName);
            }

            // Log the successful save operation
            Console.WriteLine($"Saved barcode '{codeText}' to '{fileName}'.");
        }

        // Indicate that the batch process has finished
        Console.WriteLine("Batch barcode generation completed.");
    }
}