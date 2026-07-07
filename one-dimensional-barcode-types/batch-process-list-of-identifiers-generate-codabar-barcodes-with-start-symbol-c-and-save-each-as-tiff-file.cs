// Title: Generate Codabar barcodes in batch and save as TIFF files
// Description: Demonstrates how to encode a list of identifiers into Codabar barcodes with start/stop symbol 'C' and store each image as a TIFF file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and Codabar settings. Typical use cases include batch creation of inventory labels, shipping tags, or any scenario requiring multiple Codabar images. Developers often need to configure start/stop symbols, choose image formats, and manage output directories.
// Prompt: Batch process a list of identifiers, generate Codabar barcodes with start symbol C, and save each as a TIFF file.
// Tags: barcode symbology, batch processing, tiff output, codabar, aspnet, aspose.barcode, barcode generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch generation of Codabar barcodes with start/stop symbol 'C' and saves them as TIFF images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates barcodes for a predefined set of identifiers and writes them to the file system.
    /// </summary>
    static void Main()
    {
        // Sample identifiers to encode as Codabar barcodes.
        string[] identifiers = new string[]
        {
            "12345",
            "67890",
            "ABCDEF",
            "987654321",
            "CODE123"
        };

        // Ensure the output directory exists.
        string outputDir = "Barcodes";
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Process each identifier and generate a corresponding barcode.
        foreach (string id in identifiers)
        {
            // Create a barcode generator for Codabar with the current identifier.
            using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, id))
            {
                // Configure the Codabar start and stop symbols to 'C'.
                generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.C;
                generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.C;

                // Build the full file path for the TIFF output.
                string filePath = Path.Combine(outputDir, $"{id}.tif");

                // Save the generated barcode as a TIFF file.
                generator.Save(filePath, BarCodeImageFormat.Tiff);

                // Inform the user that the file was saved.
                Console.WriteLine($"Saved barcode for '{id}' to '{filePath}'.");
            }
        }
    }
}