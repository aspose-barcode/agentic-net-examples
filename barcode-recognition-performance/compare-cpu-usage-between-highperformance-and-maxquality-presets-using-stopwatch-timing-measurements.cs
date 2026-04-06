using System;
using System.Diagnostics;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        const string imagePath = "barcode.png";

        // Generate a sample barcode image if it does not exist
        if (!System.IO.File.Exists(imagePath))
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Save the generated barcode to a file
                generator.Save(imagePath);
            }
        }

        // Measure recognition time with HighPerformance preset
        using (var readerHigh = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            readerHigh.QualitySettings = QualitySettings.HighPerformance;
            var sw = Stopwatch.StartNew();
            var results = readerHigh.ReadBarCodes();
            sw.Stop();
            Console.WriteLine($"HighPerformance: {sw.ElapsedMilliseconds} ms, barcodes found: {results?.Length ?? 0}");
        }

        // Measure recognition time with MaxQuality preset
        using (var readerMax = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            readerMax.QualitySettings = QualitySettings.MaxQuality;
            var sw = Stopwatch.StartNew();
            var results = readerMax.ReadBarCodes();
            sw.Stop();
            Console.WriteLine($"MaxQuality: {sw.ElapsedMilliseconds} ms, barcodes found: {results?.Length ?? 0}");
        }
    }
}