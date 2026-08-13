// Title: Generate Mailmark 4‑State Barcode with Reed‑Solomon Correction and Custom XDimension
// Description: Demonstrates how to create a Mailmark 4‑state postal barcode, apply Reed‑Solomon error correction, and set a custom XDimension of 0.5 mm before saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex postal symbologies. It showcases the use of MailmarkCodetext to build the codetext, ComplexBarcodeGenerator to render the barcode, and BarCodeImageFormat for output. Developers working with postal automation, mail sorting, or custom barcode dimensions will find these APIs essential for creating compliant Mailmark barcodes.
// Prompt: Generate a Mailmark 4‑state postal barcode with Reed‑Solomon correction and custom XDimension of 0.5 mm.
// Tags: mailmark,4-state,barcode,generation,aspose.barcode,complexbarcode,reed-solomon,xdimension,png

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that creates a Mailmark 4‑state barcode with Reed‑Solomon correction
/// and a custom XDimension, then saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Builds the Mailmark codetext, configures the generator,
    /// and writes the resulting barcode image to disk.
    /// </summary>
    static void Main()
    {
        // Initialize Mailmark codetext for a 4‑state barcode
        var mailmark = new MailmarkCodetext
        {
            Format = 4,                 // Specify 4‑state format
            VersionID = 1,              // Set version identifier
            Class = "0",                // Set class value
            SupplychainID = 384224,     // Set supply chain identifier
            ItemID = 16563762,          // Set item identifier
            // DestinationPostCodePlusDPS requires a trailing space
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Create a ComplexBarcodeGenerator using the prepared codetext
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Apply custom XDimension of 0.5 mm (affects barcode module size)
            generator.Parameters.Barcode.XDimension.Millimeters = 0.5f;

            // Define output file path and save the barcode as a PNG image
            string outputPath = "mailmark.png";
            generator.Save(outputPath, BarCodeImageFormat.Png);

            // Inform the user where the file was saved
            Console.WriteLine($"Mailmark barcode saved to {outputPath}");
        }
    }
}