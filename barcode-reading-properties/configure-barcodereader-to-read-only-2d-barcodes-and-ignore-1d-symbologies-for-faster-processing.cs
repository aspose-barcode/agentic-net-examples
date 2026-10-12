// Title: Read Only 2D Barcodes with BarCodeReader
// Description: Demonstrates configuring BarCodeReader to scan only 2D symbologies, ignoring 1D barcodes for faster processing.
// Category-Description: This example belongs to the Aspose.BarCode reading category, showcasing how to use BarCodeReader with the DecodeType.Types2D preset. It highlights typical scenarios where developers need to filter out 1D symbologies to improve performance, such as bulk image processing or real‑time scanning applications. Key API classes include BarCodeReader, DecodeType, and BarCodeResult.
// Prompt: Configure BarCodeReader to read only 2D barcodes and ignore 1D symbologies for faster processing.
// Tags: barcode, symbology, read, 2d, types2d, aspose.barcode, generation, recognition

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Sample program that generates a 2D QR code and a 1D Code128 barcode,
/// then reads only the 2D barcode using BarCodeReader configured with the Types2D preset.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarCodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Collection to hold paths of generated barcode files
        var barcodeFiles = new List<string>();

        // -------------------------------------------------
        // Generate a 2D QR Code and add its path to the list
        // -------------------------------------------------
        string qrPath = Path.Combine(tempFolder, "qr.png");
        using (var qrGenerator = new BarcodeGenerator(EncodeTypes.QR, "Hello2D"))
        {
            qrGenerator.Save(qrPath, BarCodeImageFormat.Png);
        }
        if (File.Exists(qrPath))
            barcodeFiles.Add(qrPath);

        // -------------------------------------------------
        // Generate a 1D Code128 barcode and add its path to the list
        // -------------------------------------------------
        string code128Path = Path.Combine(tempFolder, "code128.png");
        using (var code128Generator = new BarcodeGenerator(EncodeTypes.Code128, "Hello1D"))
        {
            code128Generator.Save(code128Path, BarCodeImageFormat.Png);
        }
        if (File.Exists(code128Path))
            barcodeFiles.Add(code128Path);

        // -------------------------------------------------
        // Read only 2D barcodes using the Types2D preset
        // -------------------------------------------------
        foreach (string file in barcodeFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            // Initialize BarCodeReader to decode only 2D symbologies
            using (var reader = new BarCodeReader(file, DecodeType.Types2D))
            {
                Console.WriteLine($"Reading file: {Path.GetFileName(file)}");
                BarCodeResult[] results = reader.ReadBarCodes();

                if (results.Length == 0)
                {
                    Console.WriteLine("  No 2D barcode detected.");
                }
                else
                {
                    foreach (BarCodeResult result in results)
                    {
                        Console.WriteLine($"  Detected: {result.CodeTypeName} - {result.CodeText}");
                    }
                }
            }
        }

        // -------------------------------------------------
        // Clean up temporary files and folder
        // -------------------------------------------------
        try
        {
            foreach (string file in barcodeFiles)
            {
                if (File.Exists(file))
                    File.Delete(file);
            }

            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cleanup error: {ex.Message}");
        }
    }
}