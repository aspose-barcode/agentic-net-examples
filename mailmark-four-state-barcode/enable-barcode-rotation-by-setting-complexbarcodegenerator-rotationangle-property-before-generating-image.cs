using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Prepare SwissQR codetext with required fields
        var swissQr = new SwissQRCodetext();

        // Creditor (mandatory fields)
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.AddressLine1 = "123 Main Street";
        swissQr.Bill.Creditor.PostalCode = "8000";
        swissQr.Bill.Creditor.Town = "Zurich";
        swissQr.Bill.Creditor.CountryCode = "CH";

        // Bill details
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Currency = "CHF";
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Create ComplexBarcodeGenerator and set rotation
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Rotation angle in degrees (e.g., 90 degrees)
            generator.Parameters.RotationAngle = 90f;

            // Save the rotated barcode image
            string outputFile = "SwissQR_Rotated.png";
            generator.Save(outputFile);
        }
    }
}