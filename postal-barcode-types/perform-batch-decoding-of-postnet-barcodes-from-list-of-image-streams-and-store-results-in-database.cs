// Title: Batch decode Postnet barcodes from image streams and save results
// Description: Demonstrates how to read multiple Postnet barcodes from a collection of image files, extract details, and persist them as JSON (simulating database storage).
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing the BarCodeReader class for batch decoding of Postnet symbology. Typical use cases include processing shipments, mail sorting, or any bulk scanning scenario where many barcode images need to be read and stored. Developers often need to iterate over image streams, extract barcode metadata, and integrate results with databases or services.
// Prompt: Perform batch decoding of Postnet barcodes from a list of image streams and store results in a database.
// Tags: postnet, barcode, batch decoding, json, aspose.barcode, barcodereader, decoding, database

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing; // Required for Bitmap if needed

namespace PostnetBatchDecode
{
    /// <summary>
    /// Simple record to hold decoding results for each processed image.
    /// </summary>
    public class DecodeRecord
    {
        public string FileName { get; set; }
        public string CodeText { get; set; }
        public string CodeTypeName { get; set; }
        public double ReadingQuality { get; set; }
        public int RegionX { get; set; }
        public int RegionY { get; set; }
        public int RegionWidth { get; set; }
        public int RegionHeight { get; set; }
    }

    /// <summary>
    /// Demonstrates batch decoding of Postnet barcodes from image files and persisting results.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point. Reads a list of image files, decodes Postnet barcodes, and writes results to a JSON file.
        /// </summary>
        static void Main()
        {
            // Define a sample list of image file paths containing Postnet barcodes.
            // In production these streams could be retrieved from a database, cloud storage, etc.
            var imageFiles = new List<string>
            {
                "postnet1.png",
                "postnet2.png",
                "postnet3.png"
            };

            // Collection to store decoding results.
            var results = new List<DecodeRecord>();

            // Process each image file individually.
            foreach (var filePath in imageFiles)
            {
                // Verify that the file exists before attempting to read it.
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"File not found: {filePath}");
                    continue;
                }

                // Open the image file as a read‑only stream.
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    // Initialize BarCodeReader for the Postnet symbology.
                    using (BarCodeReader reader = new BarCodeReader(fs, DecodeType.Postnet))
                    {
                        // Read all barcodes present in the image.
                        BarCodeResult[] barcodes = reader.ReadBarCodes();

                        // Iterate through each detected barcode.
                        foreach (var result in barcodes)
                        {
                            // Extract the bounding rectangle of the barcode region.
                            var rect = result.Region.Rectangle;

                            // Populate a DecodeRecord with relevant information.
                            var record = new DecodeRecord
                            {
                                FileName = Path.GetFileName(filePath),
                                CodeText = result.CodeText,
                                CodeTypeName = result.CodeTypeName,
                                ReadingQuality = result.ReadingQuality,
                                RegionX = rect.X,
                                RegionY = rect.Y,
                                RegionWidth = rect.Width,
                                RegionHeight = rect.Height
                            };

                            // Add the record to the results collection.
                            results.Add(record);

                            // Output a brief summary to the console.
                            Console.WriteLine($"Decoded from {record.FileName}: {record.CodeText} (Type: {record.CodeTypeName})");
                        }
                    }
                }
            }

            // Serialize the results to JSON (acting as a stand‑in for database storage).
            string outputPath = "postnet_decode_results.json";
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(results, jsonOptions);
            File.WriteAllText(outputPath, json);
            Console.WriteLine($"Decoding results saved to {outputPath}");

            // Real database storage could be implemented here, e.g., using SQLite or another DB.
            // Example (commented out because the required NuGet package is not available in the runner):
            // using var connection = new SqliteConnection("Data Source=postnet.db");
            // connection.Open();
            // // Create table and insert records...
        }
    }
}