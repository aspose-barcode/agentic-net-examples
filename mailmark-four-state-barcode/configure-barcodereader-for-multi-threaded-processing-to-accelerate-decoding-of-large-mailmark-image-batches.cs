// Title: Multi‑threaded Mailmark Barcode Decoding with BarCodeReader
// Description: Generates sample Mailmark barcodes, saves them as PNG files, and decodes them using BarCodeReader configured to utilize all CPU cores for faster batch processing.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, demonstrating how to configure BarCodeReader for parallel processing. It showcases the use of ComplexBarcodeGenerator for creating Mailmark barcodes and BarCodeReader with ProcessorSettings to leverage multi‑core CPUs. Developers often need to decode large sets of images efficiently, and this pattern provides a scalable solution.
// Prompt: Configure BarCodeReader for multi‑threaded processing to accelerate decoding of large Mailmark image batches.
// Tags: mailmark, barcode, decoding, multithreading, aspose.barcode, complexbarcodegenerator, barcodereader, processorsettings

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generating Mailmark barcodes and decoding them in parallel using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates sample Mailmark images, configures the reader for multi‑core processing,
    /// and outputs decoded results to the console.
    /// </summary>
    static void Main()
    {
        // Define folder for sample Mailmark images
        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "MailmarkSamples");
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // Generate a few sample Mailmark barcodes (self‑contained example)
        const int sampleCount = 5;
        for (int i = 0; i < sampleCount; i++)
        {
            // Create a valid MailmarkCodetext instance with unique ItemID
            var mailmark = new MailmarkCodetext
            {
                Format = 4,                     // 4‑state Mailmark
                VersionID = 1,
                Class = "0",
                SupplychainID = 384224,
                ItemID = 16563762 + i,          // vary ItemID to make each barcode unique
                DestinationPostCodePlusDPS = "EF61AH8T " // trailing space is required
            };

            // Generate the barcode image and save to file
            string filePath = Path.Combine(folderPath, $"Mailmark_{i + 1}.png");
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                using (var stream = new MemoryStream())
                {
                    generator.Save(stream, BarCodeImageFormat.Png);
                    File.WriteAllBytes(filePath, stream.ToArray());
                }
            }
        }

        // Configure BarCodeReader to use all available processor cores
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Environment.ProcessorCount;

        // Process the generated images using multi‑threaded decoding
        string[] imageFiles = Directory.GetFiles(folderPath, "*.png");
        foreach (string imagePath in imageFiles)
        {
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"File not found: {imagePath}");
                continue;
            }

            // Create a reader for Mailmark symbology
            using (var reader = new BarCodeReader(imagePath, DecodeType.Mailmark))
            {
                // Optional: allow decoding of slightly damaged barcodes
                reader.QualitySettings.AllowIncorrectBarcodes = true;

                try
                {
                    // Read and output each decoded barcode
                    foreach (var result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"File: {Path.GetFileName(imagePath)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error reading '{imagePath}': {ex.Message}");
                }
            }
        }

        Console.WriteLine("Processing completed.");
    }
}