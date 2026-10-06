// Title: Batch generate Codabar barcodes from SQL data
// Description: Demonstrates how to create Codabar barcodes for multiple records and save each as a JPEG file named after its identifier.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator with EncodeTypes.Codabar. It shows typical batch processing scenarios where data is retrieved from a database, each record is encoded, and the resulting images are stored. Developers often need to automate barcode creation for inventory, shipping, or tracking systems using Aspose.BarCode APIs.
// Prompt: Batch generate Codabar barcodes using data from a SQL query, saving each as a JPEG file with identifier name.
// Tags: codabar, barcode, batch, jpeg, aspose.barcode, sql

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch generation of Codabar barcodes and saving them as JPEG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes (or real data from SQL) and writes them to a temporary folder.
    /// </summary>
    static void Main()
    {
        // Sample data representing rows from a SQL query: (identifier, codeText)
        var records = new List<(string Id, string CodeText)>
        {
            ("Item001", "A12345B"),
            ("Item002", "C67890D"),
            ("Item003", "A112233B"),
            ("Item004", "C445566D"),
            ("Item005", "A777888B")
        };

        // Create a unique temporary folder for the output images
        string outputFolder = Path.Combine(Path.GetTempPath(), "CodabarBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        Console.WriteLine("Saving barcodes to: " + outputFolder);

        // Iterate over each record and generate a barcode image
        foreach (var rec in records)
        {
            // Build the full file path using the identifier as the file name
            string filePath = Path.Combine(outputFolder, rec.Id + ".jpg");

            // Initialize the barcode generator with Codabar symbology and the record's code text
            using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, rec.CodeText))
            {
                // Optional: set start/stop symbols if needed
                // generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.A;
                // generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.A;

                // Save the generated barcode as a JPEG image
                generator.Save(filePath, BarCodeImageFormat.Jpeg);
            }

            Console.WriteLine($"Generated {filePath}");
        }

        // Real implementation would retrieve data from a SQL database, e.g.:
        /*
        string connectionString = "your_connection_string";
        string query = "SELECT Identifier, CodeText FROM BarcodesTable";
        var records = new List<(string Id, string CodeText)>();
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (var command = new SqlCommand(query, connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string id = reader.GetString(0);
                    string code = reader.GetString(1);
                    records.Add((id, code));
                }
            }
        }
        */
    }
}