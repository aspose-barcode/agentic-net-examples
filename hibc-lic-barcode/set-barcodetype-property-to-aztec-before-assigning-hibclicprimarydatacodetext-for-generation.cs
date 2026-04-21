using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

class Program
{
    static void Main()
    {
        // Create the primary data codetext object
        var primaryCodetext = new HIBCLICPrimaryDataCodetext();

        // Set the barcode type to HIBC Aztec LIC before assigning data
        primaryCodetext.BarcodeType = EncodeTypes.HIBCAztecLIC;

        // Assign primary data fields
        primaryCodetext.Data = new PrimaryData
        {
            ProductOrCatalogNumber = "12345",
            LabelerIdentificationCode = "A999",
            UnitOfMeasureID = 1
        };

        // Generate the barcode image and save it
        using (var generator = new ComplexBarcodeGenerator(primaryCodetext))
        {
            generator.Save("HIBC_Aztec_LIC.png");
        }
    }
}