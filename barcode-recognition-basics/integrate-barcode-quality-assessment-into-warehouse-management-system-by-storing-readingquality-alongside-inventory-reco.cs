using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

namespace WarehouseBarcodeDemo
{
    // Simple inventory record that includes barcode reading quality
    class InventoryItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BarcodeValue { get; set; }
        public double ReadingQuality { get; set; } // percent value from BarCodeResult
    }

    class Program
    {
        static void Main()
        {
            // Sample inventory item
            var item = new InventoryItem
            {
                Id = 1,
                Name = "Widget",
                BarcodeValue = "123456789012"
            };

            // Path for temporary barcode image
            string barcodePath = Path.Combine(Path.GetTempPath(), "item_barcode.png");

            // Generate barcode image for the item's barcode value
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, item.BarcodeValue))
            {
                // Optional: set image size or other parameters here
                generator.Save(barcodePath);
            }

            // Verify that the image was created before attempting recognition
            if (!File.Exists(barcodePath))
            {
                Console.WriteLine("Failed to generate barcode image.");
                return;
            }

            // Recognize the barcode and obtain reading quality
            using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
            {
                // Use a higher quality preset to demonstrate quality settings
                reader.QualitySettings = QualitySettings.HighQuality;
                // Optionally adjust the barcode element quality mode
                reader.QualitySettings.BarcodeQuality = BarcodeQualityMode.High;

                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    // Store the reading quality (percentage) in the inventory record
                    item.ReadingQuality = result.ReadingQuality;

                    // Output details for verification
                    Console.WriteLine($"Item ID: {item.Id}");
                    Console.WriteLine($"Name: {item.Name}");
                    Console.WriteLine($"Barcode Text: {result.CodeText}");
                    Console.WriteLine($"Reading Quality: {item.ReadingQuality}%");
                }
            }

            // Clean up temporary file
            try
            {
                File.Delete(barcodePath);
            }
            catch
            {
                // Ignore any cleanup errors
            }
        }
    }
}