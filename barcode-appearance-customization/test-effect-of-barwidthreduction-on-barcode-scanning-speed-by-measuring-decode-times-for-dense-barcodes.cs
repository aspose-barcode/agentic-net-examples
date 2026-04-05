using System;
using System.Diagnostics;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Prepare a dense code text
        string denseText = new string('A', 120); // 120 characters

        // Paths for generated images
        string defaultPath = "barcode_default.png";
        string reducedPath = "barcode_reduced.png";

        // Generate barcode with default BarWidthReduction (0)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, denseText))
        {
            // Ensure default value (optional)
            generator.Parameters.Barcode.BarWidthReduction.Point = 0f;
            generator.Save(defaultPath);
        }

        // Generate barcode with BarWidthReduction applied
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, denseText))
        {
            // Apply a small reduction to compensate ink spread
            generator.Parameters.Barcode.BarWidthReduction.Point = 0.5f;
            generator.Save(reducedPath);
        }

        // Measure decode time for default barcode
        long defaultTime = MeasureDecodeTime(defaultPath);
        // Measure decode time for reduced barcode
        long reducedTime = MeasureDecodeTime(reducedPath);

        Console.WriteLine($"Decode time (default BarWidthReduction): {defaultTime} ms");
        Console.WriteLine($"Decode time (BarWidthReduction = 0.5): {reducedTime} ms");
    }

    static long MeasureDecodeTime(string imagePath)
    {
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Use high performance mode for speed measurement
            reader.QualitySettings = QualitySettings.HighPerformance;

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var results = reader.ReadBarCodes();

            stopwatch.Stop();

            // Output result count for verification (optional)
            Console.WriteLine($"Decoded {results.Length} barcode(s) from {imagePath}");

            return stopwatch.ElapsedMilliseconds;
        }
    }
}