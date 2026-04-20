using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Directory to store sample barcode images
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeSamples");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Prepare sample data: barcode type, text, file name
        var samples = new List<(BaseEncodeType encodeType, string codeText, string fileName)>
        {
            (EncodeTypes.Code128, "Sample123", "code128.png"),
            (EncodeTypes.QR, "https://example.com", "qr.png"),
            (EncodeTypes.EAN13, "1234567890128", "ean13.png")
        };

        // Generate barcode images
        foreach (var sample in samples)
        {
            string filePath = Path.Combine(outputDir, sample.fileName);
            using (var generator = new BarcodeGenerator(sample.encodeType, sample.codeText))
            {
                generator.Save(filePath);
            }
        }

        // List of files to decode (including a non‑existent file to demonstrate failure handling)
        var filesToDecode = new List<string>
        {
            Path.Combine(outputDir, "code128.png"),
            Path.Combine(outputDir, "qr.png"),
            Path.Combine(outputDir, "ean13.png"),
            Path.Combine(outputDir, "missing.png") // intentionally missing
        };

        // Process each file
        foreach (string filePath in filesToDecode)
        {
            string timestamp = DateTime.Now.ToString("o");
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"{timestamp} | FAILURE | File not found: {filePath}");
                continue;
            }

            try
            {
                // Initialize reader for all supported types
                using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
                {
                    // Perform recognition
                    BarCodeResult[] results = reader.ReadBarCodes();

                    if (results.Length == 0)
                    {
                        Console.WriteLine($"{timestamp} | FAILURE | No barcode detected in: {filePath}");
                    }
                    else
                    {
                        foreach (BarCodeResult result in results)
                        {
                            Console.WriteLine($"{timestamp} | SUCCESS | File: {filePath} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log unexpected errors as failures
                Console.WriteLine($"{timestamp} | FAILURE | Exception while processing {filePath}: {ex.Message}");
            }
        }
    }
}