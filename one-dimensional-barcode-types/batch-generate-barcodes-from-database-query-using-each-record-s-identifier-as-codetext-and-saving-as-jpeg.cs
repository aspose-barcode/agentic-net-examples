using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating Code128 barcodes for a list of identifiers
/// and saving them as JPEG images using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates barcode images for a predefined list of identifiers.
    /// </summary>
    static void Main()
    {
        // Simulated database query result: list of identifier strings.
        List<string> identifiers = new List<string>
        {
            "1001",
            "1002",
            "1003",
            "1004",
            "1005"
        };

        // Determine the output directory for the generated JPEG images.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");

        // Create the output directory if it does not already exist.
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Iterate over each identifier and generate a corresponding barcode image.
        foreach (string id in identifiers)
        {
            // Build the full file path for the current barcode image.
            string outputPath = Path.Combine(outputDir, $"barcode_{id}.jpg");

            // Create a BarcodeGenerator for Code128 using the identifier as the code text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, id))
            {
                // Set the image resolution to 300 DPI (optional configuration).
                generator.Parameters.Resolution = 300f;

                // Save the generated barcode as a JPEG file.
                generator.Save(outputPath, BarCodeImageFormat.Jpeg);
            }

            // Output a confirmation message to the console.
            Console.WriteLine($"Generated barcode for ID {id} at {outputPath}");
        }

        // Example of how to retrieve identifiers from a real database (commented out).
        /*
        // using System.Data.SqlClient;
        // string connectionString = "your_connection_string";
        // string query = "SELECT Identifier FROM YourTable";
        // using (var connection = new SqlConnection(connectionString))
        // {
        //     connection.Open();
        //     using (var command = new SqlCommand(query, connection))
        //     using (var reader = command.ExecuteReader())
        //     {
        //         while (reader.Read())
        //         {
        //             string id = reader.GetString(0);
        //             string outputPath = Path.Combine(outputDir, $"barcode_{id}.jpg");
        //             using (var generator = new BarcodeGenerator(EncodeTypes.Code128, id))
        //             {
        //                 generator.Parameters.Resolution = 300f;
        //                 generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        //             }
        //         }
        //     }
        // }
        */
    }
}