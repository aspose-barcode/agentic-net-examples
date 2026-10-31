// Title: Toggle Barcode Reader Settings via Configuration File
// Description: Demonstrates reading a configuration file to set DetectEncoding and ChecksumValidation for the Aspose.BarCode reader without recompiling the application.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create a QR code and BarCodeReader to decode it, highlighting how developers can control reader behavior (encoding detection and checksum validation) via external configuration. Typical scenarios include dynamic adjustment of decoding options in production environments without code changes.
// Prompt: Implement a configuration file allowing toggling DetectEncoding and ChecksumValidation values without recompiling the application.
// Tags: barcode, symbology, configuration, detection, checksum, aspose.barcode, generation, recognition, qr, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample program that generates a QR code, reads configuration settings,
/// applies them to the barcode reader, and decodes the generated image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Step 1: Prepare configuration file with default values
        // ------------------------------------------------------------
        string configPath = Path.Combine(Path.GetTempPath(), "barcode_config.txt");
        if (!File.Exists(configPath))
        {
            File.WriteAllText(configPath,
                "DetectEncoding=true\r\nChecksumValidation=On");
        }

        // ------------------------------------------------------------
        // Step 2: Load configuration values
        // ------------------------------------------------------------
        bool detectEncoding = false;
        ChecksumValidation checksumValidation = ChecksumValidation.Default;

        foreach (string line in File.ReadAllLines(configPath))
        {
            if (string.IsNullOrWhiteSpace(line) || !line.Contains("="))
                continue;

            string[] parts = line.Split(new[] { '=' }, 2);
            string key = parts[0].Trim();
            string value = parts[1].Trim();

            if (key.Equals("DetectEncoding", StringComparison.OrdinalIgnoreCase))
            {
                if (bool.TryParse(value, out bool boolVal))
                    detectEncoding = boolVal;
            }
            else if (key.Equals("ChecksumValidation", StringComparison.OrdinalIgnoreCase))
            {
                if (Enum.TryParse<ChecksumValidation>(value, true, out var enumVal))
                    checksumValidation = enumVal;
            }
        }

        // ------------------------------------------------------------
        // Step 3: Create temporary folder for barcode image
        // ------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(),
            "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // ------------------------------------------------------------
        // Step 4: Generate a sample QR code image
        // ------------------------------------------------------------
        using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.QR, "Sample Text"))
        {
            gen.Parameters.Barcode.XDimension.Pixels = 4;
            gen.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Step 5: Read the barcode using configuration-driven settings
        // ------------------------------------------------------------
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.QR))
        {
            reader.BarcodeSettings.DetectEncoding = detectEncoding;
            reader.BarcodeSettings.ChecksumValidation = checksumValidation;

            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
            }
        }

        // ------------------------------------------------------------
        // Step 6: Clean up temporary files
        // ------------------------------------------------------------
        try
        {
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program exit
        }
    }
}