using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Define output file path
        string outputPath = Path.Combine(Path.GetTempPath(), "qr_generated.png");

        // Ensure the directory exists
        string directory = Path.GetDirectoryName(outputPath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Prepare stopwatch for performance measurement
        Stopwatch stopwatch = new Stopwatch();

        // Generate QR Code
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Code Text"))
        {
            // Optional: set module size
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            // Optional: set error correction level
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Measure generation and saving time
            stopwatch.Start();
            generator.Save(outputPath, BarCodeImageFormat.Png);
            stopwatch.Stop();
        }

        // Log the generation time
        Console.WriteLine($"QR Code generated in {stopwatch.ElapsedMilliseconds} ms.");
        Console.WriteLine($"Saved to: {outputPath}");
    }
}