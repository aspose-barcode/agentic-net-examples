// Title: Batch generate Mailmark barcodes and save each as a BMP file
// Description: Demonstrates how to create Mailmark 4‑State barcodes for multiple customer records using Aspose.BarCode and save each barcode as an individual BMP image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as Mailmark. It showcases the use of ComplexBarcodeGenerator, MailmarkCodetext, and image export settings, which are common tasks when developers need to produce high‑volume, custom‑formatted barcodes for logistics or mailing applications.
// Prompt: Batch generate Mailmark barcodes from customer records, saving each as separate BMP files.
// Tags: mailmark, barcode, batch, bmp, generation, complexbarcode, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch generation of Mailmark 4‑State barcodes and saving them as BMP files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates sample customer data, generates Mailmark barcodes, and writes each to a BMP file.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the output images
        string outputFolder = Path.Combine(Path.GetTempPath(), "MailmarkBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Sample list of customer records to encode into barcodes
        var customers = new List<CustomerRecord>
        {
            new CustomerRecord { Class = "0", SupplychainID = 384224, ItemID = 16563762 },
            new CustomerRecord { Class = "1", SupplychainID = 384225, ItemID = 16563763 },
            new CustomerRecord { Class = "2", SupplychainID = 384226, ItemID = 16563764 },
            new CustomerRecord { Class = "3", SupplychainID = 384227, ItemID = 16563765 },
            new CustomerRecord { Class = "4", SupplychainID = 384228, ItemID = 16563766 }
        };

        int index = 1;
        foreach (var cust in customers)
        {
            // Build Mailmark 4‑State codetext using the current customer's data
            var mailmark = new MailmarkCodetext
            {
                Format = 4,
                VersionID = 1,
                Class = cust.Class,
                SupplychainID = cust.SupplychainID,
                ItemID = cust.ItemID,
                DestinationPostCodePlusDPS = "EF61AH8T "
            };

            // Generate the barcode image with the specified codetext
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                // Set the X‑dimension (module width) to 4 pixels for better readability
                generator.Parameters.Barcode.XDimension.Pixels = 4f;

                // Define the full file path for the BMP output
                string filePath = Path.Combine(outputFolder, $"Mailmark_{index}.bmp");

                // Save the generated barcode as a BMP file
                generator.Save(filePath, BarCodeImageFormat.Bmp);

                // Log the successful save operation
                Console.WriteLine($"Saved barcode {index} to: {filePath}");
            }

            index++;
        }

        // Indicate that the batch process has finished
        Console.WriteLine("Batch generation completed.");
    }

    // Simple data holder for customer information used in barcode generation
    class CustomerRecord
    {
        public string Class { get; set; }
        public int SupplychainID { get; set; }
        public int ItemID { get; set; }
    }
}