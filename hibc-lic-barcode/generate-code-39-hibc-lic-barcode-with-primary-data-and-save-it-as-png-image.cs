using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

class Program
{
    static void Main()
    {
        // Create primary data for HIBC LIC
        var primaryData = new PrimaryData
        {
            ProductOrCatalogNumber = "12345",
            LabelerIdentificationCode = "A999",
            UnitOfMeasureID = 1
        };

        // Prepare complex codetext for HIBC Code39 LIC with primary data only
        var complexCodetext = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCCode39LIC,
            Data = primaryData
        };

        // Generate and save the barcode as PNG
        using (var generator = new ComplexBarcodeGenerator(complexCodetext))
        {
            generator.Save("hibc_lic_code39.png");
        }

        Console.WriteLine("HIBC Code39 LIC barcode saved as hibc_lic_code39.png");
    }
}