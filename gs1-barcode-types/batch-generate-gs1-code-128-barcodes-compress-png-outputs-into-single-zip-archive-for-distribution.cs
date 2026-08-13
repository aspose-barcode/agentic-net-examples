// Title: Batch generate GS1 Code 128 barcodes and package them into a ZIP archive
// Description: Demonstrates creating multiple GS1‑128 (GS1 Code 128) barcodes from GTIN‑14 values, saving each as a PNG, and compressing all images into a single ZIP file for easy distribution.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes.GS1Code128. Typical use cases include bulk creation of product barcodes for inventory, labeling, or e‑commerce platforms, where developers need to automate image output and bundle results for downstream processing. The code illustrates setting visual parameters, exporting to PNG, and using .NET's ZipArchive to create a distributable archive.
// Prompt: Batch generate GS1 Code 128 barcodes, compress PNG outputs into a single ZIP archive for distribution.
// Tags: gs1,code128,barcode,generation,png,zip,compression,aspose.barcode

using System;
using System.IO;
using System.IO.Compression;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch generation of GS1 Code 128 barcodes and zipping the PNG outputs.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates barcodes for a set of GTIN‑14 values, saves them as PNG images,
    /// and stores them in a ZIP archive.
    /// </summary>
    static void Main()
    {
        // Define a collection of sample GTIN‑14 values (14 digits, leading zeros preserved)
        string[] gtins = new string[]
        {
            "00123456789012",
            "01234567890123",
            "12345678901234",
            "23456789012345",
            "34567890123456"
        };

        // Target path for the resulting ZIP archive
        string zipPath = "GS1Code128Barcodes.zip";

        // Create the ZIP archive and add each generated PNG as an entry
        using (FileStream zipFile = new FileStream(zipPath, FileMode.Create))
        using (ZipArchive archive = new ZipArchive(zipFile, ZipArchiveMode.Create))
        {
            int index = 1; // Simple counter for naming entries

            foreach (string gtin in gtins)
            {
                // GS1 Code 128 requires the Application Identifier (01) followed by a 14‑digit GTIN
                string codeText = $"(01){gtin}";

                // Initialise the barcode generator with the GS1 Code 128 symbology and the prepared text
                using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1Code128, codeText))
                {
                    // Optional visual settings: module size (X‑dimension) and bar height
                    generator.Parameters.Barcode.XDimension.Point = 2f;
                    generator.Parameters.Barcode.BarHeight.Point = 50f;

                    // Render the barcode to a memory stream in PNG format
                    using (MemoryStream ms = new MemoryStream())
                    {
                        generator.Save(ms, BarCodeImageFormat.Png);
                        ms.Position = 0; // Reset stream position before copying

                        // Create a new entry in the ZIP archive for this barcode image
                        ZipArchiveEntry entry = archive.CreateEntry($"barcode_{index}.png", CompressionLevel.Optimal);
                        using (Stream entryStream = entry.Open())
                        {
                            // Copy the PNG data into the ZIP entry
                            ms.CopyTo(entryStream);
                        }
                    }
                }

                index++;
            }
        }

        // Inform the user where the ZIP archive was created
        Console.WriteLine($"ZIP archive created: {Path.GetFullPath(zipPath)}");
    }
}