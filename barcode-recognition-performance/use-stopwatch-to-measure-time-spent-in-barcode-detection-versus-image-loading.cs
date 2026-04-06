using System;
using System.Diagnostics;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

namespace BarcodeTimingExample
{
    class Program
    {
        static void Main()
        {
            const string filePath = "barcode.png";

            // Generate a barcode image and save it to a file
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                generator.Save(filePath);
            }

            // Measure the time taken to load the image from disk
            Stopwatch loadTimer = new Stopwatch();
            loadTimer.Start();
            Bitmap bitmap = new Bitmap(filePath);
            loadTimer.Stop();

            // Measure the time taken to detect the barcode in the loaded image
            Stopwatch detectTimer = new Stopwatch();
            detectTimer.Start();
            using (BarCodeReader reader = new BarCodeReader(bitmap, DecodeType.Code128))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Detected Type: {result.CodeTypeName}, Text: {result.CodeText}");
                }
            }
            detectTimer.Stop();

            // Clean up the bitmap resource
            bitmap.Dispose();

            Console.WriteLine($"Image loading time: {loadTimer.ElapsedMilliseconds} ms");
            Console.WriteLine($"Barcode detection time: {detectTimer.ElapsedMilliseconds} ms");
        }
    }
}