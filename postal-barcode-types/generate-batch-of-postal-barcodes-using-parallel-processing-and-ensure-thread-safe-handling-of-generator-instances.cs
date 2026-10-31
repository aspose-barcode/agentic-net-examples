// Title: Parallel generation of postal barcodes
// Description: Demonstrates how to create a batch of postal barcodes (Planet, Postnet, RM4SCC) using Aspose.BarCode with parallel processing for faster execution.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on postal symbologies. It showcases the BarcodeGenerator class, common barcode parameters, and thread‑safe parallel execution using Parallel.ForEach. Developers often need to generate large numbers of barcodes efficiently for mailing, logistics, or batch printing scenarios.
// Prompt: Generate a batch of postal barcodes using parallel processing and ensure thread‑safe handling of generator instances.
// Tags: postal, barcode, generation, parallel, png, aspose.barcode, symbology

using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Entry point for the parallel postal barcode generation example.
/// </summary>
class Program
{
    /// <summary>
    /// Creates a temporary folder, defines barcode specifications, and generates the barcodes in parallel.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the batch
        string batchFolder = Path.Combine(Path.GetTempPath(), "PostalBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // Define barcode specifications: symbology, data, and output file name
        var specs = new List<(BaseEncodeType EncodeType, string CodeText, string FileName)>
        {
            (EncodeTypes.Planet, "123456", "Planet_123456.png"),
            (EncodeTypes.Planet, "9876543210", "Planet_9876543210.png"),
            (EncodeTypes.Postnet, "1159628792", "Postnet_1159628792.png"),
            (EncodeTypes.Postnet, "123456789", "Postnet_123456789.png"),
            (EncodeTypes.RM4SCC, "ABCD1234", "RM4SCC_ABCD1234.png")
        };

        // Generate barcodes in parallel; each iteration creates its own BarcodeGenerator instance (thread‑safe)
        Parallel.ForEach(specs, spec =>
        {
            string outputPath = Path.Combine(batchFolder, spec.FileName);
            try
            {
                // Instantiate a generator for the current specification
                using (var generator = new BarcodeGenerator(spec.EncodeType, spec.CodeText))
                {
                    // Set common parameters for postal barcodes
                    generator.Parameters.Barcode.XDimension.Pixels = 4f;
                    generator.Parameters.Barcode.BarHeight.Pixels = 50f;
                    generator.Parameters.Barcode.Postal.ShortBarHeight.Pixels = 20f;

                    // Enable filled bars for applicable symbologies
                    if (spec.EncodeType == EncodeTypes.Planet ||
                        spec.EncodeType == EncodeTypes.Postnet ||
                        spec.EncodeType == EncodeTypes.RM4SCC)
                    {
                        generator.Parameters.Barcode.FilledBars = true;
                    }

                    // Save the generated barcode as PNG
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                    Console.WriteLine($"Generated: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur during generation
                Console.WriteLine($"Failed to generate {spec.FileName}: {ex.Message}");
            }
        });

        Console.WriteLine("Batch generation completed.");
    }
}