// Title: MinimalXDimension default value test
// Description: Demonstrates verifying that MinimalXDimension remains zero when UseMinimalXDimension is disabled, using Aspose.BarCode quality settings.
// Category-Description: This example belongs to the Aspose.BarCode quality‑settings category, illustrating how to read and validate MinimalXDimension via BarCodeReader. It showcases typical use cases such as ensuring default quality parameters for barcode decoding, which developers often need when configuring minimal X‑dimension checks for various symbologies.
// Prompt: Create unit tests ensuring MinimalXDimension defaults to zero when UseMinimalXDimension is false.
// Tags: barcode, symbology, unit-test, minimalxdimension, qualitysettings, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Contains example code that runs simple verification tests for the MinimalXDimension
/// property of <see cref="BarCodeReader.QualitySettings"/> when the UseMinimalXDimension flag is not enabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the verification tests.
    /// </summary>
    static void Main()
    {
        RunTests();
    }

    /// <summary>
    /// Executes two basic tests:
    /// 1. Confirms that the default MinimalXDimension is zero.
    /// 2. Confirms that setting XDimension to Small does not change MinimalXDimension when UseMinimalXDimension is false.
    /// </summary>
    static void RunTests()
    {
        // Test 1: Verify that MinimalXDimension defaults to zero.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            using (var ms = new MemoryStream())
            {
                // Generate barcode image and write it to the memory stream.
                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    bitmap.Save(ms, Aspose.Drawing.Imaging.ImageFormat.Png);
                }

                ms.Position = 0; // Reset stream position for reading.

                // Read the barcode and inspect quality settings.
                using (var reader = new BarCodeReader(ms, DecodeType.Code128))
                {
                    double minimal = reader.QualitySettings.MinimalXDimension;
                    bool passed = minimal == 0;
                    Console.WriteLine($"Test_DefaultMinimalXDimensionIsZero: {(passed ? "PASS" : "FAIL")} (value={minimal})");
                }
            }
        }

        // Test 2: Ensure MinimalXDimension stays zero when XDimension is set to Small (UseMinimalXDimension remains false).
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test456"))
        {
            using (var ms = new MemoryStream())
            {
                // Generate barcode image and write it to the memory stream.
                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    bitmap.Save(ms, Aspose.Drawing.Imaging.ImageFormat.Png);
                }

                ms.Position = 0; // Reset stream position for reading.

                // Read the barcode, modify XDimension, and verify MinimalXDimension.
                using (var reader = new BarCodeReader(ms, DecodeType.Code128))
                {
                    reader.QualitySettings.XDimension = XDimensionMode.Small;
                    double minimal = reader.QualitySettings.MinimalXDimension;
                    bool passed = minimal == 0;
                    Console.WriteLine($"Test_SmallXDimensionMinimalXDimensionIsZero: {(passed ? "PASS" : "FAIL")} (value={minimal})");
                }
            }
        }
    }
}