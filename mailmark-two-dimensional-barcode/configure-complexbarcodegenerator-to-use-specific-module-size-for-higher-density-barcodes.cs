using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;

class Program
{
    static void Main()
    {
        // Prepare SwissQR codetext with mandatory fields
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Create ComplexBarcodeGenerator and configure module size for higher density
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Reduce XDimension (module size) to increase barcode density
            generator.Parameters.Barcode.XDimension.Point = 0.5f; // 0.5 points per module

            // Optional: set image resolution
            generator.Parameters.Resolution = 300;

            // Save the generated barcode image
            string outputFile = "SwissQR_HighDensity.png";
            generator.Save(outputFile);
            Console.WriteLine($"Barcode saved to {Path.GetFullPath(outputFile)}");
        }
    }
}