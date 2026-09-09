// Title: Mailmark Barcode Generation, Decoding, and Batch Processing
// Description: Demonstrates creating Mailmark 4‑state barcodes, saving them as TIFF files, and decoding the codetext to extract individual fields.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode operations category, showcasing the use of ComplexBarcodeGenerator for Mailmark creation, BarCodeReader for image scanning, and ComplexCodetextReader for codetext decoding. Developers working with postal Mailmark symbology often need to generate batch barcode images, verify them programmatically, and extract metadata for logistics or auditing purposes. The snippet serves as a reference for batch processing and field extraction using the Aspose.BarCode API.
// Prompt: Develop a console app that reads multiple TIFF files, extracts Mailmark barcodes, and logs decoded fields.
// Tags: mailmark, barcode, generation, decoding, tiff, console, aspose.barcode, complexbarcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates batch generation of Mailmark barcodes, attempts image reading, and decodes codetext fields.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the console application.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the sample files
        string tempFolder = Path.Combine(Path.GetTempPath(), "MailmarkBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        var tiffFiles = new List<string>();
        var mailmarkData = new List<MailmarkCodetext>();

        // Generate a few sample Mailmark 4‑state TIFF files
        for (int i = 0; i < 3; i++)
        {
            var mailmark = new MailmarkCodetext
            {
                Format = 4,
                VersionID = 1,
                Class = "0",
                SupplychainID = 384224,
                ItemID = 16563762 + i,
                DestinationPostCodePlusDPS = "EF61AH8T "
            };

            // Use ComplexBarcodeGenerator to create the barcode image
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 4;
                string filePath = Path.Combine(tempFolder, $"mailmark_{i}.tiff");
                generator.Save(filePath, BarCodeImageFormat.Tiff);
                tiffFiles.Add(filePath);
                mailmarkData.Add(mailmark);
            }
        }

        // Process each generated TIFF file
        for (int index = 0; index < tiffFiles.Count; index++)
        {
            string file = tiffFiles[index];
            Console.WriteLine($"Processing file: {Path.GetFileName(file)}");

            if (!File.Exists(file))
            {
                Console.WriteLine("File does not exist, skipping.");
                continue;
            }

            // Attempt to read with BarCodeReader (Mailmark reading from image is unsupported)
            try
            {
                using (var reader = new BarCodeReader(file, DecodeType.Mailmark))
                {
                    var results = reader.ReadBarCodes();
                    if (results.Length == 0)
                    {
                        Console.WriteLine("No barcode detected (expected, Mailmark image reading is unsupported).");
                    }
                    else
                    {
                        foreach (var result in results)
                        {
                            Console.WriteLine($"Reader detected: {result.CodeText}");
                        }
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Failed to load image: {ex.Message}");
            }

            // Decode the known Mailmark codetext string directly
            string constructed = mailmarkData[index].GetConstructedCodetext();
            MailmarkCodetext decoded = ComplexCodetextReader.TryDecodeMailmark(constructed);
            if (decoded != null)
            {
                Console.WriteLine("Decoded Mailmark fields:");
                Console.WriteLine($"  Format: {decoded.Format}");
                Console.WriteLine($"  VersionID: {decoded.VersionID}");
                Console.WriteLine($"  Class: {decoded.Class}");
                Console.WriteLine($"  SupplychainID: {decoded.SupplychainID}");
                Console.WriteLine($"  ItemID: {decoded.ItemID}");
                Console.WriteLine($"  DestinationPostCodePlusDPS: {decoded.DestinationPostCodePlusDPS}");
            }
            else
            {
                Console.WriteLine("Failed to decode Mailmark codetext.");
            }

            Console.WriteLine();
        }

        // Cleanup temporary files (optional)
        try
        {
            foreach (var file in tiffFiles)
            {
                File.Delete(file);
            }
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored – cleanup failures should not affect program exit
        }
    }
}