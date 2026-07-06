// Title: Batch Mailmark Barcode Generation to BMP Files
// Description: Demonstrates generating Mailmark barcodes for a list of customer records and saving each barcode as an individual BMP image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on complex barcode types such as Mailmark. It showcases the use of Aspose.BarCode.Generation.ComplexBarcodeGenerator and Aspose.BarCode.ComplexBarcode.MailmarkCodetext to create barcodes programmatically. Developers often need to batch‑process records, embed specific data fields, and export barcodes to image formats for printing or archival purposes.
// Prompt: Batch generate Mailmark barcodes from customer records, saving each as separate BMP files.
// Tags: mailmark, barcode, generation, bmp, aspose.barcode, complexbarcode

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Generates Mailmark barcodes for a collection of customer records and saves each barcode as a BMP file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates an output folder, iterates over sample records,
    /// builds Mailmark codetext, generates the barcode, and writes it to disk.
    /// </summary>
    static void Main()
    {
        // Define the output directory and ensure it exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "MailmarkBarcodes");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Sample customer records (limited to 5 for safe execution)
        var records = new List<(int ItemID, string DestinationPostCodePlusDPS)>
        {
            (16563762, "EF61AH8T "),
            (16563763, "EF61AH8T "),
            (16563764, "EF61AH8T "),
            (16563765, "EF61AH8T "),
            (16563766, "EF61AH8T ")
        };

        int index = 1;
        foreach (var record in records)
        {
            // Build the Mailmark codetext with required fields for each record
            var mailmark = new MailmarkCodetext
            {
                Format = 4,                     // 4‑state (unspecified/default)
                VersionID = 1,
                Class = "0",                    // Test/Null class
                SupplychainID = 384224,
                ItemID = record.ItemID,
                DestinationPostCodePlusDPS = record.DestinationPostCodePlusDPS
            };

            // Generate the barcode and save it as a BMP image
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                string filePath = Path.Combine(outputDir, $"Mailmark_{index}.bmp");
                generator.Save(filePath, BarCodeImageFormat.Bmp);
                Console.WriteLine($"Saved Mailmark barcode to: {filePath}");
            }

            index++;
        }
    }
}