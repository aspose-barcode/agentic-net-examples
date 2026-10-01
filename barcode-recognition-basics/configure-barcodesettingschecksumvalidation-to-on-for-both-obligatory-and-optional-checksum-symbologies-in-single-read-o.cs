// Title: Barcode checksum validation during read operation
// Description: Demonstrates enabling checksum validation for both obligatory and optional checksum symbologies when reading a barcode.
// Category-Description: This example belongs to the Aspose.BarCode reading category, showcasing how to configure BarcodeSettings.ChecksumValidation to ensure data integrity across all supported symbologies. It highlights the use of BarCodeReader, BarcodeSettings, and related result classes, which developers commonly employ when validating scanned barcodes in inventory, logistics, or point‑of‑sale systems. The snippet serves as a reference for configuring checksum checks in bulk read scenarios.
/// Prompt: Configure BarcodeSettings.ChecksumValidation to On for both obligatory and optional checksum symbologies in a single read operation.
/// Tags: barcode symbology, checksum validation, read operation, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a barcode (if needed) and reads it with checksum validation enabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode image if it does not exist,
    /// then reads the image while enforcing checksum validation for all symbologies.
    /// </summary>
    static void Main()
    {
        // Define a temporary file path for the barcode image
        string imagePath = Path.Combine(Path.GetTempPath(), "checksum_demo.png");

        // Ensure the barcode image exists; generate it if missing
        if (!File.Exists(imagePath))
        {
            // Create a Code128 barcode (has an obligatory checksum) with sample data
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
            {
                // Save the generated barcode as a PNG file
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }
        }

        // Initialize a barcode reader for the generated image, targeting Code128 symbology
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Enable checksum validation for both obligatory and optional checksum symbologies
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            // Execute the read operation and iterate through all detected barcodes
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Output the decoded text, type, and reading quality to the console
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"ReadingQuality: {result.ReadingQuality}");
            }
        }
    }
}