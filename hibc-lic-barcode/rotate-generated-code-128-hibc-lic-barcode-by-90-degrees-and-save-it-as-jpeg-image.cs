// Title: Rotate a Code 128 HIBC LIC barcode and save as JPEG
// Description: Demonstrates generating a HIBC Code 128 LIC barcode, rotating it 90° and exporting it as a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as HIBC. It showcases the use of ComplexBarcodeGenerator, EncodeTypes, and barcode parameter settings like rotation and X‑dimension. Developers often need to create rotated barcodes for label layouts, packaging, or compliance printing, and this snippet provides a concise reference.
// Prompt: Rotate the generated Code 128 HIBC LIC barcode by 90 degrees and save it as a JPEG image.
// Tags: code128, hibc, rotation, jpeg, aspose.barcode, complexbarcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates a HIBC Code 128 LIC barcode, rotates it 90 degrees, and saves it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, applies rotation, and writes the output file.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output JPEG file.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "HIBCLICPrimaryRotated.jpg");

        // Prepare the HIBC LIC primary data codetext with required fields.
        HIBCLICPrimaryDataCodetext codetext = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCCode128LIC,
            Data = new PrimaryData()
        };
        codetext.Data.ProductOrCatalogNumber = "12345";
        codetext.Data.LabelerIdentificationCode = "A999";
        codetext.Data.UnitOfMeasureID = 1;

        // Create a ComplexBarcodeGenerator using the prepared codetext.
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(codetext))
        {
            // Rotate the barcode image by 90 degrees.
            generator.Parameters.RotationAngle = 90f;

            // Set the X-dimension (module width) in pixels for better visual quality.
            generator.Parameters.Barcode.XDimension.Pixels = 5f;

            // Save the generated barcode as a JPEG file.
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}