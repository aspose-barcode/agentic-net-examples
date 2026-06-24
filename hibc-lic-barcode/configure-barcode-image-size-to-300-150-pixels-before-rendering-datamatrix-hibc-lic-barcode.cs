using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation of a HIBC DataMatrix LIC barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Prepares HIBC LIC data, generates a barcode,
    /// and saves it as a PNG image.
    /// </summary>
    static void Main()
    {
        // Prepare primary HIBC LIC data for a DataMatrix barcode
        var primaryCodetext = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCDataMatrixLIC,
            Data = new PrimaryData
            {
                ProductOrCatalogNumber = "12345",   // Product or catalog identifier
                LabelerIdentificationCode = "A999", // Labeler ID code
                UnitOfMeasureID = 1                // Unit of measure identifier
            }
        };

        // Generate the barcode with the required image dimensions (300 × 150 pixels)
        using (var generator = new ComplexBarcodeGenerator(primaryCodetext))
        {
            // Set image width to 300 points (pixels)
            generator.Parameters.ImageWidth.Point = 300f;

            // Set image height to 150 points (pixels)
            generator.Parameters.ImageHeight.Point = 150f;

            const string outputPath = "hibc_datamatrix.png";

            // Save the generated barcode image to the specified file
            generator.Save(outputPath);

            // Inform the user where the image was saved
            Console.WriteLine($"Barcode image saved to: {outputPath}");
        }
    }
}