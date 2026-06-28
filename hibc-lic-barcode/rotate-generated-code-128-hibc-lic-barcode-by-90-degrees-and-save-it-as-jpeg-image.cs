using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates creation of a HIBC Code128 LIC barcode, rotation, and saving as JPEG.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Prepares primary data, generates the barcode, rotates it, and saves the image.
    /// </summary>
    static void Main()
    {
        // Prepare primary data for HIBC Code128 LIC barcode
        var primaryCodetext = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCCode128LIC,
            Data = new PrimaryData
            {
                ProductOrCatalogNumber = "12345",      // Product or catalog number
                LabelerIdentificationCode = "A999",   // Labeler identification code
                UnitOfMeasureID = 1                    // Unit of measure identifier
            }
        };

        // Create a ComplexBarcodeGenerator with the prepared data
        using (var generator = new ComplexBarcodeGenerator(primaryCodetext))
        {
            // Set rotation angle to 90 degrees (clockwise)
            generator.Parameters.RotationAngle = 90f;

            // Save the generated barcode as a JPEG file
            generator.Save("hibc_code128_lic.jpg");
        }

        // Inform the user that the barcode image has been saved
        Console.WriteLine("Barcode image saved as hibc_code128_lic.jpg");
    }
}