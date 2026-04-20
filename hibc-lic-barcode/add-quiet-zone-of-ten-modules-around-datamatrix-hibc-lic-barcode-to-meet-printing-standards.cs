using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

class Program
{
    static void Main()
    {
        // Prepare secondary data for HIBC LIC barcode
        var complexCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCDataMatrixLIC,
            LinkCharacter = 'L',
            Data = new SecondaryAndAdditionalData
            {
                LotNumber = "LOT123",
                SerialNumber = "SERIAL123"
            }
        };

        // Generate the barcode
        using (var generator = new ComplexBarcodeGenerator(complexCodetext))
        {
            // Define module size (x-dimension) in points
            generator.Parameters.Barcode.XDimension.Point = 2f; // 2 points per module

            // Add a quiet zone of ten modules on each side
            float quietModules = 10f;
            float quietZone = quietModules * generator.Parameters.Barcode.XDimension.Point;
            generator.Parameters.Barcode.Padding.Left.Point = quietZone;
            generator.Parameters.Barcode.Padding.Right.Point = quietZone;
            generator.Parameters.Barcode.Padding.Top.Point = quietZone;
            generator.Parameters.Barcode.Padding.Bottom.Point = quietZone;

            // Save the barcode image
            generator.Save("HIBC_DataMatrix_QuietZone.png");
        }
    }
}