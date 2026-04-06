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
        // Create a temporary file for the barcode image
        string tempFile = Path.Combine(Path.GetTempPath(), "sample_barcode.png");

        // Generate a Code128 barcode and save it to the temporary file
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(tempFile, BarCodeImageFormat.Png);
        }

        // Full image scanning
        var fullStopwatch = Stopwatch.StartNew();
        using (var fullReader = new BarCodeReader(tempFile, DecodeType.Code128))
        {
            foreach (BarCodeResult result in fullReader.ReadBarCodes())
            {
                Console.WriteLine($"[Full] Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }
        }
        fullStopwatch.Stop();
        Console.WriteLine($"Full image scan time: {fullStopwatch.ElapsedMilliseconds} ms");

        // Region‑of‑interest scanning (central rectangle)
        using (var bitmap = new Bitmap(tempFile))
        {
            // Define a central rectangle (half width and height)
            int roiX = bitmap.Width / 4;
            int roiY = bitmap.Height / 4;
            int roiWidth = bitmap.Width / 2;
            int roiHeight = bitmap.Height / 2;
            var roi = new Rectangle(roiX, roiY, roiWidth, roiHeight);

            var roiStopwatch = Stopwatch.StartNew();
            using (var roiReader = new BarCodeReader())
            {
                // Detect only Code128 barcodes
                roiReader.BarCodeReadType = new MultiDecodeType(DecodeType.Code128);
                // Set image and region for recognition
                roiReader.SetBarCodeImage(bitmap, roi);
                foreach (BarCodeResult result in roiReader.ReadBarCodes())
                {
                    Console.WriteLine($"[ROI] Type: {result.CodeTypeName}, Text: {result.CodeText}");
                }
            }
            roiStopwatch.Stop();
            Console.WriteLine($"ROI scan time: {roiStopwatch.ElapsedMilliseconds} ms");
        }

        // Clean up temporary file
        try
        {
            File.Delete(tempFile);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}