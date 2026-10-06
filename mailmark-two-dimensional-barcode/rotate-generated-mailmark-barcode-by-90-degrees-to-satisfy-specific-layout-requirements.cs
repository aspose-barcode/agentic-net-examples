// Title: Rotate Mailmark 4-State Barcode by 90 Degrees
// Description: Generates a Mailmark 4‑State barcode, rotates it 90° and saves it as a PNG image. Demonstrates how to apply rotation to complex barcodes using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of MailmarkCodetext, ComplexBarcodeGenerator, and barcode parameter settings such as X‑Dimension and RotationAngle. Developers creating postal or logistics solutions often need to generate Mailmark barcodes and adjust their orientation to fit specific label layouts or printing requirements.
// Prompt: Rotate the generated Mailmark barcode by 90 degrees to satisfy specific layout requirements.
// Tags: mailmark, barcode, rotation, png, aspose.barcode, complexbarcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Mailmark 4‑State barcode and rotating it 90°.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the output directory, builds Mailmark codetext,
    /// generates a rotated barcode, and saves it to a PNG file.
    /// </summary>
    static void Main()
    {
        // Define a temporary folder for the output image
        string outputDir = Path.Combine(Path.GetTempPath(), "MailmarkDemo");
        Directory.CreateDirectory(outputDir);

        // Full path for the rotated barcode image
        string outputPath = Path.Combine(outputDir, "Mailmark4State_Rotated.png");

        // Create Mailmark 4‑State codetext with required fields
        MailmarkCodetext mailmarkCode = new MailmarkCodetext
        {
            Format = 4,
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Generate the barcode and apply a 90° rotation
        using (var generator = new ComplexBarcodeGenerator(mailmarkCode))
        {
            // Set the module size (X‑Dimension) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Rotate the entire barcode image by 90 degrees
            generator.Parameters.RotationAngle = 90f;

            // Save the rotated barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image was saved
        Console.WriteLine($"Rotated Mailmark barcode saved to: {outputPath}");
    }
}