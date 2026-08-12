// Title: MaxiCode Aspect Ratio Adjustment Example
// Description: Demonstrates generating MaxiCode barcodes with different aspect ratios and verifying the resulting image dimensions.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on MaxiCode symbology. It showcases the use of BarcodeGenerator, MaxiCode parameters, and image analysis to validate aspect ratio settings—common tasks for developers creating custom barcode visuals or performing automated layout tests.
// Prompt: Write unit tests to verify aspect ratio adjustments affect MaxiCode barcode dimensions as expected.
// Tags: maxicode, aspectratio, barcode, generation, image, testing, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Provides a console application that generates MaxiCode barcodes with varying aspect ratios
/// and evaluates the resulting image dimensions to confirm the aspect ratio effect.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates two barcodes with different aspect ratios,
    /// compares their image aspect ratios, and outputs the test results.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the test files
        string tempFolder = Path.Combine(Path.GetTempPath(), "MaxiCodeAspectRatioTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Paths for the generated images
        string defaultPath = Path.Combine(tempFolder, "maxicode_default.png");
        string alteredPath = Path.Combine(tempFolder, "maxicode_aspect2.png");

        // Generate barcodes with different AspectRatio values
        GenerateMaxiCode("Test123", 1f, defaultPath);
        GenerateMaxiCode("Test123", 2f, alteredPath);

        // Load images and evaluate dimensions
        float defaultRatio = GetImageAspectRatio(defaultPath);
        float alteredRatio = GetImageAspectRatio(alteredPath);

        // Tolerance for floating point comparison
        const float tolerance = 0.05f;

        Console.WriteLine($"Default AspectRatio (expected 1):   Actual ratio = {defaultRatio:F2}");
        Console.WriteLine($"Altered AspectRatio (expected 2):   Actual ratio = {alteredRatio:F2}");

        bool defaultPass = Math.Abs(defaultRatio - 1f) <= tolerance;
        bool alteredPass = Math.Abs(alteredRatio - 2f) <= tolerance;

        Console.WriteLine($"Default AspectRatio test: {(defaultPass ? "PASS" : "FAIL")}");
        Console.WriteLine($"Altered AspectRatio test: {(alteredPass ? "PASS" : "FAIL")}");

        // Cleanup (optional)
        try
        {
            File.Delete(defaultPath);
            File.Delete(alteredPath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored – cleanup failures should not affect test outcome
        }
    }

    /// <summary>
    /// Generates a MaxiCode barcode with the specified aspect ratio and saves it to the given path.
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="aspectRatio">The desired aspect ratio (height/width) for the MaxiCode modules.</param>
    /// <param name="outputPath">The file path where the generated image will be saved.</param>
    static void GenerateMaxiCode(string codeText, float aspectRatio, string outputPath)
    {
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, codeText))
        {
            // Set the aspect ratio (height/width) for the MaxiCode modules
            generator.Parameters.Barcode.MaxiCode.AspectRatio = aspectRatio;

            // Save as PNG (extension determines format)
            generator.Save(outputPath);
        }
    }

    /// <summary>
    /// Loads an image file and returns its height-to-width ratio.
    /// </summary>
    /// <param name="imagePath">The path to the image file.</param>
    /// <returns>The aspect ratio calculated as height divided by width.</returns>
    static float GetImageAspectRatio(string imagePath)
    {
        using (var image = Image.FromFile(imagePath))
        {
            return (float)image.Height / image.Width;
        }
    }
}