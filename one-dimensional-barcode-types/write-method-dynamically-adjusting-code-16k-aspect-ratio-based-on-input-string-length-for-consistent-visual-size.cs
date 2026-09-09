// Title: Dynamic Code 16K barcode aspect ratio based on input length
// Description: Demonstrates how to compute and apply a suitable aspect ratio for Code 16K barcodes so that barcodes of varying text lengths maintain a consistent visual size.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on barcode symbology configuration. It shows how to use BarcodeGenerator, EncodeTypes, and the Code16K parameters (AspectRatio, XDimension) to produce PNG images. Developers often need to adjust barcode dimensions dynamically for different data lengths while preserving readability and layout consistency.
// Prompt: Write method dynamically adjusting Code 16K aspect ratio based on input string length for consistent visual size.
// Tags: barcode, code16k, aspectratio, generation, png, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates dynamic adjustment of Code 16K barcode aspect ratio based on input string length.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates Code 16K barcodes for sample strings with computed aspect ratios and saves them as PNG files.
    /// </summary>
    static void Main()
    {
        // Sample strings of varying lengths to illustrate aspect‑ratio scaling
        string[] samples = new string[]
        {
            "A",
            "ABCDE",
            "ABCDEFGHIJ",
            "ABCDEFGHIJKLMNOPQRSTU"
        };

        // Create a temporary output folder for the generated barcode images
        string outputDir = Path.Combine(Path.GetTempPath(), "Code16KDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Process each sample string
        foreach (string text in samples)
        {
            // Compute an appropriate aspect ratio based on the text length
            float aspect = ComputeAspectRatio(text);

            // Build the output file name (includes the character count for clarity)
            string filePath = Path.Combine(outputDir, $"Code16K_{text.Length}_chars.png");

            // Generate the barcode with the calculated aspect ratio
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code16K, text))
            {
                // Set the X‑dimension (module width) in pixels
                generator.Parameters.Barcode.XDimension.Pixels = 2;

                // Apply the dynamically computed aspect ratio
                generator.Parameters.Barcode.Code16K.AspectRatio = aspect;

                // Save the barcode as a PNG image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Inform the user about the generated file
            Console.WriteLine($"Generated barcode for length {text.Length} with aspect {aspect} -> {filePath}");
        }

        // Final summary of the output location
        Console.WriteLine("All barcodes generated in: " + outputDir);
    }

    /// <summary>
    /// Computes a suitable aspect ratio for a Code 16K barcode based on the length of the input text.
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <returns>A float representing the aspect ratio to apply.</returns>
    static float ComputeAspectRatio(string codeText)
    {
        // Code 16K rows: each row holds up to 5 characters
        int rows = (codeText.Length + 4) / 5;

        const float baseAspect = 20f; // Base aspect for a single row
        float aspect = baseAspect / rows;

        // Enforce a minimum recommended aspect ratio to keep the barcode readable
        if (aspect < 8f)
            aspect = 8f;

        return aspect;
    }
}