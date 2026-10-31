// Title: Batch barcode generation and reading with confidence metrics
// Description: Demonstrates generating several barcode images, storing them in a temporary folder, and then reading each image to retrieve the barcode type, text, confidence, and reading quality.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes (Code128, QR, DataMatrix, PDF417, Aztec) and BarCodeReader for decoding them while accessing detailed result properties such as Confidence and ReadingQuality. Developers often need batch processing of barcodes for validation, inventory, or data capture scenarios, and this snippet provides a concise reference for those common tasks.
// Prompt: Batch read multiple barcode images from a network share, capturing Confidence and ReadingQuality for each file.
// Tags: barcode,generation,recognition,confidence,readingquality,code128,qr,datamatrix,pdf417,aztec,png,aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates batch generation of barcode images and subsequent reading of each image to capture confidence and reading quality metrics.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates sample barcodes, saves them to a temporary directory, reads each file, and outputs detailed barcode information.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BatchRead_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // List to hold the full paths of generated barcode files
        var barcodeFiles = new List<string>();

        // Define sample barcodes to generate (type, text, output file name)
        var samples = new (BaseEncodeType encode, string text, string file)[]
        {
            (EncodeTypes.Code128, "Sample001", "code128.png"),
            (EncodeTypes.QR, "https://example.com", "qr.png"),
            (EncodeTypes.DataMatrix, "DM12345", "datamatrix.png"),
            (EncodeTypes.Pdf417, "PDF417Test", "pdf417.png"),
            (EncodeTypes.Aztec, "AztecData", "aztec.png")
        };

        // Generate barcode images and store their paths
        foreach (var s in samples)
        {
            string filePath = Path.Combine(tempFolder, s.file);
            using (var generator = new BarcodeGenerator(s.encode, s.text))
            {
                // Save each barcode as a PNG image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        // Read each barcode image and capture Confidence and ReadingQuality
        foreach (string file in barcodeFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            try
            {
                // Use all supported decode types for maximum compatibility
                BaseDecodeType decodeType = DecodeType.AllSupportedTypes;
                using (var reader = new BarCodeReader(file, decodeType))
                {
                    // Iterate through all detected barcodes in the image
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"File: {Path.GetFileName(file)}");
                        Console.WriteLine($"  CodeType: {result.CodeTypeName}");
                        Console.WriteLine($"  CodeText: {result.CodeText}");
                        Console.WriteLine($"  Confidence: {result.Confidence}");
                        Console.WriteLine($"  ReadingQuality: {result.ReadingQuality}");
                    }
                }
            }
            catch (ArgumentException ex)
            {
                // Handle cases where the file cannot be processed as a barcode
                Console.WriteLine($"Failed to read {Path.GetFileName(file)}: {ex.Message}");
            }
        }

        // Optional cleanup: delete the temporary folder and its contents
        // Directory.Delete(tempFolder, true);
    }
}