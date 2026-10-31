// Title: Barcode Quality Assessment for Inventory Items
// Description: Demonstrates generating Code128 barcodes for inventory items, reading them with Aspose.BarCode, and capturing the reading quality metric for storage.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing how to use BarcodeGenerator to create barcodes and BarCodeReader to decode them while retrieving the ReadingQuality property. Typical use cases include quality control in warehouse management, inventory tracking, and ensuring scan reliability. Developers often need to generate barcodes, assess their readability, and store quality metrics alongside business data.
// Prompt: Integrate barcode quality assessment into a warehouse management system by storing ReadingQuality alongside inventory records.
// Tags: code128, barcode-generation, barcode-recognition, reading-quality, inventory-management, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

namespace BarcodeQualityAssessment
{
    /// <summary>
    /// Represents an inventory item with associated barcode information and reading quality.
    /// </summary>
    public class InventoryItem
    {
        /// <summary>
        /// Unique identifier of the inventory item.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Human‑readable name of the inventory item.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// File path to the generated barcode image.
        /// </summary>
        public string BarcodePath { get; set; }

        /// <summary>
        /// Integer representation of the barcode reading quality (0‑100).
        /// </summary>
        public int ReadingQuality { get; set; }
    }

    /// <summary>
    /// Entry point for the barcode quality assessment example.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Generates barcodes for sample inventory items, reads them to obtain quality metrics,
        /// and outputs the results to the console.
        /// </summary>
        static void Main()
        {
            // Initialize sample inventory data.
            var items = new List<InventoryItem>
            {
                new InventoryItem { Id = 1, Name = "Widget A", BarcodePath = "" },
                new InventoryItem { Id = 2, Name = "Gadget B", BarcodePath = "" },
                new InventoryItem { Id = 3, Name = "Device C", BarcodePath = "" }
            };

            // Process each inventory item: generate a barcode, evaluate its reading quality, and store the results.
            foreach (var item in items)
            {
                // Create a temporary file path for the barcode image.
                string tempFile = Path.Combine(Path.GetTempPath(), $"barcode_{item.Id}.png");

                // Generate a Code128 barcode image and save it as PNG.
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, $"ITEM{item.Id:D4}"))
                {
                    generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                    generator.Parameters.BackColor = Aspose.Drawing.Color.White;
                    generator.Save(tempFile, BarCodeImageFormat.Png);
                }

                // Initialize quality variable.
                double quality = 0.0;

                // Read the generated barcode and capture the reading quality.
                using (var reader = new BarCodeReader(tempFile, DecodeType.AllSupportedTypes))
                {
                    foreach (var result in reader.ReadBarCodes())
                    {
                        // Use the first detected result's quality metric.
                        quality = result.ReadingQuality;
                        break;
                    }
                }

                // Store the barcode path and rounded quality value back into the inventory item.
                item.BarcodePath = tempFile;
                item.ReadingQuality = (int)Math.Round(quality);
            }

            // Output the inventory records with their associated barcode reading quality.
            Console.WriteLine("Inventory Records with Barcode Reading Quality:");
            foreach (var item in items)
            {
                Console.WriteLine($"Id: {item.Id}, Name: {item.Name}, Barcode: {item.BarcodePath}, ReadingQuality: {item.ReadingQuality}");
            }
        }
    }
}