// Title: Generate and rotate a Mailmark 4-state barcode image
// Description: Demonstrates creating a Mailmark 4-state barcode, setting its properties, and rotating the generated image by 90 degrees for layout requirements.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as Mailmark. It showcases the use of ComplexBarcodeGenerator, MailmarkCodetext, and barcode parameter settings like rotation and X-dimension. Developers often need to generate custom barcodes for mailing and logistics, adjust visual orientation, and export them to common image formats.
// Prompt: Rotate the generated Mailmark barcode by 90 degrees to satisfy specific layout requirements.
// Tags: mailmark, barcode, rotation, image, png, complexbarcode, csharp, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generation of a Mailmark 4-state barcode and rotating it by 90 degrees.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates output folder, configures Mailmark data, generates the barcode, applies rotation, and saves the image.
    /// </summary>
    static void Main()
    {
        // Prepare output directory in the temporary folder with a unique name
        string outputDir = Path.Combine(Path.GetTempPath(), "MailmarkDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Create Mailmark 4-state codetext and set required fields
        MailmarkCodetext mailmark = new MailmarkCodetext();
        mailmark.Format = 4;
        mailmark.VersionID = 1;
        mailmark.Class = "0";
        mailmark.SupplychainID = 384224;
        mailmark.ItemID = 16563762;
        mailmark.DestinationPostCodePlusDPS = "EF61AH8T ";

        // Generate the barcode with a 90-degree rotation
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Rotate the barcode image
            generator.Parameters.RotationAngle = 90f;
            // Set the X-dimension (module width) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 4;

            // Define output file path
            string outputPath = Path.Combine(outputDir, "Mailmark4State_Rotated.png");
            // Save the barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
            Console.WriteLine("Barcode saved to: " + outputPath);
        }
    }
}