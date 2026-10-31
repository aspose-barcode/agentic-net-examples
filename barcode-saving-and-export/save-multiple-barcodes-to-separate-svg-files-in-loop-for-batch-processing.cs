// Title: Batch generate multiple barcodes and save as SVG files
// Description: Demonstrates how to generate several barcodes in a loop and write each to a separate SVG file using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category. It showcases the use of the BarcodeGenerator class with EncodeTypes to create barcodes, customize parameters, and save them in various image formats such as SVG. Typical scenarios include batch processing of inventory codes, ticket numbers, or any bulk barcode creation where each barcode must be stored as an individual file. Developers often need to automate folder management, handle exceptions, and log outcomes while working with these APIs.
// Prompt: Save multiple barcodes to separate SVG files in a loop for batch processing.
// Tags: barcode generation, batch processing, svg, code39, aspose.barcode, csharp

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a temporary folder and generates a set of Code39 barcodes,
/// saving each as an individual SVG file. This illustrates batch processing with Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, saves them as SVG files, and logs the results.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the batch output
        string outputFolder = Path.Combine(Path.GetTempPath(), "BarcodeBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Define sample barcodes to generate (text and target file name)
        var barcodes = new List<(string Text, string FileName)>
        {
            ("12345", "Barcode1.svg"),
            ("ABCDE", "Barcode2.svg"),
            ("987654321", "Barcode3.svg"),
            ("XYZ123", "Barcode4.svg"),
            ("CODE39", "Barcode5.svg")
        };

        // Iterate over each barcode definition and generate the SVG file
        foreach (var (text, fileName) in barcodes)
        {
            string filePath = Path.Combine(outputFolder, fileName);
            try
            {
                // Initialize the generator with Code39 symbology and the desired text
                using (var generator = new BarcodeGenerator(EncodeTypes.Code39, text))
                {
                    // Optional: customize appearance if needed
                    // generator.Parameters.Barcode.XDimension.Point = 2f;

                    // Save the generated barcode as an SVG file
                    generator.Save(filePath, BarCodeImageFormat.Svg);
                }

                Console.WriteLine($"Saved {filePath}");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during generation or saving
                Console.WriteLine($"Failed to save {filePath}: {ex.Message}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}