using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Create primary data codetext and set required fields
        var primaryCodetext = new HIBCLICPrimaryDataCodetext();
        primaryCodetext.BarcodeType = EncodeTypes.HIBCQRLIC; // QR version of HIBC LIC
        primaryCodetext.Data = new PrimaryData
        {
            ProductOrCatalogNumber = "12345",
            LabelerIdentificationCode = "A999", // labeler ID
            UnitOfMeasureID = 1
        };

        // Generate barcode image using ComplexBarcodeGenerator
        using (var generator = new ComplexBarcodeGenerator(primaryCodetext))
        {
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Save as BMP
                bitmap.Save("hibc_primary.bmp", ImageFormat.Bmp);
            }
        }

        Console.WriteLine("Barcode image saved as hibc_primary.bmp");
    }
}