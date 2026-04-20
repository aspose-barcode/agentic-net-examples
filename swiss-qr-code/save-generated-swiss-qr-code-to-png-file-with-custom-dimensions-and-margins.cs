using System;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;

class Program
{
    static void Main()
    {
        // Create Swiss QR codetext and populate required fields
        var swissQRCodetext = new SwissQRCodetext();
        swissQRCodetext.Bill.Account = "CH9300762011623852957";
        swissQRCodetext.Bill.Amount = 199.95m;
        swissQRCodetext.Bill.Creditor.Name = "John Doe";
        swissQRCodetext.Bill.Creditor.CountryCode = "CH";
        swissQRCodetext.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Generate the Swiss QR barcode with custom size and margins
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(swissQRCodetext))
        {
            // Custom image dimensions (points)
            generator.Parameters.ImageWidth.Point = 400f;
            generator.Parameters.ImageHeight.Point = 400f;

            // Custom margins (points)
            generator.Parameters.Barcode.Padding.Left.Point = 20f;
            generator.Parameters.Barcode.Padding.Top.Point = 20f;
            generator.Parameters.Barcode.Padding.Right.Point = 20f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 20f;

            // Save the barcode as PNG
            generator.Save("SwissQR.png");
        }
    }
}