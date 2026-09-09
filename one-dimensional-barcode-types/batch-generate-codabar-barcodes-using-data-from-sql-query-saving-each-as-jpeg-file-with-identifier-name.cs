// Title: Batch generate Codabar barcodes from data and save as JPEG files
// Description: Demonstrates how to create Codabar barcodes for multiple records, using sample data that mimics a SQL query, and store each barcode as a JPEG file named with its identifier.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator with EncodeTypes.Codabar, setting optional parameters, and saving images via BarCodeImageFormat. Typical scenarios include batch processing of database records to produce printable barcode images. Developers often need to loop through data rows, generate barcodes, and write files to disk.
// Prompt: Batch generate Codabar barcodes using data from a SQL query, saving each as a JPEG file with identifier name.
// Tags: codabar, barcode generation, batch processing, jpeg, aspose.barcode, sql data

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch generation of Codabar barcodes using sample data (representing a SQL query) and saving each as a JPEG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates an output folder, iterates over sample data, generates a Codabar barcode for each entry, and saves it as a JPEG file named with the record identifier.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary output folder for the generated barcode images
        string outputFolder = Path.Combine(Path.GetTempPath(), "CodabarBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        Console.WriteLine("Output folder: " + outputFolder);

        // Sample data that would normally be retrieved from a SQL query.
        // Replace this block with actual database access code as needed.
        var sampleData = new List<(int Id, string Code)>
        {
            (1, "A12345B"),
            (2, "C67890D"),
            (3, "A112233B"),
            (4, "C445566D"),
            (5, "A777888B")
        };

        // Iterate through each record and generate a barcode image
        foreach (var item in sampleData)
        {
            // Build the output file name using the record identifier
            string fileName = $"{item.Id}.jpg";
            string filePath = Path.Combine(outputFolder, fileName);

            // Initialize the barcode generator for Codabar with the provided code text
            using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, item.Code))
            {
                // Optional: configure start/stop symbols if required
                // generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.A;
                // generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.B;

                // Save the generated barcode as a JPEG image
                generator.Save(filePath, BarCodeImageFormat.Jpeg);
            }

            Console.WriteLine($"Generated barcode for Id {item.Id} at {filePath}");
        }

        Console.WriteLine("Batch generation completed.");
    }
}