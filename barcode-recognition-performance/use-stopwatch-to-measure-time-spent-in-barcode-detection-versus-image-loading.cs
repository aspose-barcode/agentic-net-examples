using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string imagePath = "sample.png";

        // Ensure a barcode image exists; generate one if missing.
        if (!File.Exists(imagePath))
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Simple settings for the generated barcode.
                generator.Parameters.Barcode.BarHeight.Point = 50f;
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 150f;
                generator.Save(imagePath);
            }
        }

        // Measure image loading time.
        Bitmap bitmap;
        var loadTimer = Stopwatch.StartNew();
        using (var bmp = new Bitmap(imagePath))
        {
            // Clone the bitmap to keep it after disposing the using block.
            bitmap = new Bitmap(bmp);
        }
        loadTimer.Stop();

        // Measure barcode detection time.
        var detectTimer = Stopwatch.StartNew();
        using (var reader = new BarCodeReader(bitmap, DecodeType.Code128))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }
        }
        detectTimer.Stop();

        // Output timing results.
        Console.WriteLine($"Image loading time: {loadTimer.ElapsedMilliseconds} ms");
        Console.WriteLine($"Barcode detection time: {detectTimer.ElapsedMilliseconds} ms");
    }
}