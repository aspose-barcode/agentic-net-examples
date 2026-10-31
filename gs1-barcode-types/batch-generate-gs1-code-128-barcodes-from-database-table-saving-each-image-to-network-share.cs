// Title: Batch generate GS1 Code 128 barcodes and save to a network share
// Description: Demonstrates generating GS1 Code 128 barcodes for a set of records and saving each image as PNG to a network share folder.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator with EncodeTypes.GS1Code128. Typical use cases include batch barcode creation from database rows, exporting images for inventory or shipping labels, and storing results on shared storage. Developers often need to adjust barcode parameters, handle file I/O, and integrate with data sources.
// Prompt: Batch generate GS1 Code 128 barcodes from a database table, saving each image to a network share.
// Tags: gs1code128, barcode generation, batch processing, png output, aspose.barcode, file saving, network share

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates GS1 Code 128 barcodes for a collection of records
/// and saves each barcode image to a network share folder.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Simulates database records, generates barcodes, and writes PNG files.
    /// </summary>
    static void Main()
    {
        // Simulated database rows: each tuple contains an ID and GS1 Code128 text.
        var records = new List<(int Id, string CodeText)>
        {
            (1, "(01)00123456789012(21)ABC123"),
            (2, "(01)00123456789013(21)DEF456"),
            (3, "(01)00123456789014(21)GHI789"),
            (4, "(01)00123456789015(21)JKL012"),
            (5, "(01)00123456789016(21)MNO345")
        };

        // Create a unique destination folder (simulating a network share).
        string outputFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        Console.WriteLine($"Saving barcodes to: {outputFolder}");

        // Iterate through each record and generate a barcode image.
        foreach (var record in records)
        {
            string fileName = $"Barcode_{record.Id}.png";
            string filePath = Path.Combine(outputFolder, fileName);

            try
            {
                // Initialize the barcode generator with GS1 Code 128 symbology and the record's text.
                using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, record.CodeText))
                {
                    // Optional: adjust X dimension for better readability.
                    generator.Parameters.Barcode.XDimension.Pixels = 2f;

                    // Save the generated barcode as a PNG file.
                    generator.Save(filePath, BarCodeImageFormat.Png);
                }

                Console.WriteLine($"Generated barcode for ID {record.Id} at {filePath}");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during barcode generation or file saving.
                Console.WriteLine($"Failed to generate barcode for ID {record.Id}: {ex.Message}");
            }
        }

        // Note: In a real scenario, replace the simulated 'records' list with a database query,
        // e.g., using SqlConnection and SqlCommand to read Id and CodeText columns.
        // The rest of the barcode generation logic would remain the same.
    }
}