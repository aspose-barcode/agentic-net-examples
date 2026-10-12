// Title: Batch generation of ITF-14 barcodes with custom frame thickness and ZIP packaging
// Description: Demonstrates how to generate multiple ITF-14 barcodes, each with its own frame thickness, save them as PNG files, and compress the results into a ZIP archive.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and barcode parameter classes (e.g., ITF, XDimension). Typical scenarios include inventory labeling, batch printing, and automated barcode creation where each item may require distinct visual styling. Developers often need to customize barcode appearance per item and bundle the output for distribution.
// Prompt: Batch generate ITF barcodes for inventory list, applying individual frame thickness, save ZIP archive.
// Tags: itf14, barcode, batch, zip, png, aspose.barcode, generation

using System;
using System.IO;
using System.IO.Compression;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch creation of ITF-14 barcodes with individual frame thickness settings and packaging them into a ZIP archive.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates sample inventory data, generates barcodes, archives them, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Sample inventory list: each item has a 14‑digit code and a frame thickness in pixels
        var inventory = new (string Code, float FrameThickness)[]
        {
            ("12345678901231", 5f),
            ("23456789012345", 8f),
            ("34567890123456", 12f),
            ("45678901234567", 3f),
            ("56789012345678", 10f)
        };

        // Create a unique temporary folder for barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "ITFBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate barcodes – one PNG per inventory item
        for (int i = 0; i < inventory.Length; i++)
        {
            var item = inventory[i];
            string filePath = Path.Combine(tempFolder, $"barcode_{i + 1}.png");

            using (var generator = new BarcodeGenerator(EncodeTypes.ITF14, item.Code))
            {
                // Set module width (optional, improves readability)
                generator.Parameters.Barcode.XDimension.Pixels = 2f;

                // Apply frame style and the individual thickness for this item
                generator.Parameters.Barcode.ITF.BorderType = ITF14BorderType.Frame;
                generator.Parameters.Barcode.ITF.BorderThickness.Pixels = item.FrameThickness;

                // Save the generated barcode as a PNG image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // Create ZIP archive containing all generated barcodes
        string zipPath = Path.Combine(Directory.GetCurrentDirectory(), "ITF_Barcodes.zip");
        if (File.Exists(zipPath))
        {
            File.Delete(zipPath);
        }
        ZipFile.CreateFromDirectory(tempFolder, zipPath);

        // Clean up temporary folder and its contents
        Directory.Delete(tempFolder, true);

        Console.WriteLine($"Generated {inventory.Length} ITF barcodes and saved to ZIP: {zipPath}");
    }
}