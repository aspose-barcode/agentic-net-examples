using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Define the output file path
        string outputPath = Path.Combine(Environment.CurrentDirectory, "qr_code.png");

        // Ensure any existing file is removed to allow overwrite
        if (File.Exists(outputPath))
        {
            File.Delete(outputPath);
        }

        // Create a QR Code generator with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Optional: set high error correction level
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the barcode image directly to the file system
            generator.Save(outputPath);
        }

        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}