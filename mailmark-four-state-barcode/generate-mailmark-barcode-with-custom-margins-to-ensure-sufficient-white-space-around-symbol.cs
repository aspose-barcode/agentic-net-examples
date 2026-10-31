// Title: Generate Mailmark 4-State Barcode with Custom Margins
// Description: Demonstrates how to create a Mailmark 4-State barcode using Aspose.BarCode and apply custom white-space margins around the symbol.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as Mailmark. It showcases the use of ComplexBarcodeGenerator, MailmarkCodetext, and barcode parameter settings (X-dimension and padding) to produce a PNG image. Developers working with postal or logistics solutions often need to generate Mailmark symbols with precise layout requirements, making this pattern a common reference.
// Prompt: Generate a Mailmark barcode with custom margins to ensure sufficient white space around the symbol.
// Tags: mailmark, barcode, generation, png, complexbarcode, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that creates a Mailmark 4‑State barcode with custom margins and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the barcode and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Prepare output directory in the system temporary folder
        string outputDir = Path.Combine(Path.GetTempPath(), "MailmarkDemo");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "Mailmark4State.png");

        // Define Mailmark 4‑State codetext with required fields
        var mailmark = new MailmarkCodetext
        {
            Format = 4,
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Generate the barcode using ComplexBarcodeGenerator
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Set the module (X‑dimension) size to 4 pixels
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Apply custom padding of 20 pixels on all sides to ensure sufficient white space
            generator.Parameters.Barcode.Padding.Left.Pixels = 20f;
            generator.Parameters.Barcode.Padding.Top.Pixels = 20f;
            generator.Parameters.Barcode.Padding.Right.Pixels = 20f;
            generator.Parameters.Barcode.Padding.Bottom.Pixels = 20f;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Mailmark barcode saved to: {outputPath}");
    }
}