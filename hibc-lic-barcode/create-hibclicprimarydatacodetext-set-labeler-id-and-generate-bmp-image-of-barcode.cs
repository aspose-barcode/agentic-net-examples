// Title: Generate HIBC Code 128 LIC barcode with primary data and save as BMP
// Description: Demonstrates creating a HIBCLICPrimaryDataCodetext, setting the labeler ID, and generating a BMP image of the barcode.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, showcasing the use of ComplexBarcodeGenerator with HIBCCode128LIC symbology. It illustrates how to configure primary data fields, such as product number and labeler identification, and render the barcode to a bitmap image. Developers needing to produce HIBC-compliant barcodes for medical or pharmaceutical labeling can follow this pattern.
// Prompt: Create a HIBCLICPrimaryDataCodetext, set labeler ID, and generate a BMP image of the barcode.
// Tags: hibc, code128lic, barcode generation, bmp, complexbarcode, aspnet, aspose.barcode

using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a HIBCLIC primary data codetext,
/// sets the labeler identification code, and saves the generated barcode as a BMP image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Initialize HIBCLIC primary data codetext with required fields
        var primaryCodetext = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCCode128LIC,
            Data = new PrimaryData
            {
                ProductOrCatalogNumber = "12345",
                LabelerIdentificationCode = "A999", // labeler ID
                UnitOfMeasureID = 1
            }
        };

        // Generate the barcode image using ComplexBarcodeGenerator
        using (var generator = new ComplexBarcodeGenerator(primaryCodetext))
        {
            // Render the barcode to a bitmap
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Save the bitmap as a BMP file
                bitmap.Save("hibc_primary.bmp", ImageFormat.Bmp);
            }
        }

        // Inform the user that the image has been saved
        Console.WriteLine("Barcode image saved as hibc_primary.bmp");
    }
}