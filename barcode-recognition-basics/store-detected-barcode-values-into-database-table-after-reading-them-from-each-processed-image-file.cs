using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

namespace BarcodeProcessing
{
    // Simple record to hold barcode information
    public class BarcodeRecord
    {
        public string FileName { get; set; }
        public string CodeText { get; set; }
        public string CodeTypeName { get; set; }
        public int Confidence { get; set; }
    }

    class Program
    {
        static void Main()
        {
            // Folder that contains images to be processed
            string imagesFolder = "Images";

            // Ensure the folder exists
            if (!Directory.Exists(imagesFolder))
            {
                Directory.CreateDirectory(imagesFolder);
            }

            // Gather image files (png, jpg, jpeg, bmp)
            var imageFiles = Directory.GetFiles(imagesFolder, "*.*", SearchOption.TopDirectoryOnly)
                .Where(f => f.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                            f.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                            f.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                            f.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase))
                .ToList();

            // If no images are present, create a sample barcode image
            if (imageFiles.Count == 0)
            {
                string samplePath = Path.Combine(imagesFolder, "sample.png");
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
                {
                    generator.Save(samplePath);
                }
                imageFiles.Add(samplePath);
                Console.WriteLine($"No images found. Generated sample barcode at {samplePath}");
            }

            var records = new List<BarcodeRecord>();

            // Process each image file
            foreach (var filePath in imageFiles)
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"File not found: {filePath}");
                    continue;
                }

                // Initialize reader for all supported barcode types
                using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
                {
                    // Read all barcodes in the image
                    foreach (var result in reader.ReadBarCodes())
                    {
                        var record = new BarcodeRecord
                        {
                            FileName = Path.GetFileName(filePath),
                            CodeText = result.CodeText,
                            CodeTypeName = result.CodeTypeName,
                            Confidence = (int)result.Confidence
                        };
                        records.Add(record);
                        Console.WriteLine($"Detected {record.CodeTypeName} in {record.FileName}: {record.CodeText}");
                    }
                }
            }

            // Store results in a local JSON file (acts as a simple database substitute)
            string jsonPath = "barcode_results.json";
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(records, jsonOptions);
            File.WriteAllText(jsonPath, json);
            Console.WriteLine($"Barcode data saved to {jsonPath}");

            // Real database implementation (e.g., SQLite) would look like this:
            // ---------------------------------------------------------------
            // // Requires Microsoft.Data.Sqlite NuGet package
            // using var connection = new SqliteConnection("Data Source=barcodes.db");
            // connection.Open();
            // var createCmd = connection.CreateCommand();
            // createCmd.CommandText = @"CREATE TABLE IF NOT EXISTS Barcodes (
            //     FileName TEXT,
            //     CodeText TEXT,
            //     CodeTypeName TEXT,
            //     Confidence INTEGER
            // );";
            // createCmd.ExecuteNonQuery();
            // foreach (var rec in records)
            // {
            //     var insertCmd = connection.CreateCommand();
            //     insertCmd.CommandText = "INSERT INTO Barcodes (FileName, CodeText, CodeTypeName, Confidence) VALUES (@file, @text, @type, @conf);";
            //     insertCmd.Parameters.AddWithValue("@file", rec.FileName);
            //     insertCmd.Parameters.AddWithValue("@text", rec.CodeText);
            //     insertCmd.Parameters.AddWithValue("@type", rec.CodeTypeName);
            //     insertCmd.Parameters.AddWithValue("@conf", rec.Confidence);
            //     insertCmd.ExecuteNonQuery();
            // }
            // ---------------------------------------------------------------
        }
    }
}