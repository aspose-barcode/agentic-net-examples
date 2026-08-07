// Title: Generate and Save a Mailmark Barcode as JPEG
// Description: Demonstrates creating a Mailmark barcode using Aspose.BarCode and saving it as a JPEG image in a specified folder.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as Mailmark. It showcases the use of MailmarkCodetext and ComplexBarcodeGenerator classes to produce high‑security postal barcodes, a common requirement for logistics and mailing applications. Developers often need to generate these barcodes programmatically and export them to image formats for integration into documents or printing workflows.
// Prompt: Save the generated Mailmark barcode as a JPEG file to a specified output folder.
// Tags: mailmark, barcode generation, jpeg, complexbarcode, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that creates a Mailmark barcode and saves it as a JPEG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a Mailmark barcode using a MailmarkCodetext object and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define the output folder and ensure it exists
        string outputFolder = "Output";
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Create a valid MailmarkCodetext instance with required properties
        var mailmark = new MailmarkCodetext
        {
            // 4-state Mailmark format
            Format = 4,
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            // Destination post code plus DPS (trailing space is required)
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Generate the Mailmark barcode using ComplexBarcodeGenerator
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Build the full output file path
            string outputPath = Path.Combine(outputFolder, "Mailmark.jpg");
            // Save the barcode as a JPEG image
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
            Console.WriteLine($"Mailmark barcode saved to: {outputPath}");
        }
    }
}