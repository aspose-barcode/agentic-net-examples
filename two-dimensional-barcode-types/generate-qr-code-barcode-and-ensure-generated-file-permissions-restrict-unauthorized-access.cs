using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main(string[] args)
    {
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);
        string filePath = Path.Combine(outputDir, "qr_code.png");

        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            generator.Save(filePath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"QR Code saved to: {filePath}");
        // Note: Restricting file permissions (ACLs) requires OS-specific calls and elevated privileges,
        // which are not demonstrated here because they are not portable in the CI environment.
    }
}