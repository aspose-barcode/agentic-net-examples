// Title: Validate AllowIncorrectBarcodes does not affect confidence for correct barcodes
// Description: Demonstrates generating a Code128 barcode, reading it with different AllowIncorrectBarcodes settings, and confirming that the confidence score remains unchanged.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating how to configure QualitySettings (AllowIncorrectBarcodes) while using BarCodeReader. Developers often need to control tolerance for imperfect barcodes without impacting confidence metrics for valid scans. The sample shows typical usage of BarcodeGenerator, BarCodeReader, and BarCodeResult classes.
// Prompt: Validate that setting AllowIncorrectBarcodes to true does not affect confidence of correctly decoded barcodes.
// Tags: code128, barcode, confidence, allowincorrectbarcodes, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates that enabling AllowIncorrectBarcodes does not change the confidence of correctly decoded barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, reads it with different settings, and compares confidence values.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the generated image
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "code128.png");

        // Generate a correct Code128 barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Read the barcode with AllowIncorrectBarcodes set to false
        BarCodeConfidence? confidenceFalse = ReadConfidence(imagePath, false);
        // Read the same barcode with AllowIncorrectBarcodes set to true
        BarCodeConfidence? confidenceTrue = ReadConfidence(imagePath, true);

        // Output the confidence values for both settings
        Console.WriteLine($"Confidence (AllowIncorrectBarcodes = false): {confidenceFalse}");
        Console.WriteLine($"Confidence (AllowIncorrectBarcodes = true):  {confidenceTrue}");

        // Compare the confidence values to verify they are identical
        if (confidenceFalse.HasValue && confidenceTrue.HasValue && confidenceFalse.Value == confidenceTrue.Value)
        {
            Console.WriteLine("Confidence unchanged by AllowIncorrectBarcodes setting.");
        }
        else
        {
            Console.WriteLine("Confidence changed by AllowIncorrectBarcodes setting.");
        }

        // Clean up temporary files and folder
        try
        {
            if (File.Exists(imagePath))
                File.Delete(imagePath);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect validation
        }
    }

    /// <summary>
    /// Reads a barcode from the specified file and returns its confidence value.
    /// </summary>
    /// <param name="path">Full path to the barcode image file.</param>
    /// <param name="allowIncorrect">Whether to allow incorrect barcodes during recognition.</param>
    /// <returns>Confidence of the first detected barcode, or null if none found.</returns>
    static BarCodeConfidence? ReadConfidence(string path, bool allowIncorrect)
    {
        // Verify that the file exists before attempting to read
        if (!File.Exists(path))
        {
            Console.WriteLine($"File not found: {path}");
            return null;
        }

        // Initialize the barcode reader for Code128 symbology
        using (var reader = new BarCodeReader(path, DecodeType.Code128))
        {
            // Apply the AllowIncorrectBarcodes setting
            reader.QualitySettings.AllowIncorrectBarcodes = allowIncorrect;

            // Perform the read operation
            BarCodeResult[] results = reader.ReadBarCodes();

            // Return the confidence of the first result if available
            if (results != null && results.Length > 0)
            {
                return results[0].Confidence;
            }
            else
            {
                Console.WriteLine("No barcode detected.");
                return null;
            }
        }
    }
}