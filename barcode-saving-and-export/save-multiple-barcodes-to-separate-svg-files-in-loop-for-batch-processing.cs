// Title: Batch generation of Code39 barcodes saved as SVG files
// Description: Demonstrates how to generate multiple Code39 barcodes and save each as a separate SVG file using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes for creating barcodes. Typical scenarios include batch processing of product identifiers, inventory tags, or any situation where many barcodes must be produced automatically. Developers often need to loop through data sets, generate barcodes, and export them to image files such as SVG for scalable rendering.
// Prompt: Save multiple barcodes to separate SVG files in a loop for batch processing.
// Tags: code39, barcode generation, svg, batch processing, aspose.barcode, generation

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Provides an example of generating multiple Code39 barcodes and saving each as an SVG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary folder, generates a set of barcodes,
    /// and saves each barcode to a separate SVG file.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the SVG files
        string outputFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Define the list of barcode texts to be encoded
        List<string> codeTexts = new List<string>
        {
            "CODE39-001",
            "CODE39-002",
            "CODE39-003",
            "CODE39-004",
            "CODE39-005"
        };

        // Iterate over each text, generate a barcode, and save it as SVG
        for (int i = 0; i < codeTexts.Count; i++)
        {
            string text = codeTexts[i];
            string filePath = Path.Combine(outputFolder, $"barcode_{i + 1}.svg");

            // Initialize the barcode generator for Code39 symbology with the current text
            BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code39, text);

            try
            {
                // Save the generated barcode to the specified SVG file
                generator.Save(filePath, BarCodeImageFormat.Svg);
                Console.WriteLine($"Saved barcode {i + 1} to {filePath}");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during the save operation
                Console.WriteLine($"Error saving barcode {i + 1}: {ex.Message}");
            }
        }

        Console.WriteLine("Batch barcode generation completed.");
    }
}