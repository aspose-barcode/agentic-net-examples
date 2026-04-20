using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        var swissQRCodetext = new SwissQRCodetext();

        // Required creditor information
        swissQRCodetext.Bill.Creditor.Name = "John Doe";
        swissQRCodetext.Bill.Creditor.Street = "Main Street 1";
        swissQRCodetext.Bill.Creditor.PostalCode = "8000";
        swissQRCodetext.Bill.Creditor.Town = "Zurich";
        swissQRCodetext.Bill.Creditor.CountryCode = "CH";

        // Required account information
        swissQRCodetext.Bill.Account = "CH9300762011623852957";

        // Optional fields
        swissQRCodetext.Bill.Amount = 199.95m;
        swissQRCodetext.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(swissQRCodetext))
        {
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;
            generator.Parameters.BackColor = Aspose.Drawing.Color.Yellow;

            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "SwissQR.png");
            generator.Save(outputPath);
        }

        Console.WriteLine("Barcode image generated successfully.");
    }
}