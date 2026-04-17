using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Output file path
        string outputPath = "qr.png";

        // Create QR Code generator with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set high error correction level for robustness
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Ensure maximum contrast: black bars on white background
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Define image size (300x300 points)
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 300f;

            // Save the QR Code image
            generator.Save(outputPath);
        }

        Console.WriteLine($"QR Code saved to '{outputPath}'.");
    }
}