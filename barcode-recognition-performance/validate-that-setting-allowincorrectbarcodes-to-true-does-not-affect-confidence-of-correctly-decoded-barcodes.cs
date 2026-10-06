// Title: Validate AllowIncorrectBarcodes does not affect confidence for correct barcodes
// Description: Demonstrates how to generate a Code128 barcode, read it with AllowIncorrectBarcodes set to false and true, and compare the confidence values to ensure they remain unchanged.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating the use of BarCodeReader's QualitySettings, specifically the AllowIncorrectBarcodes property. It shows typical usage for developers who need to control tolerance for incorrect barcodes while preserving confidence metrics, useful in validation and quality assurance scenarios.
// Prompt: Validate that setting AllowIncorrectBarcodes to true does not affect confidence of correctly decoded barcodes.
// Tags: code128, barcode, confidence, allowincorrectbarcodes, recognition, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates validation that setting AllowIncorrectBarcodes to true does not affect the confidence of correctly decoded barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates a barcode, reads it with different AllowIncorrectBarcodes settings, and compares confidence values.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary file path for the generated barcode image.
        string tempPath = Path.Combine(Path.GetTempPath(), "tempBarcode.png");

        // Generate a correct Code128 barcode and save it as PNG.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Save(tempPath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was created successfully.
        if (!File.Exists(tempPath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode with AllowIncorrectBarcodes set to false and capture confidence.
        BarCodeConfidence confidenceFalse = BarCodeConfidence.None;
        using (var reader = new BarCodeReader(tempPath, DecodeType.Code128))
        {
            reader.QualitySettings.AllowIncorrectBarcodes = false;
            BarCodeResult[] results = reader.ReadBarCodes();
            if (results.Length > 0)
                confidenceFalse = results[0].Confidence;
        }

        // Read the same barcode with AllowIncorrectBarcodes set to true and capture confidence.
        BarCodeConfidence confidenceTrue = BarCodeConfidence.None;
        using (var reader = new BarCodeReader(tempPath, DecodeType.Code128))
        {
            reader.QualitySettings.AllowIncorrectBarcodes = true;
            BarCodeResult[] results = reader.ReadBarCodes();
            if (results.Length > 0)
                confidenceTrue = results[0].Confidence;
        }

        // Output the confidence values for comparison.
        Console.WriteLine($"Confidence with AllowIncorrectBarcodes = false: {confidenceFalse}");
        Console.WriteLine($"Confidence with AllowIncorrectBarcodes = true : {confidenceTrue}");

        // Determine whether confidence remained unchanged.
        if (confidenceFalse == confidenceTrue)
            Console.WriteLine("Confidence unchanged for correctly decoded barcode.");
        else
            Console.WriteLine("Confidence differs; check implementation.");

        // Clean up the temporary barcode image file.
        try
        {
            File.Delete(tempPath);
        }
        catch
        {
            // Ignore any errors that occur during cleanup.
        }
    }
}