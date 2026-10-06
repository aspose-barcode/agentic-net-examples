// Title: Generate Mailmark 4‑state barcode and save as PNG
// Description: Creates a Mailmark 4‑state barcode using Aspose.BarCode's ComplexBarcodeGenerator and writes it to a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It demonstrates how to build a Mailmark 4‑state barcode by configuring a MailmarkCodetext object and using ComplexBarcodeGenerator. Developers working with postal or logistics solutions often need to generate Mailmark barcodes for tracking and routing; the key API classes involved are MailmarkCodetext, ComplexBarcodeGenerator, and the barcode parameter settings. This snippet serves as a quick reference for creating and exporting such barcodes.
// Prompt: Generate a Mailmark 4‑state barcode with ComplexBarcodeGenerator and save as PNG.
// Tags: mailmark,4-state,barcode,generation,png,complexbarcode,aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generation of a Mailmark 4‑state barcode and saving it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Prepares the output folder, builds the Mailmark codetext,
    /// generates the barcode, and writes the PNG file to disk.
    /// </summary>
    static void Main()
    {
        // Prepare output directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "Mailmark4State.png");

        // Create Mailmark 4‑state codetext
        MailmarkCodetext mailmarkCode = new MailmarkCodetext();
        mailmarkCode.Format = 4;
        mailmarkCode.VersionID = 1;
        mailmarkCode.Class = "0";
        mailmarkCode.SupplychainID = 384224;
        mailmarkCode.ItemID = 16563762;
        mailmarkCode.DestinationPostCodePlusDPS = "EF61AH8T ";

        // Generate barcode using ComplexBarcodeGenerator
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(mailmarkCode))
        {
            // Set barcode X-dimension (pixel size)
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            // Save the generated barcode as PNG
            generator.Save(outputPath);
        }

        Console.WriteLine($"Mailmark 4‑state barcode saved to: {outputPath}");
    }
}