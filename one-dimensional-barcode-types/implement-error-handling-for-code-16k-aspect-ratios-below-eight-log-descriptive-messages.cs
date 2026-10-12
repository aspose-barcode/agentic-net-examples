// Title: Generate Code 16K Barcodes with Aspect Ratio Validation
// Description: Demonstrates creating Code 16K barcodes using Aspose.BarCode, validating the aspect ratio, and logging informative messages.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator, EncodeTypes, and barcode parameters to produce images. Typical use cases include generating high‑density Code 16K barcodes for inventory or tracking systems, where developers need to enforce aspect‑ratio constraints and handle generation errors gracefully. The snippet serves as a reference for developers searching for barcode generation patterns in C#.
// Prompt: Implement error handling for Code 16K aspect ratios below eight, log descriptive messages.
// Tags: barcode, code16k, aspect-ratio, error-handling, generation, png, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating Code 16K barcodes with aspect‑ratio validation and error handling.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes for a set of aspect ratios, skipping invalid ones.
    /// </summary>
    static void Main()
    {
        // Text to encode in the barcode
        string codeText = "Aspose.Barcode";

        // Define a set of aspect ratios to test
        float[] aspectRatios = { 5f, 8f, 10f };

        // Create a temporary output directory for the generated images
        string outputDir = Path.Combine(Path.GetTempPath(), "Code16K_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Iterate over each aspect ratio and attempt barcode generation
        foreach (float ratio in aspectRatios)
        {
            string filePath = Path.Combine(outputDir, $"Code16K_Aspect_{ratio}.png");
            GenerateCode16K(codeText, ratio, filePath);
        }

        // Inform the user where the files have been saved
        Console.WriteLine($"Barcode generation completed. Files are located in: {outputDir}");
    }

    /// <summary>
    /// Generates a Code 16K barcode with the specified aspect ratio, handling invalid ratios and exceptions.
    /// </summary>
    /// <param name="codeText">The text to encode.</param>
    /// <param name="aspectRatio">Desired aspect ratio for the barcode.</param>
    /// <param name="outputPath">File path where the PNG image will be saved.</param>
    static void GenerateCode16K(string codeText, float aspectRatio, string outputPath)
    {
        // Validate aspect ratio; skip generation if below the recommended minimum
        if (aspectRatio < 8f)
        {
            Console.WriteLine($"[Warning] Aspect ratio {aspectRatio} is below the recommended minimum of 8. Generation skipped.");
            return;
        }

        try
        {
            // Initialize the barcode generator for Code 16K symbology
            using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, codeText))
            {
                // Set barcode visual parameters
                generator.Parameters.Barcode.XDimension.Pixels = 2;
                generator.Parameters.Barcode.Code16K.AspectRatio = aspectRatio;

                // Save the generated barcode as a PNG image
                generator.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine($"[Info] Generated Code 16K barcode with aspect ratio {aspectRatio} at '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during generation
            Console.WriteLine($"[Error] Failed to generate barcode with aspect ratio {aspectRatio}: {ex.Message}");
        }
    }
}