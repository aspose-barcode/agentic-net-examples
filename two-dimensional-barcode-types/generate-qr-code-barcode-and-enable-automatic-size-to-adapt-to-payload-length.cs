using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Sample payload; the barcode size will adapt to this text length
        string payload = "https://example.com/very/long/path?query=parameters";

        // Create a QR Code generator with the payload
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, payload))
        {
            // Enable automatic sizing mode
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Optional: set a reasonable resolution (dpi)
            generator.Parameters.Resolution = 300;

            // Save the generated QR Code image
            generator.Save("qr.png");
        }
    }
}