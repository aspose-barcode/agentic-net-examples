using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Create a QR Code generator with the desired symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Set the data to encode
            generator.CodeText = "Hello World";

            // Set manual module size (XDimension) to 4 pixels
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Save the barcode as a PNG image
            generator.Save("qr.png");
        }
    }
}