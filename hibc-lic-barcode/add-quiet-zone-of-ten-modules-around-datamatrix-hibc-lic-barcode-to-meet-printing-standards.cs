using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation of a HIBC DataMatrix LIC barcode with custom module size,
/// quiet zone, and resolution using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates and saves a HIBC DataMatrix LIC barcode image.
    /// </summary>
    static void Main()
    {
        // Prepare primary HIBC LIC data (required fields)
        var primaryCodetext = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCDataMatrixLIC,
            Data = new PrimaryData
            {
                ProductOrCatalogNumber = "12345",   // Product or catalog identifier
                LabelerIdentificationCode = "A999", // Labeler ID
                UnitOfMeasureID = 1                 // Unit of measure identifier
            }
        };

        // Define module size (XDimension) in points (1 point = 1/72 inch)
        float moduleSize = 2f; // 2 points per module

        // Construct output file path in the current working directory
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "hibc_datamatrix.png");

        // Generate the barcode with a quiet zone of ten modules on each side
        using (var generator = new ComplexBarcodeGenerator(primaryCodetext))
        {
            // Set the size of a single module (XDimension)
            generator.Parameters.Barcode.XDimension.Point = moduleSize;

            // Calculate quiet zone size (10 modules * module size)
            float quietZone = 10f * moduleSize;

            // Apply quiet zone padding to all sides
            generator.Parameters.Barcode.Padding.Left.Point   = quietZone;
            generator.Parameters.Barcode.Padding.Top.Point    = quietZone;
            generator.Parameters.Barcode.Padding.Right.Point  = quietZone;
            generator.Parameters.Barcode.Padding.Bottom.Point = quietZone;

            // Optional: set image resolution for higher quality output
            generator.Parameters.Resolution = 300f;

            // Save the generated barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}