// Title: Dynamic Aspect Ratio Adjustment for Code 16K Barcodes
// Description: Demonstrates how to calculate and apply a variable aspect ratio to Code 16K barcodes so that the visual size stays consistent across different input lengths.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and barcode parameter settings. It shows a common scenario where developers need to create Code 16K barcodes with a visual size that does not vary dramatically with the length of the encoded data, a frequent requirement in inventory and labeling systems.
// Prompt: Write method dynamically adjusting Code 16K aspect ratio based on input string length for consistent visual size.
// Tags: barcode, symbology, code16k, aspectratio, generation, png, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates Code 16K barcodes with a dynamically calculated aspect ratio to keep visual size consistent.
/// </summary>
class Program
{
    /// <summary>
    /// Calculates an aspect ratio that tries to keep the visual size of the barcode
    /// roughly constant regardless of the length of the encoded text.
    /// Shorter texts get a larger aspect ratio (taller), longer texts get a smaller one (wider).
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <returns>A float representing the height‑to‑width ratio.</returns>
    static float CalculateAspectRatio(string codeText)
    {
        const float baseAspect = 1.0f;                     // default height/width ratio
        // Simple heuristic: inverse proportional to length, with a minimum divisor of 1.
        float lengthFactor = Math.Max(1, codeText.Length);
        float ratio = baseAspect * (10f / lengthFactor);   // 10 is an arbitrary scaling constant
        return ratio;
    }

    /// <summary>
    /// Resolves a symbology name to the corresponding EncodeTypes field using reflection.
    /// </summary>
    /// <param name="symbologyName">The name of the symbology (e.g., "Code16K").</param>
    /// <returns>The matching <see cref="BaseEncodeType"/> instance.</returns>
    static BaseEncodeType ResolveEncodeType(string symbologyName)
    {
        var field = typeof(EncodeTypes).GetField(symbologyName);
        if (field == null)
        {
            throw new ArgumentException($"Unknown symbology: {symbologyName}");
        }
        return (BaseEncodeType)field.GetValue(null);
    }

    /// <summary>
    /// Generates a Code 16K barcode image with an aspect ratio adjusted for the supplied text.
    /// </summary>
    /// <param name="codeText">The text to encode.</param>
    /// <param name="outputPath">Full file path where the PNG image will be saved.</param>
    static void GenerateCode16K(string codeText, string outputPath)
    {
        // Resolve the Code16K encode type.
        BaseEncodeType encodeType = ResolveEncodeType("Code16K");

        // Create the barcode generator with the specified text.
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Adjust the aspect ratio based on the length of the code text.
            float aspect = CalculateAspectRatio(codeText);
            generator.Parameters.Barcode.Code16K.AspectRatio = aspect;

            // Optional: set a modest XDimension so the image is not too small.
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Save the barcode as PNG.
            generator.Save(outputPath, BarCodeImageFormat.Png);
            Console.WriteLine($"Generated '{outputPath}' with AspectRatio={aspect:F3}");
        }
    }

    /// <summary>
    /// Entry point. Generates a set of Code 16K barcodes with varying text lengths to demonstrate aspect‑ratio adjustment.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Sample inputs of varying lengths.
        string[] samples = new[]
        {
            "ABC",
            "ABCDEFGHIJ",
            "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
            "12345678901234567890",
            "LongerSampleTextToTestAspectRatioAdjustment"
        };

        // Ensure the output directory exists.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Generate a barcode for each sample.
        for (int i = 0; i < samples.Length; i++)
        {
            string text = samples[i];
            string fileName = $"Code16K_{i + 1}.png";
            string outputPath = Path.Combine(outputDir, fileName);
            GenerateCode16K(text, outputPath);
        }

        Console.WriteLine("Barcode generation completed.");
    }
}