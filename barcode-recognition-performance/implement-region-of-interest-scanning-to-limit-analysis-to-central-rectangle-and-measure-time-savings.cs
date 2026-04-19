using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        const string imagePath = "barcode.png";

        // Ensure a sample barcode image exists
        if (!File.Exists(imagePath))
        {
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                generator.Save(imagePath);
            }
        }

        // Load the image
        using (Bitmap bitmap = new Bitmap(imagePath))
        {
            // -------- Full image scan ----------
            Stopwatch fullWatch = Stopwatch.StartNew();
            using (BarCodeReader fullReader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
            {
                int fullCount = 0;
                foreach (var result in fullReader.ReadBarCodes())
                {
                    fullCount++;
                }
                Console.WriteLine($"Full scan detected {fullCount} barcode(s).");
            }
            fullWatch.Stop();

            // -------- Region‑of‑interest scan ----------
            int roiWidth = bitmap.Width / 2;
            int roiHeight = bitmap.Height / 2;
            int roiX = (bitmap.Width - roiWidth) / 2;
            int roiY = (bitmap.Height - roiHeight) / 2;
            Rectangle roi = new Rectangle(roiX, roiY, roiWidth, roiHeight);

            Stopwatch roiWatch = Stopwatch.StartNew();
            using (BarCodeReader roiReader = new BarCodeReader(bitmap, roi, DecodeType.AllSupportedTypes))
            {
                int roiCount = 0;
                foreach (var result in roiReader.ReadBarCodes())
                {
                    roiCount++;
                    // Example of accessing the detected region
                    var bounds = result.Region.Rectangle;
                    Console.WriteLine($"ROI barcode at {bounds.X},{bounds.Y},{bounds.Width},{bounds.Height}");
                }
                Console.WriteLine($"ROI scan detected {roiCount} barcode(s).");
            }
            roiWatch.Stop();

            // -------- Report timings ----------
            Console.WriteLine($"Full scan time: {fullWatch.ElapsedMilliseconds} ms");
            Console.WriteLine($"ROI scan time: {roiWatch.ElapsedMilliseconds} ms");
            Console.WriteLine($"Time saved: {fullWatch.ElapsedMilliseconds - roiWatch.ElapsedMilliseconds} ms");
        }
    }
}