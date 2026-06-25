using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating Code16K barcodes with dynamic aspect ratio based on text length.
/// </summary>
class Program
{
    /// <summary>
    /// Calculates an appropriate aspect ratio for Code16K barcodes.
    /// The ratio is scaled inversely with the length of the code text to keep visual height roughly constant.
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <returns>A float representing the aspect ratio.</returns>
    static float CalculateAspectRatio(string codeText)
    {
        // Base aspect ratio for a short code.
        const float baseAspect = 1.0f;

        // Desired visual height (in modules) we want to keep roughly constant.
        // Longer code texts need a smaller height-to-width ratio to avoid the barcode becoming too tall.
        // This simple heuristic scales the aspect ratio inversely with length.
        // Clamp the result to a reasonable range to avoid extreme shapes.
        float ratio = baseAspect * 10f / Math.Max(1, codeText.Length);
        if (ratio < 0.3f) ratio = 0.3f;
        if (ratio > 3.0f) ratio = 3.0f;
        return ratio;
    }

    /// <summary>
    /// Generates a Code16K barcode image using the specified text and saves it to the given path.
    /// </summary>
    /// <param name="codeText">The text to encode.</param>
    /// <param name="outputPath">The file path where the image will be saved.</param>
    static void GenerateCode16K(string codeText, string outputPath)
    {
        // Create the generator for Code16K symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, codeText))
        {
            // Dynamically set the aspect ratio based on the length of the code text.
            generator.Parameters.Barcode.Code16K.AspectRatio = CalculateAspectRatio(codeText);

            // Keep other settings consistent.
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;
            generator.Parameters.Barcode.BarHeight.Point = 50f; // Fixed bar height.
            generator.Parameters.Resolution = 300f; // High resolution.

            // Save the barcode image to the specified file.
            generator.Save(outputPath);
        }
    }

    /// <summary>
    /// Entry point of the program. Generates sample Code16K barcodes of varying lengths.
    /// </summary>
    static void Main()
    {
        // Sample code texts of varying lengths.
        string[] samples = new[]
        {
            "ABC123",
            "ABCDEFGHIJ1234567890",
            "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789",
            "THISISALONGERCODETEXTFORDEMONSTRATIONPURPOSES1234567890"
        };

        // Iterate through each sample, generate the barcode, and report the aspect ratio used.
        for (int i = 0; i < samples.Length; i++)
        {
            string fileName = $"code16k_{i + 1}.png";
            GenerateCode16K(samples[i], fileName);
            Console.WriteLine($"Generated {fileName} with aspect ratio {CalculateAspectRatio(samples[i]):F2}");
        }
    }
}