// Title: Disable Bar Filling for Mailmark Barcode and Compare Outputs
// Description: Demonstrates how to generate a Mailmark barcode with default filled bars and with empty bars, saving both images for visual comparison.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It shows how to work with the ComplexBarcodeGenerator and MailmarkCodetext classes to customize barcode appearance, such as toggling the FilledBars property. Developers creating postal or logistics solutions often need to render Mailmark barcodes with specific visual styles, and this snippet illustrates typical usage patterns for generating and saving PNG images.
// Prompt: Disable bar filling for a Mailmark barcode and compare visual output with default filled bars.
// Tags: mailmark barcode, filledbars, complexbarcode, aspnet, png, aspose.barcode, barcode generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Generates Mailmark barcodes with and without filled bars, then saves the images for comparison.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates output directory, builds Mailmark codetext,
    /// generates two barcode images (filled and empty bars), and writes their paths to the console.
    /// </summary>
    static void Main()
    {
        // Define output directory and ensure it exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "MailmarkOutput");
        Directory.CreateDirectory(outputDir);

        // Create Mailmark codetext with valid sample data
        var mailmark = new MailmarkCodetext
        {
            Format = 4,
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Paths for the two generated images
        string filledPath = Path.Combine(outputDir, "Mailmark_FilledBars.png");
        string emptyPath = Path.Combine(outputDir, "Mailmark_EmptyBars.png");

        // Generate barcode with default filled bars (default is true)
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            generator.Save(filledPath, BarCodeImageFormat.Png);
        }

        // Generate barcode with bars not filled
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            generator.Parameters.Barcode.FilledBars = false;
            generator.Save(emptyPath, BarCodeImageFormat.Png);
        }

        // Output the locations of the saved images
        Console.WriteLine($"Mailmark barcode with filled bars saved to: {filledPath}");
        Console.WriteLine($"Mailmark barcode with empty bars saved to: {emptyPath}");
    }
}