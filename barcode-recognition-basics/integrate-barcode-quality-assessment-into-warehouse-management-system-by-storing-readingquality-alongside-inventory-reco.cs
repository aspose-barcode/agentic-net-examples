// Title: Barcode quality assessment integration for inventory records
// Description: Demonstrates generating barcodes, reading them, capturing reading quality, and storing it with inventory data.
// Prompt: Integrate barcode quality assessment into a warehouse management system by storing ReadingQuality alongside inventory records.
// Tags: barcode, quality-assessment, inventory, json, aspose.barcode, code128

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Represents an inventory item with barcode and reading quality information.
/// </summary>
class InventoryItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string BarcodeValue { get; set; }
    public double ReadingQuality { get; set; }
}

/// <summary>
/// Entry point of the sample program that generates barcodes, evaluates their reading quality, and saves inventory data to JSON.
/// </summary>
class Program
{
    static void Main()
    {
        // Sample inventory records to be processed
        var items = new List<InventoryItem>
        {
            new InventoryItem { Id = 1, Name = "Widget A", BarcodeValue = "WGT001" },
            new InventoryItem { Id = 2, Name = "Widget B", BarcodeValue = "WGT002" },
            new InventoryItem { Id = 3, Name = "Widget C", BarcodeValue = "WGT003" }
        };

        // Directory for temporary barcode images
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeSamples");
        if (!Directory.Exists(tempDir))
        {
            // Ensure the temporary directory exists
            Directory.CreateDirectory(tempDir);
        }

        // Process each inventory item
        foreach (var item in items)
        {
            // Path for the generated barcode image
            string imagePath = Path.Combine(tempDir, $"barcode_{item.Id}.png");

            // Generate barcode image for the current item
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, item.BarcodeValue))
            {
                // Optional visual settings for better contrast
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;
                generator.Save(imagePath);
            }

            // Read the generated barcode and capture its reading quality
            using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    // Assuming the first detected barcode corresponds to our generated one
                    item.ReadingQuality = result.ReadingQuality;
                    break;
                }
            }

            // Clean up the temporary image file
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }
        }

        // Serialize the inventory list, including reading quality, to a JSON file
        string jsonPath = Path.Combine(Environment.CurrentDirectory, "inventory.json");
        var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(items, jsonOptions);
        File.WriteAllText(jsonPath, json);

        // Inform the user where the output has been saved
        Console.WriteLine("Inventory records with barcode reading quality have been saved to:");
        Console.WriteLine(jsonPath);
    }
}