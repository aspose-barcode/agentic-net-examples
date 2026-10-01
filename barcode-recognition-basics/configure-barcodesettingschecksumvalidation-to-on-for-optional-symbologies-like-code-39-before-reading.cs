// Title: Enable Checksum Validation for Code 39 Barcode Generation and Reading
// Description: Demonstrates how to generate a Code 39 barcode with an optional checksum and then read it back with checksum validation turned on.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for decoding, and the ChecksumValidation setting for optional symbologies such as Code 39. Developers often need to ensure data integrity by enabling checksum generation and validation when working with optional checksum symbologies.
// Prompt: Configure BarcodeSettings.ChecksumValidation to On for optional symbologies like Code 39 before reading.
// Tags: code39, checksum, generation, recognition, png, aspose.barcode, barcodegenerator, barcodereader

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a Code 39 barcode with checksum and reading it with checksum validation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a temporary folder, generates a barcode, and reads it back with checksum validation.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Path for the generated barcode image
        string barcodePath = Path.Combine(tempFolder, "code39.png");

        // Generate a Code39 barcode with checksum enabled
        GenerateCode39Barcode(barcodePath, "ABC123");

        // Read the barcode with checksum validation turned on
        ReadBarcodeWithChecksumValidation(barcodePath);
    }

    /// <summary>
    /// Generates a Code 39 barcode image with an optional checksum.
    /// </summary>
    /// <param name="filePath">Full path where the PNG image will be saved.</param>
    /// <param name="codeText">The text to encode in the barcode.</param>
    static void GenerateCode39Barcode(string filePath, string codeText)
    {
        // Ensure the target directory exists
        string dir = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        // Initialize the generator for Code39 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, codeText))
        {
            // Enable checksum generation for Code39 (optional checksum)
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Show the checksum in the human‑readable text (optional)
            generator.Parameters.Barcode.ChecksumAlwaysShow = true;

            // Save the barcode image as PNG
            generator.Save(filePath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode generated at: {filePath}");
    }

    /// <summary>
    /// Reads a barcode image and validates its checksum for optional symbologies like Code39.
    /// </summary>
    /// <param name="filePath">Full path to the barcode image file.</param>
    static void ReadBarcodeWithChecksumValidation(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine("Barcode image not found.");
            return;
        }

        // Create a reader configured for Code39 symbology
        using (var reader = new BarCodeReader(filePath, DecodeType.Code39))
        {
            // Enable checksum validation (On) for optional symbologies like Code39
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            try
            {
                // Iterate through all detected barcodes
                foreach (var result in reader.ReadBarCodes())
                {
                    // If a result is returned, the checksum has passed
                    Console.WriteLine($"Decoded CodeText: {result.CodeText}");
                    Console.WriteLine($"Decoded Symbology: {result.CodeTypeName}");
                }
            }
            catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
            {
                Console.WriteLine($"Failed to load image: {ex.Message}");
            }
        }
    }
}