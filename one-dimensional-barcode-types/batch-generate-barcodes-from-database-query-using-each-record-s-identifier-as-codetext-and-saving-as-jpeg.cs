// Title: Batch generate Code128 barcodes from identifiers and save as JPEG files
// Description: Demonstrates how to generate a series of Code128 barcodes using Aspose.BarCode, assigning each database record's identifier as the CodeText, and saving the images as JPEG files.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating bulk creation of barcodes from a data source. It uses the BarcodeGenerator class with EncodeTypes to define symbology, and BarCodeImageFormat for output. Developers often need to automate barcode creation for inventory, shipping, or labeling systems, and this pattern shows how to loop through records, configure generator settings, and persist images efficiently.
// Prompt: Batch generate barcodes from a database query, using each record’s identifier as CodeText and saving as JPEG.
// Tags: code128, barcode generation, batch processing, jpeg output, aspose.barcode, c#, aspnet

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates batch generation of Code128 barcodes from a list of identifiers and saves them as JPEG images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates barcodes for each identifier and writes them to a temporary folder.
    /// </summary>
    static void Main()
    {
        // Simulated database records: list of identifiers to be used as CodeText.
        List<string> identifiers = new List<string>
        {
            "ID001",
            "ID002",
            "ID003",
            "ID004",
            "ID005"
        };

        // In a real scenario, replace the above list with a database query, e.g.:
        // using (var connection = new SqlConnection(connectionString))
        // {
        //     connection.Open();
        //     using (var command = new SqlCommand("SELECT Identifier FROM MyTable", connection))
        //     using (var reader = command.ExecuteReader())
        //     {
        //         while (reader.Read())
        //         {
        //             identifiers.Add(reader.GetString(0));
        //         }
        //     }
        // }

        // Create a unique temporary folder to store generated barcode images.
        string outputFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Iterate over each identifier and generate a corresponding barcode image.
        foreach (string id in identifiers)
        {
            string filePath = Path.Combine(outputFolder, id + ".jpg");
            try
            {
                // Initialize the barcode generator with Code128 symbology and the current identifier as CodeText.
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, id))
                {
                    // Optional: configure generator parameters for JPEG output.
                    generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false;
                    generator.Parameters.Resolution = 72f; // lower DPI for smaller JPEG size
                    generator.Parameters.UseAntiAlias = false;

                    // Save the generated barcode as a JPEG file.
                    generator.Save(filePath, BarCodeImageFormat.Jpeg);
                }

                Console.WriteLine($"Generated barcode for '{id}' at: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to generate barcode for '{id}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch barcode generation completed.");
    }
}