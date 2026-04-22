using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        var mailmark = new MailmarkCodetext
        {
            Format = 4,
            VersionID = 1,
            Class = "1",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(mailmark))
        {
            generator.Parameters.Barcode.BarHeight.Millimeters = 10;
            generator.Save("Mailmark.png");
        }
    }
}