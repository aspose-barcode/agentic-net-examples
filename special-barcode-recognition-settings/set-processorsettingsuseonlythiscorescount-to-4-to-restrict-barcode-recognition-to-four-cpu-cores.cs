// Title: Restrict Barcode Recognition to Specific CPU Cores
// Description: Demonstrates how to limit Aspose.BarCode recognition to a fixed number of CPU cores using ProcessorSettings.
// Category-Description: This example belongs to the Aspose.BarCode recognition configuration category. It shows how to control multi‑core processing via the ProcessorSettings API, a common requirement when optimizing performance or managing resources in server environments. Developers often need to adjust core usage to balance throughput and CPU load when processing large batches of images.
// Prompt: Set ProcessorSettings.UseOnlyThisCoresCount to 4 to restrict barcode recognition to four CPU cores.
// Tags: barcode symbology, recognition, multithreading, core count, aspose.barcode, processorsettings, qualitysettings

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that restricts barcode recognition to a specific number of CPU cores
/// and demonstrates barcode generation and reading using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Configures core usage, ensures a sample barcode image exists, and reads barcodes from it.
    /// </summary>
    static void Main()
    {
        // Restrict barcode recognition to four CPU cores
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = 4;

        // Path to the barcode image file
        string imagePath = "sample_barcode.png";

        // Generate a sample barcode image if it does not already exist
        if (!File.Exists(imagePath))
        {
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Save the generated barcode as a PNG file
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }
        }

        // Verify that the image file is present before attempting recognition
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Perform barcode recognition using the configured core count
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Apply a high‑performance quality preset for faster processing
            reader.QualitySettings = QualitySettings.HighPerformance;

            // Iterate through all detected barcodes and output their details
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text: {result.CodeText}");
                Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
            }
        }
    }
}