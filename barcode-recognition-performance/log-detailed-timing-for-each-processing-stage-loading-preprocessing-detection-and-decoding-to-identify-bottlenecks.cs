using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Prepare a temporary barcode image
        string imagePath = Path.Combine(Path.GetTempPath(), "sample_barcode.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345ABC"))
        {
            generator.Save(imagePath);
        }

        var totalStopwatch = Stopwatch.StartNew();

        // Stage 1: Loading the image
        var loadStopwatch = Stopwatch.StartNew();
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            loadStopwatch.Stop();
            Console.WriteLine($"Loading time: {loadStopwatch.ElapsedMilliseconds} ms");

            // Stage 2: Preprocessing (e.g., setting quality settings)
            var preprocessStopwatch = Stopwatch.StartNew();
            reader.QualitySettings = QualitySettings.HighPerformance;
            preprocessStopwatch.Stop();
            Console.WriteLine($"Preprocessing time: {preprocessStopwatch.ElapsedMilliseconds} ms");

            // Stage 3: Detection (reading barcodes)
            var detectionStopwatch = Stopwatch.StartNew();
            BarCodeResult[] results = reader.ReadBarCodes();
            detectionStopwatch.Stop();
            Console.WriteLine($"Detection time: {detectionStopwatch.ElapsedMilliseconds} ms");

            // Stage 4: Decoding (extracting information)
            var decodeStopwatch = Stopwatch.StartNew();
            foreach (var result in results)
            {
                Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }
            decodeStopwatch.Stop();
            Console.WriteLine($"Decoding time: {decodeStopwatch.ElapsedMilliseconds} ms");
        }

        totalStopwatch.Stop();
        Console.WriteLine($"Total processing time: {totalStopwatch.ElapsedMilliseconds} ms");

        // Clean up temporary file
        if (File.Exists(imagePath))
        {
            File.Delete(imagePath);
        }
    }
}