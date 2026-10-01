// Title: Store detected barcode values into a JSON file (simulating a database) after reading them from generated images
// Description: Demonstrates generating sample barcodes, reading them with Aspose.BarCode, and persisting the extracted values to a JSON file, which can be replaced by a database table.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator to create images, BarCodeReader to decode multiple symbologies, and how to collect results for further processing such as database insertion. Developers working with inventory, logistics, or QR‑code scanning often need to batch‑process images and store decoded data.
// Prompt: Store detected barcode values into a database table after reading them from each processed image file.
// Tags: barcode generation, barcode recognition, json serialization, aspose.barcode, code128, qr, datamatrix

using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

namespace BarcodeProcessingDemo
{
    /// <summary>
    /// Simple POCO to hold barcode information extracted from an image file.
    /// </summary>
    public class BarcodeRecord
    {
        public string FilePath { get; set; }
        public string CodeText { get; set; }
        public string CodeTypeName { get; set; }
    }

    /// <summary>
    /// Demonstrates barcode generation, recognition, and storage of results.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the demo. Generates sample barcodes, reads them, and saves the decoded data to a JSON file.
        /// </summary>
        /// <param name="args">Command‑line arguments (not used).</param>
        static void Main(string[] args)
        {
            // Create a dedicated temporary folder for sample barcode images.
            string tempFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(tempFolder);

            // List to keep track of generated image file paths.
            List<string> imageFiles = new List<string>();

            // Generate a few sample barcode images of different symbologies.
            GenerateSampleBarcode(EncodeTypes.Code128, "ABC123", Path.Combine(tempFolder, "code128.png"), imageFiles);
            GenerateSampleBarcode(EncodeTypes.QR, "https://example.com", Path.Combine(tempFolder, "qr.png"), imageFiles);
            GenerateSampleBarcode(EncodeTypes.DataMatrix, "DM-001", Path.Combine(tempFolder, "datamatrix.png"), imageFiles);

            // Read barcodes from the generated images and collect results.
            List<BarcodeRecord> records = new List<BarcodeRecord>();

            foreach (string filePath in imageFiles)
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"File not found: {filePath}");
                    continue;
                }

                // BarCodeReader implements IDisposable, so we use a using block.
                using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
                {
                    try
                    {
                        // Read all barcodes present in the image.
                        BarCodeResult[] results = reader.ReadBarCodes();
                        foreach (var result in results)
                        {
                            if (!string.IsNullOrEmpty(result.CodeText))
                            {
                                // Store each successful decode in the records list.
                                records.Add(new BarcodeRecord
                                {
                                    FilePath = filePath,
                                    CodeText = result.CodeText,
                                    CodeTypeName = result.CodeTypeName
                                });
                            }
                        }
                    }
                    catch (ArgumentException ex)
                    {
                        // Handles cases where the file cannot be loaded as an image.
                        Console.WriteLine($"Skipping file {filePath}: {ex.Message}");
                    }
                }
            }

            // Store the collected barcode data into a JSON file (as a stand‑in for a database table).
            string jsonPath = Path.Combine(tempFolder, "Barcodes.json");
            string json = JsonSerializer.Serialize(records, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(jsonPath, json);
            Console.WriteLine($"Barcode data saved to: {jsonPath}");

            // In a real application you would insert the records into a database table here.
            // Example (commented out because the required NuGet packages are not available in the runner):
            // using var connection = new SqliteConnection("Data Source=barcodes.db");
            // connection.Open();
            // using var command = connection.CreateCommand();
            // command.CommandText = "CREATE TABLE IF NOT EXISTS Barcodes (FilePath TEXT, CodeText TEXT, CodeTypeName TEXT);";
            // command.ExecuteNonQuery();
            // foreach (var rec in records)
            // {
            //     command.CommandText = "INSERT INTO Barcodes (FilePath, CodeText, CodeTypeName) VALUES (@path, @text, @type);";
            //     command.Parameters.Clear();
            //     command.Parameters.AddWithValue("@path", rec.FilePath);
            //     command.Parameters.AddWithValue("@text", rec.CodeText);
            //     command.Parameters.AddWithValue("@type", rec.CodeTypeName);
            //     command.ExecuteNonQuery();
            // }
        }

        /// <summary>
        /// Generates a barcode image using the specified encode type and text, saves it to disk, and records the file path.
        /// </summary>
        /// <param name="encodeType">The barcode symbology to generate.</param>
        /// <param name="codeText">The data to encode in the barcode.</param>
        /// <param name="outputPath">Full path where the image will be saved.</param>
        /// <param name="fileList">List that receives the generated file path.</param>
        private static void GenerateSampleBarcode(BaseEncodeType encodeType, string codeText, string outputPath, List<string> fileList)
        {
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Save directly to a file in PNG format.
                generator.Save(outputPath, BarCodeImageFormat.Png);
                fileList.Add(outputPath);
            }
        }
    }
}