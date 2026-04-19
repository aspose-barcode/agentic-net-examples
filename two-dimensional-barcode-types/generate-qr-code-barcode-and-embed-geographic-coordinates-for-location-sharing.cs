using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main(string[] args)
    {
        // Sample geographic coordinates (latitude, longitude)
        double latitude = 37.7749;
        double longitude = -122.4194;

        // Encode coordinates in a format suitable for location sharing
        string codeText = $"geo:{latitude},{longitude}";

        // Create QR code generator with the encoded text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Set high error correction level for better readability
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Define image dimensions (points)
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 300f;

            // Optional: set image resolution (dpi)
            generator.Parameters.Resolution = 300;

            // Save the QR code image to a file
            generator.Save("qr_location.png");
        }
    }
}