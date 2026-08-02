// Title: Barcode Generation and Detection with Preset Decoding Types
// Description: Generates sample barcode images and detects them using various decoding presets, reporting the count of barcodes found per image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It demonstrates how to use BarcodeGenerator to create images, and BarCodeReader with different BaseDecodeType presets to recognize barcodes. Typical use cases include batch processing of scanned documents, quality‑control of barcode printing, and consistency analysis across different symbologies. Developers often need to switch between preset decode sets to optimize performance or focus on specific barcode types.
// Prompt: Record the number of barcodes detected per image under each preset to analyze consistency.
// Tags: barcode symbology, generation, detection, preset, aspose.barcode, png, console

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates creating barcode images and counting detected barcodes per image using different decoding presets.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, then reads each image with several decode presets and prints detection counts.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare output folder for generated barcode images
        // --------------------------------------------------------------------
        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // --------------------------------------------------------------------
        // Define sample barcodes to generate (file name, symbology, data)
        // --------------------------------------------------------------------
        var samples = new List<(string FileName, BaseEncodeType EncodeType, string CodeText)>
        {
            ("code128.png", EncodeTypes.Code128, "ABC123456"),
            ("qr.png", EncodeTypes.QR, "https://example.com"),
            ("datamatrix.png", EncodeTypes.DataMatrix, "DataMatrixSample")
        };

        // --------------------------------------------------------------------
        // Generate barcode images using default settings and save as PNG
        // --------------------------------------------------------------------
        foreach (var sample in samples)
        {
            string filePath = Path.Combine(folderPath, sample.FileName);
            using (var generator = new BarcodeGenerator(sample.EncodeType, sample.CodeText))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // --------------------------------------------------------------------
        // Define decoding presets (each preset contains a set of BaseDecodeType values)
        // --------------------------------------------------------------------
        var presets = new List<(string Name, BaseDecodeType[] DecodeTypes)>
        {
            ("AllSupportedTypes", new BaseDecodeType[] { DecodeType.AllSupportedTypes }),
            ("Code128Only", new BaseDecodeType[] { DecodeType.Code128 }),
            ("QROnly", new BaseDecodeType[] { DecodeType.QR }),
            ("DataMatrixOnly", new BaseDecodeType[] { DecodeType.DataMatrix })
        };

        // --------------------------------------------------------------------
        // Process each preset and each generated image, counting detected barcodes
        // --------------------------------------------------------------------
        foreach (var preset in presets)
        {
            Console.WriteLine($"Preset: {preset.Name}");
            foreach (var sample in samples)
            {
                string imagePath = Path.Combine(folderPath, sample.FileName);
                if (!File.Exists(imagePath))
                {
                    Console.WriteLine($"  Image not found: {sample.FileName}");
                    continue;
                }

                using (var reader = new BarCodeReader(imagePath, preset.DecodeTypes))
                {
                    var results = reader.ReadBarCodes();
                    int count = results?.Length ?? 0;
                    Console.WriteLine($"  Image: {sample.FileName} - Detected Barcodes: {count}");
                }
            }
        }
    }
}