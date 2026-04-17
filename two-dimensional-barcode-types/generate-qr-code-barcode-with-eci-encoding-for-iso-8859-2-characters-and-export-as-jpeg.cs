using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        string codeText = "ĄĆĘŁŃÓŚŹŻ";

        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            generator.CodeText = codeText;
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.ISO_8859_2;

            string outputPath = "qr_iso8859_2.jpeg";
            generator.Save(outputPath);
        }

        Console.WriteLine("QR Code saved successfully.");
    }
}