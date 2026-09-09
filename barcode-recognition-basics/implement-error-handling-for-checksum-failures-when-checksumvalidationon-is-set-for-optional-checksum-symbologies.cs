// Title: Checksum Validation Demo for Code39 Barcodes
// Description: Demonstrates generating a Code39 barcode without a checksum and reading it with different checksum validation settings.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing how to work with optional checksum symbologies. It uses BarcodeGenerator for creating barcodes and BarCodeReader for decoding, focusing on the ChecksumValidation property. Developers often need to handle checksum failures when enforcing validation, especially for symbologies like Code39 where checksums are optional.
// Prompt: Implement error handling for checksum failures when ChecksumValidation.On is set for optional checksum symbologies.
// Tags: barcode symbology, checksum validation, code39, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation and checksum validation handling using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example.
    /// Generates a Code39 barcode without checksum, reads it with default and enforced checksum validation,
    /// and handles possible checksum failures gracefully.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "ChecksumDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "Code39_NoChecksum.png");

        // Generate a Code39 barcode without checksum (optional checksum disabled)
        try
        {
            using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.Code39, "123456"))
            {
                gen.Parameters.Barcode.XDimension.Pixels = 2;
                gen.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;
                gen.Save(barcodePath, BarCodeImageFormat.Png);
                Console.WriteLine($"Barcode generated at: {barcodePath}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Barcode generation failed: {ex.Message}");
            return;
        }

        // Verify that the barcode file was created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Generated barcode file not found.");
            return;
        }

        // Read with ChecksumValidation.Default (checksum ignored) - should succeed
        Console.WriteLine("\nReading with ChecksumValidation.Default (checksum ignored):");
        try
        {
            using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.Code39))
            {
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Default;
                bool anyResult = false;
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    anyResult = true;
                    Console.WriteLine($"CodeType: {result.CodeTypeName}");
                    Console.WriteLine($"CodeText: {result.CodeText}");
                    Console.WriteLine($"1D Value: {result.Extended.OneD.Value}");
                    Console.WriteLine($"1D CheckSum: {result.Extended.OneD.CheckSum}");
                }
                if (!anyResult)
                {
                    Console.WriteLine("No barcode detected.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Reading failed: {ex.Message}");
        }

        // Read with ChecksumValidation.On (checksum enforced) - should fail due to missing checksum
        Console.WriteLine("\nReading with ChecksumValidation.On (checksum enforced):");
        try
        {
            using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.Code39))
            {
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;
                bool anyResult = false;
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    anyResult = true;
                    Console.WriteLine($"CodeType: {result.CodeTypeName}");
                    Console.WriteLine($"CodeText: {result.CodeText}");
                    Console.WriteLine($"1D Value: {result.Extended.OneD.Value}");
                    Console.WriteLine($"1D CheckSum: {result.Extended.OneD.CheckSum}");
                }
                if (!anyResult)
                {
                    // No valid barcode detected because checksum validation failed
                    Console.WriteLine("Checksum validation failed: no valid barcode detected.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Reading failed: {ex.Message}");
        }

        // Cleanup temporary files and folder (optional)
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}