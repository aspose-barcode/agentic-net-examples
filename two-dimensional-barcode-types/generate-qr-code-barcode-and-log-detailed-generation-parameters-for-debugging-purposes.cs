using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Output file path
        string outputPath = "qr_code.png";

        // Create a QR code generator with sample text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Configure QR error correction level
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Set module size (X dimension) to 3 points
            generator.Parameters.Barcode.XDimension.Point = 3f;

            // Define image dimensions
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 300f;

            // Set image resolution (dpi)
            generator.Parameters.Resolution = 300f;

            // Set foreground and background colors
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.DarkBlue;
            generator.Parameters.BackColor = Aspose.Drawing.Color.LightYellow;

            // Log detailed generation parameters
            Console.WriteLine("QR Code Generation Parameters:");
            Console.WriteLine($"CodeText: {generator.CodeText}");
            Console.WriteLine($"ErrorLevel: {generator.Parameters.Barcode.QR.ErrorLevel}");
            Console.WriteLine($"XDimension (points): {generator.Parameters.Barcode.XDimension.Point}");
            Console.WriteLine($"ImageWidth (points): {generator.Parameters.ImageWidth.Point}");
            Console.WriteLine($"ImageHeight (points): {generator.Parameters.ImageHeight.Point}");
            Console.WriteLine($"Resolution (dpi): {generator.Parameters.Resolution}");
            Console.WriteLine($"BarColor: {generator.Parameters.Barcode.BarColor}");
            Console.WriteLine($"BackColor: {generator.Parameters.BackColor}");

            // Save the QR code image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"QR code saved to {outputPath}");
    }
}