using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        var imageFiles = new List<string>();

        GenerateBarcodeImage(EncodeTypes.Code128, "12345", "code128.png");
        imageFiles.Add("code128.png");

        GenerateBarcodeImage(EncodeTypes.QR, "ABCDEF", "qr.png");
        imageFiles.Add("qr.png");

        GenerateBarcodeImage(EncodeTypes.Code39, "CODE39", "code39.png");
        imageFiles.Add("code39.png");

        var presets = new Dictionary<string, QualitySettings>
        {
            { "NormalQuality", QualitySettings.NormalQuality },
            { "HighPerformance", QualitySettings.HighPerformance },
            { "HighQuality", QualitySettings.HighQuality },
            { "MaxQuality", QualitySettings.MaxQuality }
        };

        var decodeTypes = new BaseDecodeType[] { DecodeType.Code128, DecodeType.QR, DecodeType.Code39 };

        foreach (var preset in presets)
        {
            int totalConfidence = 0;
            int resultCount = 0;

            foreach (var file in imageFiles)
            {
                using (var reader = new BarCodeReader(file, decodeTypes))
                {
                    reader.QualitySettings = preset.Value;

                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        totalConfidence += (int)result.Confidence;
                        resultCount++;
                    }
                }
            }

            double average = resultCount > 0 ? (double)totalConfidence / resultCount : 0;
            Console.WriteLine($"{preset.Key}: Average Confidence = {average:F2}");
        }

        foreach (var file in imageFiles)
        {
            try { File.Delete(file); } catch { }
        }
    }

    private static void GenerateBarcodeImage(BaseEncodeType encodeType, string codeText, string fileName)
    {
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            generator.Save(fileName);
        }
    }
}