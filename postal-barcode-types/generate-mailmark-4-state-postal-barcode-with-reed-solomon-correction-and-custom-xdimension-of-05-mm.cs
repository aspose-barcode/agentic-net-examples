// Title: Generate Mailmark 4‑state barcode with Reed‑Solomon correction and custom XDimension
// Description: Demonstrates creating a Mailmark 4‑state postal barcode using Reed‑Solomon error correction and setting a custom module size of 0.5 mm.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator together with MailmarkCodetext to produce postal barcodes. Typical use cases include printing Mailmark barcodes for mail sorting and tracking, where developers need to configure format, version, and error‑correction settings.
// Prompt: Generate a Mailmark 4‑state postal barcode with Reed‑Solomon correction and custom XDimension of 0.5 mm.
// Tags: mailmark, barcode generation, png, complexbarcode, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that creates a Mailmark 4‑state barcode with Reed‑Solomon correction
/// and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Builds the Mailmark codetext, configures the generator,
    /// and writes the resulting barcode image to disk.
    /// </summary>
    static void Main()
    {
        // Prepare Mailmark 4‑state codetext with required fields
        var mailmark = new MailmarkCodetext
        {
            Format = 4,                     // 4‑state format
            VersionID = 1,                  // version identifier
            Class = "0",                    // service class
            SupplychainID = 384224,         // supply chain identifier
            ItemID = 16563762,              // item identifier
            DestinationPostCodePlusDPS = "EF61AH8T " // valid postcode + DPS (trailing space)
        };

        // Generate the barcode using ComplexBarcodeGenerator
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Set module (X‑dimension) size to 0.5 mm
            generator.Parameters.Barcode.XDimension.Millimeters = 0.5f;

            // Save the barcode image as PNG
            generator.Save("mailmark.png");
        }

        Console.WriteLine("Mailmark barcode generated: mailmark.png");
    }
}