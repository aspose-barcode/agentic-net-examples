// Title: Access DataMatrix ReadingQuality and store results
// Description: Demonstrates generating DataMatrix barcodes, reading each barcode's ReadingQuality property, and persisting the values to a JSON file (as a stand‑in for a database).
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator to create DataMatrix symbols, BarCodeReader to decode them, and BarCodeResult.ReadingQuality to evaluate scan quality. Developers working with barcode quality metrics, data capture validation, or database logging will find this pattern useful for integrating barcode reading results into storage systems.
// Prompt: Access BarCodeResult.ReadingQuality for each DataMatrix barcode and store the numeric value in a database.
// Tags: datamatrix, readingquality, barcode, generation, recognition, json, aspnet, aspnetcore, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode;

/// <summary>
/// Demonstrates generating DataMatrix barcodes, reading their quality metrics,
/// and persisting the results (simulating database storage).
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample DataMatrix images, reads them to obtain
    /// ReadingQuality values, and writes the collected data to a JSON file.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for sample DataMatrix images
        string tempFolder = Path.Combine(Path.GetTempPath(), "DataMatrixSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Sample data texts for DataMatrix barcodes
        List<string> dataTexts = new List<string>
        {
            "Sample1",
            "DataMatrix2",
            "Test12345"
        };

        // Generate barcode images and collect their file paths
        List<string> imagePaths = new List<string>();
        foreach (string text in dataTexts)
        {
            string imagePath = Path.Combine(tempFolder, $"{text}.png");
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DataMatrix, text))
            {
                // Optional: set XDimension for better visibility
                generator.Parameters.Barcode.XDimension.Pixels = 4;
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }
            imagePaths.Add(imagePath);
        }

        // Read each barcode and collect ReadingQuality values
        List<BarcodeRecord> records = new List<BarcodeRecord>();
        foreach (string path in imagePaths)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine($"File not found: {path}");
                continue;
            }

            using (BarCodeReader reader = new BarCodeReader(path, DecodeType.DataMatrix))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    double quality = result.ReadingQuality;
                    records.Add(new BarcodeRecord
                    {
                        FilePath = path,
                        ReadingQuality = quality
                    });
                    Console.WriteLine($"File: {Path.GetFileName(path)} | ReadingQuality: {quality}");
                }
            }
        }

        // Store results locally as JSON (substitute for a database)
        string outputJson = Path.Combine(tempFolder, "ReadingQualityResults.json");
        string json = JsonSerializer.Serialize(records, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(outputJson, json);
        Console.WriteLine($"Results saved to: {outputJson}");

        // ----------------------------------------------------------------------
        // Real database storage (e.g., SQL Server) would look like this:
        // ----------------------------------------------------------------------
        // using (var connection = new System.Data.SqlClient.SqlConnection("your-connection-string"))
        // {
        //     connection.Open();
        //     foreach (var rec in records)
        //     {
        //         using (var command = new System.Data.SqlClient.SqlCommand(
        //             "INSERT INTO BarcodeReadings (FilePath, ReadingQuality) VALUES (@path, @quality)", connection))
        //         {
        //             command.Parameters.AddWithValue("@path", rec.FilePath);
        //             command.Parameters.AddWithValue("@quality", rec.ReadingQuality);
        //             command.ExecuteNonQuery();
        //         }
        //     }
        // }
        // Note: The required NuGet package (System.Data.SqlClient) is not available in the snippet runner,
        // so the example uses local JSON storage instead.
    }

    /// <summary>
    /// Simple DTO for persisting barcode file path and its reading quality.
    /// </summary>
    class BarcodeRecord
    {
        public string FilePath { get; set; }
        public double ReadingQuality { get; set; }
    }
}