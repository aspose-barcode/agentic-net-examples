using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

class Program
{
    static void Main()
    {
        ComplexBarcodeGenerator generator = null;
        BarCodeReader reader = null;
        var memoryStream = new MemoryStream();

        try
        {
            // Prepare SwissQR codetext with mandatory fields
            var swissQr = new SwissQRCodetext();
            swissQr.Bill.Creditor.Name = "John Doe";
            swissQr.Bill.Creditor.CountryCode = "CH";
            swissQr.Bill.Account = "CH9300762011623852957";
            swissQr.Bill.Amount = 199.95m;
            swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

            // Generate the complex barcode and save to a memory stream
            generator = new ComplexBarcodeGenerator(swissQr);
            generator.Save(memoryStream, BarCodeImageFormat.Png);

            // Reset stream position for reading
            memoryStream.Position = 0;

            // Read the barcode from the generated image
            reader = new BarCodeReader(memoryStream, DecodeType.QR);
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Decoded text: {result.CodeText}");
            }
        }
        finally
        {
            // Ensure resources are released
            if (reader != null) reader.Dispose();
            if (generator != null) generator.Dispose();
            memoryStream.Dispose();
        }
    }
}