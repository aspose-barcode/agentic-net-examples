using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Directory to store temporary barcode images
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "DataBarStackedTests");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Sample codetext for GS1 DataBar Stacked (14 numeric digits)
        string codeText = "01234567890123";

        // List to collect test results
        var results = new List<string>();

        // Test AspectRatio values from 8 to 15 inclusive
        for (int ratio = 8; ratio <= 15; ratio++)
        {
            string filePath = Path.Combine(outputDir, $"databar_{ratio}.png");
            bool passed = false;
            string message;

            try
            {
                // Generate barcode with specific AspectRatio
                using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DatabarStacked, codeText))
                {
                    generator.Parameters.Barcode.DataBar.AspectRatio = (float)ratio;
                    generator.Save(filePath);
                }

                // Read and verify the barcode
                using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.DatabarStacked))
                {
                    BarCodeResult[] readResults = reader.ReadBarCodes();
                    if (readResults != null && readResults.Length > 0 && readResults[0].CodeText == codeText)
                    {
                        passed = true;
                        message = $"AspectRatio {ratio}: PASS";
                    }
                    else
                    {
                        passed = false;
                        message = $"AspectRatio {ratio}: FAIL - Decoded text mismatch or no result.";
                    }
                }
            }
            catch (Exception ex)
            {
                passed = false;
                message = $"AspectRatio {ratio}: EXCEPTION - {ex.Message}";
            }

            results.Add(message);
            Console.WriteLine(message);
        }

        // Summary
        int passedCount = 0;
        foreach (var r in results)
        {
            if (r.Contains("PASS")) passedCount++;
        }
        Console.WriteLine($"Total Passed: {passedCount} / {results.Count}");

        // Clean up generated images (optional)
        try
        {
            foreach (var file in Directory.GetFiles(outputDir, "databar_*.png"))
            {
                File.Delete(file);
            }
            Directory.Delete(outputDir);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}