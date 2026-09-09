// Title: Barcode Generation and Reading with Configurable Detection Settings
// Description: Demonstrates generating a QR code, saving it as PNG, and reading it back while allowing DetectEncoding and ChecksumValidation to be toggled via a simple text configuration file.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them. Developers often need to adjust settings such as encoding detection and checksum validation to handle diverse barcode data sources, making this pattern useful for configurable barcode processing pipelines.
// Prompt: Implement a configuration file allowing toggling DetectEncoding and ChecksumValidation values without recompiling the application.
// Tags: barcode, qr, configuration, detectencoding, checksumvalidation, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates barcode generation, configuration-driven reading settings, and cleanup.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare configuration file (creates default if missing)
        // ------------------------------------------------------------
        string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "barcodeConfig.txt");
        if (!File.Exists(configPath))
        {
            File.WriteAllText(configPath,
                "DetectEncoding=true\r\nChecksumValidation=On");
        }

        // ------------------------------------------------------------
        // Load configuration values into variables
        // ------------------------------------------------------------
        bool detectEncoding = true;
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
                try
                {
                    checksumValidation = (ChecksumValidation)Enum.Parse(typeof(ChecksumValidation), value, true);
                }
                catch
                {
                    // Ignore invalid enum value; keep default
                }
            }
        }

        // ------------------------------------------------------------
        // Generate a QR barcode and save it as PNG
        // ------------------------------------------------------------
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "sample.png");

        using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.QR, "Sample Text"))
        {
            gen.Parameters.Barcode.XDimension.Pixels = 4;
            gen.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Read the barcode using the configured settings
        // ------------------------------------------------------------
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.QR))
        {
            reader.BarcodeSettings.DetectEncoding = detectEncoding;
            reader.BarcodeSettings.ChecksumValidation = checksumValidation;

            Console.WriteLine("Reading barcode with settings:");
            Console.WriteLine($"DetectEncoding = {reader.BarcodeSettings.DetectEncoding}");
            Console.WriteLine($"ChecksumValidation = {reader.BarcodeSettings.ChecksumValidation}");

            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
            }
        }

        // ------------------------------------------------------------
        // Clean up temporary files and directories
        // ------------------------------------------------------------
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program exit
        }
    }
}