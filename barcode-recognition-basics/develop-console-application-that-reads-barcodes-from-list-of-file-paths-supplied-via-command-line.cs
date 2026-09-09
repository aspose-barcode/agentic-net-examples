// Title: Read Barcodes from Files via Command Line
// Description: Demonstrates how to read barcodes from image files whose paths are provided as command‑line arguments, generating sample barcodes when no arguments are given.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition and generation category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for extracting barcode data from images. Typical scenarios include batch processing of scanned documents, automated inventory checks, and validation of printed codes. Developers often need to combine generation and recognition APIs to build end‑to‑end barcode workflows.
// Prompt: Develop a console application that reads barcodes from a list of file paths supplied via command line.
// Tags: barcode, symbology, read, console, aspose.barcode, generation, recognition, png, csharp

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Console application that reads barcodes from image files supplied via command line.
/// If no file paths are provided, it generates sample barcode images in a temporary folder
/// and then reads them back.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    /// <param name="args">Array of file paths to barcode images.</param>
    static void Main(string[] args)
    {
        // Collect file paths to process
        List<string> filesToRead = new List<string>();

        if (args != null && args.Length > 0)
        {
            // Use the file paths supplied on the command line
            filesToRead.AddRange(args);
        }
        else
        {
            // No arguments supplied – generate sample barcodes in a temporary folder
            string tempFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(tempFolder);

            // Define sample data: (symbology, text, file name)
            var samples = new (BaseEncodeType encodeType, string text, string fileName)[]
            {
                (EncodeTypes.Code128, "Sample123", "code128.png"),
                (EncodeTypes.QR, "https://example.com", "qr.png")
            };

            // Generate each sample barcode and add its path to the processing list
            foreach (var sample in samples)
            {
                string filePath = Path.Combine(tempFolder, sample.fileName);
                using (var generator = new BarcodeGenerator(sample.encodeType, sample.text))
                {
                    generator.Save(filePath, BarCodeImageFormat.Png);
                }
                filesToRead.Add(filePath);
            }
        }

        // Process each file: read and output any detected barcodes
        foreach (string filePath in filesToRead)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                using (var reader = new BarCodeReader(filePath))
                {
                    bool anyFound = false;
                    // Iterate through all detected barcodes in the image
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        anyFound = true;
                        Console.WriteLine($"{Path.GetFileName(filePath)}: {result.CodeTypeName} => {result.CodeText}");
                    }
                    if (!anyFound)
                    {
                        Console.WriteLine($"{Path.GetFileName(filePath)}: No barcodes detected.");
                    }
                }
            }
            catch (ArgumentException ex)
            {
                // Handle cases where the image cannot be loaded (e.g., unsupported format)
                Console.WriteLine($"Failed to load image '{filePath}': {ex.Message}");
            }
            catch (Exception ex)
            {
                // Catch‑all for any other processing errors
                Console.WriteLine($"Error processing '{filePath}': {ex.Message}");
            }
        }
    }
}