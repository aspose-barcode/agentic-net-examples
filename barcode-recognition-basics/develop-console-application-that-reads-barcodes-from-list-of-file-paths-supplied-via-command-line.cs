using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main(string[] args)
    {
        // If no command‑line arguments are provided, use a small default list.
        string[] filePaths = args.Length > 0 ? args : new string[]
        {
            "sample1.png",
            "sample2.png"
        };

        foreach (string path in filePaths)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine($"File not found: {path}");
                continue;
            }

            // BarCodeReader implements IDisposable, so use a full using block.
            using (var reader = new BarCodeReader(path))
            {
                // Read all barcodes from the image.
                BarCodeResult[] results = reader.ReadBarCodes();

                if (results.Length == 0)
                {
                    Console.WriteLine($"No barcodes detected in file: {path}");
                    continue;
                }

                Console.WriteLine($"Barcodes found in file: {path}");
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"  Type: {result.CodeTypeName}");
                    Console.WriteLine($"  Text: {result.CodeText}");
                }
            }
        }
    }
}