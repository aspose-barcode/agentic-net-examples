// Title: Batch generate GS1 DataMatrix barcodes from AI strings using parallel processing
// Description: Demonstrates how to convert a collection of GS1 Application Identifier (AI) strings into PNG images of GS1 DataMatrix barcodes in parallel.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing the use of BarcodeGenerator with EncodeTypes.GS1DataMatrix. It illustrates typical scenarios such as bulk barcode creation for inventory, shipping, or labeling systems, where developers need high‑throughput image output in PNG format using Aspose.BarCode classes like BarcodeGenerator, BarCodeImageFormat, and related parameters.
// Prompt: Batch convert a list of AI strings to GS1 DataMatrix PNG files using parallel processing.
// Tags: gs1 datamatrix, barcode generation, parallel processing, png, aspose.barcode, barcodegenerator

using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch conversion of GS1 AI strings to GS1 DataMatrix PNG files using parallel processing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes for each AI string in parallel and saves them as PNG files.
    /// </summary>
    static void Main()
    {
        // Define a sample list of GS1 AI strings to be encoded.
        List<string> aiStrings = new List<string>
        {
            "(01)01234567890128",
            "(01)00123456789012",
            "(01)12345678901231(21)ABC123",
            "(01)12345678901231(10)0010",
            "(01)12345678901231(30)9876"
        };

        // Create a unique temporary output folder for the generated PNG files.
        string outputDir = Path.Combine(Path.GetTempPath(), "GS1DataMatrixBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);
        Console.WriteLine("Output folder: " + outputDir);

        // Process each AI string in parallel to generate and save the corresponding barcode image.
        Parallel.ForEach(aiStrings, (codeText, state, index) =>
        {
            try
            {
                // Initialize the barcode generator for GS1 DataMatrix with the current AI string.
                using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
                {
                    // Set the X-dimension (module size) to 2 pixels for better readability.
                    generator.Parameters.Barcode.XDimension.Pixels = 2f;

                    // Build the full file path for the PNG output.
                    string filePath = Path.Combine(outputDir, $"barcode_{index}.png");

                    // Save the generated barcode as a PNG image.
                    generator.Save(filePath, BarCodeImageFormat.Png);
                    Console.WriteLine($"Saved: {filePath}");
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur during barcode generation for a specific AI string.
                Console.WriteLine($"Error generating barcode for '{codeText}': {ex.Message}");
            }
        });
    }
}