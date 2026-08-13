// Title: Batch generation of Mailmark barcodes to BMP files
// Description: Demonstrates how to create Mailmark barcodes for multiple customer records and save each barcode as an individual BMP image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as Mailmark. It showcases the use of ComplexBarcodeGenerator and MailmarkCodetext classes to produce high‑volume barcode images, a common requirement for logistics, mailing, and inventory systems. Developers often need to generate barcodes in bulk, customize codetext, and export them to various image formats.
// Prompt: Batch generate Mailmark barcodes from customer records, saving each as separate BMP files.
// Tags: mailmark, barcode generation, bmp, batch processing, aspose.barcode, complexbarcode, codetext

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that generates Mailmark barcodes for a list of customer records
/// and saves each barcode as a separate BMP file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Iterates over sample customer data,
    /// builds Mailmark codetext, generates the barcode image, and writes it to disk.
    /// </summary>
    static void Main()
    {
        // Sample customer records – in a real scenario these would come from a database or file.
        var customers = new List<CustomerRecord>
        {
            new CustomerRecord { Class = "0", SupplychainID = 384224, ItemID = 16563762 },
            new CustomerRecord { Class = "1", SupplychainID = 384224, ItemID = 16563763 },
            new CustomerRecord { Class = "2", SupplychainID = 384224, ItemID = 16563764 },
            new CustomerRecord { Class = "3", SupplychainID = 384224, ItemID = 16563765 },
            new CustomerRecord { Class = "4", SupplychainID = 384224, ItemID = 16563766 }
        };

        // Determine output folder for generated BMP files.
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "MailmarkBarcodes");
        if (!Directory.Exists(outputFolder))
        {
            // Create the folder if it does not already exist.
            Directory.CreateDirectory(outputFolder);
        }

        int index = 1;
        foreach (var record in customers)
        {
            // Construct Mailmark codetext according to the specification.
            var mailmark = new MailmarkCodetext
            {
                Format = 4,                     // 4‑state Mailmark.
                VersionID = 1,                  // Version identifier.
                Class = record.Class,           // Class as string.
                SupplychainID = record.SupplychainID,
                ItemID = record.ItemID,
                DestinationPostCodePlusDPS = "EF61AH8T " // Trailing space is required.
            };

            // Generate the barcode image using ComplexBarcodeGenerator.
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                // Build file name and path for the current barcode.
                string filePath = Path.Combine(outputFolder, $"Mailmark_{index}.bmp");
                // Save the barcode as a BMP image.
                generator.Save(filePath, BarCodeImageFormat.Bmp);
                Console.WriteLine($"Saved Mailmark barcode #{index} to: {filePath}");
            }

            index++;
        }

        Console.WriteLine("All Mailmark barcodes have been generated.");
    }

    // Simple data holder for demonstration purposes.
    private class CustomerRecord
    {
        public string Class { get; set; }
        public int SupplychainID { get; set; }
        public int ItemID { get; set; }
    }
}