using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

class Program
{
    static void Main()
    {
        // Prepare secondary and additional data for HIBC LIC QR barcode
        var complexCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCQRLIC,
            LinkCharacter = 'L',
            Data = new SecondaryAndAdditionalData
            {
                LotNumber = "LOT123",
                SerialNumber = "SERIAL123"
                // Additional fields (e.g., ExpiryDate, Quantity) can be set as needed
            }
        };

        // Generate barcode with custom margin (padding) of 5 pixels on all sides
        using (var generator = new ComplexBarcodeGenerator(complexCodetext))
        {
            generator.Parameters.Barcode.Padding.Left.Point = 5f;
            generator.Parameters.Barcode.Padding.Top.Point = 5f;
            generator.Parameters.Barcode.Padding.Right.Point = 5f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 5f;

            // Save the barcode image
            generator.Save("hibc_qr_lic.png");
        }

        Console.WriteLine("HIBC QR LIC barcode generated with 5‑pixel margin.");
    }
}