using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generation of a HIBC QR LIC barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a HIBC QR LIC barcode and saves it as an image file.
    /// </summary>
    static void Main()
    {
        // Prepare primary HIBC LIC data with product details and labeler code.
        var primaryCodetext = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCQRLIC,
            Data = new PrimaryData
            {
                ProductOrCatalogNumber = "12345",   // Product or catalog identifier
                LabelerIdentificationCode = "A999", // Labeler identification code
                UnitOfMeasureID = 1                 // Unit of measure identifier
            }
        };

        // Create a ComplexBarcodeGenerator using the prepared primary data.
        using (var generator = new ComplexBarcodeGenerator(primaryCodetext))
        {
            // Configure visual appearance: white background and black bars.
            generator.Parameters.BackColor = Color.White;
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save the generated barcode image to a PNG file.
            generator.Save("hibc_qr.png");
        }

        // Inform the user that the barcode has been generated.
        Console.WriteLine("Barcode generated: hibc_qr.png");
    }
}