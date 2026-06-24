using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation of a HIBC LIC barcode image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates primary data for a HIBC LIC barcode,
    /// generates the barcode image, and saves it as a BMP file.
    /// </summary>
    static void Main()
    {
        // Initialize primary data codetext for HIBC LIC barcode
        var primaryCodetext = new HIBCLICPrimaryDataCodetext
        {
            // Specify the barcode symbology (Code128 LIC in this example)
            BarcodeType = EncodeTypes.HIBCCode128LIC,
            // Populate required primary data fields
            Data = new PrimaryData
            {
                ProductOrCatalogNumber = "12345",          // Product or catalog identifier
                LabelerIdentificationCode = "A999",       // Labeler identification code as required
                UnitOfMeasureID = 1                        // Unit of measure identifier
            }
        };

        // Use ComplexBarcodeGenerator to create the barcode image
        using (var generator = new ComplexBarcodeGenerator(primaryCodetext))
        using (Image image = generator.GenerateBarCodeImage())
        {
            // Save the generated image to disk in BMP format
            image.Save("hibc_primary.bmp", ImageFormat.Bmp);
        }

        // Inform the user that the image has been saved
        Console.WriteLine("HIBC LIC barcode image saved as hibc_primary.bmp");
    }
}