using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

class Program
{
    static void Main()
    {
        // Sample QR code text
        const string qrText = "https://example.com";

        // Output file path
        string outputPath = "qr_300dpi.png";

        // Create and configure the QR code generator
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, qrText))
        {
            // Set high resolution (300 DPI) for high‑resolution print
            generator.Parameters.Resolution = 300f; // float literal

            // Optional: set high error correction level
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"QR code saved to: {outputPath}");
    }
}