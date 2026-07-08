// Title: Code16K Barcode Generation with Aspect Ratio Validation
// Description: Demonstrates generating Code 16K barcodes while validating that the aspect ratio meets the minimum requirement of eight.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes.Code16K. It illustrates typical use cases such as setting barcode parameters, handling invalid input values, and saving the output as PNG images. Developers working with barcode creation often need to enforce symbology‑specific constraints and log informative messages for troubleshooting.
/// Prompt: Implement error handling for Code 16K aspect ratios below eight, log descriptive messages.
/// Tags: barcode symbology, generation, png, code16k, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates Code16K barcodes for a set of aspect ratios, skipping those below the allowed minimum
/// and logging appropriate messages. Demonstrates error handling and parameter configuration using
/// Aspose.BarCode's <see cref="BarcodeGenerator"/>.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Iterates over predefined aspect ratios, validates them,
    /// generates barcodes when valid, and logs the process.
    /// </summary>
    static void Main()
    {
        // Sample aspect ratios to test, including values below and above the threshold of 8
        float[] aspectRatios = { 5.5f, 7.9f, 8.0f, 10.2f };

        // Process each aspect ratio individually
        foreach (float ratio in aspectRatios)
        {
            // Validate aspect ratio for Code16K; values below 8 are considered invalid
            if (ratio < 8f)
            {
                Console.WriteLine($"[Warning] Aspect ratio {ratio} is below the minimum allowed (8). Skipping barcode generation.");
                continue; // Skip to the next ratio
            }

            // Create and configure the barcode generator inside a using block to ensure disposal
            using (var generator = new BarcodeGenerator(EncodeTypes.Code16K))
            {
                try
                {
                    // Set a sample codetext; Code16K accepts any string
                    generator.CodeText = "SampleCode16K";

                    // Apply the validated aspect ratio
                    generator.Parameters.Barcode.Code16K.AspectRatio = ratio;

                    // Optional: set image size for consistency
                    generator.Parameters.ImageWidth.Point = 300f;
                    generator.Parameters.ImageHeight.Point = 150f;

                    // Save the barcode image to a file named with the current aspect ratio
                    string fileName = $"Code16K_Aspect_{ratio}.png";
                    generator.Save(fileName);

                    Console.WriteLine($"[Info] Generated Code16K barcode with aspect ratio {ratio} saved as '{fileName}'.");
                }
                catch (Exception ex)
                {
                    // Log any unexpected errors during generation
                    Console.WriteLine($"[Error] Failed to generate barcode with aspect ratio {ratio}: {ex.Message}");
                }
            }
        }

        // Indicate completion of the processing loop
        Console.WriteLine("Barcode processing completed.");
    }
}