using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

namespace ReadingQualitySample
{
    class Program
    {
        // Simple record to hold barcode data and its reading quality
        class BarcodeInfo
        {
            public string CodeText { get; set; }
            public double ReadingQuality { get; set; }
        }

        static void Main(string[] args)
        {
            // Prepare a list to collect results
            List<BarcodeInfo> results = new List<BarcodeInfo>();

            // Directory to store temporary barcode images
            string imageDir = "Barcodes";
            if (!Directory.Exists(imageDir))
            {
                Directory.CreateDirectory(imageDir);
            }

            // Generate and read a few DataMatrix barcodes
            for (int i = 1; i <= 5; i++)
            {
                string codeText = $"DM{i:D3}";
                string imagePath = Path.Combine(imageDir, $"dm_{i}.png");

                // Create DataMatrix barcode and save it to a file
                using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
                {
                    generator.Save(imagePath);
                }

                // Read the barcode and capture its reading quality
                using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.DataMatrix))
                {
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        // Store the code text and reading quality
                        BarcodeInfo info = new BarcodeInfo
                        {
                            CodeText = result.CodeText,
                            ReadingQuality = result.ReadingQuality
                        };
                        results.Add(info);
                    }
                }
            }

            // Store the collected data in a CSV file (as a stand‑in for a database)
            string csvPath = "reading_quality.csv";
            using (StreamWriter writer = new StreamWriter(csvPath, false))
            {
                writer.WriteLine("CodeText,ReadingQuality");
                foreach (BarcodeInfo info in results)
                {
                    writer.WriteLine($"{info.CodeText},{info.ReadingQuality}");
                }
            }

            Console.WriteLine($"Reading quality data written to '{csvPath}'.");
            // In a real scenario, replace the CSV logic with actual database insertion,
            // e.g., using a SQLite connection:
            // // using var connection = new SqliteConnection("Data Source=barcodes.db");
            // // connection.Open();
            // // using var command = connection.CreateCommand();
            // // command.CommandText = "INSERT INTO BarcodeQuality (CodeText, ReadingQuality) VALUES (@code, @quality)";
            // // command.Parameters.AddWithValue("@code", info.CodeText);
            // // command.Parameters.AddWithValue("@quality", info.ReadingQuality);
            // // command.ExecuteNonQuery();
        }
    }
}