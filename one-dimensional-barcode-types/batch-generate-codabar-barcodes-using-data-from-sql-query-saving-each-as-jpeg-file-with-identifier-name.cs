using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Output directory for generated barcode images
        string outputFolder = "Barcodes";
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // -----------------------------------------------------------------
        // In a real scenario the data would be fetched from a SQL database.
        // Example (requires System.Data.SqlClient or Microsoft.Data.SqlClient):
        // -----------------------------------------------------------------
        // var connectionString = "Server=.;Database=MyDb;Trusted_Connection=True;";
        // var query = "SELECT Identifier, CodeText FROM Barcodes";
        // var records = new List<(string Identifier, string CodeText)>();
        // using (var connection = new SqlConnection(connectionString))
        // {
        //     connection.Open();
        //     using (var command = new SqlCommand(query, connection))
        //     using (var reader = command.ExecuteReader())
        //     {
        //         while (reader.Read())
        //         {
        //             records.Add((reader.GetString(0), reader.GetString(1)));
        //         }
        //     }
        // }
        // -----------------------------------------------------------------
        // Since database access is not available in this environment, we use a
        // hard‑coded sample list to demonstrate the barcode generation logic.

        var records = new List<(string Identifier, string CodeText)>
        {
            ("Item001", "A12345B"),
            ("Item002", "C67890D"),
            ("Item003", "E11223F"),
            ("Item004", "G44556H"),
            ("Item005", "I78901J")
        };

        foreach (var record in records)
        {
            // Create a Codabar barcode generator
            using (var generator = new BarcodeGenerator(EncodeTypes.Codabar))
            {
                // Set the text to encode
                generator.CodeText = record.CodeText;

                // Optional: configure barcode appearance
                // generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                // generator.Parameters.BackColor = Aspose.Drawing.Color.White;
                // generator.Parameters.ImageWidth.Point = 300f;
                // generator.Parameters.ImageHeight.Point = 150f;

                // Build the output file path (JPEG format)
                string fileName = $"{record.Identifier}.jpeg";
                string filePath = Path.Combine(outputFolder, fileName);

                // Save the barcode image as JPEG
                generator.Save(filePath, BarCodeImageFormat.Jpeg);
            }
        }

        Console.WriteLine($"Generated {records.Count} Codabar barcode images in '{outputFolder}'.");
    }
}