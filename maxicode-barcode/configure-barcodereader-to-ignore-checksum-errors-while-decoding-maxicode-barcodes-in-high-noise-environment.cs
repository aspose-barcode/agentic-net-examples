// Title: Decode MaxiCode barcode while ignoring checksum errors in noisy conditions
// Description: Demonstrates configuring Aspose.BarCode's BarCodeReader to bypass checksum validation and use high‑performance settings when decoding MaxiCode barcodes in a high‑noise environment.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating how to adjust BarCodeReader settings such as ChecksumValidation, QualitySettings, and DeconvolutionMode for robust decoding of MaxiCode symbology. Developers working with barcode scanning in challenging image conditions can learn to disable checksum checks, enable high‑performance mode, and allow incorrect barcodes to improve detection rates.
// Prompt: Configure BarcodeReader to ignore checksum errors while decoding MaxiCode barcodes in a high‑noise environment.
// Tags: maxicode, checksumvalidation, high-noise, barcodereader, qualitysettings, aspnet.barcode, barcode recognition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates decoding a MaxiCode barcode while ignoring checksum errors and using high‑noise settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the demo
        string tempFolder = Path.Combine(Path.GetTempPath(), "MaxiCodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "maxicode.png");

        // Generate a simple MaxiCode barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "123456"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify the generated file exists before attempting to read it
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the MaxiCode barcode while ignoring checksum errors and using high‑noise settings
        using (var reader = new BarCodeReader(barcodePath, DecodeType.MaxiCode))
        {
            // Disable checksum validation to ignore checksum errors
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Off;

            // Configure quality settings for a noisy environment
            reader.QualitySettings = QualitySettings.HighPerformance;
            reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;
            reader.QualitySettings.AllowIncorrectBarcodes = true;

            // Perform the barcode detection
            BarCodeResult[] results = reader.ReadBarCodes();

            // Output the results
            if (results.Length == 0)
            {
                Console.WriteLine("No barcodes were detected.");
            }
            else
            {
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"Code Type: {result.CodeTypeName}");
                    Console.WriteLine($"Code Text: {result.CodeText}");
                }
            }
        }

        // Clean up temporary files
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program outcome
        }
    }
}