// Title: Generate a 4‑State Mailmark Barcode with Aspose.BarCode
// Description: Demonstrates how to create a Mailmark barcode, set its service type, routing code, and customer reference, and save it as an image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It shows how to use the MailmarkCodetext class together with ComplexBarcodeGenerator to produce Mailmark 4‑state barcodes, a common requirement for postal and logistics applications. Developers typically need to configure format, version, service class, routing, and item identifiers before rendering the barcode to an image or other output format.
// Prompt: Instantiate a MailmarkCodetext and set service type, routing code, and customer reference.
// Tags: mailmark, barcode, complexbarcode, generation, png, csharp, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Mailmark 4‑state barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a MailmarkCodetext, configures required fields, generates the barcode and saves it to a PNG file.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary output directory
        string outputDir = Path.Combine(Path.GetTempPath(), "MailmarkExample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "Mailmark4State.png");

        // Instantiate MailmarkCodetext and set required fields
        MailmarkCodetext mailmarkCode = new MailmarkCodetext();
        mailmarkCode.Format = 4;                     // 4‑state format
        mailmarkCode.VersionID = 1;                  // version identifier
        mailmarkCode.Class = "0";                    // service type (class)
        mailmarkCode.SupplychainID = 384224;         // routing code (supply chain)
        mailmarkCode.ItemID = 16563762;              // customer reference (item)
        mailmarkCode.DestinationPostCodePlusDPS = "EF61AH8T "; // destination postcode with DPS

        // Generate the barcode using ComplexBarcodeGenerator
        using (var generator = new ComplexBarcodeGenerator(mailmarkCode))
        {
            // Set barcode rendering parameters (pixel size of X dimension)
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            // Save the generated barcode image to the specified path
            generator.Save(outputPath);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine("Mailmark barcode generated at:");
        Console.WriteLine(outputPath);
    }
}