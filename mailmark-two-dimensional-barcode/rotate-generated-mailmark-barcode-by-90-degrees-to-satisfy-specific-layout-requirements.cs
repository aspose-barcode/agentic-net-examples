// Title: Rotating a Mailmark barcode by 90 degrees
// Description: Demonstrates generating a Mailmark 4‑state barcode and rotating it 90° for layout requirements.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, focusing on Mailmark symbology. It shows how to configure Mailmark parameters, set barcode dimensions, apply rotation, and save the image using ComplexBarcodeGenerator. Developers working with postal barcodes often need to adjust orientation for printing on pre‑designed forms, making this pattern useful for integrating Mailmark into custom workflows.
// Prompt: Rotate the generated Mailmark barcode by 90 degrees to satisfy specific layout requirements.
// Tags: mailmark, barcode, rotation, complexbarcode, generation, png, aspnet.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a Mailmark 4‑state barcode, rotates it 90°, and saves as PNG.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates output directory, configures Mailmark data, generates the barcode, and writes the file path to console.
    /// </summary>
    static void Main()
    {
        // Build a unique temporary folder for the output file
        string outputDir = Path.Combine(Path.GetTempPath(), "MailmarkExample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define the full path for the resulting PNG image
        string outputPath = Path.Combine(outputDir, "Mailmark4State_Rotated.png");

        // Set up Mailmark specific data fields
        MailmarkCodetext mailmark = new MailmarkCodetext
        {
            Format = 4,
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Generate the barcode using the complex barcode generator
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Define the module size (pixel dimension) of the barcode
            generator.Parameters.Barcode.XDimension.Pixels = 4;

            // Rotate the barcode image by 90 degrees
            generator.Parameters.RotationAngle = 90f;

            // Save the rotated barcode to the specified path
            generator.Save(outputPath);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}