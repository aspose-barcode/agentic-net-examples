// Title: Extract barcode region coordinates from PNG and save to JSON
// Description: Demonstrates how to read a PNG image, detect barcodes, extract their placement region coordinates, and store the data.
// Category-Description: This example belongs to the Aspose.BarCode barcode detection and region extraction category. It showcases the use of BarCodeReader to recognize all supported barcode types, retrieve the bounding rectangle of each detected barcode, and handle the resulting region data. Developers working with image processing, inventory systems, or document automation often need to locate barcodes within images for further processing or database storage.
// Prompt: Extract barcode placement region coordinates from a PNG file and store them in a database.
// Tags: barcode detection, barcode region extraction, png, json, aspose.barcode, barcodereader, region coordinates, data persistence

using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

namespace BarcodeRegionExtractor
{
    /// <summary>
    /// Simple DTO to hold region information; in a real scenario this could be persisted to a database.
    /// </summary>
    public class BarcodeRegionInfo
    {
        public string FileName { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }

    /// <summary>
    /// Program that extracts barcode placement regions from a PNG image and stores them.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point. Generates a sample barcode image if missing, reads barcodes, extracts region data, and saves to JSON.
        /// </summary>
        static void Main()
        {
            // Define folder for sample image and output JSON.
            string folderPath = "Barcodes";
            Directory.CreateDirectory(folderPath);

            // Full path to the PNG file to be processed.
            string imagePath = Path.Combine(folderPath, "sample.png");

            // Generate a sample barcode image if it does not already exist.
            if (!File.Exists(imagePath))
            {
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
                {
                    // Optional: configure size or colors here.
                    generator.Save(imagePath, BarCodeImageFormat.Png);
                }
                Console.WriteLine($"Generated sample barcode image at: {imagePath}");
            }

            // Verify the image file exists before attempting to read it.
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"Error: File not found - {imagePath}");
                return;
            }

            // Collection to hold extracted region information.
            var regions = new List<BarcodeRegionInfo>();

            // Use BarCodeReader to detect all supported barcode types in the image.
            using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    // Obtain the bounding rectangle of the detected barcode.
                    Rectangle rect = result.Region.Rectangle;

                    // Populate DTO with region data.
                    var info = new BarcodeRegionInfo
                    {
                        FileName = imagePath,
                        X = rect.X,
                        Y = rect.Y,
                        Width = rect.Width,
                        Height = rect.Height
                    };

                    regions.Add(info);

                    // Output detection details to console.
                    Console.WriteLine($"Detected barcode: {result.CodeText}");
                    Console.WriteLine($"Region - X:{rect.X}, Y:{rect.Y}, Width:{rect.Width}, Height:{rect.Height}");
                }
            }

            // Serialize the extracted region data to JSON as a stand‑in for database storage.
            string jsonPath = Path.Combine(folderPath, "barcode_regions.json");
            string json = JsonSerializer.Serialize(regions, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(jsonPath, json);
            Console.WriteLine($"Region data saved to: {jsonPath}");
        }
    }
}