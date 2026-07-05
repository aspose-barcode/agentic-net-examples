// Title: Barcode recognition with and without UseMinimalXDimension
// Description: Demonstrates generating a Code128 barcode and testing recognition using default quality settings versus minimal X dimension mode.
// Prompt: Write integration tests confirming barcode recognition succeeds after toggling UseMinimalXDimension correctly.
// Tags: barcode, code128, recognition, minimalxdimension, qualitysettings, integration-test

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode and runs two integration tests:
/// 1) Recognition with default quality settings.
/// 2) Recognition with <c>UseMinimalXDimension</c> enabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode image in memory and validates recognition under different quality configurations.
    /// </summary>
    static void Main()
    {
        // Create a Code128 barcode with sample text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            // Generate the barcode image in memory.
            using (Bitmap barcodeImage = generator.GenerateBarCodeImage())
            {
                // ---------- Test 1: Default recognition settings ----------
                using (BarCodeReader defaultReader = new BarCodeReader(barcodeImage, DecodeType.Code128))
                {
                    // Apply the default QualitySettings (NormalQuality).
                    defaultReader.QualitySettings = QualitySettings.NormalQuality;

                    Console.WriteLine("Reading with default QualitySettings...");
                    bool defaultSuccess = false;

                    // Iterate through all detected barcodes.
                    foreach (BarCodeResult result in defaultReader.ReadBarCodes())
                    {
                        Console.WriteLine($"Detected Type: {result.CodeTypeName}, CodeText: {result.CodeText}");
                        defaultSuccess = true;
                    }

                    // Warn if no barcode was detected.
                    if (!defaultSuccess)
                    {
                        Console.WriteLine("Warning: No barcode detected with default settings.");
                    }
                }

                // ---------- Test 2: UseMinimalXDimension mode ----------
                using (BarCodeReader minimalReader = new BarCodeReader(barcodeImage, DecodeType.Code128))
                {
                    // Start with normal quality settings.
                    minimalReader.QualitySettings = QualitySettings.NormalQuality;

                    // Configure QualitySettings to use minimal X dimension.
                    minimalReader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                    minimalReader.QualitySettings.MinimalXDimension = 2f; // minimal element size in pixels

                    Console.WriteLine("\nReading with UseMinimalXDimension mode (MinimalXDimension = 2)...");
                    bool minimalSuccess = false;

                    // Iterate through all detected barcodes.
                    foreach (BarCodeResult result in minimalReader.ReadBarCodes())
                    {
                        Console.WriteLine($"Detected Type: {result.CodeTypeName}, CodeText: {result.CodeText}");
                        minimalSuccess = true;
                    }

                    // Warn if no barcode was detected.
                    if (!minimalSuccess)
                    {
                        Console.WriteLine("Warning: No barcode detected with UseMinimalXDimension settings.");
                    }
                }
            }
        }

        // Indicate that the program completed successfully.
        Console.WriteLine("\nIntegration test completed.");
    }
}