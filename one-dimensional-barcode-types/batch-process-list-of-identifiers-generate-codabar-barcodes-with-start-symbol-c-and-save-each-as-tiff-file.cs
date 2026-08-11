// Title: Generate Codabar Barcodes in Batch and Save as TIFF
// Description: This example demonstrates how to generate Codabar barcodes with a start/stop symbol of C for a list of identifiers and save each barcode as a TIFF image file.
// Category-Description: Learn how to perform batch barcode generation using Aspose.BarCode. The sample utilizes the BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to create Codabar symbols, configure visual properties, and export images. Ideal for developers needing to automate barcode creation for inventory, shipping, or labeling workflows where multiple codes must be produced efficiently.
// Prompt: Batch process a list of identifiers, generate Codabar barcodes with start symbol C, and save each as a TIFF file.
// Tags: barcode, codabar, batch, tiff, generation, aspose.barcode, aspose.drawing, image, console

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch creation of Codabar barcodes with a start/stop symbol of 'C' and saves each as a TIFF file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcodes for a predefined list of identifiers.
    /// </summary>
    static void Main()
    {
        // Define a sample list of identifiers to encode as Codabar barcodes.
        List<string> identifiers = new List<string>
        {
            "12345",
            "67890",
            "ABCDEF",
            "987654321",
            "C12345"
        };

        // Specify the output folder where TIFF files will be stored.
        string outputFolder = "Barcodes";
        if (!Directory.Exists(outputFolder))
        {
            // Create the folder if it does not already exist.
            Directory.CreateDirectory(outputFolder);
        }

        // Iterate over each identifier and generate a corresponding barcode.
        foreach (string id in identifiers)
        {
            // Initialize a Codabar barcode generator with the current identifier as the code text.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Codabar, id))
            {
                // Configure the start and stop symbols to 'C'.
                generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.C;
                generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.C;

                // Optional: set visual colors (black barcode on white background).
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Build the full file path for the output TIFF image.
                string fileName = Path.Combine(outputFolder, $"barcode_{id}.tiff");

                // Save the generated barcode as a TIFF file.
                generator.Save(fileName, BarCodeImageFormat.Tiff);

                // Log the successful generation to the console.
                Console.WriteLine($"Generated barcode for '{id}' -> {fileName}");
            }
        }

        // Indicate that all barcodes have been processed.
        Console.WriteLine("All barcodes have been generated.");
    }
}