// Title: Verify MinimalXDimension default behavior
// Description: Demonstrates checking that MinimalXDimension is zero when UseMinimalXDimension is not enabled.
// Category-Description: This example belongs to the Aspose.BarCode quality settings category, illustrating how to inspect default values of XDimension-related properties. It uses BarcodeGenerator to create a barcode and BarCodeReader with QualitySettings to validate defaults. Developers working with barcode generation and recognition often need to ensure proper configuration of dimension settings for accurate scanning and printing.
// Prompt: Create unit tests ensuring MinimalXDimension defaults to zero when UseMinimalXDimension is false.
// Tags: barcode, code128, minimalxdimension, qualitysettings, aspose.barcode, unit-test

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a Code128 barcode, reads it back, and verifies that
/// <see cref="QualitySettings.MinimalXDimension"/> defaults to zero when <see cref="XDimensionMode.UseMinimalXDimension"/>
/// is not selected. This serves as a simple unit‑test‑style validation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, validation, and cleanup.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare a temporary folder for the generated barcode image
        // ------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeTest");
        if (!Directory.Exists(tempFolder))
        {
            Directory.CreateDirectory(tempFolder);
        }

        // Define the full path for the barcode image file
        string imagePath = Path.Combine(tempFolder, "code128.png");

        // ------------------------------------------------------------
        // Generate a simple Code128 barcode image
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Save(imagePath);
        }

        // ------------------------------------------------------------
        // Verify that MinimalXDimension defaults to zero when UseMinimalXDimension is false
        // ------------------------------------------------------------
        bool testPassed = true;
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Set XDimension mode to Auto (i.e., not using MinimalXDimension)
            reader.QualitySettings.XDimension = XDimensionMode.Auto;

            // Expect MinimalXDimension to be zero by default
            if (reader.QualitySettings.MinimalXDimension != 0f)
            {
                testPassed = false;
                Console.WriteLine($"FAILED: MinimalXDimension expected 0, but was {reader.QualitySettings.MinimalXDimension}");
            }
        }

        // Output the test result
        if (testPassed)
        {
            Console.WriteLine("PASSED: MinimalXDimension defaults to zero when UseMinimalXDimension is false.");
        }

        // ------------------------------------------------------------
        // Clean up generated files and temporary folder
        // ------------------------------------------------------------
        try
        {
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }

            if (Directory.Exists(tempFolder))
            {
                Directory.Delete(tempFolder, true);
            }
        }
        catch
        {
            // Ignored – cleanup failures should not affect the test outcome
        }
    }
}