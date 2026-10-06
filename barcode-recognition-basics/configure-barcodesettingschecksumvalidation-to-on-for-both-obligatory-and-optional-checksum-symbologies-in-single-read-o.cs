// Title: Checksum Validation for Obligatory and Optional Symbologies in a Single Read
// Description: Demonstrates how to enable checksum validation for both obligatory and optional checksum barcode symbologies during a single read operation using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode reading and generation category, illustrating the use of BarcodeGenerator, BarCodeReader, and BarcodeSettings.ChecksumValidation. Developers often need to validate checksums for various symbologies, such as Code11 (mandatory) and Code39 (optional), to ensure data integrity when scanning barcodes. The snippet shows generating barcodes with checksum settings and reading them with validation enabled, a common task in inventory, logistics, and POS systems.
// Prompt: Configure BarcodeSettings.ChecksumValidation to On for both obligatory and optional checksum symbologies in a single read operation.
// Tags: barcode symbology,checksum validation,read operation,aspose.barcode,generation,recognition

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates configuring checksum validation for both obligatory and optional checksum symbologies in a single read operation.
/// </summary>
class Program
{
    /// <summary>
    /// Generates sample barcodes, reads them with checksum validation enabled, and outputs the results.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for sample barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "ChecksumDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Paths for generated barcodes
        string code11Path = Path.Combine(tempFolder, "code11.png");
        string code39Path = Path.Combine(tempFolder, "code39.png");

        // Generate a barcode with obligatory checksum (Code11)
        using (var gen = new BarcodeGenerator(EncodeTypes.Code11, "123456"))
        {
            gen.Parameters.Barcode.XDimension.Pixels = 2f;
            gen.Save(code11Path, BarCodeImageFormat.Png);
        }

        // Generate a barcode with optional checksum (Code39) and enable checksum generation
        using (var gen = new BarcodeGenerator(EncodeTypes.Code39, "123456"))
        {
            gen.Parameters.Barcode.XDimension.Pixels = 2f;
            gen.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            gen.Save(code39Path, BarCodeImageFormat.Png);
        }

        // List of barcode image files to read
        List<string> files = new List<string> { code11Path, code39Path };

        // Iterate over each barcode image and read its content
        foreach (string file in files)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            // Use DecodeType.AllSupportedTypes to detect any symbology
            using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
            {
                // Enable checksum validation for both obligatory and optional checksum symbologies
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                // Read all barcodes found in the image
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {Path.GetFileName(file)}");
                    Console.WriteLine($"Code Type: {result.CodeTypeName}");
                    Console.WriteLine($"Code Text: {result.CodeText}");

                    // For 1D barcodes, display checksum value if available
                    if (result.Extended?.OneD != null)
                    {
                        Console.WriteLine($"Checksum: {result.Extended.OneD.CheckSum}");
                    }

                    Console.WriteLine();
                }
            }
        }

        // Clean up temporary files and folder
        try
        {
            foreach (string file in files)
            {
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
            }

            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}