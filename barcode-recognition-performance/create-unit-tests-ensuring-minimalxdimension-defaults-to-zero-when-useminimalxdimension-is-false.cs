// Title: Verify MinimalXDimension defaults to zero when UseMinimalXDimension is disabled
// Description: Demonstrates generating a Code128 barcode image and checking that the reader's QualitySettings report a MinimalXDimension of zero when the UseMinimalXDimension mode is not enabled.
// Category-Description: This example belongs to the Aspose.BarCode quality settings category, illustrating how to use the BarCodeReader.QualitySettings properties such as XDimension and MinimalXDimension. Developers often need to validate default settings for barcode decoding, especially when customizing image quality or dimension handling. The sample shows typical usage of BarcodeGenerator, BarCodeReader, and related enums for Code128 barcodes.
// Prompt: Create unit tests ensuring MinimalXDimension defaults to zero when UseMinimalXDimension is false.
// Tags: code128, barcode generation, barcode recognition, minimalxdimension, qualitysettings, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a Code128 barcode and verifies that
/// MinimalXDimension defaults to zero when UseMinimalXDimension is not enabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a temporary barcode image,
    /// reads it, checks the quality settings, outputs the test result,
    /// and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for test files
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Path for the generated barcode image
        string barcodePath = Path.Combine(tempFolder, "test.png");

        // Generate a simple Code128 barcode image
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            // No special settings needed for this test
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Perform the test: MinimalXDimension should be zero when UseMinimalXDimension is not enabled
        bool testPassed = false;
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            // Determine whether the XDimension mode is set to UseMinimalXDimension
            bool isUseMinimal = reader.QualitySettings.XDimension == XDimensionMode.UseMinimalXDimension;

            // Retrieve the MinimalXDimension value
            float minimalX = reader.QualitySettings.MinimalXDimension;

            // Validate that the mode is not UseMinimalXDimension and the value is effectively zero
            if (!isUseMinimal && Math.Abs(minimalX) < 0.0001f)
            {
                testPassed = true;
            }
        }

        // Output the test result
        Console.WriteLine(testPassed
            ? "Test Passed: MinimalXDimension defaults to zero when UseMinimalXDimension is false."
            : "Test Failed: Unexpected MinimalXDimension value.");

        // Clean up temporary files and directories
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect test result
        }
    }
}