// Title: Rotate Mailmark barcode by 90 degrees
// Description: Generates a Mailmark barcode, rotates the image 90° clockwise, and saves it as a PNG.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It demonstrates how to use the MailmarkCodetext class together with ComplexBarcodeGenerator to create a Mailmark symbology, apply image transformations (rotation), and export the result. Developers working with postal barcodes, custom layouts, or needing image manipulation in barcode workflows will find this pattern useful.
/// Prompt: Rotate the generated Mailmark barcode by 90 degrees to satisfy specific layout requirements.
/// Tags: mailmark, barcode, rotation, png, aspose.barcode, complexbarcodegenerator, imageprocessing

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Mailmark barcode, rotating it 90° clockwise, and saving the result as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Prepares Mailmark codetext, creates the barcode image, rotates it, and writes the output file.
    /// </summary>
    static void Main()
    {
        // Prepare Mailmark codetext with required fields
        var mailmark = new MailmarkCodetext
        {
            Format = 4,                         // 4‑state barcode
            VersionID = 1,
            Class = "0",                        // service type / class
            SupplychainID = 384224,
            ItemID = 16563762,                  // customer reference
            DestinationPostCodePlusDPS = "EF61AH8T " // valid postcode + DPS
        };

        // Generate the Mailmark barcode image using ComplexBarcodeGenerator
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            using (Image barcodeImage = generator.GenerateBarCodeImage())
            {
                // Rotate the image 90 degrees clockwise (no flip)
                barcodeImage.RotateFlip(RotateFlipType.Rotate90FlipNone);

                // Save the rotated barcode to a PNG file
                const string outputPath = "MailmarkRotated.png";
                barcodeImage.Save(outputPath, ImageFormat.Png);
                Console.WriteLine($"Rotated Mailmark barcode saved to: {outputPath}");
            }
        }
    }
}