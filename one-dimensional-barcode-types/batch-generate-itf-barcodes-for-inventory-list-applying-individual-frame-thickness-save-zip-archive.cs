// Title: Batch generate ITF-14 barcodes with custom frame thickness and zip them
// Description: Demonstrates how to generate ITF-14 barcodes for a list of inventory items, each with its own frame thickness, and package the resulting PNG images into a ZIP archive.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and barcode parameter settings such as XDimension and ITF border options. Typical use cases include creating bulk barcode images for inventory, packaging, or shipping labels, where each barcode may require individual visual styling. Developers often need to automate image creation and archive the results for distribution or downstream processing.
// Prompt: Batch generate ITF barcodes for inventory list, applying individual frame thickness, save ZIP archive.
// Tags: itf14, barcode, generation, batch, frame thickness, zip, aspose.barcode, png

using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch generation of ITF-14 barcodes with custom frame thickness and archiving them into a ZIP file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes for sample inventory items, saves them as PNG files, and creates a ZIP archive.
    /// </summary>
    static void Main()
    {
        // Prepare sample inventory items with code and individual frame thickness
        var items = new List<InventoryItem>
        {
            new InventoryItem { Code = "12345678901231", FrameThickness = 5f },
            new InventoryItem { Code = "12345678901232", FrameThickness = 8f },
            new InventoryItem { Code = "12345678901233", FrameThickness = 12f },
            new InventoryItem { Code = "12345678901234", FrameThickness = 15f },
            new InventoryItem { Code = "12345678901235", FrameThickness = 20f }
        };

        // Create a dedicated temporary folder for generated images
        string tempFolder = Path.Combine(Path.GetTempPath(), "ITFBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        var generatedFiles = new List<string>();

        // Generate a PNG barcode for each inventory item
        foreach (var item in items)
        {
            string filePath = Path.Combine(tempFolder, $"ITF_{item.Code}.png");
            try
            {
                using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.ITF14, item.Code))
                {
                    // Set barcode visual parameters
                    generator.Parameters.Barcode.XDimension.Pixels = 2;
                    generator.Parameters.Barcode.ITF.BorderType = ITF14BorderType.Frame;
                    generator.Parameters.Barcode.ITF.BorderThickness.Pixels = item.FrameThickness;

                    // Save the barcode image as PNG
                    generator.Save(filePath, BarCodeImageFormat.Png);
                }

                generatedFiles.Add(filePath);
                Console.WriteLine($"Generated barcode for {item.Code} at {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating barcode for {item.Code}: {ex.Message}");
            }
        }

        // Create ZIP archive containing the generated barcode images
        string zipPath = Path.Combine(tempFolder, "ITF_Barcodes.zip");
        try
        {
            using (FileStream zipToOpen = new FileStream(zipPath, FileMode.Create))
            using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
            {
                foreach (var file in generatedFiles)
                {
                    try
                    {
                        // Add each PNG file to the archive
                        archive.CreateEntryFromFile(file, Path.GetFileName(file));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to add {file} to ZIP: {ex.Message}");
                    }
                }
            }

            Console.WriteLine($"ZIP archive created at: {zipPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating ZIP archive: {ex.Message}");
        }
    }

    // Simple DTO representing an inventory item and its desired barcode frame thickness
    class InventoryItem
    {
        public string Code { get; set; }
        public float FrameThickness { get; set; }
    }
}