using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        var mailmark = new MailmarkCodetext
        {
            Format = 4,
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(mailmark))
        {
            generator.Parameters.Barcode.XDimension.Millimeters = 0.5f;
            generator.Parameters.Barcode.BarHeight.Millimeters = 10f;
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Save("Mailmark4State.png");
        }
    }
}