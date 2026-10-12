// Title: Generate Mailmark 4-State Barcode with Custom Margins
// Description: Demonstrates creating a Mailmark 4‑State barcode and applying custom white‑space margins around the symbol.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of Aspose.BarCode.ComplexBarcode and Aspose.BarCode.Generation APIs to build a Mailmark barcode, configure its visual parameters such as module size and padding, and export the result as a PNG image. Developers working with postal symbologies, custom barcode layouts, or needing precise control over barcode margins will find this pattern useful.
// Prompt: Generate a Mailmark barcode with custom margins to ensure sufficient white space around the symbol.
// Tags: mailmark, barcode, custom-margins, png, aspose.barcode, complexbarcode, generation

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a Mailmark 4‑State barcode with custom padding and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, applies custom margins, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary output directory for the generated image.
        string outputDir = Path.Combine(Path.GetTempPath(), "MailmarkDemo");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "Mailmark4State.png");

        // Define the Mailmark 4‑State codetext with required fields.
        MailmarkCodetext mailmarkCode = new MailmarkCodetext
        {
            Format = 4,
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Initialize the complex barcode generator using the Mailmark codetext.
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(mailmarkCode))
        {
            // Optionally set the module (dot) size of the barcode.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Apply custom white‑space padding around the barcode to ensure sufficient margins.
            generator.Parameters.Barcode.Padding.Left.Pixels = 20f;
            generator.Parameters.Barcode.Padding.Top.Pixels = 20f;
            generator.Parameters.Barcode.Padding.Right.Pixels = 20f;
            generator.Parameters.Barcode.Padding.Bottom.Pixels = 20f;

            // Save the generated barcode image in PNG format.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Mailmark barcode saved to: {outputPath}");
    }
}