// Title: Read barcodes from file paths supplied via command line
// Description: Demonstrates a console application that decodes barcode images whose file paths are provided as command‑line arguments, generating sample barcodes when no arguments are given.
// Category-Description: This example belongs to the Aspose.BarCode reading and generation category. It showcases the BarCodeReader class for recognizing various symbologies and the BarcodeGenerator class for creating sample images. Typical scenarios include batch processing of scanned documents, automated inventory checks, and validation of generated barcodes. Developers often need to iterate over file collections, handle unsupported formats, and output decoded values.
// Prompt: Develop a console application that reads barcodes from a list of file paths supplied via command line.
// Tags: barcode, symbology, read, console, aspose.barcode, generation, recognition, png, command-line

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Console application that reads barcodes from image files supplied via command line.
/// Generates sample barcode images when no arguments are provided.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Parses command‑line arguments, optionally creates sample barcodes,
    /// and uses <see cref="BarCodeReader"/> to decode each image, writing results to the console.
    /// </summary>
    /// <param name="args">Array of file paths to barcode images.</param>
    static void Main(string[] args)
    {
        // Determine the list of barcode image files to read.
        List<string> barcodeFiles = new List<string>();

        if (args != null && args.Length > 0)
        {
            // Use file paths supplied via command line.
            foreach (string arg in args)
            {
                if (!string.IsNullOrWhiteSpace(arg))
                {
                    barcodeFiles.Add(arg);
                }
            }
        }
        else
        {
            // No arguments supplied – create a temporary folder with sample barcodes.
            string tempFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(tempFolder);

            // Sample barcode definitions.
            var samples = new (BaseEncodeType Encode, string Text, string FileName)[]
            {
                (EncodeTypes.Code128, "1234567890", "code128.png"),
                (EncodeTypes.QR, "Hello Aspose!", "qr.png"),
                (EncodeTypes.Pdf417, "PDF417 Sample", "pdf417.png")
            };

            // Generate each sample barcode and add its file path to the list.
            foreach (var sample in samples)
            {
                string filePath = Path.Combine(tempFolder, sample.FileName);
                using (var generator = new BarcodeGenerator(sample.Encode, sample.Text))
                {
                    // Save as PNG.
                    generator.Save(filePath, BarCodeImageFormat.Png);
                }
                barcodeFiles.Add(filePath);
            }
        }

        if (barcodeFiles.Count == 0)
        {
            Console.WriteLine("No barcode files to process.");
            return;
        }

        // Process each file.
        foreach (string filePath in barcodeFiles)
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
                    // Read all barcodes in the current image.
                    foreach (var result in reader.ReadBarCodes())
                    {
                        anyFound = true;
                        Console.WriteLine($"File: {filePath}");
                        Console.WriteLine($"  CodeText : {result.CodeText}");
                        Console.WriteLine($"  Symbology: {result.CodeTypeName}");
                    }

                    if (!anyFound)
                    {
                        Console.WriteLine($"No barcode detected in file: {filePath}");
                    }
                }
            }
            catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
            {
                Console.WriteLine($"Skipping unsupported or corrupted file: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}