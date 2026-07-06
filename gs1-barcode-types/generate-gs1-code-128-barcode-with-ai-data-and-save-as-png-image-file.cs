// Title: Generate GS1 Code 128 Barcode with AI Data and Save as PNG
// Description: Creates a GS1‑compliant Code 128 barcode containing Application Identifier data and writes it to a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, demonstrating how to encode GS1 Application Identifier (AI) data using the BarcodeGenerator and EncodeTypes classes. Developers often need to create GS1‑compliant barcodes for product identification, inventory, and logistics, requiring correct AI formatting and optional checksum display. The snippet shows the essential steps to configure parameters and save the barcode as an image.
// Prompt: Generate a GS1 Code 128 barcode with AI data and save as a PNG image file.
// Tags: gs1,code128,barcode,generation,png,aspose.barcode,applicationidentifier

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation of a GS1 Code 128 barcode with AI data and saving it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and writes it to disk.
    /// </summary>
    static void Main()
    {
        // Define GS1 AI data: (01) – GTIN, (21) – Serial number
        string codeText = "(01)01234567890123(21)ABC123";

        // Initialize the barcode generator with GS1 Code 128 symbology and the AI data
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, codeText))
        {
            // Optional: display the checksum in the human‑readable text
            generator.Parameters.Barcode.ChecksumAlwaysShow = true;

            // Save the generated barcode as a PNG image file
            generator.Save("gs1code128.png");
        }

        // Inform the user that the barcode has been saved
        Console.WriteLine("GS1 Code 128 barcode saved as gs1code128.png");
    }
}