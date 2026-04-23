using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Sample list of product IDs to encode
        string[] productIds = { "PROD001", "PROD002", "PROD003", "PROD004", "PROD005" };

        // Prepare output directory
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Generate a Code 16K barcode for each product ID
        foreach (string id in productIds)
        {
            // Create a BarcodeGenerator for Code16K with the current product ID
            using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, id))
            {
                // Save the barcode as a BMP file named after the product ID
                string filePath = Path.Combine(outputFolder, $"{id}.bmp");
                generator.Save(filePath);
                Console.WriteLine($"Barcode saved: {filePath}");
            }
        }
    }
}