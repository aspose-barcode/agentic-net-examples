// Title: Validate AllowIncorrectBarcodes does not affect decoded CodeText
// Description: Demonstrates generating a Code39 barcode, reading it with AllowIncorrectBarcodes set to false and true, and confirming the decoded text remains unchanged.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them. Developers often need to verify that quality settings such as AllowIncorrectBarcodes do not unintentionally modify valid barcode data, a common requirement in automated scanning and validation pipelines.
// Prompt: Validate that enabling AllowIncorrectBarcodes does not alter the decoded CodeText of valid barcodes.
// Tags: code39, barcode, allowincorrectbarcodes, generation, recognition, csharp, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Code39 barcode, reads it with different
/// AllowIncorrectBarcodes settings, and verifies that the decoded text is unchanged.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, decodes it with two
    /// different quality settings, compares the results, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the barcode image
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define the output path and the text to encode
        string barcodePath = Path.Combine(tempDir, "code39.png");
        string codeText = "ASPOSE123";

        // Generate a valid Code39 barcode and save it as PNG
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code39, codeText))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Decode the barcode with AllowIncorrectBarcodes disabled
        string decodedFalse = ReadBarcode(barcodePath, false);
        // Decode the same barcode with AllowIncorrectBarcodes enabled
        string decodedTrue = ReadBarcode(barcodePath, true);

        // Compare the two decoded values
        bool isEqual = string.Equals(decodedFalse, decodedTrue, StringComparison.Ordinal);

        // Output the results
        Console.WriteLine($"AllowIncorrectBarcodes disabled CodeText: {decodedFalse}");
        Console.WriteLine($"AllowIncorrectBarcodes enabled  CodeText: {decodedTrue}");
        Console.WriteLine(isEqual
            ? "Success: CodeText unchanged when AllowIncorrectBarcodes is enabled."
            : "Failure: CodeText differs when AllowIncorrectBarcodes is enabled.");

        // Cleanup temporary files and directory
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignored – cleanup failures are non‑critical for this example
        }
    }

    /// <summary>
    /// Reads a barcode from the specified image file using the given AllowIncorrectBarcodes setting.
    /// </summary>
    /// <param name="imagePath">Path to the barcode image.</param>
    /// <param name="allowIncorrect">Whether to allow incorrect barcodes during decoding.</param>
    /// <returns>The decoded CodeText, or an empty string if decoding fails.</returns>
    static string ReadBarcode(string imagePath, bool allowIncorrect)
    {
        // Verify that the image file exists before attempting to read
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return string.Empty;
        }

        // Specify the barcode symbology to look for (Code39)
        BaseDecodeType decodeType = DecodeType.Code39;

        // Create a reader with the desired decode type
        using (BarCodeReader reader = new BarCodeReader(imagePath, decodeType))
        {
            // Apply the AllowIncorrectBarcodes quality setting
            reader.QualitySettings.AllowIncorrectBarcodes = allowIncorrect;

            // Perform the read operation
            BarCodeResult[] results = reader.ReadBarCodes();

            // Return the first decoded text if available
            if (results != null && results.Length > 0)
                return results[0].CodeText ?? string.Empty;
            else
                return string.Empty;
        }
    }
}