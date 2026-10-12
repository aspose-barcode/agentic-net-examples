// Title: Disable Bar Filling for Mailmark Barcode and Compare Outputs
// Description: Demonstrates how to generate a Mailmark barcode with default filled bars and with empty bars, saving both images for visual comparison.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It shows how to work with the MailmarkCodetext class and ComplexBarcodeGenerator to customize barcode appearance, such as toggling the FilledBars property. Developers creating postal or logistics solutions often need to render Mailmark symbols with different visual styles, and this snippet illustrates the typical steps: configure codetext, generate images, and adjust rendering parameters.
// Prompt: Disable bar filling for a Mailmark barcode and compare visual output with default filled bars.
// Tags: mailmark, barcode, filledbars, complexbarcode, image, png, c#, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that generates Mailmark barcodes with and without filled bars
/// to illustrate the visual difference using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates output directory, configures Mailmark codetext,
    /// generates two barcode images (filled and empty bars), and writes their paths to the console.
    /// </summary>
    static void Main()
    {
        // Define output folder relative to the current working directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "MailmarkOutput");
        Directory.CreateDirectory(outputDir);

        // Prepare Mailmark codetext with valid values
        var mailmark = new MailmarkCodetext
        {
            Format = 4,                     // 4-state Mailmark
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T " // trailing space required
        };

        // Generate barcode with default filled bars (FilledBars = true by default)
        string filledPath = Path.Combine(outputDir, "MailmarkFilledBars.png");
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            generator.Save(filledPath, BarCodeImageFormat.Png);
        }
        Console.WriteLine($"Saved default filled-bars barcode to: {filledPath}");

        // Generate barcode with empty bars (FilledBars = false)
        string emptyPath = Path.Combine(outputDir, "MailmarkEmptyBars.png");
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            generator.Parameters.Barcode.FilledBars = false;
            generator.Save(emptyPath, BarCodeImageFormat.Png);
        }
        Console.WriteLine($"Saved empty-bars barcode to: {emptyPath}");
    }
}