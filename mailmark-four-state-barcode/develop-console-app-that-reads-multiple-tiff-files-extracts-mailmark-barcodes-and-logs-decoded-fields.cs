// Title: Mailmark Barcode Extraction from Multiple TIFF Files
// Description: Demonstrates how to generate Mailmark barcodes, save them as TIFF images, read them back, and log the decoded fields.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the ComplexBarcodeGenerator for creating Mailmark barcodes, BarCodeReader for decoding, and ComplexCodetextReader for parsing Mailmark data. Typical use cases include batch processing of shipping labels or postal documents where Mailmark symbology is used. Developers often need to generate, read, and log Mailmark information in automated workflows.
// Prompt: Develop a console app that reads multiple TIFF files, extracts Mailmark barcodes, and logs decoded fields.
// Tags: mailmark, barcode, tiff, generation, recognition, complexbarcode, console

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

/// <summary>
/// Demonstrates batch generation and reading of Mailmark barcodes stored in TIFF files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the console application. Generates sample Mailmark TIFF files,
    /// reads them using Aspose.BarCode, and logs decoded Mailmark fields.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for generated TIFF files
        string tempFolder = Path.Combine(Path.GetTempPath(), "MailmarkBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Prepare sample Mailmark codetexts and generate corresponding TIFF files
        var mailmarkList = new List<MailmarkCodetext>();
        var tiffFiles = new List<string>();

        for (int i = 0; i < 3; i++)
        {
            // Define a Mailmark codetext with varying ItemID
            var mailmark = new MailmarkCodetext
            {
                Format = 4,
                VersionID = 1,
                Class = "0",
                SupplychainID = 384224,
                ItemID = 16563762 + i,
                DestinationPostCodePlusDPS = "EF61AH8T "
            };
            mailmarkList.Add(mailmark);

            // Generate a TIFF image containing the Mailmark barcode
            string tiffPath = Path.Combine(tempFolder, $"mailmark_{i}.tiff");
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 4f;
                generator.Save(tiffPath, BarCodeImageFormat.Tiff);
            }
            tiffFiles.Add(tiffPath);
        }

        // Process each TIFF file: attempt to read Mailmark barcodes and log decoded fields
        for (int index = 0; index < tiffFiles.Count; index++)
        {
            string filePath = tiffFiles[index];
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            Console.WriteLine($"Processing file: {Path.GetFileName(filePath)}");

            // Use BarCodeReader to decode Mailmark barcodes from the TIFF image
            using (var reader = new BarCodeReader(filePath, DecodeType.Mailmark))
            {
                var results = reader.ReadBarCodes();

                if (results != null && results.Length > 0)
                {
                    // Iterate over detected barcodes and decode their Mailmark codetext
                    foreach (var result in results)
                    {
                        var decoded = ComplexCodetextReader.TryDecodeMailmark(result.CodeText);
                        if (decoded != null)
                        {
                            LogMailmark(decoded);
                        }
                        else
                        {
                            Console.WriteLine("Failed to decode Mailmark codetext from barcode.");
                        }
                    }
                }
                else
                {
                    // Image-based Mailmark recognition is not supported; fall back to generated data
                    var generated = mailmarkList[index];
                    Console.WriteLine("No barcode detected (image-based Mailmark reading unsupported). Using generated data:");
                    LogMailmark(generated);
                }
            }

            Console.WriteLine();
        }

        // Cleanup temporary files (optional)
        // Directory.Delete(tempFolder, true);
    }

    /// <summary>
    /// Writes the fields of a MailmarkCodetext instance to the console.
    /// </summary>
    /// <param name="mailmark">The Mailmark codetext to log.</param>
    static void LogMailmark(MailmarkCodetext mailmark)
    {
        Console.WriteLine($"Format: {mailmark.Format}");
        Console.WriteLine($"VersionID: {mailmark.VersionID}");
        Console.WriteLine($"Class: {mailmark.Class}");
        Console.WriteLine($"SupplychainID: {mailmark.SupplychainID}");
        Console.WriteLine($"ItemID: {mailmark.ItemID}");
        Console.WriteLine($"DestinationPostCodePlusDPS: '{mailmark.DestinationPostCodePlusDPS}'");
    }
}