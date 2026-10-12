// Title: Barcode generation and reading with configurable detection settings
// Description: Demonstrates creating a QR code, saving it, and reading it using Aspose.BarCode with DetectEncoding and ChecksumValidation values loaded from a simple text configuration file.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category, illustrating how to externalize barcode reader settings such as DetectEncoding and ChecksumValidation. It uses BarcodeGenerator, BarCodeReader, and related settings classes, typical for scenarios where runtime toggling of decoding behavior is required without recompiling. Developers often need to adjust these options based on input data characteristics or compliance requirements.
// Prompt: Implement a configuration file allowing toggling DetectEncoding and ChecksumValidation values without recompiling the application.
// Tags: barcode, qr, configuration, detectencoding, checksumvalidation, generation, recognition, aspose.barcode

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a QR code, persisting it, and reading it back with
/// detection settings loaded from an external configuration file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary configuration file if missing,
    /// reads its values, generates a QR code, reads it using the configured settings,
    /// and finally cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare configuration file (creates default if it does not exist)
        // --------------------------------------------------------------------
        string configFile = Path.Combine(Path.GetTempPath(), "barcode_config.txt");
        if (!File.Exists(configFile))
        {
            var defaultConfig = new StringBuilder();
            defaultConfig.AppendLine("DetectEncoding=true");
            defaultConfig.AppendLine("ChecksumValidation=Default");
            File.WriteAllText(configFile, defaultConfig.ToString());
            Console.WriteLine($"Created default config at: {configFile}");
        }

        // --------------------------------------------------------------------
        // Load configuration values into local variables
        // --------------------------------------------------------------------
        bool detectEncoding = true;
        ChecksumValidation checksumValidation = ChecksumValidation.Default;

        foreach (var line in File.ReadAllLines(configFile))
        {
            var parts = line.Split('=', 2);
            if (parts.Length != 2) continue;

            var key = parts[0].Trim();
            var value = parts[1].Trim();

            if (key.Equals("DetectEncoding", StringComparison.OrdinalIgnoreCase))
            {
                if (bool.TryParse(value, out bool parsedBool))
                {
                    detectEncoding = parsedBool;
                }
            }
            else if (key.Equals("ChecksumValidation", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    checksumValidation = (ChecksumValidation)Enum.Parse(typeof(ChecksumValidation), value, true);
                }
                catch
                {
                    // Invalid enum value – keep default
                }
            }
        }

        // --------------------------------------------------------------------
        // Set up temporary directory for the sample barcode image
        // --------------------------------------------------------------------
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "sample_qr.png");

        // --------------------------------------------------------------------
        // Generate a QR code barcode and save it as PNG
        // --------------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Sample Text"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Read the barcode using settings from the configuration file
        // --------------------------------------------------------------------
        BaseDecodeType decodeType = DecodeType.QR;
        using (BarCodeReader reader = new BarCodeReader(barcodePath, decodeType))
        {
            // Apply configuration to the reader
            reader.BarcodeSettings.DetectEncoding = detectEncoding;
            reader.BarcodeSettings.ChecksumValidation = checksumValidation;

            Console.WriteLine("Reading barcode with settings:");
            Console.WriteLine($"DetectEncoding = {reader.BarcodeSettings.DetectEncoding}");
            Console.WriteLine($"ChecksumValidation = {reader.BarcodeSettings.ChecksumValidation}");

            // Iterate through detected barcodes (only one expected)
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
            }
        }

        // --------------------------------------------------------------------
        // Cleanup temporary files and directory
        // --------------------------------------------------------------------
        try
        {
            if (File.Exists(barcodePath))
            {
                File.Delete(barcodePath);
            }
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignored – cleanup failures should not crash the program
        }
    }
}