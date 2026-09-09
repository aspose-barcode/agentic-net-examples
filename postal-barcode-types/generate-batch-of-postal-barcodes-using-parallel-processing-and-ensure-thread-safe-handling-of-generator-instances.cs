// Title: Parallel Generation of Postal Barcodes
// Description: Demonstrates creating multiple postal barcode images (Planet, Postnet, RM4SCC) in parallel using Aspose.BarCode, saving them to a temporary folder.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator with various postal symbologies. It illustrates thread‑safe parallel processing, configuration of barcode dimensions, and image export. Developers working with bulk barcode creation, batch processing, or high‑throughput printing pipelines will find this pattern useful.
// Prompt: Generate a batch of postal barcodes using parallel processing and ensure thread‑safe handling of generator instances.
// Tags: postal, barcode, parallel, generation, png, aspose.barcode, csharp

using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a batch of postal barcodes in parallel.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a temporary output folder,
    /// defines barcode specifications, and generates each barcode image concurrently.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Create a unique temporary folder for the generated images.
        string outputFolder = Path.Combine(Path.GetTempPath(), "PostalBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        Console.WriteLine("Output folder: " + outputFolder);

        // Define a list of barcode specifications: symbology, data, and target file name.
        var specs = new List<(BaseEncodeType encodeType, string codeText, string fileName)>();
        specs.Add((EncodeTypes.Planet, "123456", "Planet_123456.png"));
        specs.Add((EncodeTypes.Postnet, "1159628792", "Postnet_1159628792.png"));
        specs.Add((EncodeTypes.RM4SCC, "ABCD1234", "RM4SCC_ABCD1234.png"));
        specs.Add((EncodeTypes.Planet, "9876543210", "Planet_9876543210.png"));
        specs.Add((EncodeTypes.Postnet, "1234567890", "Postnet_1234567890.png"));

        // Process each specification in parallel to utilize multiple CPU cores.
        Parallel.ForEach(specs, spec =>
        {
            string filePath = Path.Combine(outputFolder, spec.fileName);
            try
            {
                // Each thread creates its own BarcodeGenerator instance (thread‑safe).
                using (var generator = new BarcodeGenerator(spec.encodeType, spec.codeText))
                {
                    // Configure visual parameters for the barcode.
                    generator.Parameters.Barcode.XDimension.Pixels = 4f;
                    generator.Parameters.Barcode.BarHeight.Pixels = 50f;
                    generator.Parameters.Barcode.Postal.ShortBarHeight.Pixels = 20f;

                    // Save the generated barcode as a PNG image.
                    generator.Save(filePath, BarCodeImageFormat.Png);
                }

                Console.WriteLine($"Generated: {filePath}");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during generation.
                Console.WriteLine($"Failed to generate {spec.fileName}: {ex.Message}");
            }
        });

        Console.WriteLine("Batch generation completed.");
    }
}