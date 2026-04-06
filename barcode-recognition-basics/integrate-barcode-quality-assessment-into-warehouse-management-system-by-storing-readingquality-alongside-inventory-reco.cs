using System;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

namespace WarehouseBarcodeDemo
{
    // Simple inventory item model
    class InventoryItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BarcodeValue { get; set; }
        public double ReadingQuality { get; set; }
    }

    class Program
    {
        static void Main()
        {
            // Prepare a sample inventory item
            var item = new InventoryItem
            {
                Id = 1,
                Name = "Sample Product",
                BarcodeValue = "ITEM123"
            };

            // Generate a barcode image for the item's barcode value
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, item.BarcodeValue))
            {
                // Save the barcode to a temporary file
                generator.Save("barcode.png");
            }

            // Recognize the barcode and obtain its reading quality
            using (var reader = new BarCodeReader("barcode.png", DecodeType.Code128))
            {
                // Iterate over all detected barcodes (should be one in this case)
                foreach (var result in reader.ReadBarCodes())
                {
                    // Store the reading quality in the inventory record
                    item.ReadingQuality = result.ReadingQuality;
                }
            }

            // Simulate storing the item in a collection (e.g., a database)
            var inventory = new List<InventoryItem>();
            inventory.Add(item);

            // Output the stored information
            foreach (var invItem in inventory)
            {
                Console.WriteLine($"ID: {invItem.Id}");
                Console.WriteLine($"Name: {invItem.Name}");
                Console.WriteLine($"Barcode: {invItem.BarcodeValue}");
                Console.WriteLine($"Reading Quality: {invItem.ReadingQuality}%");
                Console.WriteLine();
            }
        }
    }
}