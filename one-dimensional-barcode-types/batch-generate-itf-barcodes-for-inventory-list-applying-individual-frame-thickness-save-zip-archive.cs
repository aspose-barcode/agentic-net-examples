// Title: Batch generation of ITF14 barcodes with custom frame thickness and ZIP packaging
// Description: Demonstrates how to generate multiple ITF14 barcodes, each with its own frame border thickness, and bundle the resulting PNG images into a ZIP archive.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and barcode parameter settings such as ITF border configuration. Typical scenarios include creating inventory labels, batch processing of barcodes, and exporting them for distribution. Developers often need to customize visual properties per barcode and archive the output for downstream systems.
// Prompt: Batch generate ITF barcodes for inventory list, applying individual frame thickness, save ZIP archive.
// Tags: itf14, barcode, batch generation, frame thickness, zip archive, aspose.barcode, png, inventory

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeBatch
{
    /// <summary>
    /// Generates a set of ITF14 barcodes with individual frame thickness settings and packages them into a ZIP file.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the example. Creates barcode images, applies per‑item border thickness, and archives the results.
        /// </summary>
        static void Main()
        {
            // Define sample inventory items, each with a 14‑digit code and a specific frame thickness.
            var items = new List<InventoryItem>
            {
                new InventoryItem { Code = "12345678901231", FrameThickness = 5f },
                new InventoryItem { Code = "98765432109876", FrameThickness = 8f },
                new InventoryItem { Code = "11111111111111", FrameThickness = 10f },
                new InventoryItem { Code = "22222222222222", FrameThickness = 12f },
                new InventoryItem { Code = "33333333333333", FrameThickness = 15f }
            };

            // Ensure the output directory exists.
            string outputDir = "Barcodes";
            if (!Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            var generatedFiles = new List<string>();

            // Iterate over each inventory item and generate its barcode.
            foreach (var item in items)
            {
                // ITF14 requires exactly 14 numeric characters; skip invalid entries.
                if (string.IsNullOrEmpty(item.Code) || item.Code.Length != 14)
                {
                    Console.WriteLine($"Skipping invalid code '{item.Code}'. ITF14 requires 14 digits.");
                    continue;
                }

                // Create a barcode generator for the ITF14 symbology.
                using (var generator = new BarcodeGenerator(EncodeTypes.ITF14, item.Code))
                {
                    // Apply a frame border and set its thickness according to the current item.
                    generator.Parameters.Barcode.ITF.BorderType = ITF14BorderType.Frame;
                    generator.Parameters.Barcode.ITF.BorderThickness.Point = item.FrameThickness;

                    // Suppress exceptions for minor code‑text issues (e.g., leading zeros).
                    generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false;

                    // Save the barcode as a PNG file.
                    string filePath = Path.Combine(outputDir, $"{item.Code}.png");
                    generator.Save(filePath, BarCodeImageFormat.Png);
                    generatedFiles.Add(filePath);
                }
            }

            // Create a ZIP archive that contains all generated barcode images.
            string zipPath = "Barcodes.zip";
            if (File.Exists(zipPath))
            {
                File.Delete(zipPath);
            }

            using (var zipStream = new FileStream(zipPath, FileMode.Create))
            using (var archive = new ZipArchive(zipStream, ZipArchiveMode.Create))
            {
                foreach (var file in generatedFiles)
                {
                    if (File.Exists(file))
                    {
                        // Add each PNG file to the archive using its file name.
                        archive.CreateEntryFromFile(file, Path.GetFileName(file));
                    }
                }
            }

            Console.WriteLine($"Generated {generatedFiles.Count} barcodes and saved to '{zipPath}'.");
        }

        /// <summary>
        /// Simple data holder for inventory items used in the barcode generation loop.
        /// </summary>
        class InventoryItem
        {
            /// <summary>
            /// The 14‑digit code to encode as an ITF14 barcode.
            /// </summary>
            public string Code { get; set; }

            /// <summary>
            /// Desired frame border thickness (in points) for the barcode image.
            /// </summary>
            public float FrameThickness { get; set; }
        }
    }
}