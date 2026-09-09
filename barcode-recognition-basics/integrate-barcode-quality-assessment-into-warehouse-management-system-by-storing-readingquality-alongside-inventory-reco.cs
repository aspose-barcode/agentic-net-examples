// Title: Barcode Quality Assessment for Inventory Items
// Description: Demonstrates generating QR barcodes for inventory items, reading them back, and capturing the reading quality metric for storage alongside inventory data.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create QR codes, BarCodeReader to decode them, and the ReadingQuality property to evaluate scan reliability. Developers building warehouse management or inventory tracking systems often need to generate barcodes, verify their readability, and store quality metrics for quality control and error handling. The example uses key API classes such as BarcodeGenerator, BarCodeReader, BarCodeResult, and related enums.
/// Prompt: Integrate barcode quality assessment into a warehouse management system by storing ReadingQuality alongside inventory records.
/// Tags: barcode, qr, quality-assessment, generation, recognition, inventory, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

namespace BarcodeQualityDemo
{
    /// <summary>
    /// Represents an inventory item with basic details and a barcode reading quality metric.
    /// </summary>
    class InventoryItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string BarcodeText { get; set; } = string.Empty;
        public int ReadingQuality { get; set; }
    }

    /// <summary>
    /// Demonstrates generating QR barcodes for inventory items, reading them back, and recording the reading quality.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the demo. Generates barcodes, reads them, captures quality, and outputs results.
        /// </summary>
        static void Main()
        {
            // Create a temporary folder to store generated barcode images.
            string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(tempFolder);

            // Sample inventory items to process.
            List<InventoryItem> items = new List<InventoryItem>
            {
                new InventoryItem { Id = 1, Name = "Widget A", BarcodeText = "WIDGETA001" },
                new InventoryItem { Id = 2, Name = "Widget B", BarcodeText = "WIDGETB002" },
                new InventoryItem { Id = 3, Name = "Widget C", BarcodeText = "WIDGETC003" }
            };

            // Process each inventory item: generate barcode, read it, and store reading quality.
            foreach (InventoryItem item in items)
            {
                // Define the file path for the barcode image.
                string imagePath = Path.Combine(tempFolder, $"item_{item.Id}.png");

                // Generate a QR barcode image for the item's barcode text.
                using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, item.BarcodeText))
                {
                    generator.Save(imagePath, BarCodeImageFormat.Png);
                }

                // Verify that the image was created before attempting to read it.
                if (File.Exists(imagePath))
                {
                    // Read the barcode from the generated image and capture its reading quality.
                    using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.QR))
                    {
                        foreach (BarCodeResult result in reader.ReadBarCodes())
                        {
                            // Round the quality to the nearest integer and assign to the item.
                            item.ReadingQuality = (int)Math.Round(result.ReadingQuality);
                        }
                    }
                }
                else
                {
                    // Log an error if barcode generation failed.
                    Console.WriteLine($"Failed to generate barcode image for item {item.Id}");
                }
            }

            // Output the inventory records with their associated reading quality values.
            Console.WriteLine("Inventory Records with Barcode Reading Quality:");
            foreach (InventoryItem item in items)
            {
                Console.WriteLine($"Id: {item.Id}, Name: {item.Name}, Barcode: {item.BarcodeText}, ReadingQuality: {item.ReadingQuality}");
            }

            // Clean up temporary files and folder.
            try
            {
                foreach (string file in Directory.GetFiles(tempFolder))
                {
                    File.Delete(file);
                }
                Directory.Delete(tempFolder);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Cleanup error: {ex.Message}");
            }
        }
    }
}