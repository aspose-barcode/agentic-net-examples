// Title: Disable Code11 checksum validation during batch barcode reading
// Description: Demonstrates how to turn off checksum verification for Code 11 barcodes when reading multiple images in a batch.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It shows how to use BarcodeGenerator to create Code 11 barcodes and BarCodeReader with BarcodeSettings to control checksum validation. Developers often need to generate barcodes in bulk and later read them without strict checksum checks, especially when dealing with legacy data or noisy scans.
// Prompt: Set BarcodeSettings.ChecksumValidation to Off to disable checksum verification for Code 11 during batch processing.
// Tags: code11, checksum, batch processing, barcode generation, barcode recognition, aspose.barcode, generation, recognition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates Code 11 barcodes, then reads them back with checksum validation disabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcode images, disables checksum validation, and reads the barcodes.
    /// </summary>
    static void Main()
    {
        // Create a folder for generated barcode images
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        Directory.CreateDirectory(outputDir);

        // Sample Code11 codetexts
        string[] codeTexts = { "12345", "67890", "112233" };

        // Generate barcode images for each codetext
        for (int i = 0; i < codeTexts.Length; i++)
        {
            string filePath = Path.Combine(outputDir, $"code11_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code11, codeTexts[i]))
            {
                // Save the barcode as a PNG image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // Read the generated barcodes with checksum validation disabled
        foreach (string file in Directory.GetFiles(outputDir, "*.png"))
        {
            using (var reader = new BarCodeReader(file, DecodeType.Code11))
            {
                // Disable checksum verification for Code 11
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Off;

                // Iterate through all detected barcodes in the image
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {Path.GetFileName(file)} | Detected CodeText: {result.CodeText}");
                }
            }
        }
    }
}