// Title: Rotate Code 128 HIBC LIC barcode and save as JPEG
// Description: Demonstrates generating a HIBC LIC Code 128 barcode, rotating it 90 degrees, and saving the result as a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on complex barcode creation (HIBC LIC) and image manipulation. It showcases the use of ComplexBarcodeGenerator, EncodeTypes, and barcode parameter settings such as rotation, colors, and output format. Developers working with healthcare or logistics barcodes often need to customize orientation and export images for labeling systems.
// Prompt: Rotate the generated Code 128 HIBC LIC barcode by 90 degrees and save it as a JPEG image.
// Tags: barcode, code128, hibc, rotation, jpeg, aspose.barcode, complexbarcode, image generation

using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Generates a HIBC LIC Code 128 barcode, rotates it 90 degrees, and saves it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Prepares the HIBC LIC codetext, configures barcode parameters,
    /// applies a 90‑degree rotation, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Prepare HIBC LIC Code 128 complex codetext with required primary data
        var hibcCodetext = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCCode128LIC,
            Data = new PrimaryData
            {
                ProductOrCatalogNumber = "12345",
                LabelerIdentificationCode = "A999",
                UnitOfMeasureID = 1
            }
        };

        // Create the generator, set rotation and colors, then save as JPEG
        using (var generator = new ComplexBarcodeGenerator(hibcCodetext))
        {
            // Rotate the barcode image by 90 degrees
            generator.Parameters.RotationAngle = 90f;

            // Set foreground (barcode) and background colors
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Define output file path and save the image
            const string outputPath = "HIBC_Code128_LIC.jpg";
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);

            // Inform the user where the file was saved
            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }
}