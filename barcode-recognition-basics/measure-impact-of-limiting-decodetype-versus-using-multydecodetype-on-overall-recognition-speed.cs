using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
class Program
{
    static void Main()
    {
        // Folder to store generated barcode images
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodesDemo");
        Directory.CreateDirectory(outputDir);

        // Generate sample barcodes (alternating Code128 and QR)
        string[] files = new string[10];
        for (int i = 0; i < files.Length; i++)
        {
            string filePath = Path.Combine(outputDir, $"barcode_{i}.png");
            using (var generator = new BarcodeGenerator(i % 2 == 0 ? EncodeTypes.Code128 : EncodeTypes.QR, $"Sample{i}"))
            {
                generator.Save(filePath);
            }
            files[i] = filePath;
        }

        // Measure recognition time with limited decode types (Code128 + QR)
        var limitedStopwatch = Stopwatch.StartNew();
        foreach (string file in files)
        {
            using (var reader = new BarCodeReader(file, DecodeType.Code128, DecodeType.QR))
            {
                // ReadBarCodes performs the actual recognition
                reader.ReadBarCodes();
            }
        }
        limitedStopwatch.Stop();

        // Measure recognition time with MultiDecodeType (AllSupportedTypes)
        var multiStopwatch = Stopwatch.StartNew();
        foreach (string file in files)
        {
            using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
            {
                reader.ReadBarCodes();
            }
        }
        multiStopwatch.Stop();

        // Output the results
        Console.WriteLine($"Limited Decode Types (Code128 + QR) time: {limitedStopwatch.ElapsedMilliseconds} ms");
        Console.WriteLine($"MultiDecodeType (AllSupportedTypes) time: {multiStopwatch.ElapsedMilliseconds} ms");
    }
}