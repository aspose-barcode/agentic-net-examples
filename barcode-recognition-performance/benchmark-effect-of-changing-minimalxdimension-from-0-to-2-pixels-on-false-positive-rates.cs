using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Parameters
        const int barcodeCount = 10;
        const string barcodeText = "Test123";
        const string tempFolder = "BarcodesTemp";

        // Ensure temporary folder exists
        if (Directory.Exists(tempFolder))
            Directory.Delete(tempFolder, true);
        Directory.CreateDirectory(tempFolder);

        // Generate barcode images
        var imagePaths = new List<string>();
        for (int i = 0; i < barcodeCount; i++)
        {
            string filePath = Path.Combine(tempFolder, $"barcode_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, barcodeText))
            {
                // Optional: set a fixed XDimension for consistency
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Save(filePath);
            }
            imagePaths.Add(filePath);
        }

        // Benchmark with MinimalXDimension = 0 (default) and = 2
        int falsePositivesZero = 0;
        int falsePositivesTwo = 0;

        foreach (string path in imagePaths)
        {
            // Read with MinimalXDimension = 0
            using (var readerZero = new BarCodeReader(path, DecodeType.Code128))
            {
                // Use normal quality preset
                readerZero.QualitySettings = QualitySettings.NormalQuality;
                // Set XDimension mode to use MinimalXDimension
                readerZero.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                // MinimalXDimension = 0 (default)
                readerZero.QualitySettings.MinimalXDimension = 0f;

                bool matched = false;
                foreach (BarCodeResult result in readerZero.ReadBarCodes())
                {
                    if (result.CodeText == barcodeText)
                    {
                        matched = true;
                        break;
                    }
                }
                if (!matched)
                    falsePositivesZero++;
            }

            // Read with MinimalXDimension = 2
            using (var readerTwo = new BarCodeReader(path, DecodeType.Code128))
            {
                readerTwo.QualitySettings = QualitySettings.NormalQuality;
                readerTwo.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                readerTwo.QualitySettings.MinimalXDimension = 2f;

                bool matched = false;
                foreach (BarCodeResult result in readerTwo.ReadBarCodes())
                {
                    if (result.CodeText == barcodeText)
                    {
                        matched = true;
                        break;
                    }
                }
                if (!matched)
                    falsePositivesTwo++;
            }
        }

        // Output results
        Console.WriteLine($"Total barcodes processed: {barcodeCount}");
        Console.WriteLine($"False positives with MinimalXDimension = 0: {falsePositivesZero}");
        Console.WriteLine($"False positives with MinimalXDimension = 2: {falsePositivesTwo}");

        // Clean up temporary files
        Directory.Delete(tempFolder, true);
    }
}