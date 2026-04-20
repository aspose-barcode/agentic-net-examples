using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        string inputFolder = "InputBarcodes";
        string outputCsv = "PlanetBarcodesReport.csv";

        // Ensure the input folder exists; create it if missing and add a sample image.
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
            string samplePath = Path.Combine(inputFolder, "SamplePlanet.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Planet, "1234567890"))
            {
                generator.Save(samplePath);
            }
        }

        var csvBuilder = new StringBuilder();
        csvBuilder.AppendLine("FileName,BarcodeType,CodeText");

        // Process all PNG files in the folder.
        string[] pngFiles = Directory.GetFiles(inputFolder, "*.png");
        foreach (string filePath in pngFiles)
        {
            if (!File.Exists(filePath))
            {
                continue; // Skip missing files gracefully.
            }

            using (var reader = new BarCodeReader(filePath, DecodeType.Planet))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    // Escape double quotes in the code text for CSV compliance.
                    string safeCodeText = result.CodeText?.Replace("\"", "\"\"");
                    csvBuilder.AppendLine($"\"{Path.GetFileName(filePath)}\",{result.CodeTypeName},\"{safeCodeText}\"");
                }
            }
        }

        // Write the CSV report.
        File.WriteAllText(outputCsv, csvBuilder.ToString(), Encoding.UTF8);
        Console.WriteLine($"Report generated: {outputCsv}");
    }
}