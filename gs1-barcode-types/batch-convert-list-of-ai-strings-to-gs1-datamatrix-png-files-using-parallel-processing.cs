// Title: Parallel batch conversion of AI strings to GS1 DataMatrix PNG images
// Description: Demonstrates how to generate GS1 DataMatrix barcodes from a list of AI strings and save them as PNG files using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on GS1 DataMatrix symbology. It showcases the BarcodeGenerator class, EncodeTypes enumeration, and image saving with BarCodeImageFormat. Typical use cases include bulk creation of GS1‑compliant DataMatrix codes for inventory, shipping, or product labeling, where developers need efficient parallel processing.
// Prompt: Batch convert a list of AI strings to GS1 DataMatrix PNG files using parallel processing.
// Tags: gs1datamatrix, barcode generation, parallel processing, png output, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides an entry point to demonstrate parallel generation of GS1 DataMatrix barcodes from AI strings.
/// </summary>
class Program
{
    /// <summary>
    /// Generates GS1 DataMatrix barcodes for each AI string in the list and saves them as PNG files in a temporary directory.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define a collection of AI (Application Identifier) strings to be encoded.
        var aiStrings = new List<string>
        {
            "(01)00123456789012(21)ABC123",
            "(01)12345678901231(10)LOT123",
            "(01)00012345678905(17)250101",
            "(01)98765432109876(30)9876",
            "(01)55555555555555(21)XYZ"
        };

        // Create a unique temporary output directory for the generated PNG files.
        string outputDir = Path.Combine(Path.GetTempPath(), "GS1DataMatrixBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);
        Console.WriteLine($"Output directory: {outputDir}");

        // Process each AI string in parallel to improve performance on multi‑core systems.
        Parallel.ForEach(aiStrings, (codeText, state, index) =>
        {
            // Build the full file path for the current barcode image.
            string filePath = Path.Combine(outputDir, $"barcode_{index + 1}.png");
            try
            {
                // Initialize the barcode generator for GS1 DataMatrix with the current AI string.
                using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
                {
                    // Set the X‑dimension (module size) to 4 pixels for better readability.
                    generator.Parameters.Barcode.XDimension.Pixels = 4f;

                    // Save the generated barcode as a PNG image.
                    generator.Save(filePath, BarCodeImageFormat.Png);
                    Console.WriteLine($"Saved: {filePath}");
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur during barcode generation or file saving.
                Console.WriteLine($"Failed for codeText '{codeText}': {ex.Message}");
            }
        });
    }
}