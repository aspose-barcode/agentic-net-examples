// Title: Dynamic Code 16K Aspect Ratio Adjustment
// Description: Demonstrates how to calculate and apply a variable aspect ratio for Code 16K barcodes so that visual size stays consistent across different input lengths.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing barcode parameter tuning. It uses BarcodeGenerator, EncodeTypes, and Code16K settings to illustrate typical tasks such as size normalization, image export, and batch processing. Developers working with barcode rendering often need to adjust dimensions like X‑Dimension and aspect ratio to meet design requirements.
// Prompt: Write method dynamically adjusting Code 16K aspect ratio based on input string length for consistent visual size.
// Tags: barcode symbology, code16k, aspect ratio, image generation, png, aspose.barcode, aspose.barcode.generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates dynamic adjustment of Code 16K barcode aspect ratio based on input string length.
/// </summary>
class Program
{
    /// <summary>
    /// Generates Code 16K barcodes for sample strings, applying calculated aspect ratios and saving them as PNG files.
    /// </summary>
    static void Main()
    {
        // Sample strings of varying lengths to illustrate aspect ratio scaling
        string[] samples = new string[]
        {
            "A",
            "ABCD",
            "ABCDEFGHIJKLMNO",
            "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        };

        // Create a temporary output directory for the generated images
        string outputDir = Path.Combine(Path.GetTempPath(), "Code16KDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Process each sample string
        foreach (string text in samples)
        {
            // Calculate an appropriate aspect ratio based on the string length
            float aspect = CalculateAspectRatio(text);

            // Build the output file path, including the length in the file name for clarity
            string filePath = Path.Combine(outputDir, $"Code16K_{text.Length}_chars.png");

            // Generate the barcode with the calculated aspect ratio
            using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.Code16K, text))
            {
                gen.Parameters.Barcode.XDimension.Pixels = 2;          // Set module width
                gen.Parameters.Barcode.Code16K.AspectRatio = aspect; // Apply dynamic aspect ratio
                gen.Save(filePath, BarCodeImageFormat.Png);           // Save as PNG
            }

            // Log the generation details to the console
            Console.WriteLine($"Generated '{text}' (len={text.Length}) with AspectRatio={aspect} -> {filePath}");
        }

        // Inform the user where the images were saved
        Console.WriteLine("All barcodes generated in: " + outputDir);
    }

    /// <summary>
    /// Calculates a suitable aspect ratio for a Code 16K barcode based on the length of the input text.
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <returns>A float representing the aspect ratio to apply.</returns>
    static float CalculateAspectRatio(string codeText)
    {
        const float baseAspect = 10f;          // Ideal aspect ratio for the reference length
        const int referenceLength = 5;         // Length at which baseAspect is optimal

        if (codeText == null) throw new ArgumentNullException(nameof(codeText));
        if (codeText.Length == 0) return baseAspect;

        // Scale the aspect ratio inversely with the length of the text
        float ratio = baseAspect * ((float)referenceLength / codeText.Length);

        // Ensure the ratio does not fall below the recommended minimum
        if (ratio < 8f) ratio = 8f;

        return ratio;
    }
}