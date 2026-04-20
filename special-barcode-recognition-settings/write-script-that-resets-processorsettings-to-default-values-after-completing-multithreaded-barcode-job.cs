using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.Common;

class Program
{
    static void Main()
    {
        // Prepare sample barcode files
        string[] codes = { "12345", "ABCDE", "9876543210", "HELLO", "WORLD" };
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodes");
        Directory.CreateDirectory(outputDir);
        string[] files = new string[codes.Length];

        for (int i = 0; i < codes.Length; i++)
        {
            string filePath = Path.Combine(outputDir, $"barcode_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codes[i]))
            {
                generator.Save(filePath);
            }
            files[i] = filePath;
        }

        // Enable multithreaded processing for the upcoming recognition job
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = Environment.ProcessorCount * 2;
        // Optionally, you could set a specific core count:
        // BarCodeReader.ProcessorSettings.UseAllCores = false;
        // BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Math.Max(1, Environment.ProcessorCount / 2);

        Console.WriteLine("Starting barcode recognition with multithreaded settings...");

        foreach (string file in files)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            using (var reader = new BarCodeReader(file, DecodeType.Code128))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {Path.GetFileName(file)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                }
            }
        }

        // Reset ProcessorSettings to their default values after the job
        BarCodeReader.ProcessorSettings.UseAllCores = false;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = 0;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = 0;

        Console.WriteLine("ProcessorSettings have been reset to default values.");
    }
}