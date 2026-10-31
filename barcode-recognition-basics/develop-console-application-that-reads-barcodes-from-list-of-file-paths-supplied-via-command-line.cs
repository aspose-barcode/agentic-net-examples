// Title: Read and Generate Barcodes from Command‑Line File Paths
// Description: Demonstrates how to generate sample barcodes when no input is provided and then read barcodes from image files supplied via command‑line arguments.
// Category-Description: This example belongs to the Aspose.BarCode reading and generation category, showcasing the use of BarcodeGenerator for creating PNG images and BarCodeReader for extracting barcode data. Typical use cases include batch processing of scanned documents or automated verification of barcode assets. Developers often need to handle multiple symbologies, manage temporary files, and gracefully handle missing or unreadable images.
// Prompt: Develop a console application that reads barcodes from a list of file paths supplied via command line.
// Tags: barcode symbology, read, generate, png, barcodegenerator, barcodereader

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Console application that generates sample barcodes (if none provided) and reads barcodes from image files supplied via command line.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Parses command‑line arguments, optionally creates sample barcode images, and reads barcodes from each file.
    /// </summary>
    /// <param name="args">Array of file paths to process.</param>
    static void Main(string[] args)
    {
        // List to hold all file paths that will be processed
        List<string> filesToProcess = new List<string>();

        // If command‑line arguments are present, treat them as file paths
        if (args.Length > 0)
        {
            foreach (string arg in args)
            {
                if (!string.IsNullOrWhiteSpace(arg))
                {
                    filesToProcess.Add(arg);
                }
            }
        }
        else
        {
            // No arguments supplied – generate sample barcodes in a temporary folder
            string tempFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(tempFolder);

            // Define sample barcodes to generate (type, text, file name)
            var samples = new List<(BaseEncodeType encodeType, string codeText, string fileName)>
            {
                (EncodeTypes.Code128, "Sample123", "code128.png"),
                (EncodeTypes.QR, "https://example.com", "qr.png"),
                (EncodeTypes.DataMatrix, "DM12345", "datamatrix.png")
            };

            // Generate each sample barcode and add its path to the processing list
            foreach (var sample in samples)
            {
                string filePath = Path.Combine(tempFolder, sample.fileName);
                using (var generator = new BarcodeGenerator(sample.encodeType, sample.codeText))
                {
                    generator.Save(filePath, BarCodeImageFormat.Png);
                }
                filesToProcess.Add(filePath);
            }
        }

        // Iterate over each file path and attempt to read barcodes
        foreach (string filePath in filesToProcess)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Initialize the barcode reader for the current image
                using (var reader = new BarCodeReader(filePath))
                {
                    bool anyFound = false;

                    // Read all barcodes present in the image
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        anyFound = true;
                        Console.WriteLine($"{Path.GetFileName(filePath)}: {result.CodeTypeName} - {result.CodeText}");
                    }

                    // If no barcodes were detected, inform the user
                    if (!anyFound)
                    {
                        Console.WriteLine($"{Path.GetFileName(filePath)}: No barcodes detected.");
                    }
                }
            }
            // Specific handling for image loading failures
            catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
            {
                Console.WriteLine($"Unable to load image '{filePath}': {ex.Message}");
            }
            // General error handling for unexpected issues
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing '{filePath}': {ex.Message}");
            }
        }
    }
}