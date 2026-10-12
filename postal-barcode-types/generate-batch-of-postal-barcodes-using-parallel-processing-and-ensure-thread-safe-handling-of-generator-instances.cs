// Title: Parallel Generation of Postal Barcodes with Thread‑Safe Generators
// Description: Demonstrates how to generate a batch of postal barcodes (Planet, Postnet, RM4SCC) in parallel, storing each as a PNG file in a temporary folder.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category. It showcases the use of BarcodeGenerator, BarCodeImageFormat, and BarCodeReader classes for creating and verifying postal symbologies. Developers often need to produce large numbers of barcodes efficiently, requiring thread‑safe handling of generator instances and parallel processing to improve performance. The snippet illustrates typical patterns for batch processing, temporary file management, and parallel verification.
// Prompt: Generate a batch of postal barcodes using parallel processing and ensure thread‑safe handling of generator instances.
// Tags: postal barcode, parallel processing, thread safety, barcode generation, aspose.barcode, png output, planet, postnet, rm4scc

using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates parallel generation and verification of postal barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a temporary batch folder, generates barcodes in parallel,
    /// saves them as PNG files, and optionally reads them back for verification.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the batch
        string batchFolder = Path.Combine(Path.GetTempPath(), "PostalBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // Define barcode specifications: symbology, data, and output file name
        var specs = new List<(BaseEncodeType Encode, string CodeText, string FileName)>
        {
            (EncodeTypes.Planet, "123456", "Planet_123456.png"),
            (EncodeTypes.Postnet, "1159628792", "Postnet_1159628792.png"),
            (EncodeTypes.RM4SCC, "ABCD1234", "RM4SCC_ABCD1234.png"),
            (EncodeTypes.Planet, "9876543210", "Planet_9876543210.png"),
            (EncodeTypes.Postnet, "1234567890", "Postnet_1234567890.png")
        };

        // Generate barcodes in parallel; each iteration creates its own BarcodeGenerator instance (thread‑safe)
        Parallel.ForEach(specs, spec =>
        {
            string outputPath = Path.Combine(batchFolder, spec.FileName);
            using (var generator = new BarcodeGenerator(spec.Encode, spec.CodeText))
            {
                // Set X‑dimension to 4 pixels for better readability
                generator.Parameters.Barcode.XDimension.Pixels = 4;
                // Save the generated barcode as a PNG image
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }
        });

        Console.WriteLine("Barcode generation completed. Files saved to:");
        Console.WriteLine(batchFolder);

        // Optional: read back generated barcodes in parallel to verify correctness
        Parallel.ForEach(specs, spec =>
        {
            string filePath = Path.Combine(batchFolder, spec.FileName);
            if (File.Exists(filePath))
            {
                using (var reader = new BarCodeReader(filePath))
                {
                    var results = reader.ReadBarCodes();
                    foreach (var result in results)
                    {
                        Console.WriteLine($"{spec.FileName}: {result.CodeText}");
                    }
                }
            }
        });
    }
}