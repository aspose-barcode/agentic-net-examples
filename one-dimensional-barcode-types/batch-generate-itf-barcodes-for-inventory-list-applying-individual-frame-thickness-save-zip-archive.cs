using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating ITF14 barcodes with custom frame thicknesses
/// and packaging them into a ZIP archive.
/// </summary>
class Program
{
    /// <summary>
    /// Represents an inventory item with a barcode value and a specific frame thickness.
    /// </summary>
    class InventoryItem
    {
        public string Code { get; set; }

        // Frame thickness in points.
        public float FrameThickness { get; set; }
    }

    /// <summary>
    /// Entry point of the application. Generates barcodes for a sample inventory,
    /// saves each as a PNG image, and stores them in a ZIP file.
    /// </summary>
    static void Main()
    {
        // Sample inventory list (safe size for runner).
        var items = new List<InventoryItem>
        {
            new InventoryItem { Code = "1234567890123", FrameThickness = 5f },
            new InventoryItem { Code = "9876543210987", FrameThickness = 8f },
            new InventoryItem { Code = "5555555555555", FrameThickness = 3f },
            new InventoryItem { Code = "1111111111111", FrameThickness = 6f },
            new InventoryItem { Code = "2222222222222", FrameThickness = 4f }
        };

        // Output ZIP file path.
        string zipPath = "ITFBarcodes.zip";

        // Create the ZIP archive and add each barcode image.
        using (var zipFileStream = new FileStream(zipPath, FileMode.Create))
        {
            using (var archive = new ZipArchive(zipFileStream, ZipArchiveMode.Create))
            {
                foreach (var item in items)
                {
                    // Generate ITF14 barcode with the item's specific frame thickness.
                    using (var generator = new BarcodeGenerator(EncodeTypes.ITF14, item.Code))
                    {
                        // Apply frame (border) thickness and ensure frame type.
                        generator.Parameters.Barcode.ITF.BorderThickness.Point = item.FrameThickness;
                        generator.Parameters.Barcode.ITF.BorderType = ITF14BorderType.Frame;

                        // Save barcode to a memory stream in PNG format.
                        using (var imageStream = new MemoryStream())
                        {
                            generator.Save(imageStream, BarCodeImageFormat.Png);
                            imageStream.Position = 0; // Reset stream position for reading.

                            // Create a ZIP entry named after the barcode value.
                            var entry = archive.CreateEntry($"{item.Code}.png");
                            using (var entryStream = entry.Open())
                            {
                                // Copy the PNG data into the ZIP entry.
                                imageStream.CopyTo(entryStream);
                            }
                        }
                    }
                }
            }
        }

        // Output the full path of the generated ZIP archive.
        Console.WriteLine($"Generated ZIP archive: {Path.GetFullPath(zipPath)}");
    }
}