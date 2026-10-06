// Title: Rotate Code 128 HIBC LIC barcode and save as JPEG
// Description: Generates a Code 128 HIBC LIC barcode, rotates it 90 degrees, and saves the image as a JPEG file.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It demonstrates how to use the ComplexBarcodeGenerator with HIBCLICPrimaryDataCodetext to create HIBC‑LIC barcodes, configure barcode parameters such as rotation and X‑dimension, and export the result to a common image format. Developers working with healthcare or logistics labeling often need to produce rotated HIBC barcodes for specific printer or label orientations.
// Prompt: Rotate the generated Code 128 HIBC LIC barcode by 90 degrees and save it as a JPEG image.
// Tags: barcode, code128, hibc, lic, rotation, jpeg, aspose.barcode, complexbarcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates rotating a Code 128 HIBC LIC barcode by 90° and saving it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode, applies rotation, and writes the output file.
    /// </summary>
    static void Main()
    {
        // Define the output directory and ensure it exists.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Full path for the resulting JPEG image.
        string outputPath = Path.Combine(outputDir, "HIBCLICPrimary_90.jpg");

        // Prepare the complex barcode data for a HIBC Code 128 LIC barcode.
        var complexCodetext = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCCode128LIC,
            Data = new PrimaryData
            {
                ProductOrCatalogNumber = "12345",
                LabelerIdentificationCode = "A999",
                UnitOfMeasureID = 1
            }
        };

        // Generate the barcode, set rotation and X‑dimension, then save as JPEG.
        using (var gen = new ComplexBarcodeGenerator(complexCodetext))
        {
            gen.Parameters.RotationAngle = 90f;               // Rotate 90 degrees.
            gen.Parameters.Barcode.XDimension.Pixels = 5f;   // Set module size.
            gen.Save(outputPath, BarCodeImageFormat.Jpeg);   // Save the image.
        }

        // Inform the user where the file was saved.
        Console.WriteLine("Barcode saved to " + outputPath);
    }
}