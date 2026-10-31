// Title: Generate Mailmark 4‑state barcode with Reed‑Solomon correction and custom XDimension
// Description: Demonstrates creating a Mailmark 4‑state postal barcode using Aspose.BarCode, setting a 0.5 mm X‑dimension, and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as Mailmark. It showcases the use of ComplexBarcodeGenerator and MailmarkCodetext classes to configure postal barcodes, apply error‑correction, and customize sizing. Developers working with postal automation, mailing systems, or logistics can refer to this pattern for generating compliant Mailmark symbols.
// Prompt: Generate a Mailmark 4‑state postal barcode with Reed‑Solomon correction and custom XDimension of 0.5 mm.
// Tags: mailmark, postal barcode, 4-state, reed-solomon, xdimension, png, aspose.barcode, complexbarcodegenerator, mailmarkcodetext

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that generates a Mailmark 4‑state barcode with Reed‑Solomon error correction
/// and a custom X‑dimension of 0.5 mm, then saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates output folder, configures Mailmark data,
    /// generates the barcode, and writes the result to disk.
    /// </summary>
    static void Main()
    {
        // Define a temporary directory for the generated image.
        string outputDir = Path.Combine(Path.GetTempPath(), "MailmarkDemo");
        Directory.CreateDirectory(outputDir);

        // Full path for the output PNG file.
        string outputPath = Path.Combine(outputDir, "Mailmark4State.png");

        // Configure the Mailmark codetext with required fields.
        var mailmark = new MailmarkCodetext
        {
            Format = 4,                     // 4‑state format.
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Generate the barcode using ComplexBarcodeGenerator.
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Set the X‑dimension to 0.5 mm.
            generator.Parameters.Barcode.XDimension.Millimeters = 0.5f;

            // Mailmark 4‑state includes Reed‑Solomon error correction automatically.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Mailmark 4‑state barcode saved to: {outputPath}");
    }
}