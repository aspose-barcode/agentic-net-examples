// Title: Generate Codabar barcodes in batch and save as TIFF files
// Description: Demonstrates how to iterate over a collection of identifiers, create Codabar barcodes with start/stop symbol 'C', and write each barcode image to a TIFF file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator with EncodeTypes.Codabar. It shows how to configure Codabar start/stop symbols, adjust X‑dimension, and save images in TIFF format—common tasks when integrating barcode creation into batch processing or reporting workflows. Developers looking for code samples on bulk barcode creation, format-specific settings, and file output will find this pattern useful.
// Prompt: Batch process a list of identifiers, generate Codabar barcodes with start symbol C, and save each as a TIFF file.
// Tags: codabar, barcode generation, batch processing, tiff, aspose.barcode, encode types, image output

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates Codabar barcodes for a list of identifiers
/// and saves each barcode as a TIFF image file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Performs batch barcode generation and file output.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary output directory for the generated TIFF files
        string outputDir = Path.Combine(Path.GetTempPath(), "CodabarBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Sample list of identifiers (numeric strings) to be encoded as barcodes
        List<string> identifiers = new List<string>
        {
            "12345",
            "67890",
            "112233",
            "445566",
            "778899"
        };

        int index = 1; // Counter used for naming output files

        // Iterate over each identifier, generate a barcode, and save it
        foreach (string id in identifiers)
        {
            // Codabar requires explicit start/stop characters; we use 'C' for both
            string codeText = $"C{id}C";

            // Initialize the barcode generator with Codabar symbology and the prepared text
            using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, codeText))
            {
                // Configure the start and stop symbols to be 'C'
                generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.C;
                generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.C;

                // Optional: set the X dimension (module width) for better readability
                generator.Parameters.Barcode.XDimension.Pixels = 2f;

                // Build the full file path for the current barcode image
                string filePath = Path.Combine(outputDir, $"Codabar_{index}.tiff");

                // Save the generated barcode as a TIFF image
                generator.Save(filePath, BarCodeImageFormat.Tiff);

                // Log the successful creation of the barcode file
                Console.WriteLine($"Saved barcode for identifier '{id}' to: {filePath}");
            }

            index++; // Increment the file counter
        }

        // Indicate that the batch processing has finished
        Console.WriteLine("Batch processing completed.");
    }
}