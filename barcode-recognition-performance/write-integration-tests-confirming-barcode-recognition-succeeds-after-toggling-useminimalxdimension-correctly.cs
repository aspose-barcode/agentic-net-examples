// Title: Barcode generation and recognition with UseMinimalXDimension toggle
// Description: Demonstrates creating a Code128 barcode image, then reading it twice—once with default settings and once with the UseMinimalXDimension option enabled—to verify successful recognition.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the BarcodeGenerator for creating barcodes and BarCodeReader with QualitySettings for decoding, highlighting typical use cases such as testing minimal X‑dimension handling. Developers often need to validate that toggling X‑dimension settings does not affect barcode readability, making this pattern useful for integration tests and CI pipelines.
// Prompt: Write integration tests confirming barcode recognition succeeds after toggling UseMinimalXDimension correctly.
// Tags: barcode, code128, generation, recognition, useminimalxdimension, qualitysettings, integration-test

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation and recognition, toggling the UseMinimalXDimension setting.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a barcode, reads it with default and minimal X dimension settings, and outputs the results.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the test files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define barcode parameters
        string barcodeText = "Test12345";
        string barcodePath = Path.Combine(tempFolder, "barcode.png");

        // Generate a Code128 barcode image and save it as PNG
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, barcodeText))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify the file was created before proceeding
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode with default settings (no XDimension change)
        bool defaultReadSuccess = ReadBarcode(barcodePath, barcodeText, false);
        // Read the barcode with UseMinimalXDimension enabled
        bool minimalXReadSuccess = ReadBarcode(barcodePath, barcodeText, true);

        // Output the results of both reads
        Console.WriteLine($"Default read success: {defaultReadSuccess}");
        Console.WriteLine($"UseMinimalXDimension read success: {minimalXReadSuccess}");

        // Clean up temporary files; ignore any errors during cleanup
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect test outcome
        }
    }

    // Helper method to read a barcode and optionally enable UseMinimalXDimension
    static bool ReadBarcode(string imagePath, string expectedText, bool enableMinimalXDimension)
    {
        BaseDecodeType decodeType = DecodeType.Code128;

        using (BarCodeReader reader = new BarCodeReader(imagePath, decodeType))
        {
            // Configure QualitySettings if minimal X dimension mode is requested
            if (enableMinimalXDimension)
            {
                // Enable minimal X dimension mode
                reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                // Optionally set a minimal dimension value (example: 1 point)
                reader.QualitySettings.MinimalXDimension = 1f;
            }

            // Perform the reading
            BarCodeResult[] results = reader.ReadBarCodes();

            // Check if any result matches the expected text
            foreach (BarCodeResult result in results)
            {
                if (result != null && result.CodeText == expectedText)
                {
                    return true;
                }
            }
        }

        // No matching result found
        return false;
    }
}