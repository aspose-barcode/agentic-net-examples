// Title: Generate HIBC QR LIC barcode with a ten‑module quiet zone
// Description: Demonstrates how to create a HIBC QR LIC barcode and add a quiet zone of ten modules around it, ensuring compliance with printing standards.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator and HIBCLICPrimaryDataCodetext to produce HIBC QR LIC barcodes, a common requirement in healthcare and logistics for product identification. Developers often need to adjust module size and padding to meet specific printing guidelines, and this snippet illustrates those typical steps.
// Prompt: Add a quiet zone of ten modules around a DataMatrix HIBC LIC barcode to meet printing standards.
// Tags: hibc, qr, lic, quietzone, png, complexbarcode, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generating a HIBC QR LIC barcode with a ten‑module quiet zone.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, applies padding, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the temporary folder
        string outputPath = Path.Combine(Path.GetTempPath(), "HIBCLIC_QuietZone.png");

        // Prepare primary data for the HIBC QR LIC barcode
        HIBCLICPrimaryDataCodetext complexCodetext = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCQRLIC,
            Data = new PrimaryData
            {
                ProductOrCatalogNumber = "12345",
                LabelerIdentificationCode = "A999",
                UnitOfMeasureID = 1
            }
        };

        // Create the barcode generator with the prepared complex codetext
        using (ComplexBarcodeGenerator gen = new ComplexBarcodeGenerator(complexCodetext))
        {
            // Set the module size (XDimension) – 5 pixels per module
            gen.Parameters.Barcode.XDimension.Pixels = 5f;

            // Calculate quiet zone size: 10 modules * module size
            float quietZone = 10f * gen.Parameters.Barcode.XDimension.Pixels;

            // Apply equal padding on all sides to create the quiet zone
            gen.Parameters.Barcode.Padding.Left.Pixels = quietZone;
            gen.Parameters.Barcode.Padding.Right.Pixels = quietZone;
            gen.Parameters.Barcode.Padding.Top.Pixels = quietZone;
            gen.Parameters.Barcode.Padding.Bottom.Pixels = quietZone;

            // Save the generated barcode image as PNG
            gen.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"HIBC QR LIC barcode with quiet zone saved to: {outputPath}");
    }
}