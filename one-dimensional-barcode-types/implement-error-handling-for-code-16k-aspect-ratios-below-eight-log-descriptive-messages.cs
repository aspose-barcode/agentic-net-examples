// Title: Code 16K Barcode Generation with Aspect Ratio Validation
// Description: Demonstrates generating a Code 16K barcode using Aspose.BarCode while enforcing a minimum aspect ratio of 8 and handling invalid values.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and barcode parameters such as XDimension and Code16K aspect ratio. Typical scenarios include creating high‑density barcodes for inventory or shipping labels where specific size constraints must be met. Developers often need to validate input parameters and log meaningful errors when constraints are violated.
// Prompt: Implement error handling for Code 16K aspect ratios below eight, log descriptive messages.
// Tags: code16k, barcode, generation, error handling, aspect ratio, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates Code 16K barcode generation with aspect‑ratio validation and error handling.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates an output folder and generates barcodes with valid and invalid aspect ratios.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the generated images
        string outputDir = Path.Combine(Path.GetTempPath(), "Code16KDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Generate a barcode with a valid aspect ratio (>= 8)
        GenerateCode16K("Aspose.Barcode", 10f, Path.Combine(outputDir, "Code16K_Aspect10.png"));

        // Attempt to generate a barcode with an invalid aspect ratio (< 8) to trigger error handling
        GenerateCode16K("Aspose.Barcode", 5f, Path.Combine(outputDir, "Code16K_Aspect5.png"));
    }

    /// <summary>
    /// Generates a Code 16K barcode using the specified text and aspect ratio, saving it to the given path.
    /// Validates that the aspect ratio meets the minimum requirement and logs errors if not.
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="aspectRatio">The desired aspect ratio; must be >= 8.</param>
    /// <param name="outputPath">File path where the barcode image will be saved.</param>
    static void GenerateCode16K(string codeText, float aspectRatio, string outputPath)
    {
        try
        {
            // Validate aspect ratio before attempting generation
            if (aspectRatio < 8f)
                throw new ArgumentOutOfRangeException(nameof(aspectRatio), "Aspect ratio must be greater than or equal to 8 for Code 16K barcodes.");

            // Initialize the barcode generator with Code16K symbology
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code16K, codeText))
            {
                // Set barcode visual parameters
                generator.Parameters.Barcode.XDimension.Pixels = 2;
                generator.Parameters.Barcode.Code16K.AspectRatio = aspectRatio;

                // Save the generated barcode as a PNG image
                generator.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Barcode generated: {outputPath} (AspectRatio={aspectRatio})");
            }
        }
        catch (ArgumentOutOfRangeException ex)
        {
            // Log validation errors with a clear message
            Console.WriteLine($"Error generating barcode: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Log any unexpected errors that may occur during generation
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}