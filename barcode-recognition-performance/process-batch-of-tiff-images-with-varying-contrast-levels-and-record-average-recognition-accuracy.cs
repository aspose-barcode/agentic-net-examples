using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        string inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "InputImages");
        string outputFile = Path.Combine(Directory.GetCurrentDirectory(), "RecognitionReport.csv");

        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
            CreateSampleTiff(Path.Combine(inputFolder, "sample1.tif"), EncodeTypes.Code128, "ABC123");
            CreateSampleTiff(Path.Combine(inputFolder, "sample2.tif"), EncodeTypes.QR, "https://example.com");
            CreateSampleTiff(Path.Combine(inputFolder, "sample3.tif"), EncodeTypes.EAN13, "1234567890128");
        }

        using (var writer = new StreamWriter(outputFile, false))
        {
            writer.WriteLine("FileName,BarcodesFound,AverageConfidence");
        }

        int totalBarcodes = 0;
        double totalConfidence = 0.0;

        string[] files = Directory.GetFiles(inputFolder, "*.tif");
        foreach (string filePath in files)
        {
            int imageBarcodes = 0;
            double imageConfidenceSum = 0.0;

            using (var reader = new BarCodeReader(filePath, DecodeType.Code128, DecodeType.QR, DecodeType.EAN13, DecodeType.Code39))
            {
                reader.QualitySettings = QualitySettings.HighQuality;

                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    imageBarcodes++;
                    imageConfidenceSum += ConfidenceToNumeric(result.Confidence);
                }
            }

            double avgConfidence = imageBarcodes > 0 ? imageConfidenceSum / imageBarcodes : 0.0;

            using (var writer = new StreamWriter(outputFile, true))
            {
                writer.WriteLine($"{Path.GetFileName(filePath)},{imageBarcodes},{avgConfidence:F2}");
            }

            totalBarcodes += imageBarcodes;
            totalConfidence += imageConfidenceSum;
        }

        double overallAvg = totalBarcodes > 0 ? totalConfidence / totalBarcodes : 0.0;
        Console.WriteLine($"Processed {totalBarcodes} barcodes across {Directory.GetFiles(inputFolder, "*.tif").Length} images.");
        Console.WriteLine($"Overall average confidence: {overallAvg:F2}");
        Console.WriteLine($"Report saved to: {outputFile}");
    }

    static void CreateSampleTiff(string filePath, BaseEncodeType type, string text)
    {
        using (var generator = new BarcodeGenerator(type, text))
        {
            generator.Save(filePath, BarCodeImageFormat.Tiff);
        }
    }

    static double ConfidenceToNumeric(BarCodeConfidence confidence)
    {
        switch (confidence)
        {
            case BarCodeConfidence.Strong:
                return 100.0;
            case BarCodeConfidence.Moderate:
                return 80.0;
            default:
                return 0.0;
        }
    }
}