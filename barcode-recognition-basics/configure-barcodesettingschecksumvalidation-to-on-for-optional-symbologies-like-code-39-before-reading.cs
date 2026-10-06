// Title: Configure Checksum Validation for Code 39 Barcode Reading
// Description: Demonstrates how to enable checksum validation when generating and reading a Code 39 barcode using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create a barcode with an optional checksum and BarCodeReader with BarcodeSettings to control checksum validation during decoding. Developers working with 1D symbologies often need to toggle checksum validation to ensure data integrity, making this pattern common in inventory, shipping, and point‑of‑sale applications.
// Prompt: Configure BarcodeSettings.ChecksumValidation to On for optional symbologies like Code 39 before reading.
// Tags: barcode symbology, checksum validation, code39, generation, recognition, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code 39 barcode with an optional checksum
/// and demonstrates reading it with different checksum validation settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, reads it with default and
    /// explicit checksum validation, and then cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the demo files
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);
        string barcodePath = Path.Combine(outputDir, "Code39.png");

        // Generate a Code 39 barcode with optional checksum enabled
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code39, "123456"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read barcode with default checksum validation (no explicit enforcement)
        Console.WriteLine("ReadChecksumCode39: Default");
        using (BarCodeReader readerDefault = new BarCodeReader(barcodePath, DecodeType.Code39))
        {
            readerDefault.BarcodeSettings.ChecksumValidation = ChecksumValidation.Default;
            foreach (BarCodeResult result in readerDefault.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"1D Value: {result.Extended.OneD.Value}");
                Console.WriteLine($"1D CheckSum: {result.Extended.OneD.CheckSum}");
            }
        }

        // Read barcode with checksum validation turned on (enforces checksum check)
        Console.WriteLine("ReadChecksumCode39: On");
        using (BarCodeReader readerOn = new BarCodeReader(barcodePath, DecodeType.Code39))
        {
            readerOn.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;
            foreach (BarCodeResult result in readerOn.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"1D Value: {result.Extended.OneD.Value}");
                Console.WriteLine($"1D CheckSum: {result.Extended.OneD.CheckSum}");
            }
        }

        // Clean up temporary files and directory
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(outputDir);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}