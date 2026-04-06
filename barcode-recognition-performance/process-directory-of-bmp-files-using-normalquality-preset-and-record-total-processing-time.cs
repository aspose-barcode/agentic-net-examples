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
        string inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "InputBmp");

        // Ensure the input folder exists
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
        }

        // If there are no BMP files, create a sample BMP barcode
        string[] bmpFiles = Directory.GetFiles(inputFolder, "*.bmp");
        if (bmpFiles.Length == 0)
        {
            string samplePath = Path.Combine(inputFolder, "sample.bmp");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                generator.CodeText = "Sample123";
                generator.Save(samplePath, BarCodeImageFormat.Bmp);
            }
            bmpFiles = new[] { samplePath };
        }

        var stopwatch = new Stopwatch();
        stopwatch.Start();

        foreach (string filePath in bmpFiles)
        {
            using (var reader = new BarCodeReader(filePath))
            {
                // Use NormalQuality preset
                reader.QualitySettings = QualitySettings.NormalQuality;

                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {Path.GetFileName(filePath)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                }
            }
        }

        stopwatch.Stop();
        Console.WriteLine($"Total processing time: {stopwatch.Elapsed.TotalSeconds:F2} seconds");
    }
}