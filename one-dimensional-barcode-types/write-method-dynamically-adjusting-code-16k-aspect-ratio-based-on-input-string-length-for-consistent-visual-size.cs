// Title: Dynamic Code 16K Aspect Ratio Based on Text Length
// Description: Demonstrates adjusting the Code 16K barcode's aspect ratio according to the length of the input string to maintain a consistent visual size.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on symbology-specific parameter tuning. It shows how to use BarcodeGenerator, EncodeTypes, and the Code16K parameters to modify aspect ratio and image dimensions. Developers creating barcodes that need uniform appearance regardless of data length can use this pattern to dynamically calculate visual properties.
// Prompt: Write method dynamically adjusting Code 16K aspect ratio based on input string length for consistent visual size.
// Tags: barcode, code16k, aspectratio, dynamic, generation, image, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates dynamic adjustment of Code 16K barcode aspect ratio based on input string length.
/// </summary>
class Program
{
    /// <summary>
    /// Calculates an aspect ratio based on the length of the codetext.
    /// Longer text results in a higher height‑to‑width ratio to keep the visual size consistent.
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <returns>A float representing the calculated aspect ratio.</returns>
    static float CalculateAspectRatio(string codeText)
    {
        const float baseAspect = 1.0f;   // Default ratio for a typical length.
        const int baseLength = 10;       // Reference length for the default ratio.

        if (codeText == null) throw new ArgumentNullException(nameof(codeText));

        // If the text length does not exceed the reference, return the base aspect.
        if (codeText.Length <= baseLength)
            return baseAspect;

        // Increase the ratio by 0.05 for each character beyond the base length.
        // Adjust the multiplier as needed for different visual requirements.
        float extraRatio = (codeText.Length - baseLength) * 0.05f;
        return baseAspect + extraRatio;
    }

    /// <summary>
    /// Entry point that generates a Code 16K barcode with a calculated aspect ratio and saves it as an image.
    /// </summary>
    static void Main()
    {
        // Sample codetext; replace with any string as needed.
        string codeText = "DynamicAspectRatioExample";

        // Determine the appropriate aspect ratio based on the input length.
        float aspectRatio = CalculateAspectRatio(codeText);

        // Create the barcode generator for Code16K using the provided codetext.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, codeText))
        {
            // Apply the calculated aspect ratio to the Code16K parameters.
            generator.Parameters.Barcode.Code16K.AspectRatio = aspectRatio;

            // Optional: set a fixed image width to keep output size predictable.
            generator.Parameters.ImageWidth.Point = 300f;

            // Use interpolation mode so the image size respects ImageWidth.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Save the barcode image to a file.
            string outputPath = "code16k.png";
            generator.Save(outputPath);

            // Inform the user about the saved file and the used aspect ratio.
            Console.WriteLine($"Barcode saved to '{outputPath}' with aspect ratio {aspectRatio:F2}");
        }
    }
}