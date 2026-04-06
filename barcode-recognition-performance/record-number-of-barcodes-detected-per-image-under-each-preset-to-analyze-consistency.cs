using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Prepare sample barcode images in memory
        var barcodeImages = new List<MemoryStream>();
        // Code128 barcode
        using (var ms = new MemoryStream())
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ABC123"))
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0;
                barcodeImages.Add(new MemoryStream(ms.ToArray()));
            }
        }
        // QR code barcode
        using (var ms = new MemoryStream())
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0;
                barcodeImages.Add(new MemoryStream(ms.ToArray()));
            }
        }

        // Define the quality presets to test
        var presets = new Dictionary<string, QualitySettings>
        {
            { "HighPerformance", QualitySettings.HighPerformance },
            { "NormalQuality", QualitySettings.NormalQuality },
            { "HighQuality", QualitySettings.HighQuality },
            { "MaxQuality", QualitySettings.MaxQuality }
        };

        // Record barcode counts per preset
        foreach (var preset in presets)
        {
            int totalCount = 0;
            foreach (var imageStream in barcodeImages)
            {
                // Ensure the stream is at the beginning for each read
                imageStream.Position = 0;
                using (var reader = new BarCodeReader(imageStream))
                {
                    // Apply the current quality preset
                    reader.QualitySettings = preset.Value;
                    // Perform recognition
                    reader.ReadBarCodes();
                    // Accumulate the number of barcodes found in this image
                    totalCount += reader.FoundCount;
                }
            }
            Console.WriteLine($"{preset.Key} preset detected {totalCount} barcode(s) across all images.");
        }

        // Cleanup memory streams
        foreach (var stream in barcodeImages)
        {
            stream.Dispose();
        }
    }
}