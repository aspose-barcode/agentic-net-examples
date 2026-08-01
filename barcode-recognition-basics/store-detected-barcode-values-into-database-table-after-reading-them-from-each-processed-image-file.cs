// Title: Generate and Detect Barcodes, Store Results in CSV
// Description: This example generates sample barcode images, reads them back, and stores detected values in a CSV file as a stand‑in for a database table.
// Category-Description: Demonstrates core Aspose.BarCode operations—barcode generation with BarcodeGenerator and barcode recognition with BarCodeReader. Typical use cases include creating barcodes for inventory, scanning documents, and persisting scan results. Developers often need to generate multiple symbologies, detect them automatically, and store the outcomes using common .NET I/O or database APIs.
// Prompt: Store detected barcode values into a database table after reading them from each processed image file.
// Tags: barcode generation,barcode recognition,csv output,aspose.barcode,code128,qr,datamatrix

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates how to generate barcode images, read them, and persist detection results.
/// </summary>
class Program
{
    /// <summary>
    /// Simple record to hold barcode detection results.
    /// </summary>
    private class BarcodeRecord
    {
        public string FileName { get; set; }
        public string CodeType { get; set; }
        public string CodeText { get; set; }
    }

    /// <summary>
    /// Entry point. Generates sample barcodes, detects them, and writes results to a CSV file.
    /// </summary>
    static void Main()
    {
        // Define folder for generated barcode images.
        string imagesFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        Directory.CreateDirectory(imagesFolder);

        // Define sample barcodes to generate (type, text, file name).
        var samples = new (BaseEncodeType type, string text, string file)[]
        {
            (EncodeTypes.Code128, "Sample123", "code128.png"),
            (EncodeTypes.QR, "https://example.com", "qr.png"),
            (EncodeTypes.DataMatrix, "DM12345", "datamatrix.png")
        };

        // Generate each sample barcode image and save as PNG.
        foreach (var sample in samples)
        {
            string imagePath = Path.Combine(imagesFolder, sample.file);
            using (var generator = new BarcodeGenerator(sample.type, sample.text))
            {
                // Save image; format inferred from file extension.
                generator.Save(imagePath);
            }
        }

        // Collect detection results in a list.
        var results = new List<BarcodeRecord>();

        // Process each PNG image in the folder.
        string[] imageFiles = Directory.GetFiles(imagesFolder, "*.png");
        foreach (string imageFile in imageFiles)
        {
            if (!File.Exists(imageFile))
            {
                Console.WriteLine($"File not found: {imageFile}");
                continue;
            }

            // Use AllSupportedTypes to detect any barcode present in the image.
            using (var reader = new BarCodeReader(imageFile, DecodeType.AllSupportedTypes))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    // Store detection details.
                    results.Add(new BarcodeRecord
                    {
                        FileName = Path.GetFileName(imageFile),
                        CodeType = result.CodeTypeName,
                        CodeText = result.CodeText
                    });

                    Console.WriteLine($"Detected {result.CodeTypeName} in {Path.GetFileName(imageFile)}: {result.CodeText}");
                }
            }
        }

        // Write results to a CSV file (acts as a stand‑in for a database table).
        string csvPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode_results.csv");
        using (var writer = new StreamWriter(csvPath, false))
        {
            writer.WriteLine("FileName,CodeType,CodeText");
            foreach (var record in results)
            {
                // Escape commas in fields to preserve CSV integrity.
                string fileName = record.FileName.Replace(",", " ");
                string codeType = record.CodeType.Replace(",", " ");
                string codeText = record.CodeText.Replace(",", " ");
                writer.WriteLine($"{fileName},{codeType},{codeText}");
            }
        }

        Console.WriteLine($"Detection results written to {csvPath}");

        // Real database insertion would go here, e.g., using ADO.NET or an ORM.
        // Example (commented out because the required NuGet packages are not available in the runner):
        // using var connection = new SqliteConnection("Data Source=barcodes.db");
        // connection.Open();
        // var command = connection.CreateCommand();
        // command.CommandText = "CREATE TABLE IF NOT EXISTS Barcodes (FileName TEXT, CodeType TEXT, CodeText TEXT);";
        // command.ExecuteNonQuery();
        // foreach (var record in results)
        // {
        //     command.CommandText = "INSERT INTO Barcodes (FileName, CodeType, CodeText) VALUES (@file, @type, @text);";
        //     command.Parameters.AddWithValue("@file", record.FileName);
        //     command.Parameters.AddWithValue("@type", record.CodeType);
        //     command.Parameters.AddWithValue("@text", record.CodeText);
        //     command.ExecuteNonQuery();
        // }
    }
}