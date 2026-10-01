// Title: Minimal X Dimension Default Verification for Code128 Barcode
// Description: Demonstrates how to generate a Code128 barcode, read it, and verify that MinimalXDimension defaults to zero when UseMinimalXDimension is not enabled.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create barcodes, BarCodeReader to decode them, and QualitySettings to inspect default dimension settings. Developers commonly need to validate default configuration values, such as MinimalXDimension, when customizing barcode rendering or decoding behavior.
// Prompt: Create unit tests ensuring MinimalXDimension defaults to zero when UseMinimalXDimension is false.
// Tags: barcode symbology, generation, recognition, minimalxdimension, unit-test, aspose.barcode, code128, qualitysettings

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Contains a simple test that verifies the default behavior of MinimalXDimension
/// when the XDimension mode is not set to UseMinimalXDimension.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the MinimalXDimension default test
    /// and reports the result to the console.
    /// </summary>
    static void Main()
    {
        try
        {
            RunMinimalXDimensionDefaultTest();
            Console.WriteLine("All tests passed.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Test failed: {ex.Message}");
        }
    }

    /// <summary>
    /// Generates a Code128 barcode, reads it back, and asserts that
    /// MinimalXDimension is zero and the XDimension mode is not UseMinimalXDimension.
    /// </summary>
    static void RunMinimalXDimensionDefaultTest()
    {
        // Generate a simple Code128 barcode and keep it in memory.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            // No need to set any XDimension or related properties.
            using (var ms = new MemoryStream())
            {
                // Save the barcode image to the memory stream.
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream for reading.

                // Prepare a reader for Code128 barcodes.
                BaseDecodeType decodeType = DecodeType.Code128;
                using (var reader = new BarCodeReader(ms, decodeType))
                {
                    // Verify that MinimalXDimension defaults to zero.
                    float minimalX = reader.QualitySettings.MinimalXDimension;
                    if (minimalX != 0f)
                    {
                        throw new Exception($"Expected MinimalXDimension to be 0, but got {minimalX}.");
                    }

                    // Ensure that XDimension mode is not set to UseMinimalXDimension.
                    // The default mode is Normal; we check it is not UseMinimalXDimension.
                    if (reader.QualitySettings.XDimension == XDimensionMode.UseMinimalXDimension)
                    {
                        throw new Exception("XDimension mode should not be UseMinimalXDimension by default.");
                    }

                    // Perform a read to ensure the barcode can be decoded (optional verification).
                    var results = reader.ReadBarCodes();
                    if (results == null || results.Length == 0)
                    {
                        throw new Exception("Failed to read the generated barcode.");
                    }

                    // Verify the decoded text matches the original.
                    if (results[0].CodeText != "Test123")
                    {
                        throw new Exception($"Decoded text mismatch. Expected 'Test123', got '{results[0].CodeText}'.");
                    }
                }
            }
        }
    }
}