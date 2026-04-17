using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main(string[] args)
    {
        // Input folder and output CSV can be passed as arguments; defaults are used otherwise.
        string inputFolder = args.Length > 0 ? args[0] : "InputImages";
        string outputCsv = args.Length > 1 ? args[1] : "BarcodeResults.csv";

        // Ensure the input folder exists.
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
        }

        // Gather image files from the folder.
        string[] allFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
        var imageExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            ".png", ".jpg", ".jpeg", ".bmp", ".tif", ".tiff", ".gif"
        };

        var csvLines = new List<string>();
        // CSV header.
        csvLines.Add("FileName,BarcodeType,CodeText,Confidence");

        int processed = 0;
        foreach (string filePath in allFiles)
        {
            if (!imageExtensions.Contains(Path.GetExtension(filePath))) continue;
            if (!File.Exists(filePath)) continue;

            // Use BarCodeReader to detect barcodes in the image.
            using (var reader = new BarCodeReader(filePath))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    // Build a CSV line with relevant metadata.
                    string line = $"{Path.GetFileName(filePath)},{result.CodeTypeName},{result.CodeText},{(int)result.Confidence}";
                    csvLines.Add(line);
                }
            }

            processed++;
            // Limit processing to a safe sample size.
            if (processed >= 10) break;
        }

        // Write results to the CSV file.
        using (var writer = new StreamWriter(outputCsv, false))
        {
            foreach (string line in csvLines)
            {
                writer.WriteLine(line);
            }
        }
    }
}