// Title: Batch Generation of GS1 Code 128 Barcodes to Network Share
// Description: Demonstrates how to generate multiple GS1 Code 128 barcodes from a collection of data and save each image as PNG to a network share (simulated with a temporary folder).
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator with EncodeTypes.GS1Code128. Typical scenarios include creating product identification barcodes from database records and exporting them as image files for printing or distribution. Developers often need to batch‑process records, configure barcode parameters, and store the resulting images on shared storage.
// Prompt: Batch generate GS1 Code 128 barcodes from a database table, saving each image to a network share.
// Tags: gs1code128, barcode, batch-generation, image-output, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that batch‑generates GS1 Code 128 barcodes from a set of records
/// and saves each barcode image to a network‑share‑like folder.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcodes for simulated database records
    /// and writes PNG files to a temporary directory that mimics a network share.
    /// </summary>
    static void Main()
    {
        // Create a unique output folder (simulating a network share location).
        string outputFolder = Path.Combine(Path.GetTempPath(), "BatchGs1Code128_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        Console.WriteLine($"Saving barcodes to {outputFolder}");

        // Simulated database records: each tuple contains an Id and the GS1 Code128 text.
        List<(int Id, string CodeText)> records = new List<(int, string)>
        {
            (1, "(01)12345678901231(21)ABC123"),
            (2, "(01)98765432109876(21)XYZ789"),
            (3, "(01)11111111111111(21)ITEM001"),
            (4, "(01)22222222222222(21)ITEM002"),
            (5, "(01)33333333333333(21)ITEM003")
        };

        // Iterate over each record, generate a barcode, and save it as a PNG file.
        foreach (var rec in records)
        {
            // Build the file name and full path for the current barcode image.
            string fileName = $"Barcode_{rec.Id}.png";
            string filePath = Path.Combine(outputFolder, fileName);

            // Use Aspose.BarCode's BarcodeGenerator to create a GS1 Code 128 barcode.
            using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, rec.CodeText))
            {
                // Set the X‑dimension (module width) to 2 pixels for better readability.
                generator.Parameters.Barcode.XDimension.Pixels = 2f;

                // Save the generated barcode image to the specified path in PNG format.
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"Generated {filePath}");
        }

        // Placeholder for real database implementation:
        // using (var connection = new SqlConnection(connectionString))
        // {
        //     // Retrieve records and generate barcodes as shown above.
        // }

        Console.WriteLine("Batch generation completed.");
    }
}