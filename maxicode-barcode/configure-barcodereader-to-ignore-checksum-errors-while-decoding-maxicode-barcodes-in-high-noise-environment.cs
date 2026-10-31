// Title: Decode MaxiCode barcode while ignoring checksum errors
// Description: Demonstrates configuring Aspose.BarCode's BarCodeReader to bypass checksum validation when reading MaxiCode barcodes, useful in noisy environments.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating how to use BarCodeReader and BarcodeGenerator for MaxiCode symbology. It shows typical use cases such as generating a barcode image, configuring reader settings to ignore checksum errors, and handling high‑noise scenarios where barcode quality may be compromised. Developers working with barcode scanning, especially in industrial or logistics applications, often need to adjust checksum validation and quality settings to improve read rates.
// Prompt: Configure BarcodeReader to ignore checksum errors while decoding MaxiCode barcodes in a high‑noise environment.
// Tags: maxicode, checksum, barcode reading, ignore checksum, aspose.barcode, decode, high-noise, barcode generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a MaxiCode barcode and reading it while disabling checksum validation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a MaxiCode barcode, reads it with checksum validation turned off,
    /// and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "MaxiCodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "maxicode.png");

        // Generate a sample MaxiCode barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "SampleData"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the MaxiCode barcode while ignoring checksum errors
        using (var reader = new BarCodeReader(barcodePath, DecodeType.MaxiCode))
        {
            // Disable checksum validation to tolerate errors in noisy scans
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Off;

            // Allow reading of barcodes that may be incorrectly formed due to noise
            reader.QualitySettings.AllowIncorrectBarcodes = true;

            // Perform the decoding operation
            BarCodeResult[] results = reader.ReadBarCodes();
            Console.WriteLine($"Barcodes read: {results.Length}");

            // Output each decoded result
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }
        }

        // Clean up temporary files and directory
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