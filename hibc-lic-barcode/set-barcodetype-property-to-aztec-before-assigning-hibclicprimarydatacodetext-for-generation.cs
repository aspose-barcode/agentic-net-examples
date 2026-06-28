using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation of a HIBCLIC Aztec barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates primary data for a HIBCLIC barcode,
    /// generates the barcode image, and saves it to disk.
    /// </summary>
    static void Main()
    {
        // Initialize primary data codetext with barcode type set to HIBCAztecLIC
        var primaryCodetext = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCAztecLIC,
            Data = new PrimaryData
            {
                // Set product or catalog number
                ProductOrCatalogNumber = "12345",
                // Set labeler identification code
                LabelerIdentificationCode = "A999",
                // Set unit of measure identifier
                UnitOfMeasureID = 1
            }
        };

        // Create a ComplexBarcodeGenerator using the primary codetext
        // and save the generated barcode image to a PNG file
        using (var generator = new ComplexBarcodeGenerator(primaryCodetext))
        {
            generator.Save("hibc_aztec.png");
        }

        // Inform the user that the barcode has been generated
        Console.WriteLine("Barcode generated: hibc_aztec.png");
    }
}