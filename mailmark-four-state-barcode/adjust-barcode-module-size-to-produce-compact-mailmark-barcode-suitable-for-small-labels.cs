// Title: Compact Mailmark Barcode Generation with Adjusted Module Size
// Description: Demonstrates how to generate a Mailmark barcode with a reduced module size for compact printing on small labels.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on Mailmark symbology. It showcases the use of BarcodeGenerator, EncodeTypes, and barcode parameters such as XDimension and BarHeight to customize the appearance. Developers often need to create high‑density barcodes for limited‑space applications like small product labels or packaging.
// Prompt: Adjust barcode module size to produce a compact Mailmark barcode suitable for small labels.
// Tags: mailmark, barcode generation, module size, compact, png, aspose.barcode, barcodegenerator, parameters

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a compact Mailmark barcode with customized module size and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates output folder, configures barcode parameters, and saves the image.
    /// </summary>
    static void Main()
    {
        // Define a temporary output folder with a unique name
        string outputFolder = Path.Combine(Path.GetTempPath(), "MailmarkDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Mailmark code to encode
        string mailmarkCode = "21B2254800659JW5O9QA6Y";

        // Full path for the resulting PNG file
        string outputPath = Path.Combine(outputFolder, "MailmarkCompact.png");

        // Initialize the barcode generator for Mailmark symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Mailmark, mailmarkCode))
        {
            // Reduce the module (X) dimension to make the barcode more compact
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Set a modest bar height suitable for small labels
            generator.Parameters.Barcode.BarHeight.Pixels = 30f;

            // Hide the border to save space
            generator.Parameters.Border.Visible = false;

            // Save the barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine("Mailmark barcode saved to: " + outputPath);
    }
}