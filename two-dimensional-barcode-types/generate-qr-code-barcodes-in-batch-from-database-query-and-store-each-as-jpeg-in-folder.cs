// Title: Batch QR Code Generation from Simulated Database Records
// Description: Demonstrates how to generate QR Code barcodes for multiple records and save each as a JPEG image in a temporary folder.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating batch creation of QR Code symbols using the BarcodeGenerator class. Typical use cases include exporting database identifiers or URLs as QR images for printing, web display, or archival. Developers often need to loop through data sources, configure QR parameters, and save images in common formats such as JPEG, PNG, or BMP.
// Prompt: Generate QR Code barcodes in batch from database query and store each as JPEG in folder.
// Tags: qr code, batch generation, barcode, jpeg, aspose.barcode, generation, encode types, image format

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch generation of QR Code barcodes from a collection of records and saves them as JPEG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that simulates database records, creates an output directory, generates QR codes, and writes them to disk.
    /// </summary>
    static void Main()
    {
        // Simulated database records (Id, CodeText)
        var records = new List<(int Id, string CodeText)>
        {
            (1, "Sample001"),
            (2, "Sample002"),
            (3, "Sample003"),
            (4, "Sample004"),
            (5, "Sample005")
        };

        // Create a unique temporary output folder for the generated images
        string outputFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        Console.WriteLine("Saving QR codes to: " + outputFolder);

        // Iterate through each record and generate a QR code image
        foreach (var record in records)
        {
            // Build the full file path for the current QR code image
            string filePath = Path.Combine(outputFolder, $"QR_{record.Id}.jpg");

            // Initialize the barcode generator with QR symbology and the record's text
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, record.CodeText))
            {
                // Optional: adjust QR-specific parameters if needed
                // generator.Parameters.Barcode.QR.QrErrorLevel = QRErrorLevel.LevelM;

                // Save the generated QR code as a JPEG file
                generator.Save(filePath, BarCodeImageFormat.Jpeg);
            }

            Console.WriteLine($"Saved QR code for Id {record.Id} to {filePath}");
        }

        Console.WriteLine("Batch QR code generation completed.");
    }
}