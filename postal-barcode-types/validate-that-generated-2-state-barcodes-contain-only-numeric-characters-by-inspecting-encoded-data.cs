using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Prepare output directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Sample code texts (some numeric, some not)
        string[] codeTexts = new string[]
        {
            "1234567890",
            "987654321",
            "ABC12345",   // invalid: contains letters
            "0011223344",
            "12A34B56"    // invalid: contains letters
        };

        // Generate barcodes
        foreach (string text in codeTexts)
        {
            string filePath = Path.Combine(outputDir, $"{text}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
            {
                // Save barcode image
                generator.Save(filePath);
            }
        }

        // Validate generated barcodes contain only numeric characters
        foreach (string text in codeTexts)
        {
            string filePath = Path.Combine(outputDir, $"{text}.png");
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            using (var reader = new BarCodeReader(filePath, DecodeType.Code128))
            {
                // Enable checksum validation (optional)
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                bool anyResult = false;
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    anyResult = true;
                    string decoded = result.CodeText ?? string.Empty;
                    bool isNumeric = true;
                    foreach (char c in decoded)
                    {
                        if (!char.IsDigit(c))
                        {
                            isNumeric = false;
                            break;
                        }
                    }

                    if (isNumeric)
                    {
                        Console.WriteLine($"[PASS] Barcode '{Path.GetFileName(filePath)}' decoded as numeric: {decoded}");
                    }
                    else
                    {
                        Console.WriteLine($"[FAIL] Barcode '{Path.GetFileName(filePath)}' contains non‑numeric characters: {decoded}");
                    }
                }

                if (!anyResult)
                {
                    Console.WriteLine($"[WARN] No barcode detected in file: {Path.GetFileName(filePath)}");
                }
            }
        }
    }
}