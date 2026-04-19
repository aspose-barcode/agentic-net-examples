using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a QR Code generator with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set the image resolution to 200 DPI
            generator.Parameters.Resolution = 200f;

            // Save the barcode as a JPEG image
            generator.Save("qr_code.jpeg");
        }
    }
}