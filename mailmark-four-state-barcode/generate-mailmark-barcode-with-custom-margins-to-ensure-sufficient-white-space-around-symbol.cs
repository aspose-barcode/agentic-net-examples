// Title: Generate Mailmark 4‑State Barcode with Custom Margins
// Description: Demonstrates how to create a Mailmark 4‑State barcode and apply custom white‑space margins around the symbol.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator and MailmarkCodetext to produce Mailmark symbols, a postal barcode used in the UK. Developers working with postal barcodes often need to control module size and padding to meet printing specifications, and this snippet illustrates those typical tasks.
// Prompt: Generate a Mailmark barcode with custom margins to ensure sufficient white space around the symbol.
// Tags: mailmark, barcode, margin, png, aspose.barcode, complexbarcodegenerator, codetext

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a Mailmark 4‑State barcode with custom padding and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, applies custom margins, and writes the image to a temporary folder.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary output directory.
        string outputDir = Path.Combine(Path.GetTempPath(), "MailmarkDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "Mailmark4State.png");

        // Build the Mailmark 4‑State codetext with required fields.
        MailmarkCodetext mailmark = new MailmarkCodetext
        {
            Format = 4,
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Generate the barcode using ComplexBarcodeGenerator.
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Define the module (dot) size in pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Apply custom white‑space padding around the barcode.
            generator.Parameters.Barcode.Padding.Left.Pixels = 20f;
            generator.Parameters.Barcode.Padding.Top.Pixels = 20f;
            generator.Parameters.Barcode.Padding.Right.Pixels = 20f;
            generator.Parameters.Barcode.Padding.Bottom.Pixels = 20f;

            // Save the generated barcode image as PNG.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Mailmark barcode saved to: {outputPath}");
    }
}