// Title: Read JPEG Barcode with HighQuality Preset
// Description: Demonstrates creating a Code128 barcode image in JPEG format and reading it using BarCodeReader with the HighQuality quality setting for balanced speed and accuracy.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create barcodes and BarCodeReader with QualitySettings to decode them. Typical scenarios include generating barcodes for product labeling and scanning them in high‑quality mode for reliable detection. Developers often work with these core API classes to integrate barcode workflows into .NET applications.
// Prompt: Create a BarCodeReader instance that reads JPEG images and applies HighQuality preset for balanced speed.
// Tags: barcode, code128, jpeg, highquality, generation, recognition, aspose.barcode, barcodereader, barcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates barcode generation and reading using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode JPEG, reads it with HighQuality preset, and outputs results.
    /// </summary>
    static void Main(string[] args)
    {
        // Create a temporary folder for the sample image
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string imagePath = Path.Combine(tempDir, "sample.jpg");

        // Generate a Code128 barcode and save it as a JPEG file
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Jpeg);
        }

        // Verify that the image file was successfully created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the JPEG image using BarCodeReader with the HighQuality preset
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            reader.QualitySettings = QualitySettings.HighQuality; // Balanced speed and accuracy
            BarCodeResult[] results = reader.ReadBarCodes();

            // Output the number of barcodes detected
            Console.WriteLine($"Barcodes read: {results.Length}");

            // Iterate through each result and display its type and decoded text
            foreach (var result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // Cleanup temporary files (optional)
        // Directory.Delete(tempDir, true);
    }
}