using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Define input and output paths
        string inputFolder = "InputMaxiCode";
        string outputCsv = "MaxiCodeReport.csv";

        // Ensure input folder exists
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
        }

        // Seed a sample MaxiCode image if the folder is empty
        string[] existingPngs = Directory.GetFiles(inputFolder, "*.png");
        if (existingPngs.Length == 0)
        {
            string samplePath = Path.Combine(inputFolder, "SampleMaxiCode.png");
            // Create a simple MaxiCode codetext (Mode 2) and generate an image
            var maxiCodeCodetext = new MaxiCodeCodetextMode2
            {
                PostalCode = "524032140",
                CountryCode = 056,
                ServiceCategory = 999
            };
            var standardMessage = new MaxiCodeStandardSecondMessage { Message = "Sample" };
            maxiCodeCodetext.SecondMessage = standardMessage;

            using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
            {
                generator.GenerateBarCodeImage();
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    File.WriteAllBytes(samplePath, ms.ToArray());
                }
            }
        }

        // Get all PNG files in the input folder
        string[] pngFiles = Directory.GetFiles(inputFolder, "*.png");

        // Prepare CSV lines
        var csvLines = new List<string>();
        csvLines.Add("FileName,BarcodeType,MaxiCodeMode,ConstructedCodetext");

        foreach (string filePath in pngFiles)
        {
            if (!File.Exists(filePath))
            {
                // Skip missing files gracefully
                continue;
            }

            // Use AllSupportedTypes as required
            using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
            {
                BarCodeResult[] results = reader.ReadBarCodes();
                foreach (BarCodeResult result in results)
                {
                    // Retrieve MaxiCode mode from extended data (if available)
                    var mode = result.Extended?.MaxiCode?.MaxiCodeMode ?? default;

                    // Decode complex MaxiCode codetext
                    MaxiCodeCodetext decoded = ComplexCodetextReader.TryDecodeMaxiCode(mode, result.CodeText);
                    string constructed = decoded?.GetConstructedCodetext() ?? string.Empty;

                    // Build CSV line
                    string line = $"{Path.GetFileName(filePath)},{result.CodeTypeName},{mode},{constructed}";
                    csvLines.Add(line);
                }
            }
        }

        // Write CSV report
        File.WriteAllLines(outputCsv, csvLines);
    }
}