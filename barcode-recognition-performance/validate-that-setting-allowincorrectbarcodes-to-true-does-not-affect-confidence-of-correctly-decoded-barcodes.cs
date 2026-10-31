// Title: Validate AllowIncorrectBarcodes does not affect confidence of a correctly decoded barcode
// Description: Generates a Code128 barcode, reads it with AllowIncorrectBarcodes set to false and true, and verifies that the confidence level remains unchanged for a valid barcode.
// Category-Description: This example belongs to the Aspose.BarCode reading and quality‑settings category. It demonstrates how to use BarcodeGenerator to create barcodes, BarCodeReader with QualitySettings to control tolerance for incorrect barcodes, and BarCodeResult.Confidence to assess decoding reliability. Developers often need to ensure that enabling tolerance does not degrade confidence for correctly decoded symbols.
// Prompt: Validate that setting AllowIncorrectBarcodes to true does not affect confidence of correctly decoded barcodes.
// Tags: barcode, code128, confidence, allowincorrectbarcodes, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates validation that the AllowIncorrectBarcodes quality setting does not change the confidence
/// of a correctly decoded barcode when toggled between false and true.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, reads it with different AllowIncorrectBarcodes settings,
    /// compares confidence values, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a temporary folder for the barcode image
        // --------------------------------------------------------------------
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodeFile = Path.Combine(tempDir, "code128.png");

        // --------------------------------------------------------------------
        // Generate a correct Code128 barcode and save it as PNG
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ASPOSE123"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Save(barcodeFile, BarCodeImageFormat.Png);
        }

        // Verify that the image was created
        if (!File.Exists(barcodeFile))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // --------------------------------------------------------------------
        // Read the barcode with AllowIncorrectBarcodes = false
        // --------------------------------------------------------------------
        BarCodeConfidence confidenceFalse = BarCodeConfidence.None;
        using (var reader = new BarCodeReader(barcodeFile, DecodeType.Code128))
        {
            reader.QualitySettings.AllowIncorrectBarcodes = false;
            BarCodeResult[] results = reader.ReadBarCodes();
            if (results.Length > 0)
                confidenceFalse = results[0].Confidence;
            else
                Console.WriteLine("No barcode detected with AllowIncorrectBarcodes = false.");
        }

        // --------------------------------------------------------------------
        // Read the same barcode with AllowIncorrectBarcodes = true
        // --------------------------------------------------------------------
        BarCodeConfidence confidenceTrue = BarCodeConfidence.None;
        using (var reader = new BarCodeReader(barcodeFile, DecodeType.Code128))
        {
            reader.QualitySettings.AllowIncorrectBarcodes = true;
            BarCodeResult[] results = reader.ReadBarCodes();
            if (results.Length > 0)
                confidenceTrue = results[0].Confidence;
            else
                Console.WriteLine("No barcode detected with AllowIncorrectBarcodes = true.");
        }

        // --------------------------------------------------------------------
        // Output the confidence values for both reads
        // --------------------------------------------------------------------
        Console.WriteLine($"Confidence with AllowIncorrectBarcodes = false: {confidenceFalse}");
        Console.WriteLine($"Confidence with AllowIncorrectBarcodes = true : {confidenceTrue}");

        // --------------------------------------------------------------------
        // Validate that confidence is unchanged and strong
        // --------------------------------------------------------------------
        if (confidenceFalse == confidenceTrue && confidenceFalse == BarCodeConfidence.Strong)
            Console.WriteLine("Validation passed: confidence unchanged for correctly decoded barcode.");
        else
            Console.WriteLine("Validation failed: confidence differs or is not Strong.");

        // --------------------------------------------------------------------
        // Cleanup temporary files and directory
        // --------------------------------------------------------------------
        try
        {
            File.Delete(barcodeFile);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}