// Title: Code 39 checksum validation performance test
// Description: Demonstrates how disabling checksum verification affects recognition speed when processing multiple Code 39 barcodes.
// Prompt: Test impact of disabling checksum verification on recognition speed for high‑volume Code 39 scans.
// Tags: code39, checksum, performance, barcode, generation, recognition, aspnet, csharp

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Program that generates a set of Code 39 barcodes, then measures recognition time with checksum validation enabled and disabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes, runs recognition with checksum on/off, and prints timing results.
    /// </summary>
    static void Main()
    {
        // Sample data for Code 39 barcodes
        List<string> samples = new List<string>
        {
            "CODE39A",
            "CODE39B",
            "CODE39C",
            "CODE39D",
            "CODE39E"
        };

        // Generate barcode images and keep them in memory
        List<Bitmap> barcodes = new List<Bitmap>();
        foreach (string text in samples)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Create a barcode generator for Code 39 and save to memory stream as PNG
                using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code39, text))
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                }

                ms.Position = 0;

                // Load bitmap from memory stream and clone it to preserve after disposing the original
                using (Bitmap bmp = new Bitmap(ms))
                {
                    barcodes.Add(new Bitmap(bmp));
                }
            }
        }

        // Measure recognition time with checksum validation ON
        Stopwatch swOn = new Stopwatch();
        swOn.Start();
        foreach (Bitmap bmp in barcodes)
        {
            using (BarCodeReader reader = new BarCodeReader(bmp, DecodeType.Code39))
            {
                // Enable checksum validation
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                // Read all barcodes in the image
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    // Access result to ensure processing
                    string code = result.CodeText;
                }
            }
        }
        swOn.Stop();

        // Measure recognition time with checksum validation OFF
        Stopwatch swOff = new Stopwatch();
        swOff.Start();
        foreach (Bitmap bmp in barcodes)
        {
            using (BarCodeReader reader = new BarCodeReader(bmp, DecodeType.Code39))
            {
                // Disable checksum validation
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Off;

                // Read all barcodes in the image
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    string code = result.CodeText;
                }
            }
        }
        swOff.Stop();

        // Output the timing results
        Console.WriteLine($"Checksum ON  - Total time: {swOn.ElapsedMilliseconds} ms");
        Console.WriteLine($"Checksum OFF - Total time: {swOff.ElapsedMilliseconds} ms");

        // Clean up generated bitmaps
        foreach (Bitmap bmp in barcodes)
        {
            bmp.Dispose();
        }
    }
}