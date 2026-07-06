// Title: Generate Mailmark barcode with custom margins
// Description: Demonstrates creating a Mailmark barcode and applying custom white‑space margins to ensure adequate surrounding space.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as Mailmark. It showcases the use of ComplexBarcodeGenerator, MailmarkCodetext, and barcode parameter settings (padding, resolution). Developers often need to customize appearance and layout of generated barcodes for printing or embedding in documents, and this snippet illustrates typical steps for configuring margins and saving the image.
// Prompt: Generate a Mailmark barcode with custom margins to ensure sufficient white space around the symbol.
// Tags: mailmark, barcode, padding, margin, png, aspnet, aspose.barcode, complexbarcodegenerator

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generating a Mailmark barcode with custom margins using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a Mailmark barcode, applies padding, sets resolution, and saves as PNG.
    /// </summary>
    static void Main()
    {
        // Prepare Mailmark codetext with required fields
        var mailmark = new MailmarkCodetext
        {
            Format = 4,                     // unspecified/default format
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Create ComplexBarcodeGenerator using the Mailmark codetext
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Set custom white space (padding) around the barcode (10 points on each side)
            generator.Parameters.Barcode.Padding.Left.Point   = 10f;
            generator.Parameters.Barcode.Padding.Top.Point    = 10f;
            generator.Parameters.Barcode.Padding.Right.Point  = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 10f;

            // Optional: set image resolution (dots per inch)
            generator.Parameters.Resolution = 300;

            // Save the generated barcode image to a PNG file
            string outputPath = "mailmark.png";
            generator.Save(outputPath, BarCodeImageFormat.Png);
            Console.WriteLine($"Mailmark barcode saved to {outputPath}");
        }
    }
}