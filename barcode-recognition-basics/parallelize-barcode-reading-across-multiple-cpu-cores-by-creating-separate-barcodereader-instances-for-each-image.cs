using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.Common;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Prepare sample barcode images
        var tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodes");
        if (!Directory.Exists(tempDir))
        {
            Directory.CreateDirectory(tempDir);
        }

        var imageFiles = new List<string>();
        for (int i = 1; i <= 5; i++)
        {
            var filePath = Path.Combine(tempDir, $"barcode{i}.png");
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, $"Sample{i}"))
            {
                // Simple image size settings
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 100f;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            imageFiles.Add(filePath);
        }

        // Enable multi‑core processing for BarCodeReader
        BarCodeReader.ProcessorSettings.UseAllCores = true;

        // Parallel reading of barcodes
        var consoleLock = new object();
        Parallel.ForEach(imageFiles, imagePath =>
        {
            if (!File.Exists(imagePath))
            {
                lock (consoleLock)
                {
                    Console.WriteLine($"File not found: {imagePath}");
                }
                return;
            }

            using (BarCodeReader reader = new BarCodeReader())
            {
                // Set decode type to Code128 (using MultiDecodeType)
                reader.BarCodeReadType = new MultiDecodeType(DecodeType.Code128);
                // Assign image file
                reader.SetBarCodeImage(imagePath);

                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    lock (consoleLock)
                    {
                        Console.WriteLine($"File: {Path.GetFileName(imagePath)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                    }
                }
            }
        });

        // Cleanup temporary files
        foreach (var file in imageFiles)
        {
            try
            {
                File.Delete(file);
            }
            catch
            {
                // Ignore any deletion errors
            }
        }

        // Remove temporary directory if empty
        try
        {
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignore if directory not empty or cannot be removed
        }
    }
}