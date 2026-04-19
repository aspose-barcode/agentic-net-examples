using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.Generation;

class Program
{
    static void Main(string[] args)
    {
        // Define input folder for BMP files
        string inputFolder = "InputBmp";

        // Ensure the folder exists
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
        }

        // Seed a sample BMP file if the folder is empty
        string[] bmpFiles = Directory.GetFiles(inputFolder, "*.bmp");
        if (bmpFiles.Length == 0)
        {
            string samplePath = Path.Combine(inputFolder, "sample.bmp");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                generator.Save(samplePath);
            }
            bmpFiles = Directory.GetFiles(inputFolder, "*.bmp");
        }

        // Start timing the processing
        var stopwatch = Stopwatch.StartNew();

        int totalBarcodes = 0;

        // Process each BMP file
        foreach (string filePath in bmpFiles)
        {
            using (var reader = new BarCodeReader(filePath))
            {
                // Apply NormalQuality preset
                reader.QualitySettings = QualitySettings.NormalQuality;

                // Read barcodes from the image
                foreach (var result in reader.ReadBarCodes())
                {
                    totalBarcodes++;
                    Console.WriteLine($"File: {Path.GetFileName(filePath)} - Type: {result.CodeTypeName}, Text: {result.CodeText}");
                }
            }
        }

        stopwatch.Stop();

        // Output summary
        Console.WriteLine($"Processed {bmpFiles.Length} file(s), found {totalBarcodes} barcode(s) in {stopwatch.Elapsed.TotalSeconds:F2} seconds.");
    }
}