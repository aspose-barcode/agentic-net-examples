using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation of a combined HIBC LIC barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a combined HIBC LIC codetext,
    /// generates the barcode image, and saves it to a file.
    /// </summary>
    static void Main()
    {
        // Build the combined HIBC LIC codetext object
        var combinedCodetext = new HIBCLICCombinedCodetext
        {
            // Specify the barcode symbology (HIBC Code 39 LIC)
            BarcodeType = EncodeTypes.HIBCCode39LIC,

            // Populate primary data fields: product number, labeler ID, and unit of measure
            PrimaryData = new PrimaryData
            {
                ProductOrCatalogNumber = "12345",
                LabelerIdentificationCode = "A999",
                UnitOfMeasureID = 1 // Example unit of measure identifier
            },

            // Populate secondary and additional data fields (e.g., lot number)
            SecondaryAndAdditionalData = new SecondaryAndAdditionalData
            {
                LotNumber = "LOT123"
            }
        };

        // Create a barcode generator using the combined codetext
        using (var generator = new ComplexBarcodeGenerator(combinedCodetext))
        {
            // Save the generated barcode as a PNG file; the generator handles image creation
            generator.Save("hibc_combined_code39.png");
        }

        // Inform the user that the barcode image has been created
        Console.WriteLine("Barcode generated: hibc_combined_code39.png");
    }
}