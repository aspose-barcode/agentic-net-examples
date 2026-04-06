using System;
using System.Collections.Generic;
using System.Diagnostics;
using Aspose.Drawing;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Generate sample barcode images in memory
        var images = new List<Bitmap>();
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            images.Add(generator.GenerateBarCodeImage());
        }
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "HelloWorld"))
        {
            images.Add(generator.GenerateBarCodeImage());
        }

        // Define quality presets to test
        var presets = new Dictionary<string, QualitySettings>
        {
            { "HighPerformance", QualitySettings.HighPerformance },
            { "NormalQuality", QualitySettings.NormalQuality },
            { "HighQuality", QualitySettings.HighQuality },
            { "MaxQuality", QualitySettings.MaxQuality }
        };

        // Iterate over each preset and each image, measuring recognition time
        int imageIndex = 1;
        foreach (var preset in presets)
        {
            string presetName = preset.Key;
            QualitySettings settings = preset.Value;

            imageIndex = 1;
            foreach (var img in images)
            {
                using (var reader = new BarCodeReader())
                {
                    // Set decode types to cover generated barcodes
                    reader.SetBarCodeReadType(DecodeType.Code128, DecodeType.QR);
                    // Assign image and quality settings
                    reader.SetBarCodeImage(img);
                    reader.QualitySettings = settings;

                    // Measure recognition duration
                    var stopwatch = Stopwatch.StartNew();
                    BarCodeResult[] results = reader.ReadBarCodes();
                    stopwatch.Stop();

                    Console.WriteLine($"{presetName} - Image {imageIndex}: Recognized {results.Length} barcode(s) in {stopwatch.ElapsedMilliseconds} ms");
                }
                imageIndex++;
            }
        }

        // Dispose generated images
        foreach (var img in images)
        {
            img.Dispose();
        }
    }
}