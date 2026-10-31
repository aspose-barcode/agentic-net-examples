// Title: Batch generate ITF14 barcodes with custom frame thickness and zip them
// Description: Demonstrates how to create multiple ITF14 barcodes, each with its own frame thickness, save them as PNG images, and package them into a ZIP archive.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes.ITF14, and barcode parameter customization such as X‑Dimension, border type, and border thickness. Typical use cases include bulk creation of inventory barcodes with varying visual styles, exporting them for distribution or printing. Developers often need to generate many barcodes programmatically, adjust appearance per item, and bundle the results for easy handling.
// Prompt: Batch generate ITF barcodes for inventory list, applying individual frame thickness, save ZIP archive.
// Tags: itf14, barcode generation, bulk, frame thickness, zip archive, aspnet, aspose.barcode, png

using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch generation of ITF14 barcodes with individual frame thickness settings and packaging them into a ZIP archive.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates temporary images, generates barcodes, zips them, and cleans up.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "ITFBarcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Sample inventory items with code text and individual frame thickness (in pixels)
        var items = new List<InventoryItem>
        {
            new InventoryItem { CodeText = "12345678901231", FrameThickness = 5f },
            new InventoryItem { CodeText = "12345678901231", FrameThickness = 8f },
            new InventoryItem { CodeText = "12345678901231", FrameThickness = 12f },
            new InventoryItem { CodeText = "12345678901231", FrameThickness = 15f },
            new InventoryItem { CodeText = "12345678901231", FrameThickness = 20f }
        };

        // Generate ITF14 barcodes, applying each item's specific frame thickness
        for (int i = 0; i < items.Count; i++)
        {
            var item = items[i];
            using (var generator = new BarcodeGenerator(EncodeTypes.ITF14, item.CodeText))
            {
                // Set basic barcode appearance
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.Parameters.Barcode.ITF.BorderType = ITF14BorderType.Frame;
                generator.Parameters.Barcode.ITF.BorderThickness.Pixels = item.FrameThickness;
                generator.Parameters.Barcode.ITF.QuietZoneCoef = 10; // default quiet zone

                // Save the generated barcode as a PNG file
                string filePath = Path.Combine(tempFolder, $"barcode_{i + 1}.png");
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // Create ZIP archive containing all generated barcodes
        string zipPath = Path.Combine(Directory.GetCurrentDirectory(), "ITFBarcodes.zip");
        if (File.Exists(zipPath))
        {
            File.Delete(zipPath);
        }
        ZipFile.CreateFromDirectory(tempFolder, zipPath);

        // Clean up temporary folder
        Directory.Delete(tempFolder, true);

        Console.WriteLine($"Generated {items.Count} ITF14 barcodes and saved to ZIP: {zipPath}");
    }

    // Simple data holder for inventory items
    class InventoryItem
    {
        public string CodeText { get; set; }
        public float FrameThickness { get; set; }
    }
}