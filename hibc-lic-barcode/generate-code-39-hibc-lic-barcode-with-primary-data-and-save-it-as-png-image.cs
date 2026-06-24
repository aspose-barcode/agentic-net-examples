using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation of a HIBC LIC Code39 barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates primary data for a HIBC LIC Code39 barcode,
    /// generates the barcode image, saves it to a file, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define the primary data required for the HIBC LIC Code39 barcode.
        var primaryCodetext = new HIBCLICPrimaryDataCodetext
        {
            // Specify the barcode type.
            BarcodeType = EncodeTypes.HIBCCode39LIC,
            // Populate the primary data fields.
            Data = new PrimaryData
            {
                ProductOrCatalogNumber = "12345",   // Product or catalog identifier.
                LabelerIdentificationCode = "A999", // Labeler identification code.
                UnitOfMeasureID = 1                 // Unit of measure identifier.
            }
        };

        // Destination file path for the generated barcode image.
        string outputPath = "hibc_code39_primary.png";

        // Create a generator for the complex barcode using the defined primary data.
        using (var generator = new ComplexBarcodeGenerator(primaryCodetext))
        {
            // Generate the barcode image as a bitmap.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Save the bitmap to the specified file in PNG format.
                bitmap.Save(outputPath, ImageFormat.Png);
            }
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}