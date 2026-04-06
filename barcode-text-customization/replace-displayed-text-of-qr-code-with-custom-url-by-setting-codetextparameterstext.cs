using System;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a QR code generator
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Set the actual data to be encoded
            generator.CodeText = "https://example.com";

            // Set custom visible text (displayed below the QR code)
            generator.Parameters.Barcode.CodeTextParameters.TwoDDisplayText = "Visit Example";

            // Save the QR code image
            generator.Save("qr.png");
        }
    }
}