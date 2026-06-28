using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch generation of Codabar barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates barcode images for a set of sample data.
    /// </summary>
    static void Main()
    {
        // NOTE: In a real scenario you would retrieve data from a SQL database.
        // The following commented code shows how you could do it using System.Data.SqlClient.
        // However, the required package is not available in the snippet runner, so we use a local sample list instead.
        /*
        var connectionString = "Data Source=SERVER;Initial Catalog=Database;Integrated Security=True;";
        var query = "SELECT Id, CodeText FROM Barcodes WHERE Symbology = 'Codabar'";
        var rows = new List<(int Id, string CodeText)>();

        using (var connection = new System.Data.SqlClient.SqlConnection(connectionString))
        {
            connection.Open();
            using (var command = new System.Data.SqlClient.SqlCommand(query, connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string codeText = reader.GetString(1);
                    rows.Add((id, codeText));
                }
            }
        }
        */

        // Sample data representing rows fetched from a SQL query.
        var rows = new List<(int Id, string CodeText)>
        {
            (101, "A12345B"),
            (102, "C67890D"),
            (103, "A11223B"),
            (104, "C44556D"),
            (105, "A78901B")
        };

        // Ensure the output directory exists; create it if it does not.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Iterate over each row and generate a barcode image.
        foreach (var row in rows)
        {
            // Resolve the Codabar symbology.
            BaseEncodeType encodeType = EncodeTypes.Codabar;

            // Create the barcode generator with the specified symbology and code text.
            using (var generator = new BarcodeGenerator(encodeType, row.CodeText))
            {
                // Optional: configure barcode parameters if needed.
                // For example, set start/stop symbols explicitly.
                // generator.Parameters.Barcode.Codabar.StartSymbol = CodabarStartSymbol.A;
                // generator.Parameters.Barcode.Codabar.StopSymbol = CodabarStopSymbol.B;

                // Build the output file path using the identifier.
                string fileName = $"Codabar_{row.Id}.jpg";
                string outputPath = Path.Combine(outputDir, fileName);

                // Save the barcode as a JPEG image.
                generator.Save(outputPath, BarCodeImageFormat.Jpeg);
                Console.WriteLine($"Saved barcode for ID {row.Id} to {outputPath}");
            }
        }

        // Indicate that the batch process has finished.
        Console.WriteLine("Batch barcode generation completed.");
    }
}