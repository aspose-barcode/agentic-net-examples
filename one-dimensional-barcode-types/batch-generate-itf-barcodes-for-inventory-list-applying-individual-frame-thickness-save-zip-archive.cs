using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace ITFBarcodeBatch
{
    class Program
    {
        // Simple model for inventory items
        class InventoryItem
        {
            public string CodeText { get; set; }
            public float FrameThickness { get; set; } // thickness in points
        }

        static void Main()
        {
            // Prepare a small sample inventory list
            var items = new List<InventoryItem>
            {
                new InventoryItem { CodeText = "1234567890123", FrameThickness = 5f },
                new InventoryItem { CodeText = "9876543210987", FrameThickness = 8f },
                new InventoryItem { CodeText = "5555555555555", FrameThickness = 3f },
                new InventoryItem { CodeText = "1111111111111", FrameThickness = 6f },
                new InventoryItem { CodeText = "2222222222222", FrameThickness = 4f }
            };

            // Folder to store generated images
            string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            // Generate a PNG barcode for each item
            foreach (var item in items)
            {
                // Create generator for ITF14 barcode
                using (var generator = new BarcodeGenerator(EncodeTypes.ITF14))
                {
                    // Set the code text
                    generator.CodeText = item.CodeText;

                    // Apply individual frame thickness and set border type to Frame
                    generator.Parameters.Barcode.ITF.BorderThickness.Point = item.FrameThickness;
                    generator.Parameters.Barcode.ITF.BorderType = ITF14BorderType.Frame;

                    // Optional: set colors (black bars on white background)
                    generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                    generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                    // Save as PNG
                    string filePath = Path.Combine(outputFolder, $"{item.CodeText}.png");
                    generator.Save(filePath, BarCodeImageFormat.Png);
                }
            }

            // Create ZIP archive containing all generated PNGs
            string zipPath = Path.Combine(Directory.GetCurrentDirectory(), "ITFBarcodes.zip");
            using (var zipStream = new FileStream(zipPath, FileMode.Create))
            {
                using (var archive = new ZipArchive(zipStream, ZipArchiveMode.Create))
                {
                    foreach (var file in Directory.GetFiles(outputFolder, "*.png"))
                    {
                        string entryName = Path.GetFileName(file);
                        archive.CreateEntryFromFile(file, entryName);
                    }
                }
            }

            // Cleanup temporary image files (optional)
            foreach (var file in Directory.GetFiles(outputFolder, "*.png"))
            {
                File.Delete(file);
            }

            // Remove the temporary folder if empty
            if (Directory.Exists(outputFolder) && Directory.GetFiles(outputFolder).Length == 0)
            {
                Directory.Delete(outputFolder);
            }

            // Indicate completion
            Console.WriteLine("ITF barcodes generated and zipped successfully.");
        }
    }
}