using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main(string[] args)
    {
        // Determine input folder (argument or default)
        string inputFolder = args.Length > 0 ? args[0] : "Barcodes";

        // Ensure the folder exists
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
        }

        // If the folder is empty, create a sample barcode image
        if (Directory.GetFiles(inputFolder).Length == 0)
        {
            string samplePath = Path.Combine(inputFolder, "sample.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                generator.Save(samplePath);
            }
        }

        // Process each file in the folder
        foreach (string filePath in Directory.GetFiles(inputFolder))
        {
            if (!File.Exists(filePath))
            {
                continue;
            }

            using (var reader = new BarCodeReader(filePath))
            {
                // Apply default checksum validation
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Default;

                // Read barcodes and output confidence levels
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {Path.GetFileName(filePath)} | Type: {result.CodeTypeName} | Confidence: {result.Confidence}");
                }
            }
        }
    }
}