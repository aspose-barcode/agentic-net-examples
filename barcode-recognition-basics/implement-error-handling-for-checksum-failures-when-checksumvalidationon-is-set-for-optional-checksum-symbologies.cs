// Title: Checksum Validation Demo for Code39 Barcodes
// Description: Shows how to generate Code39 barcodes with and without checksum and how checksum validation affects reading.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It demonstrates using BarcodeGenerator, BarCodeReader, and related settings such as EnableChecksum and ChecksumValidation. Developers often need to control checksum handling for optional checksum symbologies to ensure data integrity during scanning.
// Prompt: Implement error handling for checksum failures when ChecksumValidation.On is set for optional checksum symbologies.
// Tags: barcode, checksum, code39, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates checksum handling for optional checksum symbologies using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates sample barcodes and reads them with different checksum validation settings.
    /// </summary>
    static void Main()
    {
        // Create a temporary directory for generated barcode images
        string tempDir = Path.Combine(Path.GetTempPath(), "ChecksumDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string path = tempDir + Path.DirectorySeparatorChar;

        // Generate a barcode with checksum enabled (valid for validation On)
        string correctPath = Path.Combine(path, "code39_correct.png");
        using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.Code39, "123456"))
        {
            gen.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            gen.Save(correctPath, BarCodeImageFormat.Png);
        }

        // Generate a barcode without checksum (will fail when validation is On)
        string noChecksumPath = Path.Combine(path, "code39_nochecksum.png");
        using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.Code39, "123456"))
        {
            gen.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;
            gen.Save(noChecksumPath, BarCodeImageFormat.Png);
        }

        // Read both barcodes using the default checksum validation (should succeed for both)
        Console.WriteLine("Reading with ChecksumValidation.Default:");
        ReadBarcode(correctPath, DecodeType.Code39, ChecksumValidation.Default);
        ReadBarcode(noChecksumPath, DecodeType.Code39, ChecksumValidation.Default);

        // Read both barcodes with checksum validation forced on (barcode without checksum should fail)
        Console.WriteLine("Reading with ChecksumValidation.On:");
        ReadBarcode(correctPath, DecodeType.Code39, ChecksumValidation.On);
        ReadBarcode(noChecksumPath, DecodeType.Code39, ChecksumValidation.On);
    }

    /// <summary>
    /// Reads a barcode image using the specified decode type and checksum validation setting.
    /// </summary>
    /// <param name="filePath">Full path to the barcode image file.</param>
    /// <param name="decodeType">The symbology type to decode.</param>
    /// <param name="validation">Checksum validation mode to apply.</param>
    static void ReadBarcode(string filePath, BaseDecodeType decodeType, ChecksumValidation validation)
    {
        // Verify that the file exists before attempting to read
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"File not found: {filePath}");
            return;
        }

        // Initialize the barcode reader with the specified decode type
        using (BarCodeReader reader = new BarCodeReader(filePath, decodeType))
        {
            // Apply the requested checksum validation mode
            reader.BarcodeSettings.ChecksumValidation = validation;

            // Perform the read operation
            BarCodeResult[] results = reader.ReadBarCodes();

            // If no results are returned, indicate possible checksum failure
            if (results.Length == 0)
            {
                Console.WriteLine($"No barcode read from {Path.GetFileName(filePath)} (checksum validation may have failed).");
            }
            else
            {
                // Output details for each decoded barcode
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"File: {Path.GetFileName(filePath)}");
                    Console.WriteLine($"CodeType: {result.CodeTypeName}");
                    Console.WriteLine($"CodeText: {result.CodeText}");
                    Console.WriteLine($"1D Value: {result.Extended.OneD.Value}");
                    Console.WriteLine($"1D CheckSum: {result.Extended.OneD.CheckSum}");
                }
            }
        }
    }
}