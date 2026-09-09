// Title: Generate Mailmark 4‑state barcode with custom XDimension
// Description: Demonstrates creating a Mailmark 4‑state postal barcode using Aspose.BarCode, setting a 0.5 mm X‑dimension, and saving it as PNG.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, focusing on Mailmark 4‑state postal symbols. It showcases the use of MailmarkCodetext and ComplexBarcodeGenerator classes to configure barcode data, error correction, and sizing. Developers working with postal automation, mail sorting, or logistics can refer to this pattern for generating compliant Mailmark barcodes in .NET applications.
// Prompt: Generate a Mailmark 4‑state postal barcode with Reed‑Solomon correction and custom XDimension of 0.5 mm.
// Tags: mailmark, postal barcode, 4-state, reed-solomon, xdimension, png, aspnet, aspose.barcode, complexbarcodegenerator

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generation of a Mailmark 4‑state barcode with custom X‑dimension using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode, configures parameters, and saves the image to a temporary folder.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the output file
        string outputDir = Path.Combine(Path.GetTempPath(), "MailmarkDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "Mailmark4State.png");

        // Define the Mailmark data fields
        MailmarkCodetext mailmark = new MailmarkCodetext
        {
            Format = 4,
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Initialize the complex barcode generator with the Mailmark codetext
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Set the X‑dimension to 0.5 mm (Reed‑Solomon correction is inherent to Mailmark 4‑state)
            generator.Parameters.Barcode.XDimension.Millimeters = 0.5f;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine("Mailmark 4‑state barcode saved to: " + outputPath);
    }
}