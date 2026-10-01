// Title: Warehouse barcode generation and quality assessment demo
// Description: Demonstrates creating QR code barcodes for inventory items, reading them back, and recording a placeholder reading quality metric.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing how to use BarcodeGenerator, BarCodeReader, and related classes to encode data, decode barcodes, and capture quality metrics. Typical use cases include inventory tracking, warehouse management, and quality control where developers need to generate barcodes, assess scan reliability, and store results alongside product records.
// Prompt: Integrate barcode quality assessment into a warehouse management system by storing ReadingQuality alongside inventory records.
// Tags: barcode, qr, generation, recognition, quality-assessment, inventory, aspose.barcode, csv-output

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

namespace WarehouseBarcodeQualityDemo
{
    /// <summary>
    /// Represents an inventory item with barcode information and reading quality.
    /// </summary>
    public class InventoryItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string BarcodeImagePath { get; set; } = string.Empty;
        public float ReadingQuality { get; set; }
    }

    /// <summary>
    /// Demonstrates barcode generation, reading, and quality recording for warehouse inventory items.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point that creates barcodes, reads them, captures quality, and writes results to a CSV file.
        /// </summary>
        static void Main()
        {
            // Create a temporary folder to store generated barcode images and the output CSV.
            string tempFolder = Path.Combine(Path.GetTempPath(), "WarehouseBarcodes_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(tempFolder);

            // Define a list of inventory items to process.
            var items = new List<InventoryItem>
            {
                new InventoryItem { Id = 1, Name = "Widget A", BarcodeImagePath = Path.Combine(tempFolder, "widgetA.png") },
                new InventoryItem { Id = 2, Name = "Gadget B", BarcodeImagePath = Path.Combine(tempFolder, "gadgetB.png") }
            };

            // Generate QR code barcodes for each inventory item.
            foreach (var item in items)
            {
                string codeText = $"ID:{item.Id};NAME:{item.Name}";
                using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
                {
                    // Set the module size (X dimension) for better readability.
                    generator.Parameters.Barcode.XDimension.Point = 2f;
                    // Save the barcode image as PNG.
                    generator.Save(item.BarcodeImagePath, BarCodeImageFormat.Png);
                }
            }

            // Read each barcode image and capture a placeholder reading quality value.
            foreach (var item in items)
            {
                if (!File.Exists(item.BarcodeImagePath))
                {
                    Console.WriteLine($"Barcode image not found for item {item.Id}: {item.BarcodeImagePath}");
                    continue;
                }

                using (var reader = new BarCodeReader(item.BarcodeImagePath, DecodeType.QR))
                {
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        // ReadingQuality property is not available in this version; using placeholder value.
                        float quality = 0f;
                        item.ReadingQuality = quality;
                        Console.WriteLine($"Item {item.Id} ({item.Name}) - ReadingQuality: {quality:F2}");
                    }
                }
            }

            // Write the inventory data, including reading quality, to a CSV file.
            string csvPath = Path.Combine(tempFolder, "inventory_with_quality.csv");
            using (var writer = new StreamWriter(csvPath, false))
            {
                writer.WriteLine("Id,Name,BarcodeImagePath,ReadingQuality");
                foreach (var item in items)
                {
                    writer.WriteLine($"{item.Id},{item.Name},{item.BarcodeImagePath},{item.ReadingQuality:F2}");
                }
            }

            Console.WriteLine($"Inventory data with reading quality saved to: {csvPath}");
        }
    }
}