// Title: Batch barcode generation from identifiers
// Description: Demonstrates generating Code128 barcodes for a list of identifiers and saving each as a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator with EncodeTypes to create barcodes in bulk. Typical use cases include exporting product IDs, inventory numbers, or any database‑driven identifiers to image files for printing or digital distribution. Developers often need to loop through data sources, set barcode properties, and save images in common formats.
// Prompt: Batch generate barcodes from a database query, using each record’s identifier as CodeText and saving as JPEG.
// Tags: barcode symbology, batch generation, jpeg output, aspose.barcode, generation

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch generation of Code128 barcodes from a collection of identifiers
/// and saves each barcode as a JPEG image in a designated output folder.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcodes for sample identifiers and writes them to disk.
    /// </summary>
    static void Main()
    {
        // In a real scenario the identifiers would be read from a database.
        // For this runnable example we use a hard‑coded list of sample identifiers.
        // Replace the following block with actual DB access code when the required
        // data provider packages are available.
        List<string> identifiers = new List<string>
        {
            "ID001",
            "ID002",
            "ID003",
            "ID004",
            "ID005"
        };

        // Define the output directory for generated barcode images.
        string outputFolder = "Barcodes";

        // Ensure the output directory exists; create it if it does not.
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Iterate over each identifier and generate a corresponding barcode.
        foreach (string id in identifiers)
        {
            // Create a barcode generator for Code128 (adjust symbology as needed).
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                // Set the text to be encoded in the barcode.
                generator.CodeText = id;

                // Optional: set foreground and background colors.
                // generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                // generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Build the full file path for the JPEG image.
                string filePath = Path.Combine(outputFolder, $"barcode_{id}.jpeg");

                // Save the generated barcode as a JPEG file.
                generator.Save(filePath, BarCodeImageFormat.Jpeg);

                // Log the successful generation to the console.
                Console.WriteLine($"Generated barcode for '{id}' -> {filePath}");
            }
        }

        // End of program.
    }
}