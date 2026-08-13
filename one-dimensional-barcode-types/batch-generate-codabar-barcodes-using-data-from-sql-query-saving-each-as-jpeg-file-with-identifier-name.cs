// Title: Batch generate Codabar barcodes from data and save as JPEG files
// Description: Demonstrates how to create Codabar barcodes for multiple records and store each image as a JPEG file using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category. It shows how to use the BarcodeGenerator class with EncodeTypes.Codabar to produce barcodes in bulk, configure image format, and save them to disk. Typical use cases include generating product labels, inventory tags, or any batch barcode creation where data originates from a database query.
// Prompt: Batch generate Codabar barcodes using data from a SQL query, saving each as a JPEG file with identifier name.
// Tags: codabar, barcode generation, batch, sql, jpeg, aspose.barcode, image export

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch generation of Codabar barcodes from a data source and saving each as a JPEG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates an output folder, retrieves sample data (replace with SQL query), generates Codabar barcodes, and saves them as JPEG images.
    /// </summary>
    static void Main()
    {
        // Define the folder where barcode images will be stored.
        string outputFolder = "Barcodes";

        // Ensure the output directory exists.
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // ------------------------------------------------------------
        // In a real scenario you would fetch data from a SQL database,
        // e.g. using System.Data.SqlClient and executing a query that
        // returns an identifier and the Codabar code text.
        // The following code is a placeholder that simulates such data.
        // ------------------------------------------------------------
        List<(string Id, string CodeText)> records = GetSampleData();

        // Iterate over each record and generate a barcode image.
        foreach (var record in records)
        {
            try
            {
                // Initialize a Codabar barcode generator with the current code text.
                using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, record.CodeText))
                {
                    // Optional: set start/stop symbols if required.
                    // generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.A;
                    // generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.A;

                    // Build the full file path using the record identifier.
                    string filePath = Path.Combine(outputFolder, $"{record.Id}.jpg");

                    // Save the generated barcode as a JPEG image.
                    generator.Save(filePath, BarCodeImageFormat.Jpeg);
                    Console.WriteLine($"Saved barcode for '{record.Id}' to '{filePath}'.");
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur during barcode generation.
                Console.WriteLine($"Failed to generate barcode for '{record.Id}': {ex.Message}");
            }
        }
    }

    // Sample data generator – replace with actual SQL query results.
    static List<(string Id, string CodeText)> GetSampleData()
    {
        return new List<(string, string)>
        {
            ("Item001", "A123456A"),
            ("Item002", "B987654B"),
            ("Item003", "C555555C"),
            ("Item004", "D111111D"),
            ("Item005", "E222222E")
        };
    }
}