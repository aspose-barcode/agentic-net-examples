// Title: Rotate HIBC Code 128 LIC barcode and save as JPEG
// Description: Demonstrates generating a HIBC Code 128 LIC barcode, rotating it 90° and saving the result as a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator with HIBCLICPrimaryDataCodetext to create HIBC‑based barcodes. Typical use cases include labeling medical devices or pharmaceutical products where HIBC standards apply. Developers often need to customize barcode appearance, such as rotation, size, and output format, using the Parameters and Save APIs.
// Prompt: Rotate the generated Code 128 HIBC LIC barcode by 90 degrees and save it as a JPEG image.
// Tags: code128, hibc, rotation, jpeg, complexbarcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Generates a HIBC Code 128 LIC barcode, rotates it 90 degrees, and saves it as a JPEG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, applies rotation, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path
        string outputFile = Path.Combine(Environment.CurrentDirectory, "HIBCLIC_Code128_90.jpg");
        string outputDir = Path.GetDirectoryName(outputFile);

        // Ensure the output directory exists
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Build the HIBC LIC primary data codetext
        HIBCLICPrimaryDataCodetext complexCodetext = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCCode128LIC,
            Data = new PrimaryData
            {
                ProductOrCatalogNumber = "12345",
                LabelerIdentificationCode = "A999",
                UnitOfMeasureID = 1
            }
        };

        // Generate the barcode with a 90‑degree rotation
        using (ComplexBarcodeGenerator gen = new ComplexBarcodeGenerator(complexCodetext))
        {
            gen.Parameters.RotationAngle = 90f;               // Rotate the barcode
            gen.Parameters.Barcode.XDimension.Pixels = 5;    // Set module size
            gen.Save(outputFile, BarCodeImageFormat.Jpeg);   // Save as JPEG
        }

        Console.WriteLine($"Barcode saved to: {outputFile}");
    }
}