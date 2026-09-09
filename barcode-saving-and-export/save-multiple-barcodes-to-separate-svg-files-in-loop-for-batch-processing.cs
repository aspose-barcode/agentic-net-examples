// Title: Batch generation of Code39 barcodes saved as individual SVG files
// Description: Demonstrates how to generate multiple Code39 barcodes and save each one to a separate SVG file using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes for batch processing. Developers often need to create large numbers of barcodes for inventory, shipping, or labeling systems, and this pattern illustrates efficient looping and file handling for such scenarios.
// Prompt: Save multiple barcodes to separate SVG files in a loop for batch processing.
// Tags: code39, barcode generation, batch processing, svg output, aspose.barcode, encode types, barcodegenerator

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch creation of Code39 barcodes saved as separate SVG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a temporary output directory, iterates over a list of barcode texts,
    /// creates a <see cref="BarcodeGenerator"/> for each, and saves the result as an SVG file.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the generated barcode files
        string outputDir = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define the collection of barcode texts to be encoded
        List<string> codeTexts = new List<string>
        {
            "CODE39-1",
            "CODE39-2",
            "CODE39-3",
            "CODE39-4",
            "CODE39-5"
        };

        // Initialize a counter for naming the output SVG files
        int index = 1;

        // Loop through each text, generate a barcode, and save it as an SVG file
        foreach (string text in codeTexts)
        {
            // Build the full file path for the current barcode image
            string filePath = Path.Combine(outputDir, $"barcode_{index}.svg");

            // Use a BarcodeGenerator instance to create the barcode with Code39 symbology
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code39, text))
            {
                try
                {
                    // Save the generated barcode to the specified SVG file
                    generator.Save(filePath, BarCodeImageFormat.Svg);
                    Console.WriteLine($"Saved: {filePath}");
                }
                catch (Exception ex)
                {
                    // Log any errors that occur during the save operation
                    Console.WriteLine($"Failed to save {filePath}: {ex.Message}");
                }
            }

            // Increment the file index for the next barcode
            index++;
        }

        // Indicate that the batch processing has finished
        Console.WriteLine("Batch processing completed.");
    }
}