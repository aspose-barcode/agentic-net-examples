// Title: Validate AllowIncorrectBarcodes does not affect decoded CodeText
// Description: Demonstrates generating a Code128 barcode, reading it with AllowIncorrectBarcodes set to false and true, and confirming the decoded text remains unchanged.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating how QualitySettings.AllowIncorrectBarcodes influences decoding. It uses BarcodeGenerator for creation and BarCodeReader for recognition, common tasks when validating barcode tolerance settings. Developers often need to ensure that enabling tolerance does not modify results for valid barcodes.
// Prompt: Validate that enabling AllowIncorrectBarcodes does not alter the decoded CodeText of valid barcodes.
// Tags: code128, allowincorrectbarcodes, barcoderecognition, generation, validation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates that setting AllowIncorrectBarcodes does not change the decoded text of a valid barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, reads it with different AllowIncorrectBarcodes settings, and compares the results.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for test artifacts
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file path and original barcode text
        string barcodePath = Path.Combine(tempFolder, "code128.png");
        string originalText = "AsposeTest";

        // Generate a valid Code128 barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, originalText))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Read the barcode with AllowIncorrectBarcodes set to false
        string textWithoutAllow;
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            reader.QualitySettings.AllowIncorrectBarcodes = false;
            var results = reader.ReadBarCodes();
            textWithoutAllow = results.Length > 0 ? results[0].CodeText : null;
        }

        // Read the same barcode with AllowIncorrectBarcodes set to true
        string textWithAllow;
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            reader.QualitySettings.AllowIncorrectBarcodes = true;
            var results = reader.ReadBarCodes();
            textWithAllow = results.Length > 0 ? results[0].CodeText : null;
        }

        // Validate that the decoded texts are identical
        bool isEqual = string.Equals(textWithoutAllow, textWithAllow, StringComparison.Ordinal);
        Console.WriteLine($"Decoded without AllowIncorrectBarcodes: {textWithoutAllow ?? "null"}");
        Console.WriteLine($"Decoded with AllowIncorrectBarcodes:    {textWithAllow ?? "null"}");
        Console.WriteLine($"Texts are equal: {isEqual}");

        // Clean up temporary files and folder
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect validation
        }
    }
}