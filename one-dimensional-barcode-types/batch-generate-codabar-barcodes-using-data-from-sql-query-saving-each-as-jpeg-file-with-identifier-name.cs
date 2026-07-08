// Title: Batch generate Codabar barcodes from SQL data
// Description: Demonstrates generating Codabar barcodes for each record retrieved from a SQL query and saving them as JPEG files named with the record identifier.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes.Codabar to create barcodes in bulk. Typical use cases include batch processing of database records to produce printable barcode images. Developers often need to customize appearance, choose output formats, and automate file naming—this snippet illustrates those common steps.
// Prompt: Batch generate Codabar barcodes using data from a SQL query, saving each as a JPEG file with identifier name.
// Tags: codabar, barcode generation, batch processing, jpeg, aspose.barcode, aspose.drawing, sql

using System;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch generation of Codabar barcodes from data (simulating a SQL query) and saving each as a JPEG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Simulates fetching data from a database and generates barcodes for each record.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Simulated data representing rows fetched from a SQL query.
        // Replace this block with actual database access code as needed.
        // ------------------------------------------------------------
        var sampleData = new List<(int Id, string Code)>
        {
            (1, "A12345B"),
            (2, "C67890D"),
            (3, "E11223F"),
            (4, "G44556H"),
            (5, "I78901J")
        };

        // Iterate over each record and generate a Codabar barcode.
        foreach (var (id, code) in sampleData)
        {
            GenerateCodabar(id, code);
        }
    }

    /// <summary>
    /// Generates a Codabar barcode image for the specified identifier and code text, then saves it as a JPEG file.
    /// </summary>
    /// <param name="identifier">Unique identifier used for naming the output file.</param>
    /// <param name="codeText">The text to encode in the Codabar barcode. Must be a valid Codabar string.</param>
    static void GenerateCodabar(int identifier, string codeText)
    {
        // Construct the output file name using the identifier.
        string fileName = $"Barcode_{identifier}.jpeg";

        // Create a BarcodeGenerator for Codabar with the provided code text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, codeText))
        {
            // Optional: customize barcode appearance.
            generator.Parameters.Barcode.BarColor = Color.Black;   // Set barcode bars to black.
            generator.Parameters.BackColor = Color.White;          // Set background to white.

            // Save the generated barcode as a JPEG image.
            generator.Save(fileName, BarCodeImageFormat.Jpeg);
        }

        // Inform the user that the barcode has been generated.
        Console.WriteLine($"Generated Codabar barcode for ID {identifier} -> {fileName}");
    }
}