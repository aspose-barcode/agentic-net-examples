// Title: Batch ITF14 Barcode Generation with Custom Frame Thickness
// Description: Demonstrates how to generate ITF14 barcodes for a list of inventory items, each with its own frame thickness, and package the images into a ZIP file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on the BarcodeGenerator class and ITF14 symbology. It shows typical use cases such as creating product barcodes with customized borders, saving images in PNG format, and archiving results. Developers working on inventory management, packaging, or bulk barcode creation can use this pattern to automate barcode production.
// Prompt: Batch generate ITF barcodes for inventory list, applying individual frame thickness, save ZIP archive.
// Tags: itf14, barcode, generation, png, zip, aspose.barcode, inventory, frame-thickness

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Provides an example that batch‑generates ITF14 barcodes with per‑item frame thickness
/// and stores the resulting PNG images in a ZIP archive.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcodes, saves them as PNG files,
    /// and creates a ZIP archive containing all images.
    /// </summary>
    static void Main()
    {
        // Define a sample inventory list.
        // Each tuple holds the barcode text and the desired frame thickness (points).
        var inventory = new List<(string CodeText, float FrameThickness)>
        {
            ("12345678901231", 5f),
            ("98765432109876", 8f),
            ("55555555555555", 12f),
            ("11111111111111", 3f),
            ("22222222222222", 10f)
        };

        // Prepare an output folder for the generated PNG files.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Iterate through the inventory and generate a barcode image for each item.
        foreach (var item in inventory)
        {
            string fileName = $"{item.CodeText}.png";
            string filePath = Path.Combine(outputDir, fileName);

            // Use BarcodeGenerator with ITF14 symbology.
            using (var generator = new BarcodeGenerator(EncodeTypes.ITF14))
            {
                // Set the text to encode.
                generator.CodeText = item.CodeText;

                // Apply the specific frame thickness and set the border type to Frame.
                generator.Parameters.Barcode.ITF.BorderThickness.Point = item.FrameThickness;
                generator.Parameters.Barcode.ITF.BorderType = ITF14BorderType.Frame;

                // Save the generated barcode as a PNG image.
                generator.Save(filePath);
            }
        }

        // Create a ZIP archive that contains all generated PNG files.
        string zipPath = Path.Combine(Directory.GetCurrentDirectory(), "ITFBarcodes.zip");
        if (File.Exists(zipPath))
        {
            File.Delete(zipPath);
        }

        using (var zip = ZipFile.Open(zipPath, ZipArchiveMode.Create))
        {
            foreach (var file in Directory.GetFiles(outputDir, "*.png"))
            {
                zip.CreateEntryFromFile(file, Path.GetFileName(file));
            }
        }

        // Optional: clean up the temporary image files.
        // Directory.Delete(outputDir, true);
    }
}