using System;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        const string outputFile = "qr.png";
        const string codeText = "https://example.com";

        var timer = new Stopwatch();

        timer.Start();
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Set high error correction level for QR code
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the generated QR code image
            generator.Save(outputFile);
        }
        timer.Stop();

        Console.WriteLine($"QR code generated and saved to '{outputFile}' in {timer.ElapsedMilliseconds} ms.");
    }
}