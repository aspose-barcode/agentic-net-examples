// Title: Validate AllowIncorrectBarcodes does not affect decoded CodeText
// Description: This example generates a Code128 barcode, decodes it twice—once with AllowIncorrectBarcodes disabled and once enabled—to confirm that the decoded text remains unchanged for a valid barcode.
// Category-Description: Demonstrates Aspose.BarCode barcode generation and recognition, focusing on the QualitySettings.AllowIncorrectBarcodes property. It belongs to the barcode validation and error‑tolerance category, where developers generate barcodes, read them under different quality settings, and ensure consistent decoding results. Typical use cases include testing scanner tolerance and verifying that permissive settings do not alter correct data.
// Prompt: Validate that enabling AllowIncorrectBarcodes does not alter the decoded CodeText of valid barcodes.
// Tags: code128, allowincorrectbarcodes, barcodevalidation, generation, recognition, qualitysettings

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates that enabling AllowIncorrectBarcodes does not change the decoded CodeText of a valid Code128 barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, reads it with different AllowIncorrectBarcodes settings, and compares the results.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "AllowIncorrectTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the output file path and the text to encode
        string barcodePath = Path.Combine(tempFolder, "code128.png");
        string codeText = "ValidCode123";

        // Generate a valid Code128 barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Decode the barcode with AllowIncorrectBarcodes set to false
        string decodedFalse;
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            reader.QualitySettings.AllowIncorrectBarcodes = false;
            var results = reader.ReadBarCodes();
            decodedFalse = results.Length > 0 ? results[0].CodeText : null;
        }

        // Decode the same barcode with AllowIncorrectBarcodes set to true
        string decodedTrue;
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            reader.QualitySettings.AllowIncorrectBarcodes = true;
            var results = reader.ReadBarCodes();
            decodedTrue = results.Length > 0 ? results[0].CodeText : null;
        }

        // Compare the decoded texts to ensure they are identical
        bool isEqual = string.Equals(decodedFalse, decodedTrue, StringComparison.Ordinal);
        Console.WriteLine($"Decoded with AllowIncorrectBarcodes = false: {decodedFalse ?? "null"}");
        Console.WriteLine($"Decoded with AllowIncorrectBarcodes = true : {decodedTrue ?? "null"}");
        Console.WriteLine($"CodeText unchanged: {isEqual}");

        // Clean up temporary files and folder
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect validation
        }
    }
}