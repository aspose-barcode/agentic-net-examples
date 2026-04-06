using System;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Collections.Generic;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Define input and output paths
        string inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "InputImages");
        string reportPath = Path.Combine(Directory.GetCurrentDirectory(), "report.csv");

        // Ensure input folder exists; if not, create and seed a sample image
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
            // Create a sample barcode image
            string samplePath = Path.Combine(inputFolder, "sample.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                generator.Save(samplePath);
            }
        }

        // Supported image extensions
        string[] extensions = new[] { ".png", ".jpg", ".jpeg", ".bmp", ".gif", ".tif", ".tiff" };
        var imageFiles = new List<string>();
        foreach (var ext in extensions)
        {
            imageFiles.AddRange(Directory.GetFiles(inputFolder, "*" + ext, SearchOption.TopDirectoryOnly));
        }

        // Prepare CSV header
        var csvLines = new List<string>();
        csvLines.Add("FileName,RecognitionTimeMs,BarcodesFound");

        // Process each image file
        foreach (var filePath in imageFiles)
        {
            var stopwatch = Stopwatch.StartNew();
            int foundCount = 0;

            // Use BarCodeReader to recognize barcodes
            using (var reader = new BarCodeReader(filePath))
            {
                // Optional: set a timeout to avoid hangs on large images
                reader.Timeout = 5000; // milliseconds

                // Perform recognition
                reader.ReadBarCodes();
                foundCount = reader.FoundCount;
            }

            stopwatch.Stop();
            long elapsedMs = stopwatch.ElapsedMilliseconds;

            // Add result line to CSV
            string fileName = Path.GetFileName(filePath);
            csvLines.Add($"{fileName},{elapsedMs},{foundCount}");
        }

        // Write CSV report
        using (var writer = new StreamWriter(reportPath, false, Encoding.UTF8))
        {
            foreach (var line in csvLines)
            {
                writer.WriteLine(line);
            }
        }

        // Inform user of completion
        Console.WriteLine($"Barcode recognition completed. Report saved to: {reportPath}");
    }
}