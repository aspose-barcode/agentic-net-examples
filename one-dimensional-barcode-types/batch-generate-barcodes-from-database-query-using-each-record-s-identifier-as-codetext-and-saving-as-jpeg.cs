// Title: Batch barcode generation from identifiers
// Description: Demonstrates generating Code128 barcodes for a list of identifiers and saving each as a JPEG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator with EncodeTypes, configure parameters, and export images. Typical use cases include bulk barcode creation from database records, inventory labeling, and automated document processing. Developers often need to loop through data sources, set resolution, and handle output formats using Aspose.BarCode.Generation and Aspose.Drawing.Imaging classes.
// Prompt: Batch generate barcodes from a database query, using each record’s identifier as CodeText and saving as JPEG.
// Tags: code128, barcode generation, batch processing, jpeg output, aspose.barcode, aspose.drawing, csharp

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides an example of batch generating Code128 barcodes from a collection of identifiers
/// and saving each barcode as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcodes for a predefined list of identifiers,
    /// creates an output folder, and writes each barcode image to disk.
    /// </summary>
    static void Main()
    {
        // In a real scenario, replace this with a database query to retrieve identifiers.
        // Example using ADO.NET (not available in this runner):
        // var identifiers = GetIdentifiersFromDatabase();
        List<string> identifiers = new List<string>
        {
            "ID001",
            "ID002",
            "ID003",
            "ID004",
            "ID005"
        };

        // Create a unique temporary folder for the generated barcode images.
        string outputFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        Console.WriteLine("Saving barcodes to: " + outputFolder);

        // Iterate over each identifier and generate a corresponding barcode image.
        foreach (string id in identifiers)
        {
            // Build the full file path for the JPEG image.
            string filePath = Path.Combine(outputFolder, id + ".jpg");

            // Initialize the barcode generator with Code128 symbology and the identifier as CodeText.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, id))
            {
                // Optional: configure generator parameters.
                generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false;
                generator.Parameters.Resolution = 300f;

                // Save the generated barcode directly as a JPEG file.
                generator.Save(filePath, BarCodeImageFormat.Jpeg);
            }

            Console.WriteLine($"Generated barcode for '{id}' -> {filePath}");
        }

        Console.WriteLine("Barcode generation completed.");
    }

    // Placeholder for real database access method.
    // private static List<string> GetIdentifiersFromDatabase()
    // {
    //     var list = new List<string>();
    //     // Use System.Data.SqlClient or other ADO.NET provider to query the database.
    //     // Example:
    //     // using (var connection = new SqlConnection(connectionString))
    //     // {
    //     //     connection.Open();
    //     //     using (var command = new SqlCommand("SELECT Identifier FROM MyTable", connection))
    //     //     using (var reader = command.ExecuteReader())
    //     //     {
    //     //         while (reader.Read())
    //     //         {
    //     //             list.Add(reader.GetString(0));
    //     //         }
    //     //     }
    //     // }
    //     return list;
    // }
}