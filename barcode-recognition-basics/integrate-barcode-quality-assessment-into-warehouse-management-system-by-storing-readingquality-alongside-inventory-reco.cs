// Title: Barcode Generation, Quality Assessment, and CSV Storage for Inventory Items
// Description: Demonstrates generating Code128 barcodes, reading them to obtain quality metrics, and persisting the results alongside inventory data.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for decoding and retrieving ReadingQuality, and typical data‑persistence steps such as writing to CSV. Developers building warehouse management or inventory tracking systems often need to generate barcodes, assess their scan quality, and store this information for quality control and reporting.
// Prompt: Integrate barcode quality assessment into a warehouse management system by storing ReadingQuality alongside inventory records.
// Tags: barcode symbology, generation, recognition, quality assessment, csv, inventory, aspose.barcode, code128

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Represents an inventory item with its barcode data and the measured reading quality.
/// </summary>
class InventoryItem
{
    public string ItemId { get; set; }
    public string CodeText { get; set; }
    public double ReadingQuality { get; set; }
}

/// <summary>
/// Demonstrates barcode creation, quality evaluation, and CSV persistence for a set of inventory items.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcodes, reads them to obtain quality metrics, and saves the results to a CSV file.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Prepare sample inventory data
        // ------------------------------------------------------------
        var items = new List<InventoryItem>
        {
            new InventoryItem { ItemId = "ITEM001", CodeText = "ABC123456" },
            new InventoryItem { ItemId = "ITEM002", CodeText = "DEF987654" },
            new InventoryItem { ItemId = "ITEM003", CodeText = "GHI555777" }
        };

        // ------------------------------------------------------------
        // 2. Create a temporary folder to store generated barcode images
        // ------------------------------------------------------------
        string barcodeFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(barcodeFolder);

        var processedItems = new List<InventoryItem>();

        // ------------------------------------------------------------
        // 3. Process each inventory item: generate barcode, read quality
        // ------------------------------------------------------------
        foreach (var item in items)
        {
            // 3a. Generate barcode image for the current item
            string barcodePath = Path.Combine(barcodeFolder, item.ItemId + ".png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, item.CodeText))
            {
                // Optional visual settings for better contrast
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;
                generator.Save(barcodePath, BarCodeImageFormat.Png);
            }

            // 3b. Verify that the image file was created successfully
            if (!File.Exists(barcodePath))
            {
                Console.WriteLine($"Failed to create barcode image for {item.ItemId}");
                continue;
            }

            // 3c. Read the barcode and capture the reading quality metric
            using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
            {
                // Default quality settings are used; they can be customized via reader.QualitySettings
                BarCodeResult[] results = reader.ReadBarCodes();
                if (results.Length == 0)
                {
                    Console.WriteLine($"No barcode detected for {item.ItemId}");
                    continue;
                }

                // Assume the first result corresponds to the generated barcode
                var result = results[0];
                item.ReadingQuality = result.ReadingQuality;
                // Update CodeText in case the reader normalizes or corrects it
                item.CodeText = result.CodeText;
                processedItems.Add(item);
                Console.WriteLine($"Item {item.ItemId}: Quality={item.ReadingQuality}");
            }
        }

        // ------------------------------------------------------------
        // 4. Persist the processed inventory records (including quality) to a CSV file
        // ------------------------------------------------------------
        string csvPath = Path.Combine(Directory.GetCurrentDirectory(), "inventory_records.csv");
        using (var writer = new StreamWriter(csvPath, false))
        {
            writer.WriteLine("ItemId,CodeText,ReadingQuality");
            foreach (var itm in processedItems)
            {
                writer.WriteLine($"{itm.ItemId},{itm.CodeText},{itm.ReadingQuality}");
            }
        }

        Console.WriteLine($"Inventory records saved to {csvPath}");

        // ------------------------------------------------------------
        // 5. Clean up temporary barcode images
        // ------------------------------------------------------------
        try
        {
            foreach (var file in Directory.GetFiles(barcodeFolder))
            {
                File.Delete(file);
            }
            Directory.Delete(barcodeFolder);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cleanup error: {ex.Message}");
        }
    }
}