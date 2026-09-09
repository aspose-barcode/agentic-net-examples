// Title: Parallel barcode generation using TPL
// Description: Demonstrates generating Code128 barcodes in parallel to improve performance for large datasets.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to create image files. Typical use cases include batch processing of inventory items, bulk ticket creation, or any scenario requiring high‑throughput barcode production. Developers often need to parallelize such tasks to fully utilize CPU resources.
// Prompt: Parallelize barcode generation for large datasets using Task Parallel Library to improve performance.
// Tags: code128, barcode generation, parallel processing, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Entry point for the parallel barcode generation example.
/// </summary>
class Program
{
    /// <summary>
    /// Generates barcodes for a sample list of codes in parallel and saves them as PNG files.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for generated barcodes
        string outputFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        Console.WriteLine($"Output folder: {outputFolder}");

        // Sample dataset of code texts to be encoded
        List<string> codeTexts = new List<string>
        {
            "Item001",
            "Item002",
            "Item003",
            "Item004",
            "Item005",
            "Item006",
            "Item007",
            "Item008",
            "Item009",
            "Item010"
        };

        // Configure parallel execution options (use all logical processors)
        ParallelOptions options = new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };

        // Parallel generation using TPL
        Parallel.ForEach(codeTexts, options, codeText =>
        {
            string filePath = Path.Combine(outputFolder, $"{codeText}.png");
            try
            {
                // Initialize the barcode generator for Code128 symbology
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
                {
                    // Set common barcode parameters
                    generator.Parameters.Barcode.XDimension.Point = 2f;
                    generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                    generator.Parameters.BackColor = Aspose.Drawing.Color.White;
                    generator.Parameters.Resolution = 300f;

                    // Save the generated barcode as a PNG image
                    generator.Save(filePath, BarCodeImageFormat.Png);
                }

                Console.WriteLine($"Generated: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating barcode for '{codeText}': {ex.Message}");
            }
        });

        Console.WriteLine("Barcode generation completed.");
    }
}