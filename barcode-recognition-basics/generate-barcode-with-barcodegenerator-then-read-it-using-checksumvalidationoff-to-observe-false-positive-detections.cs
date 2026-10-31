// Title: Generate and Read Code11 Barcode with Checksum Validation Options
// Description: Demonstrates creating a Code11 barcode using BarcodeGenerator, then reading it twice—once with default checksum validation and once with validation turned off—to illustrate how disabling validation can produce false‑positive results.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for barcode creation and BarCodeReader for decoding, focusing on the ChecksumValidation setting. Developers working with one‑dimensional symbologies often need to control checksum validation to handle imperfect scans or custom validation logic. The snippet serves as a reference for toggling checksum checks while processing barcodes in C# applications.
// Prompt: Generate a barcode with BarcodeGenerator, then read it using ChecksumValidation.Off to observe false positive detections.
// Tags: code11, barcode, generation, recognition, checksumvalidation, off, default, aspose.barcode, png, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates barcode generation and reading with different checksum validation settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code11 barcode, reads it with default and off checksum validation,
    /// and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Prepare a unique temporary folder for the barcode image.
        // ------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Path for the generated barcode image.
        string barcodePath = Path.Combine(tempFolder, "Code11.png");

        // ------------------------------------------------------------
        // 2. Generate a Code11 barcode (valid checksum) and save as PNG.
        // ------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code11, "123456"))
        {
            // Set the X-dimension (module width) to 2 pixels for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // 3. Read the barcode with default checksum validation (should succeed).
        // ------------------------------------------------------------
        Console.WriteLine("Reading with ChecksumValidation.Default:");
        using (BarCodeReader readerDefault = new BarCodeReader(barcodePath, DecodeType.Code11))
        {
            // Use the default checksum validation behavior.
            readerDefault.BarcodeSettings.ChecksumValidation = ChecksumValidation.Default;

            // Iterate through all detected barcodes (only one expected).
            foreach (BarCodeResult result in readerDefault.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"Checksum: {result.Extended.OneD.CheckSum}");
            }
        }

        // ------------------------------------------------------------
        // 4. Read the same barcode with checksum validation turned off.
        //    This still succeeds, demonstrating a possible false‑positive detection.
        // ------------------------------------------------------------
        Console.WriteLine("\nReading with ChecksumValidation.Off:");
        using (BarCodeReader readerOff = new BarCodeReader(barcodePath, DecodeType.Code11))
        {
            // Disable checksum validation.
            readerOff.BarcodeSettings.ChecksumValidation = ChecksumValidation.Off;

            foreach (BarCodeResult result in readerOff.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"Checksum: {result.Extended.OneD.CheckSum}");
            }
        }

        // ------------------------------------------------------------
        // 5. Clean up temporary files and folder.
        // ------------------------------------------------------------
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);

            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored – cleanup failures should not affect program outcome.
        }
    }
}