using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Plain text message to encode in the QR Code
        string message = "Hello, this is a quick note!";

        // Output file path
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr_note.png");

        // Ensure the directory exists
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Generate QR Code barcode
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, message))
        {
            // Optional: set error correction level to high
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Optional: set resolution (dpi)
            generator.Parameters.Resolution = 300f;

            // Optional: set colors
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save as PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"QR code generated and saved to: {outputPath}");
    }
}