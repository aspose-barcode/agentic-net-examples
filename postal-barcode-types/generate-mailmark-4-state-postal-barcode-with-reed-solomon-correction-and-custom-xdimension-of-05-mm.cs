// Title: Generate Mailmark 4‑state barcode with Reed‑Solomon correction and custom XDimension
// Description: This example creates a Mailmark 4‑state postal barcode using Aspose.BarCode, applies Reed‑Solomon error correction, sets a custom XDimension of 0.5 mm, and saves the result as a PNG image.
// Category-Description: Aspose.BarCode examples for complex postal barcodes. Demonstrates how to generate Mailmark 4‑state barcodes with Reed‑Solomon correction using ComplexBarcodeGenerator and MailmarkCodetext classes. Typical use cases include encoding postal routing information for mail sorting systems. Developers often need to customize dimensions, error correction, and output formats.
// Prompt: Generate a Mailmark 4‑state postal barcode with Reed‑Solomon correction and custom XDimension of 0.5 mm.
// Tags: mailmark, postal barcode, 4-state, reed-solomon, xdimension, png, aspose.barcode, complexbarcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generation of a Mailmark 4‑state barcode with Reed‑Solomon correction
/// and a custom XDimension, then saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates output directory, configures Mailmark data,
    /// generates the barcode, and writes the file path to the console.
    /// </summary>
    static void Main()
    {
        // Define a temporary folder to store the generated barcode image.
        string outputDir = Path.Combine(Path.GetTempPath(), "MailmarkDemo");
        Directory.CreateDirectory(outputDir);

        // Full path for the resulting PNG file.
        string outputPath = Path.Combine(outputDir, "Mailmark4State.png");

        // Configure Mailmark codetext with required fields.
        var mailmark = new MailmarkCodetext
        {
            Format = 4,                     // 4‑state format
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Generate the barcode using ComplexBarcodeGenerator.
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Set XDimension to 0.5 mm (custom module size).
            generator.Parameters.Barcode.XDimension.Millimeters = 0.5f;

            // Save the barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Mailmark barcode saved to: {outputPath}");
    }
}