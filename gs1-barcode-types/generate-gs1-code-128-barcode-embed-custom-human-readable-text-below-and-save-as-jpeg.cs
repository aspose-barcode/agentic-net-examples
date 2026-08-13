// Title: Generate GS1 Code 128 barcode with custom human‑readable text
// Description: Demonstrates creating a GS1 Code 128 barcode, adding readable text below the bars, and saving the result as a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.GS1Code128. Typical use cases include encoding product identifiers (GTIN) for retail, adding human‑readable text for scanning verification, and exporting to common image formats. Developers often need to customize text placement, font, and colors when integrating barcodes into packaging or documents.
// Prompt: Generate a GS1 Code 128 barcode, embed custom human‑readable text below, and save as JPEG.
// Tags: gs1,code128,barcode,generation,human-readable,text,jpeg,aspose.barcode,aspose.drawing

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a GS1 Code 128 barcode,
/// places custom human‑readable text below the barcode,
/// and saves the image as a JPEG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates the barcode and writes it to disk.
    /// </summary>
    static void Main()
    {
        // Define the GS1 Code 128 data.
        // GS1 Code 128 requires an Application Identifier (01) with a 14‑digit GTIN.
        // Example GTIN: 01234567890123
        string gs1Code = "(01)01234567890123";

        // Initialize the barcode generator with the GS1 Code 128 symbology and data.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, gs1Code))
        {
            // Position the human‑readable text (the encoded value) below the barcode.
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // Optionally adjust the font size of the human‑readable text for better readability.
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 10f;

            // Set the colors: black bars on a white background.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save the generated barcode as a JPEG image file.
            generator.Save("gs1code128.jpg");
        }
    }
}