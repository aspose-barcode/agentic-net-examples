using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

namespace WarehouseBarcodeQuality
{
    // Simple inventory record
    public class InventoryItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BarcodeText { get; set; }
        public double ReadingQuality { get; set; } // Value from 0 to 100
    }

    /// <summary>
    /// Entry point for the Warehouse Barcode Quality application.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Generates barcodes for sample inventory items, reads them back,
        /// and records the reading quality for each item.
        /// </summary>
        static void Main()
        {
            // Define a sample list of inventory items.
            var items = new List<InventoryItem>
            {
                new InventoryItem { Id = 1, Name = "Widget A", BarcodeText = "WGT001" },
                new InventoryItem { Id = 2, Name = "Gadget B", BarcodeText = "GDT002" },
                new InventoryItem { Id = 3, Name = "Device C", BarcodeText = "DVC003" }
            };

            // Determine the output directory for generated barcode images.
            string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
            if (!Directory.Exists(outputDir))
            {
                // Create the directory if it does not already exist.
                Directory.CreateDirectory(outputDir);
            }

            // Process each inventory item.
            foreach (var item in items)
            {
                // Build the full file path for the barcode image.
                string barcodePath = Path.Combine(outputDir, $"barcode_{item.Id}.png");

                // Generate a Code128 barcode image for the current item.
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, item.BarcodeText))
                {
                    // Set a high resolution for better quality (300 DPI).
                    generator.Parameters.Resolution = 300f;
                    // Save the generated barcode to the specified path.
                    generator.Save(barcodePath);
                }

                // Verify that the barcode image was successfully created.
                if (!File.Exists(barcodePath))
                {
                    Console.WriteLine($"Failed to create barcode image for item {item.Id}");
                    continue; // Skip reading if the file does not exist.
                }

                // Read the barcode from the image and capture its reading quality.
                using (var reader = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
                {
                    foreach (var result in reader.ReadBarCodes())
                    {
                        // Store the reading quality (percentage) in the inventory record.
                        item.ReadingQuality = result.ReadingQuality;
                        // Assuming only one barcode per image; exit after the first result.
                        break;
                    }
                }
            }

            // Output the inventory records along with their barcode reading quality.
            Console.WriteLine("Inventory records with barcode reading quality:");
            foreach (var item in items)
            {
                Console.WriteLine($"Id: {item.Id}, Name: {item.Name}, Code: {item.BarcodeText}, ReadingQuality: {item.ReadingQuality:F2}%");
            }
        }
    }
}