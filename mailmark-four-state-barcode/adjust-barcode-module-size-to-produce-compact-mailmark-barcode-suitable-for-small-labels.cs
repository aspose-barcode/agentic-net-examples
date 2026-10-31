// Title: Generate Compact Mailmark Barcode for Small Labels
// Description: Demonstrates how to create a Mailmark barcode with reduced module size and bar height, producing a compact image suitable for small label printing.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on Mailmark symbology. It shows how to configure XDimension and BarHeight via the BarcodeGenerator.Parameters.Barcode API to tailor barcode size. Developers often need to adjust these settings when printing barcodes on limited‑space media such as shipping labels or product tags.
// Prompt: Adjust barcode module size to produce a compact Mailmark barcode suitable for small labels.
// Tags: mailmark, barcode, generation, compact, label, xdimension, barheight, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a compact Mailmark barcode suitable for small labels.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a temporary folder, generates a compact Mailmark barcode,
    /// and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary output folder
        string outputFolder = Path.Combine(Path.GetTempPath(), "MailmarkDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Define the full path for the resulting PNG image
        string outputPath = Path.Combine(outputFolder, "MailmarkCompact.png");

        // Sample Mailmark C-type code (valid format)
        string mailmarkCode = "21B2254800659JW5O9QA6Y";

        // Initialize the barcode generator for Mailmark symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Mailmark, mailmarkCode))
        {
            // Reduce module size (XDimension) to make the barcode more compact
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Reduce bar height to keep the overall barcode size small
            generator.Parameters.Barcode.BarHeight.Pixels = 30f;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Compact Mailmark barcode saved to: {outputPath}");
    }
}