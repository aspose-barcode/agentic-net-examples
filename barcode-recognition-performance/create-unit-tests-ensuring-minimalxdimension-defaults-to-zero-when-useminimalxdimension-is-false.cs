// Title: MinimalXDimension Default Behavior Demo
// Description: Demonstrates that MinimalXDimension defaults to zero when UseMinimalXDimension is not enabled and shows how to set a custom value.
// Category-Description: This example belongs to the Aspose.BarCode quality settings category, illustrating the use of BarCodeReader.QualitySettings to control X‑dimension handling. Developers working with barcode generation and recognition often need to verify default settings or customize dimensions for scanning accuracy. The snippet showcases key classes such as BarcodeGenerator, BarCodeReader, and XDimensionMode, typical for unit‑test scenarios and CI pipelines.
// Prompt: Create unit tests ensuring MinimalXDimension defaults to zero when UseMinimalXDimension is false.
// Tags: barcode, minimalxdimension, code128, qualitysettings, aspose.barcode, generation, recognition, unit-test

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates default and custom MinimalXDimension behavior using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates a barcode, checks default MinimalXDimension, sets a custom value, and cleans up.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for test artifacts
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the generated barcode image
        string barcodePath = Path.Combine(tempFolder, "code128.png");

        // Generate a simple Code128 barcode image and save it as PNG
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that MinimalXDimension defaults to zero when UseMinimalXDimension is not set
        bool defaultMinimalXDimensionIsZero;
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            // No explicit XDimension mode set; should be default (Auto/Normal)
            defaultMinimalXDimensionIsZero = reader.QualitySettings.MinimalXDimension == 0f;
            Console.WriteLine($"Default MinimalXDimension is zero: {defaultMinimalXDimensionIsZero}");
        }

        // Additional check: enable UseMinimalXDimension mode and assign a non‑zero MinimalXDimension
        bool customMinimalXDimensionWorks;
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            // Switch to minimal X dimension mode
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            // Set a custom minimal X dimension value
            reader.QualitySettings.MinimalXDimension = 2f;
            customMinimalXDimensionWorks = reader.QualitySettings.MinimalXDimension == 2f;
            Console.WriteLine($"Custom MinimalXDimension after enabling UseMinimalXDimension: {customMinimalXDimensionWorks}");
        }

        // Clean up temporary files and directories
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect test outcome
        }

        // Summarize test results
        if (defaultMinimalXDimensionIsZero && customMinimalXDimensionWorks)
        {
            Console.WriteLine("All MinimalXDimension tests passed.");
        }
        else
        {
            Console.WriteLine("MinimalXDimension tests failed.");
        }
    }
}