// Title: Generate Codabar Barcodes in Batch and Save as TIFF
// Description: Demonstrates how to batch‑process a list of identifiers, create Codabar barcodes with start/stop symbol C, and write each barcode to an individual TIFF image file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes.Codabar, and related parameter classes to produce barcodes. Typical scenarios include inventory labeling, shipping manifests, and point‑of‑sale systems where Codabar is required. Developers often need to generate multiple barcodes programmatically, customize dimensions, and export them to common image formats such as TIFF.
// Prompt: Batch process a list of identifiers, generate Codabar barcodes with start symbol C, and save each as a TIFF file.
// Tags: codabar, barcode generation, batch processing, tiff, aspose.barcode, csharp

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides a console application that creates Codabar barcodes for a collection of identifiers
/// and saves each barcode as a separate TIFF image file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates barcodes in a temporary folder and reports progress to the console.
    /// </summary>
    static void Main()
    {
        // Define a sample set of identifiers to encode as barcodes.
        List<string> identifiers = new List<string>
        {
            "12345",
            "67890",
            "00112233"
        };

        // Create a unique temporary output directory for the generated TIFF files.
        string outputFolder = Path.Combine(Path.GetTempPath(), "CodabarBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Iterate over each identifier, generate a Codabar barcode, and save it as TIFF.
        for (int i = 0; i < identifiers.Count; i++)
        {
            string id = identifiers[i];

            // Codabar requires explicit start/stop symbols; prepend and append 'C'.
            string codeText = $"C{id}C";

            // Initialize the barcode generator with Codabar symbology and the prepared text.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Codabar, codeText))
            {
                // Configure the start and stop symbols to be 'C'.
                generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.C;
                generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.C;

                // Optional: adjust the module (X) dimension for better readability.
                generator.Parameters.Barcode.XDimension.Pixels = 2f;

                // Build the full file path for the current barcode image.
                string filePath = Path.Combine(outputFolder, $"Codabar_{id}.tiff");

                // Save the generated barcode as a TIFF image.
                generator.Save(filePath, BarCodeImageFormat.Tiff);

                // Output the result to the console for user awareness.
                Console.WriteLine($"Saved barcode for identifier '{id}' to: {filePath}");
            }
        }

        // Indicate that all barcodes have been processed.
        Console.WriteLine("Batch processing completed.");
    }
}