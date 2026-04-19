using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Define the folder to monitor (simulated)
        string inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "InputBarcodes");
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
        }

        // Get PNG files in the folder
        string[] imageFiles = Directory.GetFiles(inputFolder, "*.png");

        // If no files exist, generate a sample barcode image
        if (imageFiles.Length == 0)
        {
            string samplePath = Path.Combine(inputFolder, "sample.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                // Optional visual settings
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;
                generator.Save(samplePath);
            }
            imageFiles = new[] { samplePath };
        }

        // Process each barcode image
        foreach (string filePath in imageFiles)
        {
            using (var reader = new BarCodeReader(filePath, DecodeType.Code128, DecodeType.Code39, DecodeType.QR, DecodeType.DataMatrix))
            {
                // Use normal quality preset
                reader.QualitySettings = QualitySettings.NormalQuality;

                // Read all barcodes in the image
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {Path.GetFileName(filePath)}");
                    Console.WriteLine($"  Type: {result.CodeTypeName}");
                    Console.WriteLine($"  CodeText: {result.CodeText}");
                    Console.WriteLine($"  Confidence: {result.Confidence}");
                    Console.WriteLine($"  ReadingQuality: {result.ReadingQuality}");
                }
            }
        }
    }
}