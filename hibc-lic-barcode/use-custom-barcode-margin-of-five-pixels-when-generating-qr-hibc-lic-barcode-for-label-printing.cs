using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generation of a HIBC QR LIC barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a primary HIBC LIC codetext,
    /// configures barcode padding, generates the QR barcode, and saves it to a file.
    /// </summary>
    static void Main()
    {
        // Define primary HIBC LIC data for the QR barcode
        var primaryCodetext = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCQRLIC,
            Data = new PrimaryData
            {
                ProductOrCatalogNumber = "12345",   // Product or catalog identifier
                LabelerIdentificationCode = "A999", // Labeler ID
                UnitOfMeasureID = 1                 // Unit of measure identifier
            }
        };

        // Initialize the barcode generator with the defined codetext
        using (var generator = new ComplexBarcodeGenerator(primaryCodetext))
        {
            // Set a uniform 5‑pixel margin (padding) on all sides of the barcode
            generator.Parameters.Barcode.Padding.Left.Pixels   = 5f;
            generator.Parameters.Barcode.Padding.Top.Pixels    = 5f;
            generator.Parameters.Barcode.Padding.Right.Pixels  = 5f;
            generator.Parameters.Barcode.Padding.Bottom.Pixels = 5f;

            // Define output file path and save the generated barcode image
            string outputPath = "hibc_qr.png";
            generator.Save(outputPath);

            // Inform the user where the barcode image was saved
            Console.WriteLine($"QR HIBC LIC barcode saved to {outputPath}");
        }
    }
}