using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Prepare temporary folder for barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        if (!Directory.Exists(tempFolder))
        {
            Directory.CreateDirectory(tempFolder);
        }

        // Sample code texts for Code39 barcodes
        string[] codes = new string[] { "CODE1", "CODE2", "CODE3", "CODE4", "CODE5" };
        string[] imagePaths = new string[codes.Length];

        // Generate barcode images
        for (int i = 0; i < codes.Length; i++)
        {
            string filePath = Path.Combine(tempFolder, $"code{i + 1}.png");
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code39, codes[i]))
            {
                generator.Save(filePath);
            }
            imagePaths[i] = filePath;
        }

        // Benchmark with checksum validation OFF
        Stopwatch swOff = Stopwatch.StartNew();
        foreach (string path in imagePaths)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine($"File not found: {path}");
                continue;
            }

            using (BarCodeReader reader = new BarCodeReader(path, DecodeType.Code39))
            {
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Off;
                // Read barcodes (result not used, just to trigger recognition)
                BarCodeResult[] results = reader.ReadBarCodes();
            }
        }
        swOff.Stop();

        // Benchmark with checksum validation ON
        Stopwatch swOn = Stopwatch.StartNew();
        foreach (string path in imagePaths)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine($"File not found: {path}");
                continue;
            }

            using (BarCodeReader reader = new BarCodeReader(path, DecodeType.Code39))
            {
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;
                BarCodeResult[] results = reader.ReadBarCodes();
            }
        }
        swOn.Stop();

        // Output timing results
        Console.WriteLine($"Checksum validation OFF total time for {codes.Length} scans: {swOff.ElapsedMilliseconds} ms");
        Console.WriteLine($"Checksum validation ON  total time for {codes.Length} scans: {swOn.ElapsedMilliseconds} ms");
    }
}