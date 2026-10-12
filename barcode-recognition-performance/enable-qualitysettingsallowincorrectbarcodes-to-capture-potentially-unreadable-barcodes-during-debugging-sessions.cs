using System;
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
        // Create a unique temporary folder
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        string originalPath = Path.Combine(tempDir, "original.png");
        string corruptedPath = Path.Combine(tempDir, "corrupted.png");

        // Generate a simple Code128 barcode
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            generator.Save(originalPath, BarCodeImageFormat.Png);
        }

        // Corrupt the barcode image by drawing a diagonal line
        using (var bitmap = new Bitmap(originalPath))
        {
            using (var graphics = Graphics.FromImage(bitmap))
            {
                using (var pen = new Pen(Color.Black, 5f))
                {
                    graphics.DrawLine(pen, 0, 0, bitmap.Width, bitmap.Height);
                }
            }
            bitmap.Save(corruptedPath, ImageFormat.Png);
        }

        // Verify that the corrupted image exists
        if (!File.Exists(corruptedPath))
        {
            Console.WriteLine("Corrupted image not found. Exiting.");
            return;
        }

        // Read with AllowIncorrectBarcodes = false
        using (var reader = new BarCodeReader(corruptedPath, DecodeType.Code128))
        {
            reader.QualitySettings = QualitySettings.MaxQuality;
            reader.QualitySettings.AllowIncorrectBarcodes = false;
            var results = reader.ReadBarCodes();
            Console.WriteLine($"AllowIncorrectBarcodes false: {results.Length} barcode(s) read");
            foreach (var result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText} (Confidence: {result.Confidence})");
            }
        }

        // Read with AllowIncorrectBarcodes = true
        using (var reader = new BarCodeReader(corruptedPath, DecodeType.Code128))
        {
            reader.QualitySettings = QualitySettings.MaxQuality;
            reader.QualitySettings.AllowIncorrectBarcodes = true;
            var results = reader.ReadBarCodes();
            Console.WriteLine($"AllowIncorrectBarcodes true: {results.Length} barcode(s) read");
            foreach (var result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText} (Confidence: {result.Confidence})");
            }
        }

        // Cleanup temporary files (optional)
        try
        {
            File.Delete(originalPath);
            File.Delete(corruptedPath);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program outcome
        }
    }
}