// Title: Real‑time barcode detection demo using Aspose.BarCode
// Description: Generates sample Code128 barcodes and reads them sequentially, showing how the FoundBarCodes collection updates after each read operation.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It demonstrates the use of BarcodeGenerator for creating barcodes and BarCodeReader for detecting them in images. Typical scenarios include scanning documents, processing camera feeds, or updating UI components with detection results. Developers often need to access FoundBarCodes and FoundCount to display real‑time detection information.
// Prompt: Design a UI component that displays real‑time barcode detection results using FoundBarCodes property updates.
// Tags: barcode, generation, recognition, code128, foundbarcodes, realtime, aspose.barcode, csharp

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating barcodes, reading them, and displaying detection results using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates sample barcodes, reads them, and outputs detection details.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for storing generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        var barcodeFiles = new List<string>();

        // Generate three sample Code128 barcodes and save them as PNG files
        for (int i = 0; i < 3; i++)
        {
            string codeText = $"CODE{i + 1}";
            string filePath = Path.Combine(tempFolder, $"barcode{i + 1}.png");

            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            barcodeFiles.Add(filePath);
        }

        // Simulate real‑time detection by reading each generated barcode file
        foreach (string file in barcodeFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
            {
                // Perform recognition; this updates FoundBarCodes and FoundCount
                reader.ReadBarCodes();

                Console.WriteLine($"Processed: {Path.GetFileName(file)}");
                Console.WriteLine($"FoundCount: {reader.FoundCount}");

                // Iterate over each detected barcode and display its details
                foreach (BarCodeResult result in reader.FoundBarCodes)
                {
                    var rect = result.Region.Rectangle;
                    Console.WriteLine($"  Type: {result.CodeTypeName}");
                    Console.WriteLine($"  Text: {result.CodeText}");
                    Console.WriteLine($"  Region: X={rect.X}, Y={rect.Y}, Width={rect.Width}, Height={rect.Height}");
                    Console.WriteLine($"  Angle: {result.Region.Angle}");
                }
            }
        }

        // Clean up temporary files and directory
        try
        {
            foreach (string file in barcodeFiles)
            {
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
            }
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored – cleanup failure should not affect program exit
        }
    }
}