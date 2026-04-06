using System;
using System.Diagnostics;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Prepare test data
        const string codeText = "1234567890";
        const string file1D = "code128.png";
        const string file2D = "qr.png";

        // Generate 1D barcode (Code128)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Save(file1D);
        }

        // Generate 2D barcode (QR)
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            generator.Save(file2D);
        }

        // Define identical quality settings for both recognitions
        var quality = QualitySettings.HighPerformance;

        // Measure recognition time for 1D barcode
        long time1D;
        using (var reader = new BarCodeReader(file1D, DecodeType.Code128))
        {
            reader.QualitySettings = quality;
            var sw = Stopwatch.StartNew();
            var results = reader.ReadBarCodes();
            sw.Stop();
            time1D = sw.ElapsedMilliseconds;

            foreach (var result in results)
            {
                Console.WriteLine($"1D Detected: {result.CodeTypeName}, Text: {result.CodeText}");
            }
        }

        // Measure recognition time for 2D barcode
        long time2D;
        using (var reader = new BarCodeReader(file2D, DecodeType.QR))
        {
            reader.QualitySettings = quality;
            var sw = Stopwatch.StartNew();
            var results = reader.ReadBarCodes();
            sw.Stop();
            time2D = sw.ElapsedMilliseconds;

            foreach (var result in results)
            {
                Console.WriteLine($"2D Detected: {result.CodeTypeName}, Text: {result.CodeText}");
            }
        }

        // Output comparison
        Console.WriteLine($"Recognition time (1D): {time1D} ms");
        Console.WriteLine($"Recognition time (2D): {time2D} ms");
        Console.WriteLine(time1D < time2D
            ? "1D barcode recognized faster."
            : time1D > time2D
                ? "2D barcode recognized faster."
                : "Both barcodes recognized in the same time.");
    }
}