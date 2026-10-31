// Title: Batch generate Codabar barcodes from data and save as JPEG files
// Description: Demonstrates how to create Codabar barcodes for multiple records, using sample data that mimics a SQL query result, and save each barcode as a JPEG file named with its identifier.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the BarcodeGenerator class for Codabar symbology. Typical use cases include bulk barcode creation for inventory, shipping, or ticketing systems where data originates from a database. Developers often need to loop through query results, configure barcode parameters, and export images in common formats such as JPEG.
// Prompt: Batch generate Codabar barcodes using data from a SQL query, saving each as a JPEG file with identifier name.
// Tags: codabar, barcode, batch, jpeg, sql, generation, aspose.barcode, aspose.drawing

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides an example of batch barcode generation using Codabar symbology.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates Codabar barcodes for each data row and saves them as JPEG files.
    /// </summary>
    static void Main()
    {
        // NOTE: In a real scenario, replace the sampleData list with a SQL query.
        // Example of actual database code (requires System.Data.SqlClient and a reachable DB):
        /*
        var sampleData = new List<(int Id, string CodeText)>();
        string connectionString = "your_connection_string";
        string query = "SELECT Id, CodeText FROM BarcodesTable";
        using (var connection = new System.Data.SqlClient.SqlConnection(connectionString))
        {
            connection.Open();
            using (var command = new System.Data.SqlClient.SqlCommand(query, connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string code = reader.GetString(1);
                    sampleData.Add((id, code));
                }
            }
        }
        */

        // Sample data to simulate database rows
        var sampleData = new List<(int Id, string CodeText)>
        {
            (1, "A123456A"),
            (2, "B98765B"),
            (3, "C001122C"),
            (4, "D555-6D"),
            (5, "AABC$D+A")
        };

        // Create a unique output folder in the system's temporary directory
        string outputFolder = Path.Combine(Path.GetTempPath(), "CodabarBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        Console.WriteLine($"Saving barcodes to: {outputFolder}");

        // Iterate over each record and generate a barcode image
        foreach (var (Id, CodeText) in sampleData)
        {
            // Build the full file path for the current barcode image
            string filePath = Path.Combine(outputFolder, $"Barcode_{Id}.jpg");

            // Initialize the barcode generator with Codabar symbology and the specific code text
            using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, CodeText))
            {
                // Optional: configure start/stop symbols if needed
                // generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.A;
                // generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.A;

                // Save the generated barcode as a JPEG file
                generator.Save(filePath, BarCodeImageFormat.Jpeg);
            }

            Console.WriteLine($"Generated barcode for Id {Id} at {filePath}");
        }

        Console.WriteLine("Batch generation completed.");
    }
}