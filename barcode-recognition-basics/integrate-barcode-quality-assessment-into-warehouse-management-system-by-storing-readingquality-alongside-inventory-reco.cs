// Title: Barcode Quality Assessment for Warehouse Inventory
// Description: Demonstrates generating Code128 barcodes, reading them, capturing reading quality, and storing results with inventory data.
// Category-Description: This example belongs to Aspose.BarCode generation and recognition operations. It shows how to use BarcodeGenerator to create barcodes, BarCodeReader to decode them, and retrieve the ReadingQuality metric. Typical use cases include inventory management, quality control, and integration of barcode data into business systems. Developers often need to generate barcodes, assess scan reliability, and persist the information alongside product records.
// Prompt: Integrate barcode quality assessment into a warehouse management system by storing ReadingQuality alongside inventory records.
// Tags: code128, barcode generation, barcode recognition, readingquality, png, json, inventory, aspose.barcode, aspose.barcode.generation, aspose.barcode.recognition

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

namespace WarehouseBarcodeDemo
{
    /// <summary>
    /// Simple inventory record that includes barcode reading quality.
    /// </summary>
    public class InventoryRecord
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CodeText { get; set; }
        public double ReadingQuality { get; set; }
    }

    /// <summary>
    /// Demonstrates barcode generation, recognition, and quality capture for inventory items.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point. Generates barcodes for sample inventory, reads them to obtain quality metrics,
        /// and serializes the enriched records to a JSON file.
        /// </summary>
        static void Main()
        {
            // Define sample inventory items.
            var inventory = new List<InventoryRecord>
            {
                new InventoryRecord { Id = 1, Name = "Widget A", CodeText = "WIDGETA123" },
                new InventoryRecord { Id = 2, Name = "Gadget B", CodeText = "GADGETB456" }
            };

            // Ensure the output directory for barcode images exists.
            string barcodeDir = "Barcodes";
            if (!Directory.Exists(barcodeDir))
            {
                Directory.CreateDirectory(barcodeDir);
            }

            // Process each inventory item: generate barcode, read it, and store quality.
            foreach (var item in inventory)
            {
                // Build the file path for the barcode image.
                string imagePath = Path.Combine(barcodeDir, $"barcode_{item.Id}.png");

                // Generate a Code128 barcode image from the item's CodeText.
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, item.CodeText))
                {
                    generator.Save(imagePath, BarCodeImageFormat.Png);
                }

                // Read the generated barcode and capture its reading quality.
                using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
                {
                    // Iterate over detected barcodes (expecting one per image).
                    foreach (var result in reader.ReadBarCodes())
                    {
                        // ReadingQuality is a double representing the quality percentage.
                        item.ReadingQuality = result.ReadingQuality;
                        // Process only the first detected barcode for this image.
                        break;
                    }
                }
            }

            // Serialize the enriched inventory records to a formatted JSON file.
            string jsonPath = "inventory.json";
            string json = JsonSerializer.Serialize(inventory, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(jsonPath, json);

            // Output summary information to the console.
            Console.WriteLine($"Processed {inventory.Count} inventory items.");
            Console.WriteLine($"Barcode images saved in '{barcodeDir}'.");
            Console.WriteLine($"Inventory data with reading quality saved to '{jsonPath}'.");
        }
    }
}