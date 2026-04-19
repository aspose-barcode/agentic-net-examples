using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        const int sampleCount = 10; // safe sample size per rule 15
        List<byte[]> barcodeImages = new List<byte[]>();

        // Generate sample barcode images in memory
        for (int i = 0; i < sampleCount; i++)
        {
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, $"CODE{i:D4}"))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    barcodeImages.Add(ms.ToArray());
                }
            }
        }

        // Measure memory before processing
        long memoryBefore = Process.GetCurrentProcess().PrivateMemorySize64;

        // Process each image with checksum validation enabled
        foreach (byte[] imageData in barcodeImages)
        {
            using (MemoryStream ms = new MemoryStream(imageData))
            {
                using (BarCodeReader reader = new BarCodeReader(ms, DecodeType.Code128))
                {
                    // Enable checksum validation
                    reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                    // Read barcodes (result is not used further)
                    foreach (var result in reader.ReadBarCodes())
                    {
                        // Access result to ensure processing
                        string codeText = result.CodeText;
                    }
                }
            }
        }

        // Measure memory after processing
        long memoryAfter = Process.GetCurrentProcess().PrivateMemorySize64;

        // Output memory usage difference in megabytes
        double memoryDiffMb = (memoryAfter - memoryBefore) / (1024.0 * 1024.0);
        Console.WriteLine($"Memory before: {memoryBefore / (1024.0 * 1024.0):F2} MB");
        Console.WriteLine($"Memory after : {memoryAfter / (1024.0 * 1024.0):F2} MB");
        Console.WriteLine($"Memory increase: {memoryDiffMb:F2} MB");
    }
}