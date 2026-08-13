// Title: Decode Multiple Barcodes in a Directory with StripFNC Disabled
// Description: The example scans a folder for barcode images, decodes every barcode using Aspose.BarCode with StripFNC set to false, and prints the type and text to the console.
// Category-Description: This sample belongs to the Aspose.BarCode recognition category, demonstrating how to use BarCodeReader to process multiple image files, configure BarcodeSettings (e.g., StripFNC), and retrieve results. Typical use cases include batch processing of scanned documents, inventory verification, or automated data extraction where developers need to read all supported symbologies from a set of images.
// Prompt: Develop a console application that decodes all barcodes in a directory with StripFNC false and prints results.
// Tags: barcode, symbology, recognition, batch, console, stripfnc, aspose.barcode, decode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates decoding all barcodes in a directory with StripFNC disabled using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, scans the folder, and decodes each image.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define the folder to store and read barcode images
        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // Generate a few sample barcode images (Code128, QR, EAN13)
        GenerateSampleBarcodes(folderPath);

        // Scan the folder for image files (png, jpg, bmp)
        string[] patterns = new[] { "*.png", "*.jpg", "*.bmp" };
        var imageFiles = new System.Collections.Generic.List<string>();
        foreach (string pattern in patterns)
        {
            string[] files = Directory.GetFiles(folderPath, pattern);
            imageFiles.AddRange(files);
        }

        if (imageFiles.Count == 0)
        {
            Console.WriteLine("No barcode images found in the folder.");
            return;
        }

        // Decode each image with StripFNC set to false
        foreach (string filePath in imageFiles)
        {
            Console.WriteLine($"Decoding file: {Path.GetFileName(filePath)}");
            using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
            {
                // Ensure StripFNC is false (default, but set explicitly)
                reader.BarcodeSettings.StripFNC = false;

                // Read all barcodes in the image
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"  Type: {result.CodeTypeName}");
                    Console.WriteLine($"  CodeText: {result.CodeText}");
                }
            }
        }
    }

    // Helper method to generate sample barcode images
    private static void GenerateSampleBarcodes(string folder)
    {
        // Code128
        string code128Path = Path.Combine(folder, "code128.png");
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            generator.Save(code128Path);
        }

        // QR Code
        string qrPath = Path.Combine(folder, "qr.png");
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            generator.Save(qrPath);
        }

        // EAN13
        string ean13Path = Path.Combine(folder, "ean13.png");
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.EAN13, "1234567890128"))
        {
            generator.Save(ean13Path);
        }
    }
}