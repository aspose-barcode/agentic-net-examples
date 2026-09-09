// Title: Batch generation of Mailmark 4‑State barcodes to BMP files
// Description: Demonstrates how to create Mailmark 4‑State barcodes for a list of customer records and save each barcode as an individual BMP image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of ComplexBarcodeGenerator and MailmarkCodetext classes to produce Mailmark barcodes. Typical scenarios include bulk creation of postal barcodes for mailing lists, inventory tagging, or logistics workflows where each record requires a separate image file. Developers often need to iterate over data sources, configure barcode parameters, and export images in common formats such as BMP or PNG.
// Prompt: Batch generate Mailmark barcodes from customer records, saving each as separate BMP files.
// Tags: mailmark, barcode, batch, bmp, aspose.barcode, complexbarcodegenerator, generation

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a set of Mailmark 4‑State barcodes from sample customer records
/// and saves each barcode as an individual BMP file in a temporary folder.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary output directory,
    /// iterates over predefined customer records, builds Mailmark codetext,
    /// generates the barcode image, and writes status messages to the console.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the batch output
        string outputFolder = Path.Combine(Path.GetTempPath(), "MailmarkBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Sample customer records to be encoded as Mailmark barcodes
        var records = new List<CustomerRecord>
        {
            new CustomerRecord { VersionID = "1", Class = "0", SupplyChainID = 384224, ItemID = 16563762 },
            new CustomerRecord { VersionID = "1", Class = "0", SupplyChainID = 384224, ItemID = 16563763 },
            new CustomerRecord { VersionID = "1", Class = "0", SupplyChainID = 384224, ItemID = 16563764 },
            new CustomerRecord { VersionID = "1", Class = "0", SupplyChainID = 384224, ItemID = 16563765 },
            new CustomerRecord { VersionID = "1", Class = "0", SupplyChainID = 384224, ItemID = 16563766 }
        };

        int index = 1;
        foreach (var rec in records)
        {
            try
            {
                // Build Mailmark 4‑State codetext from the current record
                var mailmark = new MailmarkCodetext
                {
                    Format = 4,
                    VersionID = int.Parse(rec.VersionID),
                    Class = rec.Class,
                    SupplychainID = rec.SupplyChainID,
                    ItemID = rec.ItemID,
                    DestinationPostCodePlusDPS = "EF61AH8T "
                };

                // Generate the barcode using ComplexBarcodeGenerator
                using (var generator = new ComplexBarcodeGenerator(mailmark))
                {
                    // Set the X‑dimension (module size) to 4 pixels for better readability
                    generator.Parameters.Barcode.XDimension.Pixels = 4;

                    // Compose the output file path and save the image as BMP
                    string filePath = Path.Combine(outputFolder, $"Mailmark_{index}.bmp");
                    generator.Save(filePath, BarCodeImageFormat.Bmp);
                }

                Console.WriteLine($"Generated: {index}.bmp");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during barcode generation for this record
                Console.WriteLine($"Failed to generate barcode for record {index}: {ex.Message}");
            }

            index++;
        }

        Console.WriteLine($"All barcodes saved to: {outputFolder}");
    }

    // Simple DTO representing a customer record used to build Mailmark codetext
    class CustomerRecord
    {
        public string VersionID { get; set; }
        public string Class { get; set; }
        public int SupplyChainID { get; set; }
        public int ItemID { get; set; }
    }
}