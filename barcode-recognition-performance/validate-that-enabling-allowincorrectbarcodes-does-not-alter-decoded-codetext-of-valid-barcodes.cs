// Title: Validate AllowIncorrectBarcodes does not affect decoded CodeText
// Description: Demonstrates generating a Code128 barcode, decoding it with and without the AllowIncorrectBarcodes setting, and confirming the decoded text remains unchanged.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader with QualitySettings for decoding. Developers often need to verify that decoding options such as AllowIncorrectBarcodes do not alter results for valid barcodes, a common validation scenario in automated testing pipelines.
// Prompt: Validate that enabling AllowIncorrectBarcodes does not alter the decoded CodeText of valid barcodes.
// Tags: code128, barcode generation, barcode recognition, allowincorrectbarcodes, validation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates validation that enabling <c>AllowIncorrectBarcodes</c> does not change the decoded <c>CodeText</c> of a valid barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, decodes it with different settings, compares results, and cleans up.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the test files
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "barcode.png");

        // Generate a valid Code128 barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test12345"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify the file was created before proceeding
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            Cleanup(tempFolder);
            return;
        }

        // Decode without AllowIncorrectBarcodes (default behavior)
        string codeTextDefault = DecodeBarcode(barcodePath, allowIncorrect: false);

        // Decode with AllowIncorrectBarcodes enabled
        string codeTextAllow = DecodeBarcode(barcodePath, allowIncorrect: true);

        // Compare the decoded texts to ensure they are identical
        bool textsEqual = string.Equals(codeTextDefault, codeTextAllow, StringComparison.Ordinal);
        Console.WriteLine($"Default decoding result:   {(codeTextDefault ?? "null")}");
        Console.WriteLine($"AllowIncorrectBarcodes result: {(codeTextAllow ?? "null")}");
        Console.WriteLine($"CodeText unchanged: {textsEqual}");

        // Clean up temporary files and folder
        Cleanup(tempFolder);
    }

    // Helper method to decode a barcode image with optional AllowIncorrectBarcodes setting
    private static string DecodeBarcode(string imagePath, bool allowIncorrect)
    {
        // Ensure the file exists before attempting to read
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return null;
        }

        // Resolve the decode type for Code128
        BaseDecodeType decodeType = DecodeType.Code128;

        using (var reader = new BarCodeReader(imagePath, decodeType))
        {
            // Enable or disable AllowIncorrectBarcodes as requested
            reader.QualitySettings.AllowIncorrectBarcodes = allowIncorrect;

            // Perform the read operation
            BarCodeResult[] results = reader.ReadBarCodes();

            // Return the first decoded CodeText if available
            if (results != null && results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText))
            {
                return results[0].CodeText;
            }
        }

        return null;
    }

    // Helper method to delete the temporary folder and its contents
    private static void Cleanup(string folderPath)
    {
        try
        {
            if (Directory.Exists(folderPath))
            {
                Directory.Delete(folderPath, true);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cleanup failed: {ex.Message}");
        }
    }
}