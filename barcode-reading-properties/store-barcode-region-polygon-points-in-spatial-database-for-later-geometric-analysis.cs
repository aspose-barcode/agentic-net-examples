using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

namespace BarcodeRegionCapture
{
    class Program
    {
        // Simple DTO for point coordinates
        public class PointDto
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

        // DTO for storing barcode text and its polygon points
        public class BarcodeRegionRecord
        {
            public string CodeText { get; set; }
            public List<PointDto> Points { get; set; }
        }

        static void Main()
        {
            const string imagePath = "sample_barcode.png";
            const string jsonPath = "barcode_regions.json";

            // Step 1: Generate a sample barcode image
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                // Optional: set image size or other parameters here
                generator.Save(imagePath);
            }

            // Verify that the image was created
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"Failed to create barcode image at '{imagePath}'.");
                return;
            }

            // Step 2: Load the image and recognize barcodes
            var records = new List<BarcodeRegionRecord>();

            using (var bitmap = new Bitmap(imagePath))
            using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    var points = new List<PointDto>();
                    foreach (var pt in result.Region.Points)
                    {
                        points.Add(new PointDto { X = pt.X, Y = pt.Y });
                    }

                    records.Add(new BarcodeRegionRecord
                    {
                        CodeText = result.CodeText,
                        Points = points
                    });
                }
            }

            // Step 3: Serialize the results to JSON (acts as a stand‑in for a spatial DB)
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(records, jsonOptions);
            File.WriteAllText(jsonPath, json);

            Console.WriteLine($"Barcode region data written to '{jsonPath}'.");
        }
    }
}