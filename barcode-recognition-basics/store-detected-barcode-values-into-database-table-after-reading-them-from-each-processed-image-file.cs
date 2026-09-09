// Title: Barcode generation, recognition, and JSON export example
// Description: Demonstrates creating barcode images, reading them back, and storing the detected values in a JSON file (as a stand‑in for database storage). Shows how to use Aspose.BarCode to generate and recognize multiple symbologies.
// Category-Description: This example belongs to the Aspose.BarCode “Barcode generation and recognition” category. It illustrates the use of BarcodeGenerator for creating barcodes, BarCodeReader for detecting them, and common .NET collections for handling results. Developers working with barcode automation, batch processing, or data extraction typically need to generate barcodes, read them from images, and persist the information to a database or other storage.
// Prompt: Store detected barcode values into a database table after reading them from each processed image file.
// Tags: barcode generation, barcode recognition, json output, aspose.barcode, csharp, encode types, decode types

using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating sample barcode images, reading them, and persisting the results.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcodes, reads them, and writes results to a JSON file.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for sample barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // List to hold generated image file paths
        List<string> imageFiles = new List<string>();

        // Sample barcode data (type, text, file name)
        var samples = new (BaseEncodeType encodeType, string text, string fileName)[]
        {
            (EncodeTypes.Code128, "1234567890", "code128.png"),
            (EncodeTypes.QR, "Hello QR", "qr.png"),
            (EncodeTypes.DataMatrix, "DM12345", "datamatrix.png")
        };

        // Generate barcode images and collect their file paths
        foreach (var sample in samples)
        {
            string filePath = Path.Combine(tempFolder, sample.fileName);
            using (var generator = new BarcodeGenerator(sample.encodeType, sample.text))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            imageFiles.Add(filePath);
        }

        // List to collect barcode read results
        List<BarcodeRecord> records = new List<BarcodeRecord>();

        // Read each generated image and store results
        foreach (string file in imageFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            try
            {
                using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
                {
                    BarCodeResult[] results = reader.ReadBarCodes();
                    foreach (var result in results)
                    {
                        if (!string.IsNullOrEmpty(result.CodeText))
                        {
                            records.Add(new BarcodeRecord
                            {
                                FilePath = file,
                                CodeText = result.CodeText,
                                CodeTypeName = result.CodeTypeName
                            });
                        }
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Failed to read '{file}': {ex.Message}");
            }
        }

        // Store results in a JSON file (substitute for a database)
        string outputPath = Path.Combine(tempFolder, "barcode_results.json");
        string json = JsonSerializer.Serialize(records, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(outputPath, json);

        Console.WriteLine($"Barcode reading completed. Results saved to: {outputPath}");

        // Note: In a real application, you would insert the records into a database
        // using appropriate ADO.NET or ORM libraries (e.g., Microsoft.Data.Sqlite, System.Data.SqlClient, etc.).
    }
}

/// <summary>
/// Simple DTO representing a barcode detection result.
/// </summary>
class BarcodeRecord
{
    public string FilePath { get; set; }
    public string CodeText { get; set; }
    public string CodeTypeName { get; set; }
}