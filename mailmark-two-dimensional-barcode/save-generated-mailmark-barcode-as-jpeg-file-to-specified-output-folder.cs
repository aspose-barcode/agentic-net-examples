// Title: Generate and Save Mailmark Barcode as JPEG
// Description: Creates a Mailmark barcode using Aspose.BarCode and saves it as a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, demonstrating how to work with complex barcode types such as Mailmark. It showcases the use of ComplexBarcodeGenerator and MailmarkCodetext classes to encode postal data, a common requirement for logistics and mailing applications. Developers often need to generate Mailmark barcodes and export them to image formats for printing or digital distribution.
// Prompt: Save the generated Mailmark barcode as a JPEG file to a specified output folder.
// Tags: mailmark, barcode, generation, jpeg, aspose.barcode, complexbarcode, codetext

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generating a Mailmark barcode and saving it as a JPEG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and writes it to the output folder.
    /// </summary>
    static void Main()
    {
        // Define the output folder and ensure it exists.
        string outputFolder = "Output";
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Populate the Mailmark codetext with required fields.
        var mailmark = new MailmarkCodetext
        {
            Format = 4,                     // 4-state format
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Generate the Mailmark barcode and save it as JPEG.
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            string outputPath = Path.Combine(outputFolder, "mailmark.jpeg");
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine("Mailmark barcode saved to: " + Path.Combine(outputFolder, "mailmark.jpeg"));
    }
}