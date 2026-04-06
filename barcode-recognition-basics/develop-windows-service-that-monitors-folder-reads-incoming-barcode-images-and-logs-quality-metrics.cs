using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Define folders
        string inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "InputBarcodes");
        string logFolder = Path.Combine(Directory.GetCurrentDirectory(), "Logs");

        // Ensure folders exist
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
        }
        if (!Directory.Exists(logFolder))
        {
            Directory.CreateDirectory(logFolder);
        }

        // Create a sample barcode image if none exist
        string sampleFile = Path.Combine(inputFolder, "SampleCode128.png");
        if (!File.Exists(sampleFile))
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                generator.CodeText = "Sample12345";
                // Optional: set image size using unit members
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 150f;
                // Set background and bar colors using Aspose.Drawing
                generator.Parameters.BackColor = Color.White;
                generator.Parameters.Barcode.BarColor = Color.Black;
                generator.Save(sampleFile);
            }
        }

        // Prepare log file
        string logFilePath = Path.Combine(logFolder, "BarcodeMetrics.log");
        using (var logWriter = new StreamWriter(logFilePath, false))
        {
            // Process each image in the input folder
            string[] imageFiles = Directory.GetFiles(inputFolder, "*.png");
            foreach (string imagePath in imageFiles)
            {
                logWriter.WriteLine($"Processing file: {Path.GetFileName(imagePath)}");
                using (var reader = new BarCodeReader(imagePath))
                {
                    // Use normal quality preset
                    reader.QualitySettings = QualitySettings.NormalQuality;

                    // Read all barcodes in the image
                    BarCodeResult[] results = reader.ReadBarCodes();
                    if (results.Length == 0)
                    {
                        logWriter.WriteLine("  No barcodes detected.");
                        continue;
                    }

                    foreach (BarCodeResult result in results)
                    {
                        logWriter.WriteLine($"  Type: {result.CodeTypeName}");
                        logWriter.WriteLine($"  CodeText: {result.CodeText}");
                        logWriter.WriteLine($"  Confidence: {result.Confidence}");
                        logWriter.WriteLine($"  ReadingQuality: {result.ReadingQuality}");
                    }
                }
                logWriter.WriteLine(); // Blank line between files
            }
        }

        Console.WriteLine("Barcode processing completed. Metrics written to:");
        Console.WriteLine(logFilePath);
    }
}