// Title: Generate QR HIBC LIC barcode with custom 5-pixel margin for label printing
// Description: Demonstrates how to create a HIBC LIC QR barcode using Aspose.BarCode and apply a uniform 5-pixel margin around the image, suitable for label printing scenarios.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as HIBC QR LIC. It showcases the use of ComplexBarcodeGenerator, HIBCLICPrimaryDataCodetext, and barcode padding parameters. Developers creating product labels, medical devices, or logistics tags often need to customize barcode margins to meet printer and layout requirements.
// Prompt: Use a custom barcode margin of five pixels when generating a QR HIBC LIC barcode for label printing.
// Tags: hibc, qr, barcode, generation, png, complexbarcodegenerator, hibclicprimarydatacodetext, margin, padding, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generating a HIBC QR LIC barcode with a custom 5‑pixel margin and saving it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates barcode data, configures margin, saves image, and reports the result.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the temporary directory
        string outputPath = Path.Combine(Path.GetTempPath(), "HIBCLIC_QR.png");

        try
        {
            // Prepare primary data for the HIBC LIC QR barcode
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

            // Initialize the complex barcode generator with the prepared data
            using (ComplexBarcodeGenerator gen = new ComplexBarcodeGenerator(complexCodetext))
            {
                // Apply a uniform 5‑pixel padding (margin) on all sides of the barcode
                gen.Parameters.Barcode.Padding.Left.Pixels = 5f;
                gen.Parameters.Barcode.Padding.Top.Pixels = 5f;
                gen.Parameters.Barcode.Padding.Right.Pixels = 5f;
                gen.Parameters.Barcode.Padding.Bottom.Pixels = 5f;

                // Save the generated barcode image as PNG
                gen.Save(outputPath, BarCodeImageFormat.Png);
            }

            // Inform the user where the barcode image was saved
            Console.WriteLine($"HIBC LIC QR barcode saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during barcode generation
            Console.WriteLine($"Error generating barcode: {ex.Message}");
        }
    }
}