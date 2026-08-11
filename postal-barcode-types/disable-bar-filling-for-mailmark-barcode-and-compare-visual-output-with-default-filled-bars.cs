// Title: Disable bar filling for Mailmark barcode and compare images
// Description: Demonstrates how to generate a Mailmark barcode with default filled bars and with bar filling disabled, saving both images for visual comparison.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator and MailmarkCodetext to create Mailmark symbols, a 4‑state postal barcode. Typical use cases include generating printable mail items and comparing visual styles. Developers often need to adjust rendering options such as FilledBars to meet design requirements.
// Prompt: Disable bar filling for a Mailmark barcode and compare visual output with default filled bars.
// Tags: mailmark, barcode, filledbars, complexbarcode, generation, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates Mailmark barcodes with and without filled bars to illustrate the effect of the FilledBars property.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates output folder, builds Mailmark codetext, generates two PNG images,
    /// and writes the file locations to the console.
    /// </summary>
    static void Main()
    {
        // Create output directory for generated images
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");
        Directory.CreateDirectory(outputDir);

        // Prepare Mailmark codetext (4‑state) with required fields
        var mailmark = new MailmarkCodetext
        {
            Format = 4,
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T " // trailing space required by specification
        };

        // Generate barcode with default filled bars (FilledBars = true by default)
        string filledPath = Path.Combine(outputDir, "mailmark_filled.png");
        using (var generatorFilled = new ComplexBarcodeGenerator(mailmark))
        {
            // No need to modify FilledBars; default behavior is to fill bars
            generatorFilled.Save(filledPath, BarCodeImageFormat.Png);
        }

        // Generate barcode with bars not filled (FilledBars = false)
        string noFillPath = Path.Combine(outputDir, "mailmark_nofill.png");
        using (var generatorNoFill = new ComplexBarcodeGenerator(mailmark))
        {
            generatorNoFill.Parameters.Barcode.FilledBars = false;
            generatorNoFill.Save(noFillPath, BarCodeImageFormat.Png);
        }

        // Output the locations of the generated images
        Console.WriteLine("Mailmark barcodes generated:");
        Console.WriteLine($"Filled bars image: {filledPath}");
        Console.WriteLine($"No filled bars image: {noFillPath}");
    }
}