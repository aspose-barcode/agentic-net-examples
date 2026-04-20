using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Prepare Swiss QR bill data
        var swissCodetext = new SwissQRCodetext();
        swissCodetext.Bill.Account = "CH9300762011623852957";
        swissCodetext.Bill.Amount = 199.95m;
        swissCodetext.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
        swissCodetext.Bill.Creditor.CountryCode = "CH";
        swissCodetext.Bill.Creditor.Name = "Creditor Ltd.";
        swissCodetext.Bill.Creditor.Street = "Main Street 1";
        swissCodetext.Bill.Creditor.PostalCode = "8000";
        swissCodetext.Bill.Creditor.Town = "Zurich";

        // Create generator with the complex codetext
        using (var generator = new ComplexBarcodeGenerator(swissCodetext))
        {
            // Set QR error correction level to high
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Generate barcode image into a memory stream (no file I/O)
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                // Reset stream position for potential further use
                ms.Position = 0;
            }

            // Retrieve the constructed codetext from the generator
            string constructed = swissCodetext.GetConstructedCodetext();

            // Decode the codetext back to a SwissQRCodetext instance
            SwissQRCodetext decoded = ComplexCodetextReader.TryDecodeSwissQR(constructed);
            if (decoded == null)
                throw new Exception("Failed to decode Swiss QR codetext.");

            // Verify that key fields match the original data
            if (decoded.Bill.Account != swissCodetext.Bill.Account)
                throw new Exception("Account mismatch.");
            if (decoded.Bill.Amount != swissCodetext.Bill.Amount)
                throw new Exception("Amount mismatch.");
            if (decoded.Bill.Version != swissCodetext.Bill.Version)
                throw new Exception("Version mismatch.");
            if (decoded.Bill.Creditor.CountryCode != swissCodetext.Bill.Creditor.CountryCode)
                throw new Exception("Creditor country code mismatch.");
            if (decoded.Bill.Creditor.Name != swissCodetext.Bill.Creditor.Name)
                throw new Exception("Creditor name mismatch.");

            // Simple success message
            Console.WriteLine("All Swiss QR bill fields verified successfully.");
        }
    }
}