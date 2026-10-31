using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a unique temporary folder for test artifacts
        string tempFolder = Path.Combine(Path.GetTempPath(), "QrTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define test cases with different error correction levels
        var testCases = new List<(QRErrorLevel level, string description)>
        {
            (QRErrorLevel.LevelL, "Low"),
            (QRErrorLevel.LevelM, "Medium")
        };

        bool allPassed = true;

        foreach (var (level, description) in testCases)
        {
            string filePath = Path.Combine(tempFolder, $"qr_{description}.png");
            // Generate QR Code
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Aspose QR Test"))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 4f;
                generator.Parameters.Barcode.QR.ErrorLevel = level;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Read and validate QR Code
            using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.QR))
            {
                BarCodeResult[] results = reader.ReadBarCodes();
                bool success = results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText);
                Console.WriteLine($"Test '{description}' (ErrorLevel={level}): {(success ? "PASS" : "FAIL")}");
                if (!success) allPassed = false;
            }
        }

        // Summary
        Console.WriteLine(allPassed ? "ALL TESTS PASSED" : "ONE OR MORE TESTS FAILED");

        // Cleanup
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}