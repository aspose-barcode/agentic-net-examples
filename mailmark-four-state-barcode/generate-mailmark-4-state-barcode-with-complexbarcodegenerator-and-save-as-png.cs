// Title: Generate Mailmark 4‑state barcode with ComplexBarcodeGenerator
// Description: Demonstrates creating a Mailmark 4‑state barcode using Aspose.BarCode's ComplexBarcodeGenerator and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, illustrating how to work with the MailmarkCodetext class and ComplexBarcodeGenerator to produce 4‑state barcodes. Developers often need to encode postal routing information, service class, and item identifiers for mail processing systems, and this snippet shows the typical steps for configuring the codetext and exporting the barcode image.
// Prompt: Generate a Mailmark 4‑state barcode with ComplexBarcodeGenerator and save as PNG.
// Tags: mailmark, barcode, complexbarcodegenerator, png, aspose.barcode, 4-state

using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Mailmark 4‑state barcode and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the Mailmark codetext, generates the barcode, and writes it to disk.
    /// </summary>
    static void Main()
    {
        // Initialize Mailmark 4‑state codetext with required fields
        var mailmark = new MailmarkCodetext();
        mailmark.Format = 4;                         // 4‑state format identifier
        mailmark.VersionID = 1;                      // Version of the Mailmark specification
        mailmark.Class = "0";                        // Service type / class code
        mailmark.SupplychainID = 384224;             // Routing code (supply chain identifier)
        mailmark.ItemID = 16563762;                  // Customer reference (item identifier)
        mailmark.DestinationPostCodePlusDPS = "EF61AH8T "; // Destination postcode plus DPS (trailing space required)

        // Generate the barcode using ComplexBarcodeGenerator and save it as a PNG file
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            generator.Save("mailmark.png");          // Write the barcode image to the file system
        }
    }
}