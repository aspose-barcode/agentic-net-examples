// Title: Batch generation of Mailmark barcodes to BMP files
// Description: Demonstrates how to generate multiple Mailmark barcodes from customer data and save each as an individual BMP image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on the ComplexBarcodeGenerator and MailmarkCodetext classes. It shows a typical batch‑processing scenario where developers create a series of Mailmark barcodes from records and export them as bitmap files for downstream systems such as printing or archival.
// Prompt: Batch generate Mailmark barcodes from customer records, saving each as separate BMP files.
// Tags: mailmark, barcode, batch, bmp, generation, aspose.barcode, complexbarcodegenerator

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch creation of Mailmark barcodes and saving them as BMP files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample Mailmark records, creates barcodes, and writes BMP files to a temporary folder.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary output folder for the generated BMP files
        string outputFolder = Path.Combine(Path.GetTempPath(), "MailmarkBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Prepare a list of sample customer records represented as MailmarkCodetext objects
        var records = new List<MailmarkCodetext>
        {
            CreateMailmark(4, 1, "0", 384224, 16563760),
            CreateMailmark(4, 1, "0", 384224, 16563761),
            CreateMailmark(4, 1, "0", 384224, 16563762),
            CreateMailmark(4, 1, "0", 384224, 16563763),
            CreateMailmark(4, 1, "0", 384224, 16563764)
        };

        // Iterate over each record, generate a barcode, and save it as a BMP file
        int index = 1;
        foreach (var mailmark in records)
        {
            string filePath = Path.Combine(outputFolder, $"Mailmark_{index}.bmp");

            // Use ComplexBarcodeGenerator to create the Mailmark barcode from the codetext
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                // Set the X-dimension (module width) to 4 pixels for better readability
                generator.Parameters.Barcode.XDimension.Pixels = 4;

                // Save the generated barcode image in BMP format
                generator.Save(filePath, BarCodeImageFormat.Bmp);
            }

            Console.WriteLine($"Saved: {filePath}");
            index++;
        }

        Console.WriteLine("Batch generation completed.");
    }

    /// <summary>
    /// Helper method that builds a MailmarkCodetext instance from supplied field values.
    /// </summary>
    /// <param name="format">Mailmark format identifier.</param>
    /// <param name="versionId">Version identifier.</param>
    /// <param name="classValue">Class value as a string.</param>
    /// <param name="supplyChainId">Supply chain identifier.</param>
    /// <param name="itemId">Item identifier.</param>
    /// <returns>A populated MailmarkCodetext object.</returns>
    static MailmarkCodetext CreateMailmark(int format, int versionId, string classValue, int supplyChainId, int itemId)
    {
        var mailmark = new MailmarkCodetext
        {
            Format = format,
            VersionID = versionId,
            Class = classValue,
            SupplychainID = supplyChainId,
            ItemID = itemId,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };
        return mailmark;
    }
}