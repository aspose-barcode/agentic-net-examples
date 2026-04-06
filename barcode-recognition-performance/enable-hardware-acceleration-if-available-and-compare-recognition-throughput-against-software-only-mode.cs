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
        // Path for temporary barcode image
        string imagePath = Path.Combine(Path.GetTempPath(), "temp_barcode.png");

        // Generate a barcode image
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(imagePath);
        }

        // Ensure the image file exists
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Measure software‑only (single core) recognition
        BarCodeReader.ProcessorSettings.UseAllCores = false;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = 1;
        long softwareTicks = MeasureRecognition(imagePath);

        // Measure hardware‑accelerated (multi‑core) recognition
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        long hardwareTicks = MeasureRecognition(imagePath);

        // Output results
        Console.WriteLine($"Software‑only mode elapsed: {softwareTicks} ms");
        Console.WriteLine($"Hardware‑accelerated mode elapsed: {hardwareTicks} ms");

        // Clean up temporary file
        try { File.Delete(imagePath); } catch { }
    }

    // Runs recognition on the given image and returns elapsed milliseconds
    static long MeasureRecognition(string imagePath)
    {
        var stopwatch = Stopwatch.StartNew();

        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Use high‑performance quality preset for fair comparison
            reader.QualitySettings = QualitySettings.HighPerformance;

            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Access result to ensure processing occurs
                Console.WriteLine($"Detected: {result.CodeText}");
            }
        }

        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }
}