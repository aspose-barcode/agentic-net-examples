using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generation of Mailmark barcodes using Aspose.BarCode library.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a set of Mailmark barcodes and saves them as BMP files.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare output directory where barcode images will be saved.
        // --------------------------------------------------------------------
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "MailmarkBarcodes");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // --------------------------------------------------------------------
        // Sample customer records (limited to a safe batch size).
        // Each record is represented by a MailmarkCodetext instance.
        // --------------------------------------------------------------------
        var customers = new List<MailmarkCodetext>
        {
            new MailmarkCodetext { Format = 4, VersionID = 1, Class = "0", SupplychainID = 384224, ItemID = 16563762, DestinationPostCodePlusDPS = "EF61AH8T " },
            new MailmarkCodetext { Format = 4, VersionID = 1, Class = "1", SupplychainID = 384224, ItemID = 16563763, DestinationPostCodePlusDPS = "EF61AH8T " },
            new MailmarkCodetext { Format = 4, VersionID = 1, Class = "2", SupplychainID = 384224, ItemID = 16563764, DestinationPostCodePlusDPS = "EF61AH8T " },
            new MailmarkCodetext { Format = 4, VersionID = 1, Class = "3", SupplychainID = 384224, ItemID = 16563765, DestinationPostCodePlusDPS = "EF61AH8T " },
            new MailmarkCodetext { Format = 4, VersionID = 1, Class = "4", SupplychainID = 384224, ItemID = 16563766, DestinationPostCodePlusDPS = "EF61AH8T " }
        };

        // --------------------------------------------------------------------
        // Iterate over each customer record, generate the barcode, and save it.
        // --------------------------------------------------------------------
        int index = 1;
        foreach (var mailmark in customers)
        {
            // Build a unique file name for each barcode image.
            string fileName = $"Mailmark_{index:D3}.bmp";
            string filePath = Path.Combine(outputDir, fileName);

            try
            {
                // Generate the barcode using the ComplexBarcodeGenerator.
                using (var generator = new ComplexBarcodeGenerator(mailmark))
                {
                    // Save the generated barcode as a BMP file.
                    generator.Save(filePath, BarCodeImageFormat.Bmp);
                }

                Console.WriteLine($"Generated: {filePath}");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during barcode generation.
                Console.WriteLine($"Failed to generate barcode for record {index}: {ex.Message}");
            }

            index++;
        }
    }
}