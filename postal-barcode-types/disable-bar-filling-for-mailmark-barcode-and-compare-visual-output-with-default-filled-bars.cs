// Title: Disable Bar Filling for Mailmark Barcode
// Description: Demonstrates generating a Mailmark barcode with default filled bars, then disabling bar filling and saving both images for visual comparison.
// Category-Description: This example belongs to the Aspose.BarCode ComplexBarcode generation category. It showcases the use of ComplexBarcodeGenerator and MailmarkCodetext to create Mailmark symbology, a common requirement in postal automation. Developers often need to customize visual properties such as bar filling, and this snippet illustrates how to toggle the FilledBars property and compare outputs.
// Prompt: Disable bar filling for a Mailmark barcode and compare visual output with default filled bars.
// Tags: mailmark, barcode, filledbars, complexbarcode, generation, png

using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a Mailmark barcode, saves it with default filled bars,
/// then disables bar filling and saves the unfilled version for comparison.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates Mailmark codetext, generates two images,
    /// and writes the output file paths to the console.
    /// </summary>
    static void Main()
    {
        // Prepare Mailmark codetext with valid sample data
        var mailmark = new MailmarkCodetext
        {
            Format = 4,                     // 4‑state Mailmark
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T " // trailing space required
        };

        // Generate barcode with default filled bars and save it
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            string filledPath = "mailmark_filled.png";
            generator.Save(filledPath);
            Console.WriteLine($"Default filled Mailmark saved to: {filledPath}");

            // Disable bar filling for the same generator instance
            generator.Parameters.Barcode.FilledBars = false;

            // Save the unfilled barcode image
            string unfilledPath = "mailmark_unfilled.png";
            generator.Save(unfilledPath);
            Console.WriteLine($"Unfilled Mailmark saved to: {unfilledPath}");
        }
    }
}