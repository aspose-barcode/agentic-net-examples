// Title: Generate Codabar barcodes in batch and save as TIFF files
// Description: Demonstrates how to iterate over a list of identifiers, create Codabar barcodes with start/stop symbol C, and write each barcode to a TIFF image file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator with EncodeTypes.Codabar. It shows typical batch processing scenarios where multiple codes are produced, start/stop symbols are customized, and images are saved in a specific format such as TIFF. Developers working with inventory, shipping, or ticketing systems often need to generate many barcodes programmatically, and this snippet provides a concise reference.
// Prompt: Batch process a list of identifiers, generate Codabar barcodes with start symbol C, and save each as a TIFF file.
// Tags: codabar, barcode generation, batch processing, tiff, aspose.barcode, encode types

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch generation of Codabar barcodes with start/stop symbol C and saves them as TIFF files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a temporary output folder, generates barcodes for a predefined list,
    /// and writes each barcode image to a TIFF file.
    /// </summary>
    static void Main()
    {
        // Define a sample collection of identifiers to encode as barcodes
        List<string> identifiers = new List<string>
        {
            "12345",
            "67890",
            "112233",
            "445566",
            "778899"
        };

        // Create a dedicated temporary folder for the output TIFF files
        string outputFolder = Path.Combine(Path.GetTempPath(), "CodabarBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Iterate over each identifier, generate a barcode, and save it as a TIFF image
        for (int i = 0; i < identifiers.Count; i++)
        {
            string codeText = identifiers[i];
            string filePath = Path.Combine(outputFolder, $"Codabar_{i + 1}.tiff");

            // Initialize the barcode generator with Codabar symbology and the current identifier
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Codabar, codeText))
            {
                // Configure start and stop symbols to 'C' as required by the example
                generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.C;
                generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.C;

                // Save the generated barcode image in TIFF format
                generator.Save(filePath, BarCodeImageFormat.Tiff);
            }

            // Output the location of the generated barcode for verification
            Console.WriteLine($"Generated barcode for '{codeText}' at: {filePath}");
        }

        // Indicate that the batch processing has finished
        Console.WriteLine("Batch processing completed.");
    }
}